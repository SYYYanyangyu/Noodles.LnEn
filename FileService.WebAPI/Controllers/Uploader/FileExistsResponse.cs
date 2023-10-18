
namespace FileService.WebAPI.Controllers.Uploader;


//IsExists是否存在这样的文件，如果存在，则Url代表这个文件的路径
// record 只能比较值类型
public record FileExistsResponse(bool IsExists, Uri? Url);
