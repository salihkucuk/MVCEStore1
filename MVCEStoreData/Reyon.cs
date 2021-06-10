using Microsoft.EntityFrameworkCore;
using MvcEStoreData.Insfrastructure;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcEStoreData
{
    public class Reyon : BaseEntity
    {
        #region Properties
        [Display(Name = "Reyon Adı")]
        [Required(ErrorMessage ="{0} alanı boş bırakılamaz!")]
        [MaxLength(50, ErrorMessage ="{0} alanı en fazla {1} karakter olmalıdır!")]
        public string Name { get; set; }
        public int SortOrder { get; set; }
        #endregion

        #region Navigation

        public virtual ICollection<Category> Categories { get; set; } = new HashSet<Category>();


        #endregion

        public override void Build(ModelBuilder builder)
        {

            builder.Entity<Reyon>(entity =>
            {
                entity
              .Property(p => p.Name)
              .HasMaxLength(50)
              .IsRequired();

                entity
                .HasIndex(p => p.Name)
                .IsUnique();

                entity
                .HasMany(p => p.Categories)
                .WithOne(p => p.Reyon)
                .HasForeignKey(p => p.ReyonId)
                .OnDelete(DeleteBehavior.Restrict);
            });
        }



    }
}