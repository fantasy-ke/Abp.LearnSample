using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogSample.Comments;
using BlogSample.Tags;
using BlogSample.Users;
using Microsoft.Extensions.Caching.Distributed;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Caching;
using Volo.Abp.EventBus.Local;

namespace BlogSample.Posts;

public class PostAppService : BlogSampleAppService, IPostAppService
{
    protected IBlogUserLookupService _userLookupService { get; }
    readonly IPostRepository _postRepository;
    readonly ITagRepository _tagRepository;
    readonly ICommentRepository _commentRepository;
    private readonly IDistributedCache<List<PostCacheItem>> _postsCache;
    private readonly ILocalEventBus _localEventBus;

    public PostAppService(IPostRepository postRepository, ITagRepository tagRepository,
        IBlogUserLookupService userLookupService, ICommentRepository commentRepository, IDistributedCache<List<PostCacheItem>> postsCache, ILocalEventBus localEventBus)
    {
        _postRepository = postRepository;
        _tagRepository = tagRepository;
        _userLookupService = userLookupService;
        _commentRepository = commentRepository;
        _postsCache = postsCache;
        _localEventBus = localEventBus;
    }

    public async Task<ListResultDto<PostWithDetailsDto>> GetListByBlogIdAndTagName(Guid id, string tagName)
    {
        // 根据blogId查询文章数据
        var posts = await _postRepository.GetPostsByBlogId(id);
        // 根据tagName筛选tag
        var tag = tagName.IsNullOrWhiteSpace() ? null : await _tagRepository.FindByNameAsync(id, tagName);
        var userDictionary = new Dictionary<Guid, BlogUserDto>();
        var postDtos = new List<PostWithDetailsDto>(ObjectMapper.Map<List<Post>, List<PostWithDetailsDto>>(posts));

        // 给文章Tags赋值
        foreach (var postDto in postDtos)
        {
            postDto.Tags = await GetTagsOfPost(postDto.Id);
        }

        // 筛选掉不符合要求的文章
        if (tag != null)
        {
            postDtos = await FilterPostsByTag(postDtos, tag);
        }

        // 赋值作者信息
        foreach (var postDto in postDtos)
        {
            if (postDto.CreatorId.HasValue)
            {
                if (!userDictionary.ContainsKey(postDto.CreatorId.Value))
                {
                    var creatorUser = await _userLookupService.FindByIdAsync(postDto.CreatorId.Value);
                    if (creatorUser != null)
                    {
                        userDictionary[creatorUser.Id] = ObjectMapper.Map<BlogUser, BlogUserDto>(creatorUser);
                    }
                }

                if (userDictionary.ContainsKey(postDto.CreatorId.Value))
                {
                    postDto.Writer = userDictionary[(Guid)postDto.CreatorId];
                }
            }
        }

        return new ListResultDto<PostWithDetailsDto>(postDtos);
    }

    public async Task<ListResultDto<PostWithDetailsDto>> GetTimeOrderedListAsync(Guid blogId)
    {
        var postCacheItems = await _postsCache.GetOrAddAsync(
            blogId.ToString(),
            async () => await GetTimeOrderedPostsAsync(blogId),
            () => new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddHours(1)
            }
        );
        var postsWithDetails = ObjectMapper.Map<List<PostCacheItem>, List<PostWithDetailsDto>>(postCacheItems);
        
        foreach (var post in postsWithDetails)
        {
            if (post.CreatorId.HasValue)
            {
                var creatorUser = await _userLookupService.FindByIdAsync(post.CreatorId.Value);
                if (creatorUser != null)
                {
                    post.Writer = ObjectMapper.Map<BlogUser, BlogUserDto>(creatorUser);
                }
            }
        }

