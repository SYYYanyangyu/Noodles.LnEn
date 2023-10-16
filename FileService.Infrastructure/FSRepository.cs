using FileService.Domain;
using FileService.Domain.Entitys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Infrastructure;

public class FSRepository : IFSRepository
{
    private readonly FSDbContext dbContext;

    public FSRepository(FSDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    // find
    public Task<UploadedItem?> FindFileAsync(long fileSize, string sha256Hash)
    {
        return dbContext.UploadItems.FirstOrDefaultAsync(u => u.FileSizeInBytes == fileSize
        && u.FileSHA256Hash == sha256Hash);
    }
}

