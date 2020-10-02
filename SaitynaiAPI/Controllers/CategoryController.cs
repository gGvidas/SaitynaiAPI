using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaitynaiAPI.DTOs.CategoryDTOs;
using SaitynaiAPI.DTOs;
using SaitynaiAPI.Entities;
using SaitynaiAPI.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace SaitynaiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly SaitynaiDbContext _context;
        public CategoryController(ICategoryRepository categoryRepository, SaitynaiDbContext context)
        {
            _categoryRepository = categoryRepository;
            _context = context;
        }

        /// <summary>
        /// Fetches all categories
        /// </summary>
        /// <returns>A list of categories</returns>
        [HttpGet]
        [ProducesResponseType(typeof(Task<ICollection<GetCategoryCollectionResponse>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(ConvertToDTO.ToGetResponse(await _categoryRepository.GetAll()));
        }

        /// <summary>
        /// Fetches a category that matches the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Category that matches the id</returns>
        /// <response code="200">Returns the category that matches the id</response>
        /// <response code="404">If there are no categories that match the id</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetCategoryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            Category cat = _categoryRepository.Find(id);
            if (cat != null) return Ok(ConvertToDTO.ToGetResponse(cat));
            else return NotFound();
        }
        
        /// <summary>
        /// Creates a new category
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/category
        ///     {
        ///         "name": "A new category"
        ///     }
        /// </remarks>
        /// <param name="categoryRequest"></param>
        /// <returns>Newly created category</returns>
        /// <response code="201">Returns the created category</response>
        [Authorize(Policy = "Admin")]
        [HttpPost]
        [ProducesResponseType(typeof(CreateCategoryResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] CategoryRequest categoryRequest)
        {
            Category category = ConvertFromDTO.FromCreateRequest(categoryRequest);
            _categoryRepository.Create(category);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get),new {id = category.Id}, ConvertToDTO.ToCreateResponse(category));
        }

        /// <summary>
        /// Changes the title of the category that matches the id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PATCH /api/category/1
        ///     {
        ///         "name": "A patched category"
        ///     }
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="categoryRequest"></param>
        /// <returns>No content</returns>
        /// <response code="204">Returns no content</response>
        [Authorize(Policy = "Admin")]
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryRequest categoryRequest)
        {
            Category category = _categoryRepository.Find(id);
            if (category != null)
            {
                category.Name = categoryRequest.Name;
                _categoryRepository.Update(category);
                await _context.SaveChangesAsync();
            }
            return NoContent();
        }

        /// <summary>
        /// Deletes the category that matches the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>No content</returns>
        /// <response code="204">Returns no content</response>
        [Authorize(Policy = "Admin")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            Category category = _categoryRepository.Find(id);
            if (category != null)
            {
                _categoryRepository.Delete(category);
                await _context.SaveChangesAsync();
            }
            return NoContent();
        }
    }
}
