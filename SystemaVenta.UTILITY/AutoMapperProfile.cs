﻿using AutoMapper;
using SistemaVenta.DTO;
using SistemaVenta.MODEL;
using System.Globalization;

namespace SIstemaVenta.UTILITY
{
    public class AutoMapperProfile : Profile
    {
        protected AutoMapperProfile()
        {
            #region Rol
            CreateMap<Rol, RolDTO>().ReverseMap();
            #endregion

            #region Menu
            CreateMap<Menu, MenuDTO>().ReverseMap();
            #endregion

            #region Usuario
            CreateMap<Usuario, UsuarioDTO>()
                .ForMember(destino =>
                    destino.RolDescripcion,
                    opt => opt.MapFrom(origen => origen.IdRolNavigation.Nombre)
                    )
                .ForMember(destino => destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0));

            CreateMap<Usuario, SesionDTO>()
                .ForMember(destino =>
                    destino.RolDescripcion,
                    opt => opt.MapFrom(origen => origen.IdRolNavigation.Nombre)
                    );

            CreateMap<UsuarioDTO, Usuario>()
                .ForMember(destino =>
                    destino.IdRolNavigation,
                    opt => opt.Ignore()
                    )
                .ForMember(destino => destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false));
            #endregion

            #region categoria
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
            #endregion

            #region Producto
            CreateMap<Producto, ProductoDTO>()
                .ForMember(destino => destino.DescripcionCategoria,
                    origen => origen.MapFrom(opt => opt.IdCategoriaNavigation.Nombre))
                 .ForMember(destino => destino.Precio,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-CO"))))
                  .ForMember(destino => destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0));

            CreateMap<ProductoDTO, Producto>()
                .ForMember(destino => destino.IdCategoriaNavigation,
                    origen => origen.Ignore())
                 .ForMember(destino => destino.Precio,
                    opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Precio, new CultureInfo("es-CO"))))
                  .ForMember(destino => destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false));
            #endregion

            #region Venta
            CreateMap<Venta, VentaDTO>()
                 .ForMember(destino => destino.TotalTexto,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-CO"))))
                  .ForMember(destino => destino.FechaRegistro,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.FechaRegistro.Value, new CultureInfo("dd/MM/yyyy"))));

            CreateMap<VentaDTO, Venta>()
                .ForMember(destino => destino.Total,
                   opt => opt.MapFrom(origen => Convert.ToDecimal(origen.TotalTexto, new CultureInfo("es-CO"))));
            #endregion

            #region detalleVenta
            CreateMap<DetalleVenta, DetalleVentaDTO>()
                 .ForMember(destino =>
                    destino.DescripcionProducto,
                    opt => opt.MapFrom(origen => origen.IdProductoNavigation.Nombre)
                    )
                 .ForMember(destino => destino.PrecioTexto,
                   opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-CO"))))
                   .ForMember(destino => destino.TotalTexto,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-CO"))));

            CreateMap<DetalleVentaDTO, DetalleVenta>()
                 .ForMember(destino =>
                    destino.IdProductoNavigation,
                    opt => opt.Ignore()
                    )
                 .ForMember(destino => destino.Precio,
                   opt => opt.MapFrom(origen => Convert.ToDecimal(origen.PrecioTexto, new CultureInfo("es-CO"))))
                   .ForMember(destino => destino.Total,
                    opt => opt.MapFrom(origen => Convert.ToDecimal(origen.TotalTexto, new CultureInfo("es-CO"))));
            #endregion

            #region reporte
            CreateMap<DetalleVenta, ReporteDTO>()
                .ForMember(destino => destino.FechaRegistro,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.IdVentaNavigation.FechaRegistro.Value.ToString("dd/MM/yyyy"))))
                .ForMember(destino => destino.NumeroDocumento,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.IdVentaNavigation.NumeroDocumento)))
                .ForMember(destino => destino.TipoPago,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.IdVentaNavigation.TipoPago)))
                .ForMember(destino => destino.TotalVenta,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.IdVentaNavigation.Total.Value, new CultureInfo("es-CO"))))
                .ForMember(destino => destino.Producto,
                    opt => opt.MapFrom(origen => origen.IdProductoNavigation.Nombre))
                .ForMember(destino => destino.precio,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-CO"))))
                .ForMember(destino => destino.Total,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-CO"))));
            #endregion
        }
    }
}
