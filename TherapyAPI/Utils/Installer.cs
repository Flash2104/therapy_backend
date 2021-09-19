using System;
using Microsoft.Extensions.DependencyInjection;
using Utils.SberbankAcquiring;
using Utils.SmsC;

namespace Utils
{
    public static class Installer
    {
        public static void AddUtilsServices(this IServiceCollection container)
        {
            container
                .AddScoped<SmscHelper>()
                .AddScoped<SberbankService>();
        }
    }
}
