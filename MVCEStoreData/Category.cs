using Microsoft.EntityFrameworkCore;
using MvcEStoreData.Insfrastructure;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcEStoreData
{
    public class Category:BaseEntity
    {
        #region Properties
        [Display(Name = "Kategori Adı")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        [MaxLength(50, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır!")]
        public string Name { get; set; }

        public int SortOrder { get; set; }

        [Display(Name = "Reyon")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        public int ReyonId { get; set; }

        

        #endregion

        #region Navigation

        public virtual Reyon Reyon { get; set; }

        public virtual ICollection<Banner> Banners { get; set; } = new HashSet<Banner>();

        public virtual ICollection<CategoryProduct> CategoryProducts { get; set; } = new HashSet<CategoryProduct>();

        public override void Build(ModelBuilder builder)
        {
            builder.Entity<Category>(entity =>
            {
                entity
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);

                entity
                .HasIndex(p => new { p.Name, p.ReyonId })
                .IsUnique();

                entity
                .HasMany(p => p.Banners)
                .WithOne(p => p.Category)
                .HasForeignKey(p=>p.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

                

            });
        }

        #endregion

    }
}