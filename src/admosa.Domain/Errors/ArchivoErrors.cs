namespace admosa.Domain.Errors;

public static class ArchivoErrors
{
    public static AppException NombreObligatorio() =>
        AppException.Validation("archivo.nombre_obligatorio", "El nombre del archivo es obligatorio.");

    public static AppException RutaObligatoria() =>
        AppException.Validation("archivo.ruta_obligatoria", "La ruta del archivo es obligatoria.");

    public static AppException UsuarioObligatorio() =>
        AppException.Validation("archivo.usuario_obligatorio", "El usuario es obligatorio.");

    public static AppException NoEncontrado(Guid id) =>
        AppException.NotFound("archivo.no_encontrado", $"No existe el archivo con id {id}.");
}