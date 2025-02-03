using ICAD.Data.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICAD.Data.DataContext
{
    public class IcadDataContext : DbContext
    {
        public IcadDataContext() : base("name=ICADConnection")
        {

        }

        public virtual DbSet<AccountInfo> AccountRecords { get; set; }
        //public virtual DbSet<BvnData> Bvns { get; set; }
    }
}
