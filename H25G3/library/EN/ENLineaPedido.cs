namespace Library.EN
{
    public class ENLineaPedido
    {
        public int LineaPedidoId { get; set; }
        public int PedidoId { get; set; }
        public string Comprador_UName { get; set; }
        public string Vendedor_UName { get; set; }
        public int ArticuloId { get; set; }
        public decimal Importe { get; set; }


    }
}