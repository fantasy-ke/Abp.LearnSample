using Acme.BookStore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Acme.BookStore.Authors;

public class AuthorRepository : EfCoreRepository<BookStoreDbContext, Author, Guid>, IAuthorRepository
{
    public AuthorRepository(
       IDbContextProvider<BookStoreDbContext> dbContextProvider
       ) : base(dbContextProvider)
    {
    }
    public async Task<Author> FindByNameAsync(string name)
    {
        var queryable = await GetQueryableAsync();
        return await queryable.FirstOrDefaultAsync(author => author.Name == name)!;
    }

    public async Task<List<Author>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string filter = null)
    {
        var queryable = await GetQueryableAsync();
        return await queryable
            .WhereIf<Author, IQueryable<Author>>(
                !filter.IsNullOrWhiteSpace(),
                author => author.Name.Contains(filter)
            )
            //.OrderBy(sorting)
            .As<IQueryable<Author>>()
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync();
    }
}
