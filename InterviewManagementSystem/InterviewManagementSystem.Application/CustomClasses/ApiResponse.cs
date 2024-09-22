using Newtonsoft.Json;

namespace InterviewManagementSystem.Application.CustomClasses
{
    public sealed class ApiResponse<T>
    {
        public T? Data { get; set; } = default;

        public int? StatusCode { get; set; }

        public bool IsSuccess { get => StatusCode >= 200 && StatusCode <= 399; }


        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? Message { get; set; }



        public void Test()
        {

            DateTime? a = null;
            ArgumentNullException.ThrowIfNull(a, "shit fuck");
        }
    }
}
