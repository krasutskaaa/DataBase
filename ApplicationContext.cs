using DataBase.Abstractions;
using DataBase.Abstractions.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Client> Clients => Set<Client>();
        public DbSet<Card> Cards => Set<Card>();
        public DbSet<Coach> Coaches => Set<Coach>();
        public ApplicationContext()
        {
            Database.EnsureCreated();
            //Database.EnsureDeleted();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=fitnesscenter.db");
        }
       
    }
}  
