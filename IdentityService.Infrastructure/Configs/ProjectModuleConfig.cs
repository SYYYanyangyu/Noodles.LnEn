using IdentityService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityService.Infrastructure;

public class ModuleConfig : IEntityTypeConfiguration<ProjectModule>
{
    public void Configure(EntityTypeBuilder<ProjectModule> builder)
    {
        try
        {
            builder.ToTable("T_ProjectModule");

            builder.HasKey(e => e.Id);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
