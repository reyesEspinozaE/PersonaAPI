using Microsoft.AspNetCore.Mvc;
using PersonaAPI.Context;
using PersonaAPI.Models;
using PersonaAPI.Services;
using PersonaAPI.Services.Filters;

namespace PersonaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(ValidateTokenFilter))]
    public class PersonasController : ControllerBase
    {
        private readonly IPersonaService _personaService;

        public PersonasController(IPersonaService personaService)
        {
            _personaService = personaService;
        }

        //[HttpGet] // Obtener todas las personas
        //public async Task<IActionResult> GetPersonas()
        //{
        //    List<Persona> personas = _Context.personas.ToList();
        //    return Ok(personas);
        //}


        /// <summary>
        /// Obtiene todas las personas
        /// </summary>
        /// <returns>Lista de personas</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var personas = await _personaService.GetAllAsync();
                return Ok(new
                {
                    success = true,
                    data = personas,
                    message = "Personas obtenidas exitosamente"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Error interno del servidor",
                    error = ex.Message
                });
            }
        }

        /// <summary>
        /// Obtiene una persona por ID
        /// </summary>
        /// <param name="id">ID de la persona</param>
        /// <returns>Persona encontrada</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new
                    {
                        success = false,
                        message = "ID debe ser mayor a 0"
                    });

                var persona = await _personaService.GetByIdAsync(id);

                if (persona == null)
                    return NotFound(new
                    {
                        success = false,
                        message = $"Persona con ID {id} no encontrada"
                    });

                return Ok(new
                {
                    success = true,
                    data = persona,
                    message = "Persona encontrada exitosamente"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Error interno del servidor",
                    error = ex.Message
                });
            }
        }

        /// <summary>
        /// Crea una nueva persona
        /// </summary>
        /// <param name="persona">Datos de la persona</param>
        /// <returns>Persona creada</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Persona persona)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new
                    {
                        success = false,
                        message = "Datos inválidos",
                        errors = ModelState
                    });

                var nuevaPersona = await _personaService.CreateAsync(persona);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = nuevaPersona.IdPersona },
                    new
                    {
                        success = true,
                        data = nuevaPersona,
                        message = "Persona creada exitosamente"
                    });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Error interno del servidor",
                    error = ex.Message
                });
            }
        }

        /// <summary>
        /// Actualiza una persona existente
        /// </summary>
        /// <param name="id">ID de la persona</param>
        /// <param name="persona">Datos actualizados</param>
        /// <returns>Persona actualizada</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Persona persona)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new
                    {
                        success = false,
                        message = "ID debe ser mayor a 0"
                    });

                if (!ModelState.IsValid)
                    return BadRequest(new
                    {
                        success = false,
                        message = "Datos inválidos",
                        errors = ModelState
                    });

                var personaActualizada = await _personaService.UpdateAsync(id, persona);

                if (personaActualizada == null)
                    return NotFound(new
                    {
                        success = false,
                        message = $"Persona con ID {id} no encontrada"
                    });

                return Ok(new
                {
                    success = true,
                    data = personaActualizada,
                    message = "Persona actualizada exitosamente"
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Error interno del servidor",
                    error = ex.Message
                });
            }
        }

        /// <summary>
        /// Elimina una persona
        /// </summary>
        /// <param name="id">ID de la persona</param>
        /// <returns>Confirmación de eliminación</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new
                    {
                        success = false,
                        message = "ID debe ser mayor a 0"
                    });

                var eliminado = await _personaService.DeleteAsync(id);

                if (!eliminado)
                    return NotFound(new
                    {
                        success = false,
                        message = $"Persona con ID {id} no encontrada"
                    });

                return Ok(new
                {
                    success = true,
                    message = "Persona eliminada exitosamente"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Error interno del servidor",
                    error = ex.Message
                });
            }
        }
    }
}
