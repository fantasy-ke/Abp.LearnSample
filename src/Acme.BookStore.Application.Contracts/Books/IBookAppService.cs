using System;
using System.Linq;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.Books
{
    public interface IBookAppService: ICrudAppService<
        BookDto, // Used to show books
        Guid, // Primary key of the book entity
        PagedAndSortedResultRequestDto, // Used for paging and sorting
        CreateUpdateBookDto> // Used to create or update a book
    {
    }
}
