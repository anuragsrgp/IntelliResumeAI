using IntelliResumeAI.API.Models;
using IntelliResumeAI.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IntelliResumeAI.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AIController : ControllerBase
    {
        private readonly GroqService _groqService;

        public AIController(
            GroqService groqService)
        {
            _groqService = groqService;
        }

        [HttpPost("analyze")]
        public async Task<IActionResult> Analyze(
            ResumeAnalysisRequest request)
        {
            var result =
                await _groqService
                    .AnalyzeResume(
                        request.ResumeText);

            return Content(
                result,
                "application/json");
        }
    }
}