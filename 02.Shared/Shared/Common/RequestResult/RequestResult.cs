namespace Shared.Common.RequestResult
{
    public class RequestResult : IRequestResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public dynamic? Data { get; set; }
        public string Module { get; set; }

        // Constructor con parámetros
        public RequestResult(dynamic? data, bool success, string message, string module = "", bool isWarning = false)
        {
            Data = data;
            Success = success;
            Message = message;
            Module = module;
        }

        // Metodo estatico para devolver consulta realizada con éxito
        public static RequestResult SuccessResult(dynamic? data = null, string message = "Registros consultados con éxito", string module = "") => new RequestResult(data, true, message, module, false);

        // Metodo estatico para devolver registro creado con éxito
        public static RequestResult SuccessRecord(dynamic? data = null, string message = "Registro creado con éxito", string module = "") => new RequestResult(data, true, message, module, false);

        // Metodo estatico para devolver registro modificado con éxito
        public static RequestResult SuccessUpdate(dynamic? data = null, string message = "Registro modificado con éxito", string module = "") => new RequestResult(data, true, message, module, false);

        // Método estático para devolver un registro eliminado
        public static RequestResult SuccessOperation(dynamic? data = null, string? message = "Operación exitosa", string module = "") => new RequestResult(data, true, message, module);

        // Método estático para devolver una operacion fallido
        public static RequestResult FailureOperation(string message, dynamic? data = null, string module = "") => new RequestResult(null, false, message, module);

        // Método estático para devolver error de consulta
        public static RequestResult ErrorResult(string message = "Ocurrió un error al consultar la información", string module = "") => new RequestResult(null, false, message, module);

        // Método estático para devolver error de guardado
        public static RequestResult ErrorRecord(string message = "Ocurrió un error al crear el registro", string module = "") => new RequestResult(null, false, message, module);

        // Metodo estatico para devolver consulta realizada con éxito
        public static RequestResult SuccessResultNoRecords(string? message = "No se encontraron registros", string module = "") => new RequestResult(null, true, message, module, false);

        // Método estático para devolver un registro eliminado
        public static RequestResult SuccessDelete(string message = "Registro eliminado con éxito", string module = "") => new RequestResult(null, true, message, module);
    }

    public interface IRequestResult
    {
        bool Success { get; set; }
        string Message { get; set; }
        dynamic? Data { get; set; }
        string Module { get; set; }
    }
}
