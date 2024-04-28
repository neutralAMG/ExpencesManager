

namespace Expences.Aplication.Core
{
    public class ServiceResult<T> where T : class
    {
        public bool IsSuccess { get; set; } = true;
        public string? Message { get; set; }
        public T? Data { get; set; }

    }
}
