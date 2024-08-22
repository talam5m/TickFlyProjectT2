namespace BusinessLayer.DTOs
{
    public class ResponseDto<T>
    {
        public T? Result { get; set; }
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
        public int StatusCode { get; set; }
        public int TotalCount { get; set; }
    }
}
