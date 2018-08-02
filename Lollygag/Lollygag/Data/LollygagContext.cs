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
    }
}
