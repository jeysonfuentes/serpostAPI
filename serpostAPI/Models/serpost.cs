using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace serpostAPI.Models
{
    public class Serpost
    {
        public string RetornoCadena2 { get; set; } //NroTracking - Lugar Tracking
        public string RetornoCadena3 { get; set; }//EstadoPedido - Fecha Tracking
        public string RetornoCadena4 { get; set; }//Descripcion de tracking
        public string RetornoCadena5 { get; set; } //Origen
        public string RetornoCadena6 { get; set; }//destino
        public string RetornoCadena7 { get; set; }//TipoCertificado
        public string RetornoCadena12 { get; set; }//DireccionDestino
    }


}
