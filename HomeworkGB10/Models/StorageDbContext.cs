﻿using HomeworkGB10.Abstractions;
using HomeworkGB10.Models.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace HomeworkGB10.Models
{
    public class StorageDbContext(string connectionString) : DbContext, IStorageDbContext
    {
        private readonly string _connectionString = connectionString;
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Storage> Storages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new StorageConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
