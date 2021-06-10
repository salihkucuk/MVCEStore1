using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace MvcEStoreData.Insfrastructure
{
    public abstract class BaseEntity: IBaseEntity
    {
        public int Id { get; set; }

        [Display(Name="Yayınla")]
        public bool Enabled { get; set; }

        public DateTime Date { get; set; }

        public int UserId { get; set; }


        public virtual User User { get; set; }

        public abstract void Build(ModelBuilder builder);


    }
}