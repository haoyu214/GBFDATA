using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LDLR.Core;
using System.Data.Entity;
using System.Reflection;
using System.Data.Entity.ModelConfiguration;
using GreeterClient.Model;
using Quartz;
using log4net;
namespace GreeterClient.DB
{
    public class GreetDbContext : DbContext

    {
        public GreetDbContext() : base("WWB")
        {
            this.Configuration.AutoDetectChangesEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;
            log4net.LogManager.GetLogger(this.GetType()).Debug("调用基类数据库构造方法");
            Database.SetInitializer<GreetDbContext>(null);
        }

        public DbSet<Company> Company { get; set; }
        public DbSet<TAB_PLATFORM_INFO> TAB_PLATFORM_INFO { get; set; }
        public DbSet<STAT_INTERFACE_STAT> STAT_INTERFACE_STAT { get; set; }
        public DbSet<STAT_OFFLINE_SERVICE> STAT_OFFLINE_SERVICE { get; set; }
        public DbSet<STAT_SERVICE_DATA> STAT_SERVICE_DATA { get; set; }
        public DbSet<STAT_DOUBLE_SYSTEM_SERVIICE> STAT_DOUBLE_SYSTEM_SERVIICE { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
            // .Where(type => !String.IsNullOrEmpty(type.Namespace))
            // .Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            //foreach (var type in typesToRegister)
            //{
            //    dynamic configurationInstance = Activator.CreateInstance(type);
            //    modelBuilder.Configurations.Add(configurationInstance);

            //    modelBuilder.Entity<Company>();
            //}
        
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
            //modelBuilder.Entity<Company>();
           base.OnModelCreating(modelBuilder);

        }




    }
}
