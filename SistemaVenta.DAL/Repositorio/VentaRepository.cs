using SistemaVenta.DAL.Contrato;
using SistemaVenta.DAL.DBContext;
using SistemaVenta.MODEL;

namespace SistemaVenta.DAL.Repositorio
{
    public class VentaRepository: GeneryRepository<Venta>, IVentaRepository
    {
        private readonly DbventaContext _context;

        public VentaRepository(DbventaContext context): base(context) 
        {
            _context = context;
        }

        public async Task<Venta> Registrar(Venta venta) 
        { 
            Venta ventaGenerada = new Venta();

            using (var transaction = _context.Database.BeginTransaction()) {
                try
                {
                    foreach (DetalleVenta dv in venta.DetalleVenta)
                    {
                        Producto producto_encontrado = _context.Productos.Where(P => P.IdProducto == dv.IdProducto).First();

                        producto_encontrado.Stock = producto_encontrado.Stock - dv.Cantidad;
                        _context.Productos.Update(producto_encontrado);
                    }
                    await _context.SaveChangesAsync();

                    NumeroDocumento correlativo = _context.NumeroDocumentos.First();
                    correlativo.UltimoNumero = correlativo.UltimoNumero + 1;
                    correlativo.FechaRegistro = DateTime.Now;

                    _context.NumeroDocumentos.Update(correlativo);
                    await _context.SaveChangesAsync();

                    int CantidadDigitos = 4;
                    string ceros = string.Concat(Enumerable.Repeat("0", CantidadDigitos));
                    string numeroVenta = ceros + correlativo.UltimoNumero.ToString();
                    //000001
                    numeroVenta = numeroVenta.Substring(numeroVenta.Length - CantidadDigitos, CantidadDigitos);

                    venta.NumeroDocumento = numeroVenta;

                    await _context.AddAsync(venta);
                    await _context.SaveChangesAsync();

                    ventaGenerada = venta;

                    transaction.Commit();
                    
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            return ventaGenerada;
        }
    }
}
