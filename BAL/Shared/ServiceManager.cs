using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Model.ApplicationConfig;
using Microsoft.EntityFrameworkCore;
using Repository.UnitOfWork;
using BAL.IService;
using BAL.Services;
using BAL.IServices;
using BAL.Common;

namespace BAL.Shared
{
    public class ServiceManager
    {
        public static void SetServiceInfo(IServiceCollection services,AppSetting appSetting)
        {
            services.AddDbContextPool<DataContext>(options =>
            {
                options.UseSqlServer(appSetting.ConnectionStrings);
            });
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductService,ProductService>();
            services.AddScoped<ISaleService, SaleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<TokenProvider, TokenProvider>();

        }
    }
}
