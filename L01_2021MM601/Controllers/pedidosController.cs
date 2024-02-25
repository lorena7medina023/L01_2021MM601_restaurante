using L01_2021MM601.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace L01_2021MM601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class pedidosController : ControllerBase
    {
        private readonly RestauranteContext _restauranteContext;

        public pedidosController(RestauranteContext pedidoContex)
        {
            _restauranteContext = pedidoContex;

        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<Pedidos> listadoPedidos = (from e in _restauranteContext.Pedidos
                                           select e).ToList();

            if (listadoPedidos.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoPedidos);
        }

        //cliente
        [HttpGet]
        [Route("Cliente/{id}")]
        public IActionResult Get(int id)
        {
            Pedidos? Pedidos = (from e in _restauranteContext.Pedidos
                                where e.clienteId == id
                                select e).FirstOrDefault();

            if (Pedidos == null)
            {
                return NotFound();
            }
            return Ok(Pedidos);

        }

        //motorista
        [HttpGet]
        [Route("Motorista/{id}")]
        public IActionResult ObtenerMotorista(int id)
        {
            Pedidos? Pedidos = (from e in _restauranteContext.Pedidos
                                where e.motoristaId == id
                                select e).FirstOrDefault();

            if (Pedidos == null)
            {
                return NotFound();
            }
            return Ok(Pedidos);

        }

        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarPedido([FromBody] Pedidos pedidos)
        {
            try
            {
                _restauranteContext.Pedidos.Add(pedidos);
                _restauranteContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarPedido(int id, [FromBody] Pedidos pedidoModificar)
        {
            //Para actualizar un registro, se obtiene el registro original de la base de datos
            //al cual alteramos alguna propiedad
            Pedidos? pedidoActual = (from e in _restauranteContext.Pedidos
                                     where e.pedidoId == id
                                     select e).FirstOrDefault();

            //Verificamos que exista el registro segun su ID
            if (pedidoActual == null)
            {
                return NotFound();
            }

            //Si se encuentra el registro, se altera los campos modificables
            pedidoActual.motoristaId = pedidoModificar.motoristaId;
            pedidoActual.clienteId = pedidoModificar.clienteId;
            pedidoActual.platoId = pedidoModificar.platoId;
            pedidoActual.cantidad = pedidoModificar.cantidad;
            pedidoActual.precio = pedidoModificar.precio;

            //Se marca el registro como modificado en el contexto
            //y se envia la modificacion a la base de datos
            _restauranteContext.Entry(pedidoActual).State = EntityState.Modified;
            _restauranteContext.SaveChanges();

            return Ok(pedidoModificar);
        }


        [HttpDelete]
        [Route("Eliminar/id{id}")]
        public IActionResult EliminarPedido(int id)
        {
            //
            Pedidos? pedidos = (from e in _restauranteContext.Pedidos
                               where e.pedidoId == id
                               select e).FirstOrDefault();

            //Verificamos que exista el registro segun su ID
            if (pedidos == null)
                return NotFound();

            //Ejecutamos la accion de eliminar el registro
            _restauranteContext.Pedidos.Attach(pedidos);
            _restauranteContext.Pedidos.Remove(pedidos);
            _restauranteContext.SaveChanges();

            return Ok(pedidos);
        }
    }
}
