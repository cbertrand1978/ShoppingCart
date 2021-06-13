using System;

namespace CartProcessingService.API.Offers
{
    public interface IVisitor<TEntity> where TEntity: IVisitor<TEntity>
    {
        void Visit(IOffer<TEntity> offer);
    }
}
