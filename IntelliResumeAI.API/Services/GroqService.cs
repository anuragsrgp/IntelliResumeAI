using System.Text;
using System.Text.Json;

namespace IntelliResumeAI.API.Services
{
    public class GroqService
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;

        public GroqService(
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
                _config["Groq:ApiKey"];

            var prompt = $@"
You are an expert ATS Resume Analyzer.

Analyze the uploaded resume.

Return ONLY valid JSON.

Do not return:
- markdown
- explanation
- text before JSON
- text after JSON

Return format:

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
                model = "llama-3.3-70b-versatile",
                temperature = 0.2,
                messages = new[]
                {
                    new
                    {
                        role = "user",
                        content = prompt
                    }
                }
            };

            var requestJson =
                JsonSerializer.Serialize(
                    requestBody);

            var request =
                new HttpRequestMessage(
                    HttpMethod.Post,
                    "https://api.groq.com/openai/v1/chat/completions");

            request.Headers.Add(
                "Authorization",
                $"Bearer {apiKey}");

            request.Content =
                new StringContent(
                    requestJson,
                    Encoding.UTF8,
                    "application/json");

            var response =
                await _httpClient.SendAsync(
                    request);

            var result =
                await response.Content
                    .ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(result);
            }

            var responseJson =
                JsonDocument.Parse(result);

            var content =
                responseJson.RootElement
                    .GetProperty("choices")[0]
                    .GetProperty("message")
                    .GetProperty("content")
                    .GetString();

            if (string.IsNullOrWhiteSpace(content))
            {
                return "{}";
            }

            content = content
                .Replace("```json", "")
                .Replace("```", "")
                .Trim();

            return content;
        }
    }
}