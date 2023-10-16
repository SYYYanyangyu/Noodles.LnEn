using FileService.Domain.Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Infrastructure.Configs
{
    public class UploadedItemConfig : IEntityTypeConfiguration<UploadedItem>
    {
        public void Configure(EntityTypeBuilder<UploadedItem> builder)
        {
            builder.ToTable("T_FS_UploadedItems");
            //因为SQLServer对于Guid主键默认创建聚集索引，因此会造成每次插入新数据，都会数据库重排。
            //因此我们取消主键的默认的聚集索引
            builder.HasKey(e => e.Id).IsClustered(false);
            builder.Property(e => e.FileName).IsUnicode().HasMaxLength(1024);
            builder.Property(e => e.FileSHA256Hash).IsUnicode(false).HasMaxLength(64);
            builder.HasIndex(e => new { e.FileSHA256Hash, e.FileSizeInBytes });//经常要按照这两个列进行查询，因此把它们两个组成复合索引，提高查询效率。
        }
    }
}
