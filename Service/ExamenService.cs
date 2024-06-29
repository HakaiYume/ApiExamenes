using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiExamne.Data;
using ApiExamne.Data.Models;
using ApiExamne.Dto;

namespace ApiExamne.Services
{
    public class ExamenService : IExamenService
    {
        private readonly ApiExamenesContext _context;
        private IConfiguration config;

        public ExamenService(ApiExamenesContext context, IConfiguration configuration)
        {
            _context = context;
            config = configuration;
        }

        public async Task<IEnumerable<ExamenResponseDto>> GetAllAsync()
        {
            return await Task.FromResult(_context.Examen.Select(e => new ExamenResponseDto
            {
                ExamenId = e.ExamenId,
                Nombre = e.Nombre
            }));
        }

        public async Task<ExamenResponseDto?> GetByIdAsync(int id)
        {
            var examen = _context.Examen.FirstOrDefault(e => e.ExamenId == id);
            if (examen == null) return null;

            return await Task.FromResult(new ExamenResponseDto
            {
                ExamenId = examen.ExamenId,
                Nombre = examen.Nombre
            });
        }

        public async Task<ExamenResponseDto> CreateAsync(ExamenRequestDto examenDto)
        {
            var nuevoExamen = new Examan
            {
                Nombre = examenDto.Nombre
            };
            _context.Examen.Add(nuevoExamen);

            return await Task.FromResult(new ExamenResponseDto
            {
                ExamenId = nuevoExamen.ExamenId,
                Nombre = nuevoExamen.Nombre
            });
        }

        public async Task<bool> UpdateAsync(int id, ExamenRequestDto examenDto)
        {
            var examen = _context.Examen.FirstOrDefault(e => e.ExamenId == id);
            if (examen == null) return false;

            examen.Nombre = examenDto.Nombre;

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var examen = _context.Examen.FirstOrDefault(e => e.ExamenId == id);
            if (examen == null) return false;

            _context.Examen.Remove(examen);
            return await Task.FromResult(true);
        }
    }
}
