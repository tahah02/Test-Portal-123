using ChannelApp.DTOs;
using ChannelApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ChannelApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _service;
        public StudentsController(IStudentService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var student = await _service.GetByIdAsync(id);
            return student is null ? NotFound() : Ok(student);
        }

        [HttpPost]
        [Authorize(Policy = "StudentCreate")]
        public async Task<IActionResult> Create([FromBody] CreateStudentRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var createdBy = User.Identity?.Name ?? "anonymous";
            try
            {
                var student = await _service.CreateAsync(request, createdBy);
                return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpPut("{id:guid}")]
        [Authorize(Policy = "StudentUpdate")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateStudentRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var updatedBy = User.Identity?.Name ?? "anonymous";
            try
            {
                var student = await _service.UpdateAsync(id, request, updatedBy);
                return student is null ? NotFound() : Ok(student);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Policy = "StudentDelete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}