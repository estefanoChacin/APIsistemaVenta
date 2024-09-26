namespace SistemaVenta.DTO
{
    internal class DashBoardDTO
    {
        public int TotalVentas { get; set; }
        public string? TtoalIngresos { get; set; }
        public List<VentaSemanaDTO> VentasUltimaSemana { get; set; }
    }
}
