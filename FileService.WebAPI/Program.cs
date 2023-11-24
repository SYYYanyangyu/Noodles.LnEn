using CommonInitializer;
using FileService.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureDbConfiguration();
builder.ConfigureExtraServices(new InitializerOptions
{
    LogFilePath = "e:/temp/FileService.log",
    EventBusQueueName = "FileService.WebAPI",
});

// Add services to the container.
// Add services to the container.
builder.Services//.AddOptions() //asp.net core项目中AddOptions()不写也行，因为框架一定自动执行了
    .Configure<SMBStorageOptions>(builder.Configuration.GetSection("FileService:SMB"))
    .Configure<UpYunStorageOptions>(builder.Configuration.GetSection("FileService:UpYun"));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FileService.WebAPI v1"));
}

app.UseStaticFiles();
app.UseDefault();

app.MapControllers();


app.Run();
