using Microsoft.EntityFrameworkCore;
using System;

namespace ImageShareWithLikes.Data
{
    public class ImageDBContext : DbContext
    {

        private readonly string _connectionString;

        public ImageDBContext(string cs)
        {
            _connectionString = cs;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        public DbSet<Image> Images { get; set; }
    }
}