namespace IntelliResumeAI.API.Models
{
    public class ResumeAnalysis
    {
        public int Id { get; set; }

        public int ResumeId { get; set; }

        public string AnalysisResult { get; set; } = "";

        public DateTime CreatedAt { get; set; }
            = DateTime.Now;
    }
}