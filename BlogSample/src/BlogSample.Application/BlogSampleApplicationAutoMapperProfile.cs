using AutoMapper;
using BlogSample.Blogs;

namespace BlogSample;

public class BlogSampleApplicationAutoMapperProfile : Profile
{
    public BlogSampleApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<Blog, BlogDto>();
    }
}
