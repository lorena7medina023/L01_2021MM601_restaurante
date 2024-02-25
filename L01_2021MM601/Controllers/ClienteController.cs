using L01_2021MM601.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2021MM601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly RestauranteContext _restauranteContext;

        public ClienteController(RestauranteContext ClienteContex)
        {
            _restauranteContext = ClienteContex;

        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<Clientes> listadoCliente = (from e in _restauranteContext.Clientes
                                          select e).ToList();

            if (listadoCliente.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoCliente);
        }

        //cliente
        [HttpGet]
        [Route("Cliente/{id}")]
        public IActionResult Get(int id)
        {
            Clientes? cliente = (from e in _restauranteContext.Clientes
                              where e.clienteId == id
                              select e).FirstOrDefault();

            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);

        }

        //Por Direccion
        [HttpGet]
        [Route("Direccion/{direccion}")]
        public IActionResult ObtenerCliente(string direccion)
        {
            Clientes? cliente = (from e in _restauranteContext.Clientes
                              where e.direccion == direccion
                              select e).FirstOrDefault();

            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);

        }

        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarCliente([FromBody] Clientes cliente)
        {
            try
            {
                _restauranteContext.Clientes.Add(cliente);
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
        public IActionResult ActualizarPlato(int id, [FromBody] Clientes clienteModificar)
        {
            //Para actualizar un registro, se obtiene el registro original de la base de datos
            //al cual alteramos alguna propiedad
            Clientes? clienteActual = (from e in _restauranteContext.Clientes
                                   where e.clienteId == id
                                   select e).FirstOrDefault();

            //Verificamos que exista el registro segun su ID
            if (clienteActual == null)
            {
                return NotFound();
            }

            //Si se encuentra el registro, se altera los campos modificables
            clienteActual.nombreCliente = clienteModificar.nombreCliente;
            clienteActual.direccion = clienteModificar.direccion;

            //Se marca el registro como modificado en el contexto
            //y se envia la modificacion a la base de datos
            _restauranteContext.Entry(clienteActual).State = EntityState.Modified;
            _restauranteContext.SaveChanges();

            return Ok(clienteModificar);
        }


        [HttpDelete]
        [Route("Eliminar/id{id}")]
        public IActionResult EliminarPlato(int id)
        {
            //
            Clientes? cliente = (from e in _restauranteContext.Clientes
                              where e.clienteId == id
                              select e).FirstOrDefault();

            //Verificamos que exista el registro segun su ID
            if (cliente == null)
                return NotFound();

            //Ejecutamos la accion de eliminar el registro
            _restauranteContext.Clientes.Attach(cliente);
            _restauranteContext.Clientes.Remove(cliente);
            _restauranteContext.SaveChanges();

            return Ok(cliente);
        }
    }
}
