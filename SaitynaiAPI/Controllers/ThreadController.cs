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

        /// <summary>
        /// Fetches specified thread
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Specified thread</returns>
        /// <response code="200">Returns specified thread</response>
        /// <response code="404">If no threads exist under specified id</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetThreadResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            Thread thread = _threadRepository.Find(id);
            if (thread != null) return Ok(ConvertToDTO.ToGetResponse(thread));
            else return NotFound();
        }

        /// <summary>
        /// Fetches all threads
        /// </summary>
        /// <returns>A list of threads</returns>
        /// <response code="200">Returns a list of threads</response>
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<GetThreadCollectionResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(ConvertToDTO.ToGetResponse(await _threadRepository.GetAll()));
        }

        /// <summary>
        /// Fetches all threads that belong to specified category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns>A list of threads</returns>
        /// <response code="200">Returns a list of threads</response>
        [HttpGet("category/{categoryId}")]
        [ProducesResponseType(typeof(ICollection<GetThreadCollectionResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByCategory(int categoryId)
        {
            return Ok(ConvertToDTO.ToGetResponse(await _threadRepository.GetByCategory(categoryId)));
        }

        /// <summary>
        /// Creates a new thread
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/thread
        ///     {
        ///         "title": "Hello",
        ///         "body": "Hi everyone",
        ///         "categoryId": 1
        ///     }
        /// </remarks>
        /// <param name="threadRequest"></param>
        /// <returns>Newly created thread</returns>
        /// <response code="201">Returns a new thread</response>
        /// <response code="404">If category doesn't exist</response>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(CreateThreadResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create([FromBody] CreateThreadRequest threadRequest)
        {
            int id = int.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "Id").Value);
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

        /// <summary>
        /// Changes the body of an existing thread
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PATCH /api/thread/1
        ///     {
        ///         "body": "Hi everyone!"
        ///     }
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="threadRequest"></param>
        /// <returns></returns>
        /// <response code="204">Returns no content</response>
        /// <response code="401">If user is not authorized</response>
        [Authorize]
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateThreadRequest threadRequest)
        {
            Thread thr = _threadRepository.Find(id);
            bool admin = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "Admin").Value == "True";
            int userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "Id").Value);
            if (thr != null && (thr.UserId == userId || admin ))
            {
                thr.Body = threadRequest.Body;
                _threadRepository.Update(thr);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else return Unauthorized();
        }

        /// <summary>
        /// Deletes specified thread
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
            Thread thread = _threadRepository.Find(id);
            bool admin = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "Admin").Value == "True";
            int userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "Id").Value);
            if (thread != null && (thread.UserId == userId || admin))
            {
                _threadRepository.Delete(thread);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else return Unauthorized();
        }
    }
}
