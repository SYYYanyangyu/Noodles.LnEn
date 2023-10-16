using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Domain
{
    public enum StorageType
    {
        Public,//供公众访问的存储设备
        Backup//内网备份用的存储设备
    }
}
