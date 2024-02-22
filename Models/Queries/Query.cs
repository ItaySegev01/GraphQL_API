using GraphQL_API_X_clone.Models.Schemas;
using GraphQL_API_X_clone.Services;

namespace GraphQL_API_X_clone.Models.Queries
{
    public partial class Query
    {
        private readonly PostService _postService;
        private readonly UserService _userService;

        public Query(PostService postService, UserService userService)
        {
            _postService = postService;
            _userService = userService;
        }

        public async Task<IEnumerable<Post>> GetPostsAsync()
        {
            try
            {
                return await _postService.GetAsync();
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error($"something went wrong message: {ex.Message}", "FAILD"));
            }
        }

        public async Task<Post> GetPostAsync(string id)
        {
            try
            {
                return await _postService.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error($"something went wrong message: {ex.Message}", "FAILD"));
            }
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            try
            {
                return await _userService.GetAsync();
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }

        }

        public async Task<User> GetUserAsync(string id)
        {
            try
            {
                var user = await _userService.GetByIdAsync(id);
                if (user == null)
                {
                    throw new GraphQLException(new Error("this User is Null"));
                }
                return user;
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }
    }
}
