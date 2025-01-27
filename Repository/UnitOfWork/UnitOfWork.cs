using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Model;
using Model.ApplicationConfig;
using Model.Entities;
using Repository.Respositories.IRespositories;
using Repository.Respositories.Respositories;

namespace Repository.UnitOfWork
{
    public class UnitOfWork:IUnitOfWork
    {
        private DataContext _dataContext;
        public UnitOfWork(DataContext dataContext,IOptions<AppSetting>appsetting) { 
        _dataContext=dataContext;
            AppSetting  = appsetting.Value;
            Products=new ProductRespository(dataContext);
            Sales = new SaleRespository(dataContext);
            Users = new UserRepository(dataContext);

        }
        public IProductRepository Products {  get; set; }
        public ISaleRepository Sales { get; set; }
        public IUserRepository Users { get; set; }

        public AppSetting AppSetting { get; set; }  
        public void Dispose()
        {
            _dataContext.Dispose();
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _dataContext.SaveChangesAsync();
        }
    }
}
