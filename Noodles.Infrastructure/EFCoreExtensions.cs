using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Noodles.DomainCommons.Models;

namespace Noodles.Infrastructure;

public static class EFCoreExtensions
{
   public static void EnableSoftDeletionGlobalFilter(this ModelBuilder modelBuilder)
   {
      var entityTypesHasSoftDeletion = modelBuilder.Model.GetEntityTypes()
         .Where(syy => syy.ClrType.IsAssignableTo(typeof(ISoftDelete)));

      foreach (var entityType in entityTypesHasSoftDeletion)
      {
         var isDeletedProperty = entityType.FindProperty(nameof(ISoftDelete.IsDeleted));
         var parameter = Expression.Parameter(entityType.ClrType, "p");
         var filter = Expression.Lambda(Expression.Not(Expression.Property(parameter, isDeletedProperty.PropertyInfo)), parameter);
         entityType.SetQueryFilter(filter);
      }
   }
   
   public static IQueryable<T> Query<T>(this DbContext ctx) where T : class, IEntity
   {
      return ctx.Set<T>().AsNoTracking();
   }
}