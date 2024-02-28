
namespace FlowrSpot.Domain.Auditables
{
    public abstract class BaseAuditable
    {
        public DateTimeOffset Created { get; set; }

        public string? CreatedBy { get; set; }

        public DateTimeOffset Updated { get; set; }

        public string? UpdatedBy { get; set; }
    }
}
