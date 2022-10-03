using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WSVenta.Models;
using WSVenta.Models.Response;
using WSVenta.Models.Request;
namespace WSVenta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            using (VentaRealContext db = new VentaRealContext())
            {
                Respuesta oRespuesta = new Respuesta();
                oRespuesta.Exito = 0;

                try
                {
                    var lst = db.Clientes.ToList();
                    oRespuesta.Exito = 1;
                    oRespuesta.Data = lst;
                }
                catch (Exception ex)
                {
                    oRespuesta.Mensaje = ex.Message;
                }

                return Ok(oRespuesta);
            }
        }

        [HttpPost]
        public IActionResult Add(ClienteRequest cliente)
        {
            Respuesta respuesta = new Respuesta();

            try
            {
                using (VentaRealContext db = new VentaRealContext())
                {
                    Cliente oCliente = new Cliente();
                    oCliente.Nombre = cliente.Nombre;
                    db.Clientes.Add(oCliente);
                    db.SaveChanges();
                    respuesta.Exito = 1;
                }
            }
            catch (Exception ex)
            {

                respuesta.Mensaje = ex.Message;
            }

            return Ok(respuesta);
        }

        [HttpPut]
        public IActionResult Edit(ClienteRequest cliente)
        {
            Respuesta respuesta = new Respuesta();

            try
            {
                using (VentaRealContext db = new VentaRealContext())
                {
                    Cliente oCliente = db.Clientes.Find(cliente.Id);
                    oCliente.Nombre = cliente.Nombre;
                    db.Entry(oCliente).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                    respuesta.Exito = 1;
                }
            }
            catch (Exception ex)
            {

                respuesta.Mensaje = ex.Message;
            }

            return Ok(respuesta);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Respuesta respuesta = new Respuesta();

            try
            {
                using (VentaRealContext db = new VentaRealContext())
                {
                    Cliente oCliente = db.Clientes.Find(id);
                    db.Remove(oCliente);
                    db.SaveChanges();
                    respuesta.Exito = 1;
                }
            }
            catch (Exception ex)
            {

                respuesta.Mensaje = ex.Message;
            }

            return Ok(respuesta);
        }
    }
}
