using Newtonsoft.Json;

namespace InterviewManagementSystem.Application.CustomClasses
{
    public sealed class ApiResponse<T>
    {

        //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public T? Data { get; set; } = default;


        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? Message { get; set; }
    }
}
