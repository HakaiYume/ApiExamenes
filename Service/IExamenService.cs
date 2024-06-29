using System.Collections.Generic;
using System.Threading.Tasks;
using ApiExamne.Data.Models;
using ApiExamne.Dto;

namespace ApiExamne.Services
{
    public interface IExamenService
    {
        Task<IEnumerable<ExamenResponseDto>> GetAllAsync();
        Task<ExamenResponseDto?> GetByIdAsync(int id);
        Task<ExamenResponseDto> CreateAsync(ExamenRequestDto examenDto);
        Task<bool> UpdateAsync(int id, ExamenRequestDto examenDto);
        Task<bool> DeleteAsync(int id);
    }
}
