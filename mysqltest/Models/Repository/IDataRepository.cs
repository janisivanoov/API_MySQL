using System.Collections.Generic;
using mysqltest.Paging;

namespace mysqltest.Models.Repository
{
    public interface IDataRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll(QueryParameters queryParameters);

        TEntity Get(long id);

        void Add(TEntity entity);

        void Update(TEntity dbEntity, TEntity entity);

        void Delete(TEntity entity);
    }
}