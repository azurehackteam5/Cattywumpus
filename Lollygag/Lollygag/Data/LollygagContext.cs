using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Lollygag.Models
{
    public class LollygagContext : DbContext
    {
        public LollygagContext (DbContextOptions<LollygagContext> options)
            : base(options)
        {
        }

        public DbSet<Lollygag.Models.Product> Product { get; set; }

        public DbSet<Lollygag.Models.LGCatalog> LGCatalog { get; set; }
        
        public DbSet<Lollygag.Models.LGCompany> LGCompany { get; set; }

        public DbSet<Lollygag.Models.LGRole> LGRole { get; set; }
        public DbSet<Lollygag.Models.LGUser> LGUser { get; set; }
    }
}
