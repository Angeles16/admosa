using admosa.Domain.Errors;

namespace admosa.Domain.Model;

public class Archivo
{
    public Guid Id { get; private set; }
    public string Nombre { get; private set; }
    
    //public Guid UsuarioId { get; private set; }
    public string RutaArchivo { get; private set; }
    public DateTime FechaCreacion { get; private set; }
    public EstadoArchivo Estado { get; private set; }


    public Archivo(Guid id, string nombre, string rutaArchivo, DateTime fechaCreacion, EstadoArchivo estadoArchivo)
    {
        Id = id;
        Nombre = nombre;
        RutaArchivo = rutaArchivo;
        FechaCreacion = fechaCreacion;
        Estado = estadoArchivo;
    }

    public static Archivo Crear(string nombre, string rutaArchivo)
    {
        if (string.IsNullOrWhiteSpace(nombre)) throw ArchivoErrors.NombreObligatorio();
        if (string.IsNullOrWhiteSpace(rutaArchivo)) throw ArchivoErrors.RutaObligatoria();
        //if (usuarioId == Guid.Empty)                throw ArchivoErrors.UsuarioObligatorio();

        return new Archivo(
            Guid.NewGuid(),
            nombre.Trim(),
            rutaArchivo.Trim(),
            DateTime.UtcNow,
            EstadoArchivo.Activo);
    }
}