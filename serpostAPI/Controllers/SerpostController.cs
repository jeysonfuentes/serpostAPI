using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using serpostAPI.Models;

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
                string urlEnvioComun = "http://clientes.serpost.com.pe/prj_online/Web_Busqueda.aspx/Buscar_Envio_Comun";
                HttpResponseMessage respuesta_http = await httpclient.PostAsJsonAsync(url, request);
                HttpResponseMessage respuesta_detalle = await httpclient.PostAsJsonAsync(urlDetalle, request);
                
                if (respuesta_http.IsSuccessStatusCode)
                {
                    respuesta = await respuesta_http.Content.ReadAsAsync<ResponseTraking>();
                    respuestaDetalle = await respuesta_detalle.Content.ReadAsAsync<ResponseTraking>();
                }
                Tracking tracking = new Tracking();
                
                tracking.origen = respuesta.d.FirstOrDefault().RetornoCadena5;
                tracking.estadoEnvio = respuesta.d.FirstOrDefault().RetornoCadena3;
                tracking.nroTracking = respuesta.d.FirstOrDefault().RetornoCadena2;
                tracking.destino = respuesta.d.FirstOrDefault().RetornoCadena6;
                tracking.tipoEnvio = respuesta.d.FirstOrDefault().RetornoCadena7;
                tracking.observacion = respuesta.d.FirstOrDefault().RetornoCadena12;
                if (respuestaDetalle.d != null)
                {
                    foreach (var item in respuestaDetalle.d)
                    {
                        TrackingDetalle trackingDetalle = new TrackingDetalle();
                        trackingDetalle.fecha = item.RetornoCadena3;
                        trackingDetalle.destino = item.RetornoCadena2;
                        trackingDetalle.descripcion = item.RetornoCadena4;
                        tracking.detalle.Add(trackingDetalle);
                    }

                }else
                {
                    ResponseTrakingComun respuestaComun = new ResponseTrakingComun();
                    HttpResponseMessage respuesta_comun = await httpclient.PostAsJsonAsync(urlEnvioComun, request);
                    if (respuesta_comun.IsSuccessStatusCode)
                    {
                        respuestaComun = await respuesta_comun.Content.ReadAsAsync<ResponseTrakingComun>();
                        
                    }
                    if (respuestaComun.d != null)
                    {
                        tracking.serpostComun = respuestaComun.d;
                    }
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

    public class ResponseTraking
    {
        public List<Serpost> d { get; set; }
    }

    public class ResponseTrakingComun
    {
        public SerpostComun d { get; set; }
    }

   
 

    
    
}
