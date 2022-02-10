using AzureFunctionCrud;
using AzureFunctionCrud.Models.Context;
using AzureFunctionCrud.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]
namespace AzureFunctionCrud
{
    public class Startup : FunctionsStartup
    {
        IConfiguration configuration { get; set; }
        public override void Configure(IFunctionsHostBuilder builder)
        {
            configuration = builder.GetContext().Configuration;

            string connectionString = configuration["DefaultConnection"];
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddTransient<IStudentService, StudentService>();
        }
    }
}
