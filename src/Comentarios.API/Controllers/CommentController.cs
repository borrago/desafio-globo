using Comentarios.API.Models;
using Comentarios.API.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Comentarios.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentController : ControllerBase
{
    private readonly MongoDBService _mongoDBService;

    public CommentController(MongoDBService mongoDBService)
    {
        _mongoDBService = mongoDBService;
    }

    [HttpPost("new")]
    public async Task<ActionResult<Comentario>> AddComment(Comentario comment)
    {
        comment.Id = ObjectId.GenerateNewId().ToString();
        await _mongoDBService.CreateAsync(comment);
        var locationUri = $"{Request.Host}/host/api/list/{comment.Id}";
        return Created(locationUri, comment);
    }

    [HttpGet("list/{id}")]
    public async Task<ActionResult<List<Comentario>>> GetCommentsByContentId(string id)
    {
        var comments = await _mongoDBService.GetByIdAsync(id);
        return Ok(comments);
    }
}
