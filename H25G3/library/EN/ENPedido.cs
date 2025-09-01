using System;

namespace Library.EN
{
    public class ENPedido
    {
        public int PedidoId { get; set; }
        public string CompradorUsername { get; set; }
        public DateTime FechaPedido { get; set; }
        public decimal ImporteTotal { get; set; }

        public ENPedido() { }


        public ENPedido(int pedidoId, string comprador, DateTime fecha, decimal importe)
        {
            PedidoId = pedidoId;
            CompradorUsername = comprador;
            FechaPedido = fecha;
            ImporteTotal = importe;
            
        }
    }
}