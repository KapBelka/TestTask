using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTask
{
    static public class ServiceProviderExtensions
    {
        public static void AddStorageService(this IServiceCollection services)
        {
            services.AddScoped<StorageService>();
        }
    }
}
