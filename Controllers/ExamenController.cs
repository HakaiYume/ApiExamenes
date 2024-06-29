using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiExamne.Services;
using ApiExamne.Dto;

namespace ApiExamne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamenController : ControllerBase
    {
        private readonly IExamenService _examenService;

        public ExamenController(IExamenService examenService)
        {
            _examenService = examenService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExamenResponseDto>>> GetAll()
        {
            try
            {
                var examenes = await _examenService.GetAllAsync();
                return Ok(examenes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener los exámenes.", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExamenResponseDto>> GetById(int id)
        {
            try
            {
                var examen = await _examenService.GetByIdAsync(id);
                if (examen == null)
                {
                    return NotFound(new { message = "Examen no encontrado." });
                }

                return Ok(examen);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener el examen.", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<ExamenResponseDto>> Create(ExamenRequestDto examenDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdExamen = await _examenService.CreateAsync(examenDto);
                return CreatedAtAction(nameof(GetById), new { id = createdExamen.ExamenId }, createdExamen);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al crear el examen.", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ExamenRequestDto examenDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updated = await _examenService.UpdateAsync(id, examenDto);
                if (!updated)
                {
                    return NotFound(new { message = "Examen no encontrado." });
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al actualizar el examen.", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _examenService.DeleteAsync(id);
                if (!deleted)
                {
                    return NotFound(new { message = "Examen no encontrado." });
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al eliminar el examen.", error = ex.Message });
            }
        }
    }
}
