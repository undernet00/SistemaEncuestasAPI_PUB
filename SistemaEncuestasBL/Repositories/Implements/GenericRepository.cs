using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Threading.Tasks;
using SistemaEncuestasBL.Data;

namespace SistemaEncuestasBL.Repositories.Implements
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly SistemaEncuestasContext encuestaContext;

        public GenericRepository(SistemaEncuestasContext encuestaContext)
        {
            this.encuestaContext = encuestaContext;
        }
        
        public async Task<TEntity> GetById(int id)
        {
            var entidad = await encuestaContext.Set<TEntity>().FindAsync(id);

            if (entidad == null) throw new Exception("No existe Entidad con ese ID.");

            return entidad;
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await encuestaContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> Insert(TEntity entity)
        {
            if(entity == null)  throw new Exception("La Entidad no puede ser NULL.");

            encuestaContext.Set<TEntity>().Add(entity);
            await encuestaContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            if (entity == null) throw new Exception("La Entidad no puede ser NULL.");

            encuestaContext.Set<TEntity>().AddOrUpdate(entity);
            await encuestaContext.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);

            if (entity == null)
                throw new Exception("La Entity es NULL.");

            encuestaContext.Set<TEntity>().Remove(entity);
            await encuestaContext.SaveChangesAsync();
        }

    }
}
