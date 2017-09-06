using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using JPalmaresWebApp.Models;

namespace JPalmaresWebApp.Models
{
    public class JPalmaresWebAppContext : DbContext
    {
        public JPalmaresWebAppContext(DbContextOptions<JPalmaresWebAppContext> options)
            : base(options)
        {
        }

        public DbSet<JPalmaresWebApp.Models.Partite> Partite { get; set; }

        public DbSet<JPalmaresWebApp.Models.Squadre> Squadre { get; set; }

        public DbSet<JPalmaresWebApp.Models.Trofei> Trofei { get; set; }

        public DbSet<JPalmaresWebApp.Models.Vittorie> Vittorie { get; set; }
    }
}
