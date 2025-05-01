using apicampusjob.Configuaration;
using apicampusjob.Extensions;
using apicampusjob.Repository;
using apicampusjob.Service;
using Microsoft.OpenApi.Models;
using secondhand_market.Repository;
using static apicampusjob.Service.UserServicecs;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using dotenv.net;
using DotNetEnv;
using apicampusjob.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using apicampusjob.Hubs;

var builder = WebApplication.CreateBuilder(args);

Env.Load();
builder.Services.Configure<CloudinarySettings>(options =>
{
    options.CloudName = Environment.GetEnvironmentVariable("CLOUDINARY_CLOUD_NAME");
    options.ApiKey = Environment.GetEnvironmentVariable("CLOUDINARY_API_KEY");
    options.ApiSecret = Environment.GetEnvironmentVariable("CLOUDINARY_API_SECRET");
});
builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
});


// Add services to the container.
var appSettings = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettings);
GlobalSettings.IncludeConfig(appSettings.Get<AppSettings>());

builder.Services.ConfigureDbContext(GlobalSettings.AppSettings.Database.DatabaseConfig);
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Nhập token vào đây:",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    //var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //opt.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    opt.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.WithOrigins(
                "http://localhost:5173",        // Thêm origin của client
                "http://localhost:8080",
                "http://localhost:3000",
                "http://127.0.0.1:5500",        // Cho file HTML local
                "http://192.168.0.106:5173"     // IP local nếu cần
            )  // Trong môi trường phát triển, hoặc danh sách cụ thể trong sản xuất
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials(); // Quan trọng cho SignalR
    });
});

builder.Services.AddAppScopedService();
builder.Services.AddSignalR(); // trong phần cấu hình dịch vụ
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IRegionsService, RegionsService>();
builder.Services.AddScoped<ICompaniesService, CompaniesService>();
builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddScoped<IJobScheduleService, JobScheduleService>();
builder.Services.AddScoped<CloudinaryService>();
builder.Services.AddScoped<ICVService,CVService>();
builder.Services.AddScoped<IStudentAvailabilityService, StudentAvailabilityService>();
builder.Services.AddScoped<ISkillService, SkillService>();
builder.Services.AddScoped<IStudentSkillService, StudentSkillService>();
builder.Services.AddScoped<IJobSkillService, JobSkillService>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();
builder.Services.AddScoped<IOtpService, OtpService>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IConversationRepository, ConversationRepository>();
builder.Services.AddScoped<IConversationService, ConversationService>();

builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();
builder.Services.AddScoped<IJobSkillRepository, JobSkillRepository>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();
builder.Services.AddScoped<ICompaniesRepository, CompaniesRepository>();
builder.Services.AddScoped<IRegionsRepository, RegionsRepository>();
builder.Services.AddScoped<ISessionRepository, SessionRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRegionsRepository, RegionsRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IJobRepository, JobRepository>();
builder.Services.AddScoped<IJobScheduleRepository, JobScheduleRepository>();
builder.Services.AddScoped<ICVRepository, CVRepository>();
builder.Services.AddScoped<IStudentAvailabilityRepository, StudentAvailabilityRepository>();
builder.Services.AddScoped<IStudentSkillRepository, StudentSkillRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();

app.MapHub<ChatHub>("/chatHub");
app.MapControllers();
app.UseRouting();
app.UseWebSockets();
app.Run();
