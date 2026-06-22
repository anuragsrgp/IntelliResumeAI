namespace IntelliResumeAI.API.Models
{
    public class AtsAnalysisResponse
    {
        public int AtsScore { get; set; }

        public List<string> Skills { get; set; }

        public List<string> MissingSkills { get; set; }

        public List<string> Strengths { get; set; }

        public List<string> Weaknesses { get; set; }

        public List<string> Suggestions { get; set; }

        public List<string> InterviewQuestions { get; set; }
    }
}