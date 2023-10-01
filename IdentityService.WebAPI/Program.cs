using CommonInitializer;
using IdentityService.Domain;
using IdentityService.Domain.Entities;
using IdentityService.Infrastructure;
using IdentityService.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureDbConfiguration();
builder.ConfigureExtraServices(new InitializerOptions
{
    EventBusQueueName = "IdentityService.WebAPI",
    LogFilePath = "home/temp/IdentityService.log"
});

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "IdentityService.WebAPI", Version = "v1" });
    //c.AddAuthenticationHeader();
});


builder.Services.AddDataProtection();

IdentityBuilder idBuilder = builder.Services.AddIdentityCore<User>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    
    options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
    options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
});

idBuilder = new IdentityBuilder(idBuilder.UserType, typeof(Role), builder.Services);
idBuilder.AddEntityFrameworkStores<IdDbContext>().AddDefaultTokenProviders()
    .AddRoleManager<RoleManager<Role>>()
    .AddUserManager<IdUserManager>();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddScoped<IEmailSender, MockEmailSender>();
    builder.Services.AddScoped<ISmsSender, MockSmsSender>();
}
else
{
    builder.Services.AddScoped<IEmailSender, SendCloudEmailSender>();
    builder.Services.AddScoped<ISmsSender, SendCloudSmsSender>();
}

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IdentityService.WebAPI v1"));
}

app.UseDefault();
app.MapControllers();
app.Run();