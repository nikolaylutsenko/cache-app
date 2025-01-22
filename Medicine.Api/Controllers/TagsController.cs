namespace Medicine.Api.Controllers;

using Core.Repositories;
using Database.Enteties;
using Medicine.Database.UnitOfWork.Repositories;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
public class TagsController(IRepository<Tag> tags)
{
    [HttpGet("{id}", Name = "Get By Id")]
    public async Task<IResult> GetById(Guid id, CancellationToken token)
    {
        var result = await tags.Get(id, token);

        return result.Match(s => Results.Ok(s), e => Results.NotFound());
    }

    [HttpGet(Name = "Get All Tags")]
    public async Task<IResult> Get(CancellationToken token)
    {
        return Results.Ok((await tags.Get(x => true, token)).Value);
    }
}
