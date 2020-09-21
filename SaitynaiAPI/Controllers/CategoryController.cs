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

        [HttpGet]
        [ProducesResponseType(typeof(Task<ICollection<GetCategoryCollectionResponse>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(ConvertToDTO.ToGetResponse(await _categoryRepository.GetAll()));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetCategoryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            Category cat = _categoryRepository.Find(id);
            if (cat != null) return Ok(ConvertToDTO.ToGetResponse(cat));
            else return NotFound();
        }
        
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
