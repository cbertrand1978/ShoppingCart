using System;

namespace CartProcessingService.API.Offers
{
    public interface IOffer<TEntity> where TEntity : IVisitor<TEntity>
    {
        void Apply(TEntity item);
    }
}
