using Microsoft.AspNetCore.Mvc;
using ZooShop.Application.Common.Interfaces.Repositories;
using ZooShop.Domain.Animals;
using ZooShop.Application.Common;
using ZooShop.Application.Animals.Exceptions;

namespace ZooShop.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimalsController : ControllerBase
{
    private readonly IAnimalRepository _animalRepository;

    public AnimalsController(IAnimalRepository animalRepository)
    {
        _animalRepository = animalRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Animal>>> GetAll(CancellationToken cancellationToken)
    {
        var animals = await _animalRepository.GetAll(cancellationToken);
        return Ok(animals);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Animal>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var animal = await _animalRepository.GetById(new AnimalId(id), cancellationToken);
        return animal.Match<ActionResult>(
            a => Ok(a),
            () => NotFound(new AnimalNotFoundException(new AnimalId(id))));
    }

    [HttpPost]
    public async Task<ActionResult<Animal>> Create([FromBody] Animal animal, CancellationToken cancellationToken)
    {
        var createdAnimal = await _animalRepository.Add(animal, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = createdAnimal.Id }, createdAnimal);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] Animal animal, CancellationToken cancellationToken)
    {
        if (id != animal.Id.Value)
        {
            return BadRequest();
        }

        var updatedAnimal = await _animalRepository.Update(animal, cancellationToken);
        return Ok(updatedAnimal);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var animalId = new AnimalId(id);
        var animal = await _animalRepository.GetById(animalId, cancellationToken);

        return await animal.Match<Task<IActionResult>>(
            async a =>
            {
                await _animalRepository.Delete(a, cancellationToken);
                return NoContent();
            },
            () => Task.FromResult<IActionResult>(NotFound(new AnimalNotFoundException(animalId))));
    }
}