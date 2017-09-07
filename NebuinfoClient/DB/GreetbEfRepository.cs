using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LDLR.Core;
using LDLR.Core.EFCore;
namespace GreeterClient.DB
{
   public class GreetbEfRepository<T>:EFRepository<T> where T:class
    {
        GreetDbContext gb = new GreetDbContext();
        public GreetbEfRepository() : base(new GreetDbContext())
        { }


        public void DeleteAll()
        {
           string[] p = { };
           gb.Database.ExecuteSqlCommand("truncate table [dbo].[tCompany]",  p);
        }
    }
}
