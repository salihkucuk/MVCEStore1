using Microsoft.EntityFrameworkCore;
using MvcEStoreData.Insfrastructure;


namespace MvcEStoreData
{
    public class CategoryProduct:IBaseEntity
    {
        #region Properties

        public int CategoryId { get; set; }

        public int ProductId { get; set; }

        #endregion

        #region Navigation

        public virtual Category Category { get; set; }

        public virtual Product Product { get; set; }

       

        #endregion
        public void Build(ModelBuilder builder)
        {
            builder.Entity<CategoryProduct>(entity =>
            {
            
                entity
                .HasKey(p => new { p.CategoryId, p.ProductId });

                entity
                    .HasOne(p => p.Category)
                    .WithMany(p => p.CategoryProducts)
                    .HasForeignKey(p => p.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity
                    .HasOne(p => p.Product)
                    .WithMany(p => p.CategoryProducts)
                    .HasForeignKey(p => p.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade);

            });
        }

    }
}