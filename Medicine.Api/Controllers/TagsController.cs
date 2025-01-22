// namespace Medicine.Api.Controllers;

// using Database.Enteties;
// using Medicine.Database.UnitOfWork.Repositories;
// using Microsoft.AspNetCore.Mvc;

// [ApiController]
// [Route("[controller]")]
// [ProducesResponseType(StatusCodes.Status200OK)]
// [ProducesResponseType(StatusCodes.Status404NotFound)]
// public class TagsController(IRepositoryV2<Tag> tags)
// {
//     [HttpGet("{id}", Name = "Get By Id")]
//     public async Task<IResult> GetById(Guid id, CancellationToken token)
//     {
//         var result = await tags.Find(id, token);

//         // will use service instead
//         //return result.Match(s => Results.Ok(s), e => Results.NotFound());
//     }

//     [HttpGet(Name = "Get All Tags")]
//     public async Task<IResult> Get(CancellationToken token)
//     {
//         return Results.Ok((await tags.Get(x => true, token)).Value);
//     }
// }
