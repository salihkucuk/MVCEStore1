using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MvcEStoreData.Insfrastructure;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcEStoreData
{
    public class Product : BaseEntity
    {
        #region Properties
        [Display(Name = "Ürün Adı")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        [MaxLength(250, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır!")]
        public string Name { get; set; }

        public string Picture { get; set; }

        [Display(Name = "Ürün Kodu")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        public string ProductCode { get; set; }

        [Display(Name = "Barkod")]
        [RegularExpression(@"^[0-9]{13}$", ErrorMessage ="Lütfen geçerli bir barkod no yazınız.")]
        public string Barcode { get; set; }

        public decimal Price { get; set; }

        [Display(Name = "İndirim Oranı")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        [RegularExpression(@"^[0-9]{0,2}$", ErrorMessage = "Lütfen geçerli bir {0} yazınız.")]
        public int Discount { get; set; }

        [Display(Name = "Açıklamalar")]
        [DataType(DataType.MultilineText)]
        public string Descriptions { get; set; }

        public int Reviews { get; set; }

        [Display(Name = "Marka")]
        public int? BrandId { get; set; }

        [NotMapped]
        [Display(Name = "Fiyat")]
        [RegularExpression(@"^[0-9]+(\,[0-9]{1,2})?$", ErrorMessage = "Lütfen geçerli bir {0} yazınız.")]
        public string PriceText { get; set; }
        
        [NotMapped]
        [Display(Name = "Görsel")]  
        public IFormFile PictureFile { get; set; }
        
        [NotMapped]
        [Display(Name = "Kategoriler")]
        public int[] SelectedCategories { get; set; }
        
        [NotMapped]
        [Display(Name = "Görsel Galerisi")]
        public IFormFile[] PictureFiles { get; set; }
        
        [NotMapped]
        public int[] PictureFilesToDeleted { get; set; }

        #endregion

        #region Navigation

        public virtual Brand Brand { get; set; }

        public virtual ICollection<CategoryProduct> CategoryProducts { get; set; } = new HashSet<CategoryProduct>();
        public virtual ICollection<OrderItem> OrderItems  { get; set; } = new HashSet<OrderItem>();
        public virtual ICollection<ProductPicture> ProductPictures  { get; set; } = new HashSet<ProductPicture>();




        #endregion

        public override void Build(ModelBuilder builder)
        {
            builder.Entity<Product>(entity =>   
            {
                entity
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(250);

                entity
                .HasIndex(p => new { p.Name })
                .IsUnique();

                entity
                .HasIndex(p => new { p.Price })
                .IsUnique(false);

                entity
                .Property(p => p.Price)
                .HasPrecision(18, 4);

                entity
                .Property(p => p.Picture)
                .IsUnicode(false);

                entity
                .Property(p => p.ProductCode)
                .IsRequired();

                //char(13)
                entity
                .Property(p => p.Barcode)
                .HasMaxLength(13)
                .IsUnicode(false)
                .IsFixedLength();

                entity
                .HasMany(p => p.OrderItems)
                .WithOne(p => p.Product)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

                entity
                .HasMany(p => p.ProductPictures)
                .WithOne(p => p.Product)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            });


        }


    }
}