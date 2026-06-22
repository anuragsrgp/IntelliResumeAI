using System.Text;
using System.Text.Json;

namespace IntelliResumeAI.API.Services
{
    public class GeminiService
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;

        public GeminiService(
            IConfiguration config,
            HttpClient httpClient)
        {
            _config = config;
            _httpClient = httpClient;
        }

        public async Task<string> AnalyzeResume(
            string resumeText)
        {
            var apiKey =
                _config["Gemini:ApiKey"];

            var url =
                $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={apiKey}";

            var prompt = $@"
You are an expert ATS Resume Analyzer and Career Coach.

Analyze the uploaded document.

First determine whether the uploaded document is a Resume.

If document is NOT a resume return ONLY:

{{
  ""isResume"": false,
  ""message"": ""Please upload a valid resume.""
}}

If document IS a resume return ONLY valid JSON.

IMPORTANT:
- Do not return markdown.
- Do not return explanation.
- Do not return text outside JSON.
- ATS score must be dynamic.
- Detect profile automatically.
- Generate profile specific suggestions.
- Generate profile specific interview questions.
- Generate at least 10 interview questions.

Return JSON in this format:

{{
  ""isResume"": true,
  ""profile"": """",
  ""experienceLevel"": """",
  ""atsScore"": 0,
  ""profileSummary"": """",
  ""skills"": [],
  ""missingSkills"": [],
  ""strengths"": [],
  ""weaknesses"": [],
  ""suggestions"": [],
  ""learningRoadmap"": [],
  ""interviewQuestions"": [
    {{
      ""question"": """",
      ""difficulty"": """",
      ""answer"": """",
      ""example"": """"
    }}
  ]
}}

Resume Text:

{resumeText}
";

            var requestBody = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new
                            {
                                text = prompt
                            }
                        }
                    }
                }
            };

            var json =
                JsonSerializer.Serialize(
                    requestBody);

            var content =
                new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json");

            var response =
                await _httpClient.PostAsync(
                    url,
                    content);

            response.EnsureSuccessStatusCode();

            return await response.Content
                .ReadAsStringAsync();
        }
    }
}