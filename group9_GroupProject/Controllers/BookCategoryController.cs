using AutoMapper;
using group9_GroupProject.DTO;
using group9_GroupProject.Models;
using group9_GroupProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace group9_GroupProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookCategoryController : ControllerBase
    {
        private BookCategoryRepository _bookCategoryRepository;
        private readonly IMapper _mapper;

        public BookCategoryController(BookCategoryRepository bookCategoryRepository, IMapper mapper)
        {
            _bookCategoryRepository = bookCategoryRepository;
            _mapper = mapper;
        }

        // GET: api/<BookCategoryController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetBookCategoryDto>>> GetAll()
        {
            var categories = await _bookCategoryRepository.GetAllCategoriesAsync();
            var categoryDtos = _mapper.Map<IEnumerable<GetBookCategoryDto>>(categories);
            return Ok(categoryDtos);
        }

        // GET api/<BookCategoryController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetBookCategoryDto>> GetById(int id)
        {
            var categories = await _bookCategoryRepository.GetCategoryByIdAsync(id);
            var categoryDtos = _mapper.Map<GetBookCategoryDto>(categories);
            return Ok(categoryDtos);
        }

        // POST api/<BookCategoryController>
        [HttpPost]
        public async Task<ActionResult<GetBookCategoryDto>> Post([FromBody] PostBookCategoryDto postBookCategoryDto)
        {
            var categories = _mapper.Map<BookCategory>(postBookCategoryDto);
            var result = await _bookCategoryRepository.AddBookCategoryAsync(categories);
            return Ok(_mapper.Map<GetBookCategoryDto>(result));
        }

        // PUT api/<BookCategoryController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<GetBookCategoryDto>> Put(int id, [FromBody] PostBookCategoryDto postBookCategoryDto)
        {
            var categories = _mapper.Map<BookCategory>(postBookCategoryDto);
            var result = await _bookCategoryRepository.UpdateBookCategoryAsync(id, categories);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<GetBookCategoryDto>(result));
        }

        // DELETE api/<BookCategoryController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool result = await _bookCategoryRepository.DeleteBookCategoryAsync(id);
            return Ok(result);
        }


        [HttpPatch("{id}")]
        public async Task<ActionResult<GetBookCategoryDto>> PatchUpdate(int id, [FromBody] JsonPatchDocument<PostBookCategoryDto> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest("Patch document cannot be null.");
            }

            //Get the existing entity
            var bookCategoryEntity = await _bookCategoryRepository.GetCategoryByIdAsync(id);
            if (bookCategoryEntity == null)
            {
                return NotFound();
            }

            //Convert the patched DTO into a dictionary of changed fields
            var updates = new Dictionary<string, object>();
            foreach (var operation in patchDocument.Operations)
            {
                // Example: /PublisherName → PublisherName
                var propertyName = operation.path.TrimStart('/');
                updates[propertyName] = operation.value;
            }

            //Call your repository
            var updatedEntity = await _bookCategoryRepository.PatchBookCategoryAsync(id, updates);
            if (updatedEntity == null)
            {
                return NotFound();
            }

            // 6️ Map back to a DTO for the response
            var resultDto = _mapper.Map<GetBookCategoryDto>(updatedEntity);

            return Ok(resultDto);
        }
    }
}
