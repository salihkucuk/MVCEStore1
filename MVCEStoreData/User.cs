using Microsoft.AspNetCore.Identity;
using MvcEStoreData.Insfrastructure;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MvcEStoreData
{   
    public enum Genders
    {
        Male, Female
    }
    public class User :IdentityUser<int>, IBaseEntity
    {
        #region Properties

        public string Name { get; set; }

        public Genders? Gender { get; set; }

        #endregion

        #region Navigation


        public virtual ICollection<Banner> Banners { get; set; } = new HashSet<Banner>();

        public virtual ICollection<Brand> Brands { get; set; } = new HashSet<Brand>();

        public virtual ICollection<Category> Categories { get; set; } = new HashSet<Category>();

        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();

        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();

        public virtual ICollection<ProductPicture> ProductPictures { get; set; } = new HashSet<ProductPicture>();

        public virtual ICollection<Reyon> Reyons { get; set; } = new HashSet<Reyon>();

        



        #endregion

        public void Build(ModelBuilder builder)
        {
            builder.Entity<User>(entity =>
            {
                entity
                .Property(p => p.Name)
                .IsRequired();

                entity
                .HasMany(p => p.Banners)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

                entity
                .HasMany(p => p.Brands)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

                entity
                .HasMany(p => p.Categories)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

                entity
                .HasMany(p => p.Orders)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

                entity
                .HasMany(p => p.Products)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

                entity
                .HasMany(p => p.ProductPictures)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

                entity
                .HasMany(p => p.Reyons)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}