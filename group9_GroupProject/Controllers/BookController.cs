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
    public class BookController : ControllerBase
    {
        private BooksRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookController(BooksRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        // GET: api/<BookCategoryController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetBookDto>>> GetAll()
        {
            var books = await _bookRepository.GetAllBooksAsync();
            var bookDtos = _mapper.Map<IEnumerable<GetBookDto>>(books);
            return Ok(bookDtos);
        }

        // GET api/<BookCategoryController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetBookDto>> GetById(int id)
        {
            var books = await _bookRepository.GetBookByIdAsync(id);
            var bookDtos = _mapper.Map<GetBookDto>(books);
            return Ok(bookDtos);
        }

        [HttpGet("{id}/details")]
        public async Task<ActionResult<GetBookDetailDto>> GetDetailsById(int id)
        {
            var bookDetail = await _bookRepository.GetBookDetailByIdAsync(id);
            var bookDtos = _mapper.Map<GetBookDetailDto>(bookDetail);
            return Ok(bookDtos);
        }

        // POST api/<BookCategoryController>
        [HttpPost]
        public async Task<ActionResult<GetBookDto>> Post([FromBody] PostBookDto postBookDto)
        {
            var books = _mapper.Map<Book>(postBookDto);
            var result = await _bookRepository.AddBookAsync(books);
            return Ok(_mapper.Map<GetBookDto>(result));
        }

        // PUT api/<BookCategoryController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<GetBookDto>> Put(int id, [FromBody] PostBookDto postBookDto)
        {
            var books = _mapper.Map<Book>(postBookDto);
            var result = await _bookRepository.UpdateBookAsync(id, books);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<GetBookDto>(result));
        }

        // DELETE api/<BookCategoryController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool result = await _bookRepository.DeleteBookAsync(id);
            return Ok(result);
        }


        [HttpPatch("{id}")]
        public async Task<ActionResult<GetBookDto>> PatchUpdate(int id, [FromBody] JsonPatchDocument<PostBookDto> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest("Patch document cannot be null.");
            }

            //Get the existing entity
            var bookCategoryEntity = await _bookRepository.GetBookByIdAsync(id);
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
            var updatedEntity = await _bookRepository.PatchBookAsync(id, updates);
            if (updatedEntity == null)
            {
                return NotFound();
            }

            // 6️ Map back to a DTO for the response
            var resultDto = _mapper.Map<GetBookDto>(updatedEntity);

            return Ok(resultDto);
        }
    }
}
