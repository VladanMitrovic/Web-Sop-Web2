using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using web2projekat.Models;

namespace web2projekat.Data
{
    public class web2projekatContext : DbContext
    {
        public web2projekatContext (DbContextOptions<web2projekatContext> options)
            : base(options)
        {
        }

        public DbSet<web2projekat.Models.Artikal> Artikal { get; set; } = default!;

        public DbSet<web2projekat.Models.Korisnik>? Korisnik { get; set; }

        public DbSet<web2projekat.Models.Narudzbina>? Narudzbina { get; set; }
    }
}
