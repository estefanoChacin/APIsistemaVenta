using SistemaVenta.DAL.Contrato;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace SistemaVenta.DAL.Repositorio
{
    public class GeneryRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbContext _dbContext;

        public GeneryRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> Obtener(Expression<Func<T, bool>> filtro)
        {
            try
            {
                T model = await _dbContext.Set<T>().FirstOrDefaultAsync(filtro);
                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<T> Crear(T modelo)
        {
            try
            {
                _dbContext.Set<T>().AddAsync(modelo);
                await _dbContext.SaveChangesAsync();
                return modelo;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Editar(T modelo)
        {
            try
            {
                 _dbContext.Set<T>().Update(modelo);
                await _dbContext.SaveChangesAsync();
                return true;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(T modelo)
        {
            try
            {
                _dbContext.Set<T>().Remove(modelo);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IQueryable<T>> Consultar(Expression<Func<T, bool>> filtro = null)
        {
            try
            {
                IQueryable<T> queryModelo = filtro == null ? _dbContext.Set<T>() : _dbContext.Set<T>().Where(filtro);
                return queryModelo;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
