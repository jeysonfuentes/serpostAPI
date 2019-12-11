using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace serpostAPI.Models
{
    public class SerpostComun
    {
        public int Retornoentero { get; set; }
        public string RetornoMensaje { get; set; }
        public string RetornoTexto { get; set; }
        public string RetornoEntUsuario { get; set; }
        public List<dynamic> ListCabeceraEnvio { get; set; }
        public List<dynamic> ListDetalleEnvio { get; set; }
        public List<dynamic> ListReporte { get; set; }
    }
}
