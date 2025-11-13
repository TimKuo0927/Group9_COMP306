
using group9_GroupProject.Mapping;
using group9_GroupProject.Models;
using group9_GroupProject.Services;
using Microsoft.EntityFrameworkCore;

namespace group9_GroupProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<LmsContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("LmsConnection")));
            builder.Services.AddControllers().AddNewtonsoftJson();
            builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile).Assembly);
            builder.Services.AddScoped<PublisherRepository>();
            builder.Services.AddScoped<BookCategoryRepository>();
            builder.Services.AddScoped<BooksRepository>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
          

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
