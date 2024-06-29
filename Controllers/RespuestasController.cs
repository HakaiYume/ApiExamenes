using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiExamne.Services;
using ApiExamne.Dto;

namespace ApiExamne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RespuestaController : ControllerBase
    {
        private readonly IRespuestaService _respuestaService;

        public RespuestaController(IRespuestaService respuestaService)
        {
            _respuestaService = respuestaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RespuestaResponseDto>>> GetAll()
        {
            try
            {
                var respuestas = await _respuestaService.GetAllAsync();
                return Ok(respuestas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener las respuestas.", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RespuestaResponseDto>> GetById(int id)
        {
            try
            {
                var respuesta = await _respuestaService.GetByIdAsync(id);
                if (respuesta == null)
                {
                    return NotFound(new { message = "Respuesta no encontrada." });
                }

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener la respuesta.", error = ex.Message });
            }
        }

        [HttpGet("pregunta/{preguntaId}")]
        public async Task<ActionResult<IEnumerable<RespuestaResponseDto>>> GetByPreguntaId(int preguntaId)
        {
            try
            {
                var respuestas = await _respuestaService.GetByPreguntaIdAsync(preguntaId);
                return Ok(respuestas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener las respuestas de la pregunta.", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<RespuestaResponseDto>> Create(RespuestaRequestDto respuestaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdRespuesta = await _respuestaService.CreateAsync(respuestaDto);
                return CreatedAtAction(nameof(GetById), new { id = createdRespuesta.RespuestaId }, createdRespuesta);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al crear la respuesta.", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, RespuestaRequestDto respuestaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updated = await _respuestaService.UpdateAsync(id, respuestaDto);
                if (!updated)
                {
                    return NotFound(new { message = "Respuesta no encontrada." });
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al actualizar la respuesta.", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _respuestaService.DeleteAsync(id);
                if (!deleted)
                {
                    return NotFound(new { message = "Respuesta no encontrada." });
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al eliminar la respuesta.", error = ex.Message });
            }
        }
    }
}
