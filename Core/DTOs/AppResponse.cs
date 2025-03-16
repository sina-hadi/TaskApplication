namespace Core.DTOs
{
    public class AppResponse<T>
    {
        private AppResponse(
            int status = 0, T? data = default, string message = "", string devMessage = "")
        {
            Status = status;
            Data = data;
            Message = message;
            DevMessage = devMessage;
        }

        public static AppResponse<T> Create(
            int status = 0, T? data = default, string message = "", string devMessage = "")
        {
            return new AppResponse<T>(status, data, message: message, devMessage: devMessage);
        }

        public int Status { get; set; }
        public string Message { get; set; }
        public string DevMessage { get; set; }
        public T? Data { get; set; }
    }

    public class AppResponse
    {
        private AppResponse(
            int status = 0, string message = "", string devMessage = "")
        {
            Status = status;
            Message = message;
            DevMessage = devMessage;
        }

        public static AppResponse Create(
            int status = 0, string message = "", string devMessage = "")
        {
            return new AppResponse(status, message: message, devMessage: devMessage);
        }

        public int Status { get; set; }
        public string Message { get; set; }
        public string DevMessage { get; set; }
    }

}
