
namespace Domain.Additional
{
    public class HealthCheckResponse
    {
        public string Status { get; set; }  
        public IEnumerable<IndividualHealthCheckResponse> HealthChecks { get; set; }
        public TimeSpan HealthChechDuration { get; set; }   

    }
}
