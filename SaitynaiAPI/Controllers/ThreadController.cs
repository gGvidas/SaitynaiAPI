using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using SaitynaiAPI.DTOs;
using SaitynaiAPI.DTOs.ThreadDTOs;
using SaitynaiAPI.Entities;
using SaitynaiAPI.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaitynaiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThreadController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IThreadRepository _threadRepository;
        private readonly SaitynaiDbContext _context;
        public ThreadController(ICategoryRepository categoryRepository, IThreadRepository threadRepository, SaitynaiDbContext context)
        {
            _categoryRepository = categoryRepository;
            _threadRepository = threadRepository;
            _context = context;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetThreadResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            Thread thread = _threadRepository.Find(id);
            if (thread != null) return Ok(ConvertToDTO.ToGetResponse(thread));
            else return NotFound();
        }

        [HttpGet]
        [ProducesResponseType(typeof(ICollection<GetThreadCollectionResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(ConvertToDTO.ToGetResponse(await _threadRepository.GetAll()));
        }

        [HttpGet("category/{categoryId}")]
        [ProducesResponseType(typeof(ICollection<GetThreadCollectionResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByCategory(int categoryId)
        {
            return Ok(ConvertToDTO.ToGetResponse(await _threadRepository.GetByCategory(categoryId)));
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(CreateThreadResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create([FromBody] CreateThreadRequest threadRequest)
        {
            int id = int.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.NameId).Value);
            if (_categoryRepository.Find(threadRequest.CategoryId) != null)
            {
                Thread thread = ConvertFromDTO.FromCreateRequest(threadRequest);
                thread.UserId = id;
                _threadRepository.Create(thread);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(Get), new { id = thread.Id }, ConvertToDTO.ToCreateResponse(thread));
            }
            else
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateThreadRequest threadRequest)
        {
            Thread thr = _threadRepository.Find(id);
            int userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.NameId).Value);
            if (thr != null && thr.UserId == userId)
            {
                thr.Body = threadRequest.Body;
                _threadRepository.Update(thr);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else return Unauthorized();
        }

        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            Thread thread = _threadRepository.Find(id);
            int userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.NameId).Value);
            if (thread != null && thread.UserId == userId)
            {
                _threadRepository.Delete(thread);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else return Unauthorized();
        }
    }
}
