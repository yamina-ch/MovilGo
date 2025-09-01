using System;
using System.Text.RegularExpressions;
namespace Library.EN
{
    public class ENMetodoPago
    {
        public string NumeroTarjeta { get; set; }
        public string CVV { get; set; }
        public int MesCaducidad { get; set; }
        public int AnoCaducidad { get; set; }
        public string Username { get; set; }
        public string TipoTarjeta { get; private set; }

        public ENMetodoPago() { }

        public ENMetodoPago(string numTarjeta, string cvv, int mesCad, int anoCad, string username)
        {
            
            if (string.IsNullOrWhiteSpace(numTarjeta))
                throw new ArgumentException("El número de tarjeta no puede estar vacío");

            if (string.IsNullOrWhiteSpace(cvv))
                throw new ArgumentException("El CVV no puede estar vacío");

            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("El nombre de usuario no puede estar vacío");

           
            numTarjeta = numTarjeta.Replace(" ", "").Replace("-", "");

            if (!Regex.IsMatch(numTarjeta, @"^\d{16}$"))
                throw new ArgumentException("El número de tarjeta debe tener 16 dígitos");


            
            if (!Regex.IsMatch(cvv, @"^\d{3,4}$"))
                throw new ArgumentException("El CVV debe tener 3 o 4 dígitos");

           
            if (mesCad < 1 || mesCad > 12)
                throw new ArgumentException("El mes de caducidad debe estar entre 1 y 12");

            int añoCompleto = 2000 + anoCad;
            DateTime ahora = DateTime.Now;

            if (añoCompleto < ahora.Year || (añoCompleto == ahora.Year && mesCad < ahora.Month))
                throw new ArgumentException("La tarjeta está expirada");

            
            NumeroTarjeta = numTarjeta;
            CVV = cvv;
            MesCaducidad = mesCad;
            AnoCaducidad = anoCad;
            Username = username;
            TipoTarjeta = DeterminarTipoTarjeta(numTarjeta);
        }

        private string DeterminarTipoTarjeta(string numeroTarjeta)
        {
            if (string.IsNullOrEmpty(numeroTarjeta)) return "Desconocido";

            
            if (Regex.IsMatch(numeroTarjeta, @"^3[47]"))
                return "American Express";

            
            if (numeroTarjeta.StartsWith("4"))
                return "Visa";

            
            if (Regex.IsMatch(numeroTarjeta, @"^5[1-5]"))
                return "Mastercard";

            
            if (numeroTarjeta.StartsWith("6011") ||
                Regex.IsMatch(numeroTarjeta, @"^64[4-9]") ||
                numeroTarjeta.StartsWith("65"))
                return "Discover";

            return "Otra";
        }

        public string ObtenerUltimos4Digitos()
        {
            return NumeroTarjeta.Length >= 4 ?
                   NumeroTarjeta.Substring(NumeroTarjeta.Length - 4) : "****";
        }
    }
}