using mysqltest.Models.Repository;
using System.Collections.Generic;
using System.Linq;
using mysqltest.Paging;

namespace mysqltest.Models.DataManager
{
    public class ClubManager : IDataRepository<Club>
    {
        private ClubsContext _clubsContext;

        public ClubManager(ClubsContext context)
        {
            _clubsContext = context;
        }

        public IEnumerable<Club> GetAll(QueryParameters qp)
        {
            return _clubsContext.Clubs.OrderBy(c => c.Id).Skip((qp.PageNumber-1)*qp.PageSize).Take(qp.PageSize).ToList();
        }

        public Club Get(long id)
        {
            return _clubsContext.Clubs
                  .FirstOrDefault(c => c.Id == id);
        }

        public void Add(Club entity)
        {
            entity.CreatedDate = System.DateTime.Now;
            entity.UpdatedDate = System.DateTime.Now;
            _clubsContext.Clubs.Add(entity);
            _clubsContext.SaveChanges();
        }

        public void Update(Club club, Club entity)
        {
            club.Name = entity.Name;
            club.Type = entity.Type;

            _clubsContext.SaveChanges();
        }

        public void Delete(Club club)
        {
            _clubsContext.Clubs.Remove(club);
            _clubsContext.SaveChanges();
        }

        //TODO: Thats for BaseEntity for Club.Models

        /*
         public override int SaveChanges()
{
    var entries = ChangeTracker
        .Entries()
        .Where(e => e.Entity is BaseEntity && (
                e.State == EntityState.Added
                || e.State == EntityState.Modified));

    foreach (var entityEntry in entries)
    {
        ((BaseEntity)entityEntry.Entity).UpdatedDate = DateTime.Now;

        if (entityEntry.State == EntityState.Added)
        {
            ((BaseEntity)entityEntry.Entity).CreatedDate = DateTime.Now;
        }
    }

    return base.SaveChanges();
}
         */
    }
}