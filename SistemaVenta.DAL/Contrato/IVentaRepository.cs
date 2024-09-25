using SistemaVenta.MODEL;

namespace SistemaVenta.DAL.Contrato
{
    public interface IVentaRepository:IGenericRepository<Venta>
    {
        Task<Venta> Registrar(Venta venta);
    }
}
