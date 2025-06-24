using Microsoft.EntityFrameworkCore;
using PersonaAPI.Context;
using PersonaAPI.Models;

namespace PersonaAPI.Services
{
    public class PersonaService : IPersonaService
    {
        private readonly AppDbContext _context;

        public PersonaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Persona>> GetAllAsync()
        {
            try
            {
                return await _context.personas
                    .OrderBy(p => p.Nombre)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // Log del error (puedes usar ILogger aquí)
                throw new Exception("Error al obtener las personas", ex);
            }
        }

        public async Task<Persona?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.personas
                    .FirstOrDefaultAsync(p => p.IdPersona == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener la persona con ID {id}", ex);
            }
        }

        public async Task<Persona> CreateAsync(Persona persona)
        {
            try
            {
                // Validaciones de negocio
                await ValidatePersonaAsync(persona);

                // Establecer fecha de registro
                persona.FechaRegistro = DateTime.UtcNow;
                persona.IdPersona = null; // Asegurar que sea auto-generado

                _context.personas.Add(persona);
                await _context.SaveChangesAsync();

                return persona;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear la persona", ex);
            }
        }

        public async Task<Persona?> UpdateAsync(int id, Persona persona)
        {
            try
            {
                var existingPersona = await GetByIdAsync(id);
                if (existingPersona == null)
                    return null;

                // Validaciones de negocio
                await ValidatePersonaAsync(persona, id);

                // Actualizar propiedades
                existingPersona.Nombre = persona.Nombre;
                existingPersona.Apellido = persona.Apellido;
                existingPersona.FechaNacimiento = persona.FechaNacimiento;
                existingPersona.Email = persona.Email;
                existingPersona.Telefono = persona.Telefono;
                existingPersona.Direccion = persona.Direccion;
                // No actualizar FechaRegistro ni IdPersona

                _context.personas.Update(existingPersona);
                await _context.SaveChangesAsync();

                return existingPersona;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar la persona con ID {id}", ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var persona = await GetByIdAsync(id);
                if (persona == null)
                    return false;

                _context.personas.Remove(persona);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar la persona con ID {id}", ex);
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            try
            {
                return await _context.personas
                    .AnyAsync(p => p.IdPersona == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al verificar existencia de persona con ID {id}", ex);
            }
        }

        // Método privado para validaciones de negocio
        private async Task ValidatePersonaAsync(Persona persona, int? excludeId = null)
        {
            // Validar que el email no esté duplicado
            var emailExists = await _context.personas
                .AnyAsync(p => p.Email == persona.Email &&
                         (excludeId == null || p.IdPersona != excludeId));

            if (emailExists)
                throw new ArgumentException("Ya existe una persona con este email");

            // Validar edad mínima (ejemplo: mayor de 18 años)
            var edad = DateTime.Now.Year - persona.FechaNacimiento.Year;
            if (persona.FechaNacimiento.Date > DateTime.Now.AddYears(-edad).Date)
                edad--;

            if (edad < 18)
                throw new ArgumentException("La persona debe ser mayor de 18 años");

            // Validar fecha de nacimiento no sea futura
            if (persona.FechaNacimiento.Date > DateTime.Now.Date)
                throw new ArgumentException("La fecha de nacimiento no puede ser futura");
        }
    }
}
