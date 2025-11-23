using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using XUnitBlog.Data.Ioc;
using XUnitBlog.Domain.Services;

var builder = WebApplication.CreateBuilder(args);

var key = builder.Configuration.GetValue<string>("Jwt:Secret");
var issuer = builder.Configuration.GetValue<string>("Jwt:Issuer");
var keyBytes = Encoding.UTF8.GetBytes(key);

builder
    .Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var token = context.Request.Cookies["XUnitBlog_Token"];
                if (!string.IsNullOrEmpty(token))
                {
                    context.Token = token;
                }
                return Task.CompletedTask;
            },
            OnChallenge = context =>
            {
                // Impede resposta JSON padrão
                context.HandleResponse();

                // Redireciona para Signin
                context.Response.Redirect("/Auth/Login");
                return Task.CompletedTask;
            },
        };
    });

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Admin");
});

builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddRepositories();

builder.Services.AddScoped<IHashService, HashService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<PostService>();
builder.Services.AddScoped<FileService>(provider =>
{
    var env = provider.GetRequiredService<IWebHostEnvironment>();
    var wwwrootPath = env.WebRootPath;

    return new FileService(wwwrootPath);
});
builder.Services.AddScoped<IJwtService, JwtService>(instance => new JwtService(key, issuer));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.MapRazorPages();

app.Run();
