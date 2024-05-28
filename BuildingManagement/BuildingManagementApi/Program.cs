using BuildingManagementApi.Filters;
using DataAccess;
using ServiceFactory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ExceptionFilter));
});
builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddBusinessLogicServices();
builder.Services.AddDataAccessServices(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddScoped<BuildingManagementApi.Filters.AuthenticationFilter>();

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

// Seed data, BORRAR CUANDO ENTREGUEMOSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS!!!!!!!!!!!!!!!!!!!!!!!!!
////using (var scope = app.Services.CreateScope())
////{
////    var services = scope.ServiceProvider;
////    try
////    {
////        var context = services.GetRequiredService<BuildingManagementDbContext>();
////        BuildingManagementDbContext.SeedData(context);
////    }
////    catch (Exception ex)
////    {
////        // Handle exception
////        Console.WriteLine("An error occurred while seeding the database: " + ex.Message);
////    }
////}
// fin DE Seed data, BORRAR CUANDO ENTREGUEMOSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS!!!!!!!!!!!!!!!!!!!!!!!!!

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Aplicar política de CORS
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
