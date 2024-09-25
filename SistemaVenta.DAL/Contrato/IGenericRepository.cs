﻿using System.Linq.Expressions;

namespace SistemaVenta.DAL.Contrato
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Obtener(Expression<Func<T, bool>> filtro);
        Task<T> Crear(T modelo);
        Task<bool> Editar(T modelo);
        Task<bool> Eliminar(T modelo);
        Task<IQueryable<T>> Consultar(Expression<Func<T, bool>> filtro = null);

    }
}
