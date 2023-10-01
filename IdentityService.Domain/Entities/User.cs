using Microsoft.AspNetCore.Identity;
using Noodles.DomainCommons.Models;

namespace IdentityService.Domain.Entities;

public class User:IdentityUser<Guid> , IHasCreationTime, IHasDeletionTime, ISoftDelete
{
    public DateTime CreationTime { get; init; }

    public DateTime? DeletionTime { get; private set; }

    public bool IsDeleted { get; private set; }

    /// <summary>
    /// constructor
    /// 通过使用关键字 base，可以调用基类的构造函数，并将参数 userName 传递给它
    /// 基类中的 :this() 是只在调用当前构造函数的同时继续调用类的默认构造函数
    /// </summary>
    /// <param name="userName"></param>
    public User(string userName) : base(userName)
    {
        Id = Guid.NewGuid();
        CreationTime = DateTime.Now;
    }
    
    public void SoftDelete()
    {
        this.IsDeleted = true;
        this.DeletionTime = DateTime.Now;
    }
}