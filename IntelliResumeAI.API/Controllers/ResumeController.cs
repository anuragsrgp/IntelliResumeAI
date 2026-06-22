using IntelliResumeAI.API.Data;
using IntelliResumeAI.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UglyToad.PdfPig;
using System.Text;
using System.Security.Claims;

namespace IntelliResumeAI.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ResumeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ResumeController(
            AppDbContext context
        )
        {
            _context = context;
        }

        [HttpPost("upload")]
        public IActionResult UploadResume(
            IFormFile file
        )
        {
            if (file == null)
            {
                return BadRequest(
                    "Please Select File"
                );
            }

            var email =
                User.FindFirst(
                    ClaimTypes.Email
                )?.Value;

            var user =
                _context.Users
                .FirstOrDefault(
                    x => x.Email == email
                );

            if (user == null)
            {
                return Unauthorized();
            }

            string uploadsFolder =
                Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "Uploads"
                );

            string fileName =
                Guid.NewGuid().ToString()
                +
                Path.GetExtension(
                    file.FileName
                );

            string fullPath =
                Path.Combine(
                    uploadsFolder,
                    fileName
                );

            using (
                var stream =
                new FileStream(
                    fullPath,
                    FileMode.Create
                )
            )
            {
                file.CopyTo(stream);
            }

            Resume resume =
                new Resume
                {
                    UserId = user.Id,

                    FileName =
                        file.FileName,

                    FilePath =
                        fullPath,

                    UploadDate =
                        DateTime.Now
                };

            _context.Resumes.Add(
                resume
            );

            _context.SaveChanges();
            StringBuilder resumeContent = new StringBuilder();

            using (PdfDocument document = PdfDocument.Open(fullPath))
            {
                foreach (var page in document.GetPages())
                {
                    resumeContent.AppendLine(page.Text);
                }
            }

            string resumeText = resumeContent.ToString();
            return Ok(new
            {
                Message = "Resume Uploaded",
                ResumeId = resume.Id,
                ResumeText = resumeText
            });
        }
    }
}