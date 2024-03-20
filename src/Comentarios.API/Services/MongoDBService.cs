using Comentarios.API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Comentarios.API.Services;

public class MongoDBService
{

    private readonly IMongoCollection<Comentario> _comentarioCollection;

    public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
    {
        var client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _comentarioCollection = database.GetCollection<Comentario>(mongoDBSettings.Value.CollectionName);
    }

    public async Task CreateAsync(Comentario playlist)
    {
        await _comentarioCollection.InsertOneAsync(playlist);
        return;
    }

    public async Task<Comentario> GetByIdAsync(string id)
    {
        return await _comentarioCollection.Find(f => f.Id == id).SingleOrDefaultAsync();
    }
}