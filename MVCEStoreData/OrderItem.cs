using Microsoft.EntityFrameworkCore;
using MvcEStoreData.Insfrastructure;


namespace MvcEStoreData
{
    public class OrderItem:IBaseEntity
    {
        #region Properties
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
        #endregion

        #region Navigation

        public virtual Order Order   { get; set; }
        public virtual Product Product   { get; set; }



        #endregion

        public void Build(ModelBuilder builder)
        {
            builder.Entity<OrderItem>(entity =>
            {
                entity
                .Property(p => p.Price)
                .HasPrecision(18, 4);
            });
        }


    }
}