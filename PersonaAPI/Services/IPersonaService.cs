using PersonaAPI.Models;

namespace PersonaAPI.Services
{
    public interface IPersonaService
    {
        Task<IEnumerable<Persona>> GetAllAsync();
        Task<Persona?> GetByIdAsync(int id);
        Task<Persona> CreateAsync(Persona persona);
        Task<Persona?> UpdateAsync(int id, Persona persona);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<Persona>> FilterAsync(string? nombre, string? apellido, string? email);
    }
}