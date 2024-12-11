using ZooShop.Api.Dtos;
using ZooShop.Api.Modules.Errors;
using ZooShop.Application.Common.Interfaces.Queries;
using ZooShop.Application.Animals.Commands;
using ZooShop.Domain.Animals;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ZooShop.Api.Controllers;

[Route("animals")]
[ApiController]
public class AnimalsController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IAnimalQueries _animalQueries;

    public AnimalsController(ISender sender, IAnimalQueries animalQueries)
    {
        _sender = sender;
        _animalQueries = animalQueries;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<AnimalDto>>> GetAll(CancellationToken cancellationToken)
    {
        var entities = await _animalQueries.GetAll(cancellationToken);
        return Ok(entities.Select(AnimalDto.FromDomainModel).ToList());
    }

    [HttpGet("{animalId:guid}")]
    public async Task<ActionResult<AnimalDto>> Get([FromRoute] Guid animalId, CancellationToken cancellationToken)
    {
        var entity = await _animalQueries.GetById(new AnimalId(animalId), cancellationToken);
        return entity.Match<ActionResult<AnimalDto>>(
            u => Ok(AnimalDto.FromDomainModel(u)),
            () => NotFound());
    }

    [HttpPost]
    public async Task<ActionResult<AnimalDto>> Create([FromBody] AnimalDto request, CancellationToken cancellationToken)
    {
        var input = new CreateAnimalCommand
        {
            Name = request.Name,
            Species = request.Species,
            Age = request.Age,
            Price = request.Price,
            CategoryId = request.CategoryId
        };

        var result = await _sender.Send(input, cancellationToken);
        return result.Match<ActionResult<AnimalDto>>(
            c =>  AnimalDto.FromDomainModel(c),
            e => e.ToObjectResult());
    }
    [HttpPut("{animalId:guid}")]
    public async Task<ActionResult<AnimalDto>> Update([FromRoute] Guid animalId, [FromBody] AnimalDto request, CancellationToken cancellationToken)
    {
        var input = new UpdateAnimalCommand
        {
            AnimalId = animalId,
            Name = request.Name,
            Species = request.Species,
            Age = request.Age,
            Price = request.Price,
            CategoryId = request.CategoryId
        };

        var result = await _sender.Send(input, cancellationToken);
        return result.Match<ActionResult<AnimalDto>>(
            f => Ok(AnimalDto.FromDomainModel(f)),
            e => e.ToObjectResult());
    }

    [HttpDelete("{animalId:guid}")]
    public async Task<ActionResult<AnimalDto>> Delete([FromRoute] Guid animalId, CancellationToken cancellationToken)
    {
        var input = new DeleteAnimalCommand
        {
            AnimalId = animalId
        };

        var result = await _sender.Send(input, cancellationToken);
        return result.Match<ActionResult<AnimalDto>>(
            c => Ok(AnimalDto.FromDomainModel(c)),
            e => e.ToObjectResult());
    }
}