using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Data;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interface;
using PokemonReviewApp.Model;
using PokemonReviewApp.Repository;
using System.Security.Cryptography.X509Certificates;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : Controller
    {


        private readonly IOwnerRepository _ownerRepository;

        private readonly IMapper _mapper;

        public OwnerController(IOwnerRepository ownerRepository , IMapper mapper)
        {
            _ownerRepository = ownerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Owner>))]
        public IActionResult GetOwners()
        {
            var owners = _mapper.Map<List<OwnerDto>>(_ownerRepository.GetOwners());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(owners);
        }

        [HttpGet("{ownerId}")]
        [ProducesResponseType(200, Type = typeof(Owner))]
        [ProducesResponseType(400)]
        public IActionResult GetOwner(int ownerId)
        {
            if (!_ownerRepository.OwnerExists(ownerId))
                return NotFound();
            var owner = _mapper.Map<OwnerDto>(_ownerRepository.GetOwner(ownerId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(owner);
        }

        [HttpGet("{ownerId}/pokemon")]
        [ProducesResponseType(200, Type = typeof(Owner))]
        [ProducesResponseType(400)]

        public IActionResult GetPokemonByOwner(int ownerId)
        {
            if (!_ownerRepository.OwnerExists(ownerId))
            { 
                return NotFound(); 
            }
            
            var owner = _mapper.Map<List<PokemonDto>> (
                _ownerRepository.GetPokemonByOwner(ownerId));
            if (!ModelState.IsValid) 
                return BadRequest();
            
            return Ok(owner);
        }

        [HttpPost]
        [ProducesResponseType (204)]
        [ProducesResponseType (400)]

        public IActionResult CreateOwner( [FromBody] OwnerDto ownerCreate)
        {
            if (ownerCreate == null)
                return BadRequest(ModelState);

            var owner = _ownerRepository.GetOwners()
                .Where(l => l.Name.Trim().ToUpper() == ownerCreate.Name.TrimEnd().ToUpper()) 
                .FirstOrDefault();
            
            if (owner != null)
            {
                ModelState.AddModelError("", "Owner already Exists");
                return StatusCode(422, ModelState);
            }

            if (ModelState.IsValid)
                return BadRequest(ModelState);

            var ownerMap = _mapper.Map<Owner>(ownerCreate);


            if (!_ownerRepository.CreateOwner(ownerMap))
            {
                ModelState.AddModelError("", "something wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("successfully saved");
            
        }
    }
}
