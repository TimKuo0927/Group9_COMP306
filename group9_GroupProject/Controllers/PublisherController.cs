using AutoMapper;
using group9_GroupProject.DTO;
using group9_GroupProject.Models;
using group9_GroupProject.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Runtime;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace group9_GroupProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private PublisherRepository _publisherRepository;
        private readonly IMapper _mapper;

        public PublisherController(PublisherRepository publisherRepository, IMapper mapper)
        {
            _publisherRepository = publisherRepository;
            _mapper = mapper;
        }

        // GET: api/<PublisherController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetPublisherDto>>> GetAll()
        {
            var publishers = await _publisherRepository.GetAllPublishersAsync();
            var publisherDtos = _mapper.Map<IEnumerable<GetPublisherDto>>(publishers);
            return Ok(publisherDtos);
        }

        // GET api/<PublisherController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetPublisherDto>> GetById(int id)
        {
            var publishers = await _publisherRepository.GetPublisherByIdAsync(id);
            var publisherDtos = _mapper.Map<GetPublisherDto>(publishers);
            return Ok(publisherDtos);
        }

        // POST api/<PublisherController>
        [HttpPost]
        public async Task<ActionResult<GetPublisherDto>> Post([FromBody] PostPublisherDto postPublisherDto)
        {
            var publisher =  _mapper.Map<Publisher>(postPublisherDto);
            var result = await _publisherRepository.AddPublisherAsync(publisher);
            return Ok(_mapper.Map<GetPublisherDto>(result));
        }

        // PUT api/<PublisherController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<GetPublisherDto>> Put(int id, [FromBody] PutPublisherDto putPublisherDto)
        {
            var publisher = _mapper.Map<Publisher>(putPublisherDto);
            var result = await _publisherRepository.UpdatePublisherAsync(id,publisher);
            if(result == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<GetPublisherDto>(result));
        }

        // DELETE api/<PublisherController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
           bool result =  await _publisherRepository.DeletePublisherAsync(id);
           return Ok(result);
        }


        [HttpPatch("{id}")]
        public async Task<ActionResult<GetPublisherDto>> PatchUpdate(int id,[FromBody] JsonPatchDocument<PostPublisherDto> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest("Patch document cannot be null.");
            }

            //Get the existing entity
            var publisherEntity = await _publisherRepository.GetPublisherByIdAsync(id);
            if (publisherEntity == null)
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

            //Call your repository PatchPublisherAsync
            var updatedEntity = await _publisherRepository.PatchPublisherAsync(id, updates);
            if (updatedEntity == null)
            {
                return NotFound();
            }

            // 6️ Map back to a DTO for the response
            var resultDto = _mapper.Map<GetPublisherDto>(updatedEntity);

            return Ok(resultDto);
        }


    }
}
