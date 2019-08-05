using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseOrganizer.MobileAppService.Models;

namespace WarehouseOrganizer.MobileAppService
{
    public partial class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            bool isCreate = Database.EnsureCreated();
            if (isCreate) CreateDemoData();
        }

        public DbSet<Item> Items { get; set; }

        public DbSet<WarehousePlace> WarehousePlaces { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=warehouseOrgDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>().HasOne(p => p.WarehousePlace)
                .WithMany(t => t.ItemsOnPlace)
                .HasForeignKey(p => p.WarehousePlaceId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Item>().HasIndex(p => p.WarehousePlaceId);
        }

        protected void CreateDemoData()
        {
            for (int i = 1; i < 10; i++)
            {
                var place = new WarehousePlace();
                place.PlaceColumn = i * 3;
                place.PlaceRow = i * 2;
                place.PlaceName = $"Place Name - {i}";
                WarehousePlaces.Add(place);
            }

            try
            {
                SaveChanges();
            }
            catch (Exception exc)
            {
                //TODO:Logger
                throw exc;
            }

            WarehousePlace[] placeToBind = new WarehousePlace[2];

            placeToBind[0] = WarehousePlaces.FirstOrDefault();
            placeToBind[1] = WarehousePlaces.Skip(1).FirstOrDefault();

            for (int i = 1; i < 20; i++)
            {
                var item = new Item();
                item.ItemType = $"Detail Type - {i}";
                item.SizeWidth = i * 2;
                item.SizeHeight = i * 4;
                item.SizeDepth = i * 3;
                item.DateOfProduction = DateTime.Now.Date;
                item.WarehousePlace = placeToBind[i % 2];

                Items.Add(item);
            }

            try
            {
                SaveChanges();
            }
            catch (Exception exc)
            {
                //TODO:Logger
                throw exc;
            }
            
        }
    }
}
