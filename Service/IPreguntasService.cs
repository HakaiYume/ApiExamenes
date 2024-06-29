using System.Collections.Generic;
using System.Threading.Tasks;
using ApiExamne.Dto;

namespace ApiExamne.Services
{
    public interface IPreguntaService
    {
        Task<IEnumerable<PreguntaResponseDto>> GetAllAsync();
        Task<PreguntaResponseDto?> GetByIdAsync(int id);
        Task<IEnumerable<PreguntaResponseDto>> GetByExamenIdAsync(int examenId);
        Task<IEnumerable<PreguntaResponseDto>> GetRandomQuestionsByExamenAsync(int examenId, int count);
        Task<PreguntaResponseDto> CreateAsync(PreguntaRequestDto preguntaDto);
        Task<bool> UpdateAsync(int id, PreguntaRequestDto preguntaDto);
        Task<bool> DeleteAsync(int id);
    }
}
