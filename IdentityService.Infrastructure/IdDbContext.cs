using System.Reflection;
using IdentityService.Domain;
using IdentityService.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Infrastructure;

public class IdDbContext: IdentityDbContext<User, Role, Guid>
{


    public DbSet<ProjectModule> Module { get; private set; }//不要忘了写set，否则拿到的DbContext的Categories

    public IdDbContext(DbContextOptions<IdDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        try
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            modelBuilder.EnableSoftDeletionGlobalFilter();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}