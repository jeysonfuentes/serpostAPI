using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace serpostAPI.Models
{
    public class Tracking
    {
        public string origen { get; set; }
        public string estadoEnvio { get; set; }
        public string nroTracking { get; set; }
        public string destino { get; set; }
        public string tipoEnvio { get; set; }
        public string observacion { get; set; }
        public List<TrackingDetalle> detalle { get; set; }
        public SerpostComun serpostComun { get; set; }
        public Tracking()
        {
            detalle = new List<TrackingDetalle>();
            serpostComun = new SerpostComun();
        }
    }
}
