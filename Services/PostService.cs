using GraphQL_API_X_clone.Interfaces;
using GraphQL_API_X_clone.Models;
using GraphQL_API_X_clone.Models.Mutations;
using GraphQL_API_X_clone.Models.Schemas;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GraphQL_API_X_clone.Services
{
    public class PostService : ISchemasActions<Post, PostInput>
    {
        private readonly IMongoCollection<Post> _postsCollection;

        public PostService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _postsCollection = database.GetCollection<Post>("posts");
        }

        public async Task<Post> CreateAsync(Post post)
        {
           
            await _postsCollection.InsertOneAsync(post);
            var filter = Builders<Post>.Filter.Eq(x => x.Content, post.Content);
            return await _postsCollection.FindAsync(filter).GetAwaiter().GetResult().FirstOrDefaultAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var all = await _postsCollection.Find(new BsonDocument()).ToListAsync();   
            foreach (var post in all)
            {  
                if (post.postsIds.Contains(id))
                {
                    var filter = Builders<Post>.Filter.Eq(x => x.Id, post.Id);
                    var update = Builders<Post>.Update.Pull("replies", id);
                    await _postsCollection.UpdateOneAsync(filter, update);
                }
            }
            var deleteFilter = Builders<Post>.Filter.Eq(x => x.Id, id);
            await _postsCollection.DeleteOneAsync(deleteFilter);
            return;

        }

        public async Task<ICollection<Post>> GetAsync()
        {
            var list = await _postsCollection.Find(new BsonDocument()).ToListAsync();
            list.OrderByDescending(p  => p.CreatedAt);
            return list;
        }

        public async Task<Post> GetByIdAsync(string id)
        {
            var filter = Builders<Post>.Filter.Eq(x => x.Id, id);
            using (var cursor = await _postsCollection.FindAsync(filter))
            {
                var post = await cursor.FirstOrDefaultAsync();
                return post;
            }
        }

        public async Task<Post> UpdateAsync(string id, PostInput post)
        {
            var filter = Builders<Post>.Filter.Eq(u => u.Id, id);
            var update = Builders<Post>.Update
                .Set(p => p.Content, post.Content)
                .Set(p => p.Media, post.Media);
            await _postsCollection.UpdateOneAsync(filter, update);
            return await GetByIdAsync(id);
        }

        public async Task<Post> UpdateLikesAsync(string postId, string userId)
        {
            var post = await GetByIdAsync(postId);
            UpdateDefinition<Post> update;
            if (post.userIds.Contains(userId))
            {
                update = Builders<Post>.Update.Pull("likes", userId);
            }
            else { update = Builders<Post>.Update.Push("likes", userId); }
            var filter = Builders<Post>.Filter.Eq(u => u.Id, postId);
            await _postsCollection.UpdateOneAsync(filter, update);
            return await GetByIdAsync(postId);
        }

        public async Task<Post> AddReplyAsync(string postId, string replyId)
        {
            var filter = Builders<Post>.Filter.Eq(x => x.Id, postId);
            var update = Builders<Post>.Update.Push("replies", replyId);
            await _postsCollection.UpdateOneAsync(filter, update);
            return await GetByIdAsync(postId);
        }

        public async Task DeletePostsByIdAsync(string userId)
        {
            var filter = Builders<Post>.Filter.Eq(p => p.userId, userId);
            await _postsCollection.DeleteManyAsync(filter);
            return;
        }
    }
}
