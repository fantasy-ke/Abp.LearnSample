using Volo.Abp.Uow;
using Volo.Abp.Users;

namespace BlogSample.Users;

public class BlogUserLookupService: UserLookupService<BlogUser, IBlogUserRepository>, IBlogUserLookupService
{
    public BlogUserLookupService(
        IBlogUserRepository userRepository,
        IUnitOfWorkManager unitOfWorkManager)
        : base(
            userRepository,
            unitOfWorkManager)
    {

    }

    protected override BlogUser CreateUser(IUserData externalUser)
    {
        return new BlogUser(externalUser);
    }
}