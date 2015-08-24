using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZJ.Intern4job.Model.DAL;
using ZJ.Intern4job.Model.Repository.Interface;

namespace ZJ.Intern4job.Model.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        protected Intern4jobEntities context;

        public BaseRepository(Intern4jobEntities c)
        {
            context = c;
        }

        public void Create(T obj4create)
        {
            context.Set<T>().Add(obj4create);
        }

        public T GetByModel(T model4query)
        {
            return context.Set<T>().Find(model4query);
        }

        public void Delete(T obj4delete)
        {
            context.Set<T>().Attach(obj4delete);
            context.Entry(obj4delete).State = EntityState.Deleted;
        }

        public void BulkDelete(List<T> objs4delete)
        {
            foreach (T obj in objs4delete)
            {
                Delete(obj);
            }
        }

        public void Update(T obj4update)
        {
            context.Set<T>().Attach(obj4update);
            context.Entry(obj4update).State = EntityState.Modified;
        }

        public List<T> GetPageList(Func<T, bool> where, Func<T, dynamic> orderbyKey, string orderbyValue, int pageOffset, int pageSize)
        {
            return string.Equals(orderbyValue, "asc", StringComparison.OrdinalIgnoreCase) ?
                context.Set<T>().Where(where).OrderBy(orderbyKey).Skip(pageOffset).Take(pageSize).ToList()
                :
                context.Set<T>().Where(where).OrderByDescending(orderbyKey).Skip(pageOffset).Take(pageSize).ToList();
        }

        public int GetPageCount(Func<T, bool> where)
        {
            return context.Set<T>().Where(where).Count();
        }

        public List<T> GetAll()
        {
            return context.Set<T>().ToList();
        }
    }
}