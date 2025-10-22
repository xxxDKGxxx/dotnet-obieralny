using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanHub.Backend.Infrastructure.Data;

namespace LoanHub.Backend.Infrastructure;
public static class DatabaseConfig
{
    public static void AddApplicationDbContext(this IServiceCollection services, string connectionString) =>
    services.AddDbContext<AppDbContext>(options =>
         options.UseSqlServer(connectionString));
}
