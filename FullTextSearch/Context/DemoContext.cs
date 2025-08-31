using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FullTextSearch.Domain;
using Microsoft.EntityFrameworkCore;

namespace FullTextSearch.Context
{
    public class DemoContext : DbContext
    {
        public DemoContext(DbContextOptions<DemoContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
    }
}