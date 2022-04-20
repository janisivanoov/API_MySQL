using mysqltest.Models.Repository;
using System.Collections.Generic;
using System.Linq;
using mysqltest.Paging;

namespace mysqltest.Models.DataManager
{
    public class StudentManager : IDataRepository<Student>
    {
        private ClubsContext _clubsContext;

        public StudentManager(ClubsContext context)
        {
            _clubsContext = context;
        }

        public IEnumerable<Student> GetAll(QueryParameters qp)
        {
            return _clubsContext.Students.OrderBy(c => c.Id).Skip((qp.PageNumber - 1) * qp.PageSize).Take(qp.PageSize).ToList();
        }

        public Student Get(long id)
        {
            return _clubsContext.Students
                  .FirstOrDefault(c => c.Id == id);
        }

        public void Add(Student entity)
        {
            _clubsContext.Students.Add(entity);
            _clubsContext.SaveChanges();
        }

        public void Update(Student student, Student entity)
        {
            student.FirstName = entity.FirstName;
            student.LastName = entity.LastName;
            student.BirthDate = entity.BirthDate;
            student.Password = entity.Password;

            _clubsContext.SaveChanges();
        }

        public void Delete(Student student)
        {
            _clubsContext.Students.Remove(student);
            _clubsContext.SaveChanges();
        }

        //TODO: Thats for BaseEntity for Student.Models

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