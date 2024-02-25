using L01_2021MM601.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2021MM601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class platosController : ControllerBase
    {
        private readonly RestauranteContext _restauranteContext;

        public platosController(RestauranteContext platosContex)
        {
            _restauranteContext = platosContex;

        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<Platos> listadoPlatos = (from e in _restauranteContext.Platos
                                            select e).ToList();

            if (listadoPlatos.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoPlatos);
        }

        //cliente
        [HttpGet]
        [Route("Plato/{id}")]
        public IActionResult Get(int id)
        {
            Platos? Platos = (from e in _restauranteContext.Platos
                                where e.platoId == id
                                select e).FirstOrDefault();

            if (Platos == null)
            {
                return NotFound();
            }
            return Ok(Platos);

        }

        //NombrePlato
        [HttpGet]
        [Route("Nombre Plato/{nombrePlato}")]
        public IActionResult ObtenerMotorista(string nombrePlato)
        {
            Platos? Platos = (from e in _restauranteContext.Platos
                                where e.nombrePlato == nombrePlato
                             select e).FirstOrDefault();

            if (Platos == null)
            {
                return NotFound();
            }
            return Ok(Platos);

        }

        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarPlato([FromBody] Platos platos)
        {
            try
            {
                _restauranteContext.Platos.Add(platos);
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
        public IActionResult ActualizarPlato(int id, [FromBody] Platos platoModificar)
        {
            //Para actualizar un registro, se obtiene el registro original de la base de datos
            //al cual alteramos alguna propiedad
            Platos? platoActual = (from e in _restauranteContext.Platos
                                     where e.platoId == id
                                     select e).FirstOrDefault();

            //Verificamos que exista el registro segun su ID
            if (platoActual == null)
            {
                return NotFound();
            }

            //Si se encuentra el registro, se altera los campos modificables
            platoActual.nombrePlato = platoModificar.nombrePlato;
            platoActual.precio = platoModificar.precio;

            //Se marca el registro como modificado en el contexto
            //y se envia la modificacion a la base de datos
            _restauranteContext.Entry(platoActual).State = EntityState.Modified;
            _restauranteContext.SaveChanges();

            return Ok(platoModificar);
        }


        [HttpDelete]
        [Route("Eliminar/id{id}")]
        public IActionResult EliminarPlato(int id)
        {
            //
            Platos? platos = (from e in _restauranteContext.Platos
                                where e.platoId == id
                                select e).FirstOrDefault();

            //Verificamos que exista el registro segun su ID
            if (platos == null)
                return NotFound();

            //Ejecutamos la accion de eliminar el registro
            _restauranteContext.Platos.Attach(platos);
            _restauranteContext.Platos.Remove(platos);
            _restauranteContext.SaveChanges();

            return Ok(platos);
        }
    }
}
