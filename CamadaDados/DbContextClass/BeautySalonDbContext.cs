using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamadaDados.DbContextClass
{
    public class BeautySalonDbContext : DbContext
    {
        public BeautySalonDbContext() : base("BeautySalonDB")
        {
        }

        public DbSet<ClienteModel> Clientes { get; set; }

    }
}
