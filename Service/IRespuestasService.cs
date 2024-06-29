using System.Collections.Generic;
using System.Threading.Tasks;
using ApiExamne.Dto;

namespace ApiExamne.Services
{
    public interface IRespuestaService
    {
        Task<IEnumerable<RespuestaResponseDto>> GetAllAsync();
        Task<RespuestaResponseDto?> GetByIdAsync(int id);
        Task<IEnumerable<RespuestaResponseDto>> GetByPreguntaIdAsync(int preguntaId);
        Task<RespuestaResponseDto> CreateAsync(RespuestaRequestDto respuestaDto);
        Task<bool> UpdateAsync(int id, RespuestaRequestDto respuestaDto);
        Task<bool> DeleteAsync(int id);
    }
}
