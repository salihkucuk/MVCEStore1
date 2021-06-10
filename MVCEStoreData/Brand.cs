using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MvcEStoreData.Insfrastructure;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcEStoreData
{
    public class Brand:BaseEntity
    {
        #region Properties
        [Display(Name = "Marka Adı")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        [MaxLength(50, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır!")]
        public string Name { get; set; }

        public string Picture { get; set; }

        public int SortOrder { get; set; }

        [NotMapped]
        [Display(Name = "Görsel")]
        public IFormFile PictureFile { get; set; }

        #endregion

        #region Navigation

        public virtual ICollection<Product> Products{get; set; } = new HashSet<Product>();

        public override void Build(ModelBuilder builder)
        {
            builder.Entity<Brand>(entity =>
            {
                entity
                .Property(p => p.Picture)
                .IsRequired()
                .IsUnicode(false);

                entity
               .Property(p => p.Name)
               .HasMaxLength(50)
               .IsRequired();

                entity
                .HasIndex(p => p.Name)
                .IsUnique();

                entity
                .HasMany(p => p.Products)
                .WithOne(p => p.Brand)
                .HasForeignKey(p => p.BrandId)
                .OnDelete(DeleteBehavior.SetNull);

            });
        }

        #endregion


    }
}