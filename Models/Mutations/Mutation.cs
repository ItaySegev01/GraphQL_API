using GraphQL_API_X_clone.Models.Schemas;
using GraphQL_API_X_clone.Services;

namespace GraphQL_API_X_clone.Models.Mutations;

public class Mutation
{
    private readonly PostService _postService;
    private readonly UserService _userService;

    public Mutation(PostService postService, UserService userService)
    {
        _postService = postService;
        _userService = userService;
    }


    // Pots :

    public async Task<Post> AddPostAsync(string userId, PostInput postInput)
    {
        try
        {
            var newPost = new Post()
            {
                Content = postInput.Content,
                Media = postInput.Media,
                userId = userId,
                userIds = new List<string>(),
                postsIds = new List<string>(),
                CreatedAt = DateTime.UtcNow,
            };
            return await _postService.CreateAsync(newPost);
        }
        catch (Exception ex)
        {
            throw new GraphQLException(new Error($"something went wrong message: {ex.Message}", "FAILD"));
        }
    }
    public async Task<Post> AddReplyAsync(string userId, string postId, PostInput postInput)
    {
        try
        {
            var newPost = new Post()
            {
                Content = postInput.Content,
                Media = postInput.Media,
                userId = userId,
                userIds = new List<string>(),
                postsIds = new List<string>(),
                CreatedAt = DateTime.UtcNow,
            };
            var reply = await _postService.CreateAsync(newPost);
            return await _postService.AddReplyAsync(postId, reply.Id);
        }
        catch (Exception ex)
        {
            throw new GraphQLException(new Error($"something went wrong message: {ex.Message}", "FAILD"));
        }
    }

    public async Task<Post> UpdatePostAsync(string postId, PostInput postInput)
    {
        try
        {
            return await _postService.UpdateAsync(postId, postInput);
        }
        catch (Exception ex)
        {
            throw new GraphQLException(new Error($"something went wrong message: {ex.Message}", "FAILD"));
        }
    }

    public async Task<bool> RemovePostAsync(string postId)
    {
        try
        {
            await _postService.DeleteAsync(postId);
            return true;

        }
        catch (Exception ex)
        {
            throw new GraphQLException(new Error($"something went wrong message: {ex.Message}", "FAILD"));
        }
    }

    public async Task<Post> UpdateLikesAsync(string postId, string userId)
    {
        try
        {
            return await _postService.UpdateLikesAsync(postId, userId);
        }
        catch (Exception ex)
        {
            throw new GraphQLException(new Error($"something went wrong message: {ex.Message}", "FAILD"));
        }
    }

    // Users :

    public async Task<User> AddUserAsync(UserInput userInput)
    {
        try
        {
            var newUser = new User()
            {
                Image = userInput.Image,
                UserName = userInput.UserName,
                Description = userInput.Description,
                Email = userInput.Email,
                CreatedAt = DateTime.UtcNow,
                userIds = new List<string>()
            };
            return await _userService.CreateAsync(newUser);
        }
        catch (Exception ex)
        {
            throw new GraphQLException(new Error($"something went wrong message: {ex.Message}", "FAILD"));
        }
    }


    public async Task<bool> RemoveUserAsync(string id)
    {
        try
        {
            await _userService.DeleteAsync(id);
            return true;
        }
        catch (Exception ex)
        {
            throw new GraphQLException(new Error($"something went wrong , message: {ex.Message}", "FAILD"));
        }
    }

    public async Task<User> UpdateUserAsync(string id, UserInput userInput)
    {
        try
        {
            return await _userService.UpdateAsync(id, userInput);
        }
        catch (Exception ex)
        {
            throw new GraphQLException(new Error($"something went wrong message: {ex.Message}", "FAILD"));
        }
    }

    public async Task<User> UpdateUserFollowersAsync(string userId, string otherUserId)
    {
        try
        {
            return await _userService.UpdateFollowersAsync(userId, otherUserId);
        }
        catch (Exception ex)
        {
            throw new GraphQLException(new Error($"something went wrong message: {ex.Message}", "FAILD"));
        }
    }

}

