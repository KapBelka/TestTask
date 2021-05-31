using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask.Models;

namespace TestTask
{
    public class DataContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductStorage>()
               .HasKey(x => new { x.ProductId, x.StorageId });
            modelBuilder.Entity<ProductStorage>()
                .HasOne(pt => pt.Product)
                .WithMany(p => p.Storages)
                .HasForeignKey(pt => pt.ProductId);

            modelBuilder.Entity<ProductStorage>()
                .HasOne(pt => pt.Storage)
                .WithMany(t => t.Products)
                .HasForeignKey(pt => pt.StorageId);

            modelBuilder.Entity<TransferProduct>()
                .HasKey(x => new { x.ProductId, x.TransferId });

            modelBuilder.Entity<TransferProduct>()
                .HasOne(pt => pt.Product)
                .WithMany(p => p.Transfers)
                .HasForeignKey(pt => pt.ProductId);

            modelBuilder.Entity<TransferProduct>()
                .HasOne(pt => pt.Transfer)
                .WithMany(t => t.Products)
                .HasForeignKey(pt => pt.TransferId);

            modelBuilder.Entity<Transfer>()
               .HasKey(x => x.Id);

            modelBuilder.Entity<Transfer>()
                .HasOne(pt => pt.FromStorage)
                .WithMany(p => p.DeliveryOut)
                .HasForeignKey(pt => pt.FromStorageId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transfer>()
                .HasOne(pt => pt.ToStorage)
                .WithMany(t => t.DeliveryIn)
                .HasForeignKey(pt => pt.ToStorageId)
                .OnDelete(DeleteBehavior.Restrict);


            Product[] products = new Product[] {
                new Product { Id = 1, Name = "Товар1" },
                new Product { Id = 2, Name = "Товар2" },
                new Product { Id = 3, Name = "Товар3" },
                new Product { Id = 4, Name = "Товар4" },
                new Product { Id = 5, Name = "Товар5" },
                new Product { Id = 6, Name = "Товар6" },
                new Product { Id = 7, Name = "Товар7" },
            };
            Storage[] storages = new Storage[] {
                new Storage { Id = 1, Name = "Склад1" },
                new Storage { Id = 2, Name = "Склад2" },
                new Storage { Id = 3, Name = "Склад3" },
            };
            ProductStorage[] productstorages = new ProductStorage[] {
                new ProductStorage { StorageId = 1, ProductId = 1, Count = 2501 },
                new ProductStorage { StorageId = 1, ProductId = 3, Count = 141 },
                new ProductStorage { StorageId = 1, ProductId = 5, Count = 123 },
                new ProductStorage { StorageId = 1, ProductId = 7, Count = 10234 },
                new ProductStorage { StorageId = 2, ProductId = 2, Count = 1024 },
                new ProductStorage { StorageId = 2, ProductId = 4, Count = 359 },
                new ProductStorage { StorageId = 2, ProductId = 6, Count = 401 },
                new ProductStorage { StorageId = 2, ProductId = 1, Count = 5012 },
                new ProductStorage { StorageId = 3, ProductId = 2, Count = 301 },
                new ProductStorage { StorageId = 3, ProductId = 3, Count = 53 },
                new ProductStorage { StorageId = 3, ProductId = 5, Count = 492 },
                new ProductStorage { StorageId = 3, ProductId = 6, Count = 5 },
            };

            modelBuilder.Entity<Product>().HasData(products);
            modelBuilder.Entity<Storage>().HasData(storages);
            modelBuilder.Entity<ProductStorage>().HasData(productstorages);
            base.OnModelCreating(modelBuilder);
        }
    }
}
