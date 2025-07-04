using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;

namespace BlogSample.Users;

public interface IBlogUserRepository: IRepository<BlogUser, Guid>, IUserRepository<BlogUser>
{
    Task<List<BlogUser>> GetUsersAsync(int maxCount, string filter);
}