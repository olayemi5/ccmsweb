using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICAD.Data.DataContext
{
    public class InfopoolContext : DbContext
    {
        public InfopoolContext() : base ("name=InfopoolDb")
        {

        }
    }
}
