namespace Noodles.DomainCommons.Models;

public interface IHasModificationTime
{
    DateTime? LastModificationTime { get; }
}