        return new ListResultDto<PostWithDetailsDto>(postsWithDetails);
    }

    public async Task<PostWithDetailsDto> GetForReadingAsync(GetPostInput input)
    {
        var post = await _postRepository.GetPostByUrl(input.BlogId, input.Url);

        post.IncreaseReadCount();

        var postDto = ObjectMapper.Map<Post, PostWithDetailsDto>(post);

        postDto.Tags = await GetTagsOfPost(postDto.Id);

        if (postDto.CreatorId.HasValue)
        {
            var creatorUser = await _userLookupService.FindByIdAsync(postDto.CreatorId.Value);

            postDto.Writer = ObjectMapper.Map<BlogUser, BlogUserDto>(creatorUser);
        }

        return postDto;
    }

    public async Task<PostWithDetailsDto> GetAsync(Guid id)
    {
        var post = await _postRepository.GetAsync(id);

        var postDto = ObjectMapper.Map<Post, PostWithDetailsDto>(post);

        postDto.Tags = await GetTagsOfPost(postDto.Id);

        if (postDto.CreatorId.HasValue)
        {
            var creatorUser = await _userLookupService.FindByIdAsync(postDto.CreatorId.Value);

            postDto.Writer = ObjectMapper.Map<BlogUser, BlogUserDto>(creatorUser);
        }

        return postDto;
    }

    public async Task DeleteAsync(Guid id)
    {
        // 查找文章
        var post = await _postRepository.GetAsync(id);
        // 根据文章获取Tags
        var tags = await GetTagsOfPost(id);
        // 减少Tag引用数量
        await _tagRepository.DecreaseUsageCountOfTagsAsync(tags.Select(t => t.Id).ToList());
        // 删除评论
        await _commentRepository.DeleteOfPost(id);
        // 删除文章
        await _postRepository.DeleteAsync(id);

        await PublishPostChangedEventAsync(id);
    }


    public async Task<PostWithDetailsDto> CreateAsync(CreatePostDto input)
    {
        input.Url = await RenameUrlIfItAlreadyExistAsync(input.BlogId, input.Url);

        var post = new Post(
            id: GuidGenerator.Create(),
            blogId: input.BlogId,
            title: input.Title,
            coverImage: input.CoverImage,
            url: input.Url
        )
        {
            Content = input.Content,
            Description = input.Description
        };

        await _postRepository.InsertAsync(post);

        var tagList = SplitTags(input.Tags);
        await SaveTags(tagList, post);

        await PublishPostChangedEventAsync(input.BlogId);
        return ObjectMapper.Map<Post, PostWithDetailsDto>(post);
    }

    public async Task<PostWithDetailsDto> UpdateAsync(Guid id, UpdatePostDto input)
    {
        var post = await _postRepository.GetAsync(id);

        input.Url = await RenameUrlIfItAlreadyExistAsync(input.BlogId, input.Url, post);

        post.SetTitle(input.Title);
        post.SetUrl(input.Url);
        post.Content = input.Content;
        post.Description = input.Description;
        post.CoverImage = input.CoverImage;

        post = await _postRepository.UpdateAsync(post);

        var tagList = SplitTags(input.Tags);
        await SaveTags(tagList, post);

        return ObjectMapper.Map<Post, PostWithDetailsDto>(post);
    }

    private async Task<string> RenameUrlIfItAlreadyExistAsync(Guid blogId, string url, Post existingPost = null)
    {
        if (await _postRepository.IsPostUrlInUseAsync(blogId, url, existingPost?.Id))
        {
            return url + "-" + Guid.NewGuid().ToString().Substring(0, 5);
        }

        return url;
    }

    private async Task SaveTags(ICollection<string> newTags, Post post)
    {
        await RemoveOldTags(newTags, post);

        await AddNewTags(newTags, post);
    }

    private async Task RemoveOldTags(ICollection<string> newTags, Post post)
    {
        foreach (var oldTag in post.Tags.ToList())
        {
            var tag = await _tagRepository.GetAsync(oldTag.TagId);

            var oldTagNameInNewTags = newTags.FirstOrDefault(t => t == tag.Name);

            if (oldTagNameInNewTags == null)
            {
                post.RemoveTag(oldTag.TagId);

                tag.DecreaseUsageCount();
                await _tagRepository.UpdateAsync(tag);
            }
            else
            {
                newTags.Remove(oldTagNameInNewTags);
            }
        }
    }

    private async Task AddNewTags(IEnumerable<string> newTags, Post post)
    {
        var tags = await _tagRepository.GetListAsync(post.BlogId);

        foreach (var newTag in newTags)
        {
            var tag = tags.FirstOrDefault(t => t.Name == newTag);

            if (tag == null)
            {
                tag = await _tagRepository.InsertAsync(new Tag(GuidGenerator.Create(), post.BlogId, newTag, 1));
            }
            else
            {
                tag.IncreaseUsageCount();
                tag = await _tagRepository.UpdateAsync(tag);
            }

            post.AddTag(tag.Id);
        }
    }

    private List<string> SplitTags(string tags)
    {
        if (tags.IsNullOrWhiteSpace())
        {
            return new List<string>();
        }

        return new List<string>(tags.Split(",").Select(t => t.Trim()));
    }
    
    private async Task<List<PostCacheItem>> GetTimeOrderedPostsAsync(Guid blogId)
    {
        var posts = await _postRepository.GetOrderedList(blogId);

        return ObjectMapper.Map<List<Post>, List<PostCacheItem>>(posts);
    }
    
    private async Task PublishPostChangedEventAsync(Guid blogId)
    {
        await _localEventBus.PublishAsync(new PostChangedEvent(blogId));
    }


    private async Task<List<TagDto>> GetTagsOfPost(Guid id)
    {
        var tagIds = (await _postRepository.GetAsync(id)).Tags;

        var tags = await _tagRepository.GetListAsync(tagIds.Select(t => t.TagId));

        return ObjectMapper.Map<List<Tag>, List<TagDto>>(tags);
    }

    private Task<List<PostWithDetailsDto>> FilterPostsByTag(IEnumerable<PostWithDetailsDto> allPostDtos, Tag tag)
    {
        var filteredPostDtos = allPostDtos.Where(p => p.Tags?.Any(t => t.Id == tag.Id) ?? false).ToList();

        return Task.FromResult(filteredPostDtos);
    }
}