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

        /// <summary>
        /// Fetches comment that matches the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Comment that matches the id</returns>
        /// <response code="200">Returns the comment that matches the id</response>
        /// <response code="404">If no comment matches the id</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetCommentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            Comment comment = _commentRepository.Find(id);
            if (comment != null) return Ok(ConvertToDTO.ToGetResponse(comment));
            else return NotFound();
        }

        /// <summary>
        /// Fetches all comments that belong to the user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>A list of comments</returns>
        /// <response code="200">Returns a list of comments</response>
        /// <response code="404">If no user exists under specified id</response>
        [HttpGet("user/{userId}")]
        [ProducesResponseType(typeof(ICollection<GetCommentResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByUser(int userId)
        {
            return Ok(ConvertToDTO.ToGetResponse(await _commentRepository.GetByUser(userId)));
        }

        /// <summary>
        /// Fetches all comments that belong to the specified thread
        /// </summary>
        /// <param name="threadId"></param>
        /// <returns>A list of comments</returns>
        /// <response code="200">Returns a list of comments</response>
        /// <response code="404">If no thread exists under specified id</response>
        [HttpGet("thread/{threadId}")]
        [ProducesResponseType(typeof(ICollection<GetCommentResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByThread(int threadId)
        {
            return Ok(ConvertToDTO.ToGetResponse(await _commentRepository.GetByThread(threadId)));
        }

        /// <summary>
        /// Creates a new comment
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/comment
        ///     {
        ///         "body": "Hello",
        ///         "threadId": 1
        ///     }
        /// </remarks>
        /// <param name="commentRequest"></param>
        /// <returns>Newly created comment</returns>
        /// <response code="201">Returns a new comment</response>
        /// <response code="404">If thread wasn't found</response>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(CreateCommentResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create([FromBody] CreateCommentRequest commentRequest)
        {
            int id = int.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "Id").Value);
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

        /// <summary>
        /// Changes the body of the specified comment
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PATCH /api/comment/1
        ///     {
        ///         "body": "Hi"
        ///     }
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="commentRequest"></param>
        /// <returns>No content</returns>
        /// <response code="204">Returns no content</response>
        /// <response code="401">If user is not authorized</response>
        [Authorize]
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCommentRequest commentRequest)
        {
            Comment comment = _commentRepository.Find(id);
            bool admin = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "Admin").Value == "True";
            int userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "Id").Value);
            if (comment != null && (comment.UserId == userId || admin))
            {
                comment.Body = commentRequest.Body;
                _commentRepository.Update(comment);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else return Unauthorized();
        }

        /// <summary>
        /// Deletes specified comment
        /// </summary>
        /// <param name="id"></param>
        /// <returns>No content</returns>
        /// <response code="204">Returns no content</response>
        /// <response code="401">If user is not authorized</response>
        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete(int id)
        {
            Comment comment = _commentRepository.Find(id);
            bool admin = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "Admin").Value == "True";
            int userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "Id").Value);
            if (comment != null && (comment.UserId == userId || admin ))
            {
                _commentRepository.Delete(comment);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else return Unauthorized();
        }
    }
}
