using Microsoft.EntityFrameworkCore;

namespace MvcEStoreData.Insfrastructure
{
    public interface IBaseEntity
    {
        void Build(ModelBuilder builder);
    }
}