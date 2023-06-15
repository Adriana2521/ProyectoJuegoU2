using Microsoft.EntityFrameworkCore;
using TopoLocoApi.Models;

namespace TopoLocoApi.Repositories
{
    public class Repository <T> where T : class
    {
        private readonly Sistem21TopolocoContext Context;
        public Repository(Sistem21TopolocoContext context)
        {
            this.Context = context;
        }
        public DbSet<T> Get()
        {
            return Context.Set<T>();
        }
        public T? Get(object id)
        {
            return Context.Find<T>(id);
        }
        public void Insert(T entity)
        {
            Context.Add(entity);
            Context.SaveChanges();
        }
        public void Update(T entity)
        {
            Context.Update(entity);
            Context.SaveChanges();
        }
        public void Delete(T entity)
        {
            Context.Remove(entity);
            Context.SaveChanges();
        }
    }
}
