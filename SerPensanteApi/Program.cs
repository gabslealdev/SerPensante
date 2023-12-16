using SerPensanteApi.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
builder.Services.AddDbContext<SpenDataContext>();

var app = builder.Build();
app.MapControllers();

app.Run();