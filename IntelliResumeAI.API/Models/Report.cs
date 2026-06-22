namespace IntelliResumeAI.API.Models
{
    public class Report
    {
        public int Id { get; set; }

        public int ResumeId { get; set; }

        public int ATSScore { get; set; }

        public string Skills { get; set; } = string.Empty;

        public string MissingSkills { get; set; } = string.Empty;

        public string Strengths { get; set; } = string.Empty;

        public string Weaknesses { get; set; } = string.Empty;

        public string Suggestions { get; set; } = string.Empty;

        public string InterviewQuestions { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }
    }
}