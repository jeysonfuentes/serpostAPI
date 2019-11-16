using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace serpostAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SerpostController : ControllerBase
    {
        private readonly HttpClient httpclient = new HttpClient();
        // GET api/serpost
        [HttpPost]
        public async Task<Tracking> serpost(RequestSerpost request)
        {
            try
            {
                ResponseTraking respuesta = new ResponseTraking();
                ResponseTraking respuestaDetalle = new ResponseTraking();
                string url = "http://clientes.serpost.com.pe/prj_online/Web_Busqueda.aspx/Consultar_Tracking";
                string urlDetalle = "http://clientes.serpost.com.pe/prj_online/Web_Busqueda.aspx/Consultar_Tracking_Detalle_IPS";
                HttpResponseMessage respuesta_http = await httpclient.PostAsJsonAsync(url, request);
                HttpResponseMessage respuesta_detalle = await httpclient.PostAsJsonAsync(urlDetalle, request);
                if (respuesta_http.IsSuccessStatusCode)
                {
                    respuesta = await respuesta_http.Content.ReadAsAsync<ResponseTraking>();
                    respuestaDetalle = await respuesta_detalle.Content.ReadAsAsync<ResponseTraking>();
                }
                Tracking tracking = new Tracking();
        //        public string RetornoCadena2 { get; set; } //NroTracking
        //public string RetornoCadena3 { get; set; }//EstadoPedido
        //public string RetornoCadena5 { get; set; } //Origen
        //public string RetornoCadena6 { get; set; }//destino
        //public string RetornoCadena7 { get; set; }//TipoCertificado
        //public string RetornoCadena12 { get; set; }//DireccionDestino
                tracking.origen = respuesta.d.FirstOrDefault().RetornoCadena5;
                tracking.estadoEnvio = respuesta.d.FirstOrDefault().RetornoCadena3;
                tracking.nroTracking = respuesta.d.FirstOrDefault().RetornoCadena2;
                tracking.destino = respuesta.d.FirstOrDefault().RetornoCadena6;
                tracking.tipoEnvio = respuesta.d.FirstOrDefault().RetornoCadena7;
                tracking.observacion = respuesta.d.FirstOrDefault().RetornoCadena12;
                foreach (var item in respuestaDetalle.d)
                {
                    TrackingDetalle trackingDetalle = new TrackingDetalle();
                    trackingDetalle.fecha = item.RetornoCadena3;
                    trackingDetalle.destino = item.RetornoCadena2;
                    trackingDetalle.descripcion = item.RetornoCadena4;
                    tracking.detalle.Add(trackingDetalle);
                }
                return tracking;
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }
    }

    

    public class RequestSerpost
    {
            public string Anio { get; set; }
            public int Filtro { get; set; }
            public string Tracking { get; set; }
    }

    public class Tracking
    {
        public string origen { get; set; }
        public string estadoEnvio { get; set; }
        public string nroTracking { get; set; }
        public string destino { get; set; }
        public string tipoEnvio { get; set; }
        public string observacion { get; set; }
        public List<TrackingDetalle> detalle { get; set; }
        public Tracking()
        {
            detalle = new List<TrackingDetalle>();
        }
    }

    public class TrackingDetalle
    {
        public string fecha { get; set; }
        public string descripcion { get; set; }
        public string destino { get; set; }
    }

    public class ResponseTraking
    {
        public List<d> d { get; set; }
    }
    public class d
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
