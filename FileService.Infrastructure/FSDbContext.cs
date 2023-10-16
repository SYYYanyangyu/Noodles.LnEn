using FileService.Domain.Entitys;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Noodles.Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Infrastructure
{
    public class FSDbContext: BaseDbContext
    {
        public DbSet<UploadedItem> UploadItems { get; private set; }

        public FSDbContext(DbContextOptions<FSDbContext> options, IMediator mediator) : base(options, mediator)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //  Entity Framework Core 从当前 DbContext 所在的程序集
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
