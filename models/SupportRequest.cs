public class SupportRequest
{
    public int Id { get; set; }
    public string Description { get; set; }
    public DateTime SubmissionTime { get; set; }
    public DateTime ResolutionDeadline { get; set; }
    public bool IsResolved { get; set; }
}