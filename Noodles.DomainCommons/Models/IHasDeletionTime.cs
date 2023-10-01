namespace Noodles.DomainCommons.Models;

public interface IHasDeletionTime
{
     DateTime? DeletionTime { get; }
}