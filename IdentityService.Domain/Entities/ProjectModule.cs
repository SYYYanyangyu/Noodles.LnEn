using Noodles.DomainCommons.Models;

namespace IdentityService.Domain;

public record ProjectModule : BaseEntity
{


    /// <summary>
    /// ModuleName
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// ModuleNameDesc
    /// </summary>
    public string NameDesc { get; private set; }

    /// <summary>
    /// ModulePath
    /// </summary>
    public string Path { get; private set; }

    /// <summary>
    /// moduleDescription
    /// </summary>
    public string Description { get; private set; }

    /// <summary>
    /// Moduleleves
    /// </summary>
    public int Leves { get; private set; }

    /// <summary>
    /// temporary variable
    /// </summary>
    public string Temp { get; private set; }

    public static ProjectModule Create(Guid id, string name, string nameDesc, string description, string path)
    {
        ProjectModule module = new ProjectModule();
        module.Id = id;
        module.Name = name;
        module.NameDesc = nameDesc;
        module.Description = description;
        module.Path = path;
        return module;
    }

    /// <summary>
    /// 设置私有属性后 对该属性进行修改 需要在方法内部进行修改
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public ProjectModule ChangeName(string name)
    {
        this.Name = name;
        return this;
    }

    public ProjectModule ChangeNameDesc(string nameDesc)
    {
        this.NameDesc = nameDesc;
        return this;
    }

    public ProjectModule ChangePath(string path)
    {
        this.Path = path;
        return this;
    }

    public ProjectModule ChangeDescription(string description)
    {
        this.Description = description;
        return this;
    }
}
