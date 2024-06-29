using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiExamne.Services;
using ApiExamne.Dto;

namespace ApiExamne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreguntaController : ControllerBase
    {
        private readonly IPreguntaService _preguntaService;

        public PreguntaController(IPreguntaService preguntaService)
        {
            _preguntaService = preguntaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PreguntaResponseDto>>> GetAll()
        {
            try
            {
                var preguntas = await _preguntaService.GetAllAsync();
                return Ok(preguntas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener las preguntas.", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PreguntaResponseDto>> GetById(int id)
        {
            try
            {
                var pregunta = await _preguntaService.GetByIdAsync(id);
                if (pregunta == null)
                {
                    return NotFound(new { message = "Pregunta no encontrada." });
                }

                return Ok(pregunta);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener la pregunta.", error = ex.Message });
            }
        }

        [HttpGet("examen/{examenId}")]
        public async Task<ActionResult<IEnumerable<PreguntaResponseDto>>> GetByExamenId(int examenId)
        {
            try
            {
                var preguntas = await _preguntaService.GetByExamenIdAsync(examenId);
                return Ok(preguntas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener las preguntas del examen.", error = ex.Message });
            }
        }

        [HttpGet("random/{examenId}/{count}")]
        public async Task<ActionResult<IEnumerable<PreguntaResponseDto>>> GetRandomQuestionsByExamen(int examenId, int count)
        {
            try
            {
                var preguntas = await _preguntaService.GetRandomQuestionsByExamenAsync(examenId, count);
                return Ok(preguntas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener preguntas aleatorias del examen.", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<PreguntaResponseDto>> Create(PreguntaRequestDto preguntaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdPregunta = await _preguntaService.CreateAsync(preguntaDto);
                return CreatedAtAction(nameof(GetById), new { id = createdPregunta.PreguntaId }, createdPregunta);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al crear la pregunta.", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PreguntaRequestDto preguntaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updated = await _preguntaService.UpdateAsync(id, preguntaDto);
                if (!updated)
                {
                    return NotFound(new { message = "Pregunta no encontrada." });
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al actualizar la pregunta.", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _preguntaService.DeleteAsync(id);
                if (!deleted)
                {
                    return NotFound(new { message = "Pregunta no encontrada." });
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al eliminar la pregunta.", error = ex.Message });
            }
        }
    }
}
