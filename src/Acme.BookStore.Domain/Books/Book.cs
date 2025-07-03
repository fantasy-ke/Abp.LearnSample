using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.BookStore.Books;

public class Book:AuditedAggregateRoot<Guid>
{
    /// <summary>
    /// 书本名
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 类型
    /// </summary>
    public BookType Type { get; set; }

    /// <summary>
    /// 上传时间
    /// </summary>
    public DateTime PublishDate { get; set; }

    /// <summary>
    /// 单价
    /// </summary>
    public float Price { get; set; }

}
