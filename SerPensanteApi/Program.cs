using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SerPensanteApi;
using SerPensanteApi.Services;
using SerPensanteApi.Data;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;


var builder = WebApplication.CreateBuilder(args);
ConfigureAuthentication(builder);
ConfigureMvc(builder);
ConfigureService(builder);

var app = builder.Build();
Configuration.JwtKey = app.Configuration.GetValue<string>("JwtKey");

app.UseAuthentication();
app.UseAuthorization();
app.UseResponseCompression();
app.UseStaticFiles();
app.MapControllers();
app.UseStaticFiles();
app.Run();

void ConfigureAuthentication(WebApplicationBuilder buider)
{
    var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);
    
    builder.Services.AddResponseCompression(options =>
    {
        options.Providers.Add<GzipCompressionProvider>();
        
    });

    builder.Services.Configure<GzipCompressionProviderOptions>(options => 
    {
        options.Level = CompressionLevel.Optimal;
    });

    builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(x =>
    {
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });
}

void ConfigureMvc(WebApplicationBuilder builder)
{
    builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        }).AddJsonOptions(x => {
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
        });
}

void ConfigureService(WebApplicationBuilder builder)
{
    builder.Services.AddDbContext<SpenDataContext>();
    builder.Services.AddTransient<TokenService>();
}


