using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MvcEStoreData.Insfrastructure;
using System;
using System.Linq;
using System.Reflection;

namespace MvcEStoreData
{
    public class AppDbContext : IdentityDbContext<User, Role, int>
    {

        public AppDbContext(DbContextOptions options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(p => !p.IsAbstract && p.GetInterfaces().Any(q => q.Name == nameof(IBaseEntity)))
                .ToList()
                .ForEach(p => p.GetMethod(nameof(IBaseEntity.Build)).Invoke(Activator.CreateInstance(p), new[] { builder }));

           
        }

        public virtual DbSet<Banner> Banners { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductPicture> ProductPictures { get; set; }
        public virtual DbSet<Reyon> Reyons { get; set; }
        public virtual DbSet<CategoryProduct> CategoryProducts { get; set; }
        



    }
}