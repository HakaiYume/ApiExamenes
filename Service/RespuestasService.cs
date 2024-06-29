using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiExamne.Data;
using ApiExamne.Data.Models;
using ApiExamne.Dto;
using Microsoft.EntityFrameworkCore;

namespace ApiExamne.Services
{
    public class RespuestaService : IRespuestaService
    {
        private readonly ApiExamenesContext _context;

        public RespuestaService(ApiExamenesContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RespuestaResponseDto>> GetAllAsync()
        {
            return await _context.Respuesta
                .Select(r => new RespuestaResponseDto
                {
                    RespuestaId = r.RespuestaId,
                    Texto = r.Texto,
                    EsCorrecta = r.EsCorrecta
                }).ToListAsync();
        }

        public async Task<RespuestaResponseDto?> GetByIdAsync(int id)
        {
            var respuesta = await _context.Respuesta.FirstOrDefaultAsync(r => r.RespuestaId == id);
            if (respuesta == null) return null;

            return new RespuestaResponseDto
            {
                RespuestaId = respuesta.RespuestaId,
                Texto = respuesta.Texto,
                EsCorrecta = respuesta.EsCorrecta
            };
        }

        public async Task<IEnumerable<RespuestaResponseDto>> GetByPreguntaIdAsync(int preguntaId)
        {
            return await _context.Respuesta
                .Where(r => r.PreguntaId == preguntaId)
                .Select(r => new RespuestaResponseDto
                {
                    RespuestaId = r.RespuestaId,
                    Texto = r.Texto,
                    EsCorrecta = r.EsCorrecta
                }).ToListAsync();
        }

        public async Task<RespuestaResponseDto> CreateAsync(RespuestaRequestDto respuestaDto)
        {
            var nuevaRespuesta = new Respuestum
            {
                Texto = respuestaDto.Texto,
                EsCorrecta = respuestaDto.EsCorrecta,
                PreguntaId = respuestaDto.PreguntaId
            };

            _context.Respuesta.Add(nuevaRespuesta);
            await _context.SaveChangesAsync();

            return new RespuestaResponseDto
            {
                RespuestaId = nuevaRespuesta.RespuestaId,
                Texto = nuevaRespuesta.Texto,
                EsCorrecta = nuevaRespuesta.EsCorrecta
            };
        }

        public async Task<bool> UpdateAsync(int id, RespuestaRequestDto respuestaDto)
        {
            var respuesta = await _context.Respuesta.FirstOrDefaultAsync(r => r.RespuestaId == id);
            if (respuesta == null) return false;

            respuesta.Texto = respuestaDto.Texto;
            respuesta.EsCorrecta = respuestaDto.EsCorrecta;
            respuesta.PreguntaId = respuestaDto.PreguntaId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var respuesta = await _context.Respuesta.FirstOrDefaultAsync(r => r.RespuestaId == id);
            if (respuesta == null) return false;

            _context.Respuesta.Remove(respuesta);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
