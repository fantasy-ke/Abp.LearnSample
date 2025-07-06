using AutoMapper;
using BlogSample.Blogs;
using BlogSample.Comments;
using BlogSample.Posts;
using BlogSample.Tags;
using BlogSample.Users;
using Volo.Abp.AutoMapper;
using Volo.Abp.Identity;

namespace BlogSample;

public class BlogSampleApplicationAutoMapperProfile : Profile
{
    public BlogSampleApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<Blog, BlogDto>();
        CreateMap<BlogUser, BlogUserDto>();

        CreateMap<Post, PostCacheItem>().Ignore(x => x.CommentCount).Ignore(x => x.Tags);
        CreateMap<Post, PostWithDetailsDto>().Ignore(x => x.Writer).Ignore(x => x.CommentCount).Ignore(x => x.Tags);
        CreateMap<PostCacheItem, PostWithDetailsDto>()
            .Ignore(x => x.Writer)
            .Ignore(x => x.CommentCount)
            .Ignore(x => x.Tags);

        
        CreateMap<Comment, CommentWithDetailsDto>().Ignore(x => x.Writer);

        CreateMap<Tag, TagDto>();
    }
}
