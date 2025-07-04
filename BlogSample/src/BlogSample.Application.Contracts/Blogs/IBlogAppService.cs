using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace BlogSample.Blogs;

public interface IBlogAppService: IApplicationService
{
    Task<ListResultDto<BlogDto>> GetListAsync();

    Task<BlogDto> GetByShortNameAsync(string shortName);

    Task<BlogDto> GetAsync(Guid id);
}