using Newtonsoft.Json;

namespace InterviewManagementSystem.Application.CustomClasses
{
    public sealed class ApiResponse<T>
    {
        public T? Data { get; set; }

        public int? StatusCode { get; set; }

        public bool IsSuccess { get => StatusCode >= 200 && StatusCode <= 299; }


        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? Message { get; set; }
    }
}
