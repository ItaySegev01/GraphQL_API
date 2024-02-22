using GraphQL_API_X_clone.Interfaces;
using GraphQL_API_X_clone.Models;
using GraphQL_API_X_clone.Models.Mutations;
using GraphQL_API_X_clone.Models.Schemas;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GraphQL_API_X_clone.Services
{
    public class UserService : ISchemasActions<User, UserInput>
    {
        private readonly IMongoCollection<User> _usersCollection;

        private readonly PostService _postsService;

        public UserService(IOptions<MongoDBSettings> mongoDBSettings, PostService postsService)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _usersCollection = database.GetCollection<User>("users");
            _postsService = postsService;
        }

        public async Task<User> CreateAsync(User user)
        {
            var filter = Builders<User>.Filter.Eq(x => x.Email, user.Email);
            var excisingUser = await _usersCollection.FindAsync(filter).GetAwaiter().GetResult().FirstOrDefaultAsync();
            if (excisingUser != null)
                return excisingUser;
            await _usersCollection.InsertOneAsync(user);
            return await _usersCollection.FindAsync(filter).GetAwaiter().GetResult().FirstOrDefaultAsync();
        }

        public async Task DeleteAsync(string id)
        {
            // delete all user posts before the user
            await _postsService.DeletePostsByIdAsync(id);
            var filter = Builders<User>.Filter.Eq(x => x.Id, id);
            await _usersCollection.DeleteOneAsync(filter);
            return;
        }

        public async Task<ICollection<User>> GetAsync()
        {
            return await _usersCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<User> GetByIdAsync(string id)
        {
            var filter = Builders<User>.Filter.Eq(x => x.Id, id);
            using (var cursor = await _usersCollection.FindAsync(filter))
            {
                var user = await cursor.FirstOrDefaultAsync();
                return user;
            }
        }


        public async Task<User> UpdateAsync(string id, UserInput user)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, id);
            var update = Builders<User>.Update
              .Set(u => u.Image, user.Image)
              .Set(u => u.Description, user.Description)
              .Set(u => u.UserName, user.UserName);
            await _usersCollection.UpdateOneAsync(filter, update);
            return await GetByIdAsync(id);
        }

        public async Task<User> UpdateFollowersAsync(string id, string otherId)
        {
            var user = await GetByIdAsync(id);
            UpdateDefinition<User> update;
            if (user.userIds.Contains(otherId)){
                update = Builders<User>.Update.Pull("followers", otherId);
            }
            else
            { update = Builders<User>.Update.Push("followers", otherId);}     
            var filter = Builders<User>.Filter.Eq(u => u.Id, id);
            await _usersCollection.UpdateOneAsync(filter, update);
            return await GetByIdAsync(id);
        }
    }
}
