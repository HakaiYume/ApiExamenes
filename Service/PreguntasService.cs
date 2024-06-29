using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiExamne.Data;
using ApiExamne.Data.Models;
using ApiExamne.Dto;
using Microsoft.EntityFrameworkCore;

namespace ApiExamne.Services
{
    public class PreguntaService : IPreguntaService
    {
        private readonly ApiExamenesContext _context;

        public PreguntaService(ApiExamenesContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PreguntaResponseDto>> GetAllAsync()
        {
            return await _context.Pregunta
                .Select(p => new PreguntaResponseDto
                {
                    PreguntaId = p.PreguntaId,
                    Texto = p.Texto
                }).ToListAsync();
        }

        public async Task<PreguntaResponseDto?> GetByIdAsync(int id)
        {
            var pregunta = await _context.Pregunta
                .Include(p => p.Respuesta)
                .FirstOrDefaultAsync(p => p.PreguntaId == id);

            if (pregunta == null) return null;

            return new PreguntaResponseDto
            {
                PreguntaId = pregunta.PreguntaId,
                Texto = pregunta.Texto
            };
        }

        public async Task<IEnumerable<PreguntaResponseDto>> GetByExamenIdAsync(int examenId)
        {
            return await _context.Pregunta
                .Where(p => p.ExamenId == examenId)
                .Select(p => new PreguntaResponseDto
                {
                    PreguntaId = p.PreguntaId,
                    Texto = p.Texto
                }).ToListAsync();
        }

        public async Task<IEnumerable<PreguntaResponseDto>> GetRandomQuestionsByExamenAsync(int examenId, int count)
        {
            return await _context.Pregunta
                .Where(p => p.ExamenId == examenId)
                .OrderBy(r => Guid.NewGuid())
                .Take(count)
                .Select(p => new PreguntaResponseDto
                {
                    PreguntaId = p.PreguntaId,
                    Texto = p.Texto,

                    Respuestas = p.Respuesta.Select(r => new RespuestaResponseDto
                    {
                        RespuestaId = r.RespuestaId,
                        Texto = r.Texto,
                        EsCorrecta = r.EsCorrecta
                    })
                }).ToListAsync();
        }

        public async Task<PreguntaResponseDto> CreateAsync(PreguntaRequestDto preguntaDto)
        {
            var nuevaPregunta = new Preguntum
            {
                Texto = preguntaDto.Texto,
                ExamenId = preguntaDto.ExamenId
            };

            _context.Pregunta.Add(nuevaPregunta);
            await _context.SaveChangesAsync();

            return new PreguntaResponseDto
            {
                PreguntaId = nuevaPregunta.PreguntaId,
                Texto = nuevaPregunta.Texto
            };
        }

        public async Task<bool> UpdateAsync(int id, PreguntaRequestDto preguntaDto)
        {
            var pregunta = await _context.Pregunta.FirstOrDefaultAsync(p => p.PreguntaId == id);
            if (pregunta == null) return false;

            pregunta.Texto = preguntaDto.Texto;
            pregunta.ExamenId = preguntaDto.ExamenId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var pregunta = await _context.Pregunta.FirstOrDefaultAsync(p => p.PreguntaId == id);
            if (pregunta == null) return false;

            _context.Pregunta.Remove(pregunta);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
