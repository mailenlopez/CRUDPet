using Microsoft.AspNetCore.Mvc;
using MediatR;
using Domain.Entities;
using System.Threading;
using Application.Pet.Queries.GetPets;
using Application.Pet.Queries.GetPetById;
using Application.Pet.Commands.CreatePet;
using Application.Pet.Commands.UpdatePet;
using Application.Pet.Commands.DeletePet;
using Application.Common.Exceptions;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PetController(IMediator mediator)
        {
            _mediator = mediator;
        }


        // GET: api/<PetController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pet>>> GetAll(CancellationToken cancellationToken)
        {
             var response = await _mediator.Send(new GetPetsQuery(), cancellationToken);
             return Ok(response);
        }

        // GET api/<PetController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetPetByIdDto>> GetPetById(int id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetPetByIdQuery(id), cancellationToken);
            if (response == null)
                return NotFound();

            return Ok(response);
        }

        // POST api/<PetController>
        [HttpPost]
        public async Task<ActionResult<int>> Post(CreatePetCommand command, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command, cancellationToken);
            return Ok(response);
        }

        // PUT api/<PetController>/5
        [HttpPut]
        public async Task<ActionResult> Put(UpdatePetCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return Ok();

        }

        // DELETE api/<PetController>/5
        [HttpDelete]
        public async Task<ActionResult> Delete(DeletePetCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            if (!response.IsDeleted)
                throw new NotFoundException("Pet id " + request.Id + " does not exist.");

            return Ok();
        }
    }
}
