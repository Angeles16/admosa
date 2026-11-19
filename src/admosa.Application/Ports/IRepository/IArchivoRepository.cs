

using admosa.Domain.Model;

namespace admosa.Aplication.Ports.IRepository;

public interface IArchivoRepository
{
    Task<Archivo> GetByIdAsync(Guid id);
    Task<IReadOnlyList<Archivo>> GetAllAsync();
    Task AddAsync(Archivo archivo);
    Task UpdateAsync(admosa.Domain.Model.Archivo archivo);
    Task DeleteAsync(Guid id);
}