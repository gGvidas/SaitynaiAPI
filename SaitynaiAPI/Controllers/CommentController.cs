using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using SaitynaiAPI.DTOs;
using SaitynaiAPI.DTOs.CommentDTOs;
using SaitynaiAPI.Entities;
using SaitynaiAPI.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SaitynaiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IThreadRepository _threadRepository;
        private readonly SaitynaiDbContext _context;
        public CommentController(ICommentRepository commentRepository, IThreadRepository threadRepository, SaitynaiDbContext context)
        {
            _commentRepository = commentRepository;
            _threadRepository = threadRepository;
            _context = context;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetCommentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            Comment comment = _commentRepository.Find(id);
            if (comment != null) return Ok(ConvertToDTO.ToGetResponse(comment));
            else return NotFound();
        }

        [HttpGet("user/{userId}")]
        [ProducesResponseType(typeof(ICollection<GetCommentResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByUser(int userId)
        {
            return Ok(ConvertToDTO.ToGetResponse(await _commentRepository.GetByUser(userId)));
        }

        [HttpGet("thread/{threadId}")]
        [ProducesResponseType(typeof(ICollection<GetCommentResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByThread(int threadId)
        {
            return Ok(ConvertToDTO.ToGetResponse(await _commentRepository.GetByThread(threadId)));
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(CreateCommentResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create([FromBody] CreateCommentRequest commentRequest)
        {
            int id = int.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.NameId).Value);
            if (_threadRepository.Find(commentRequest.ThreadId) != null)
            {
                Comment comment = ConvertFromDTO.FromCreateRequest(commentRequest);
                comment.UserId = id;
                _commentRepository.Create(comment);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetByThread), new { threadId = comment.ThreadId }, ConvertToDTO.ToCreateResponse(comment));
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
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCommentRequest commentRequest)
        {
            Comment comment = _commentRepository.Find(id);
            int userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.NameId).Value);
            if (comment != null && comment.UserId == userId)
            {
                comment.Body = commentRequest.Body;
                _commentRepository.Update(comment);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else return Unauthorized();
        }

        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete(int id)
        {
            Comment comment = _commentRepository.Find(id);
            int userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.NameId).Value);
            if (comment != null && comment.UserId == userId)
            {
                _commentRepository.Delete(comment);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else return Unauthorized();
        }
    }
}
