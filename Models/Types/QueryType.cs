using GraphQL_API_X_clone.Models.Queries;

namespace GraphQL_API_X_clone.Models.Types;

public class QueryType : ObjectType<Query>
{

    protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
    {
        descriptor.Field(f => f.GetUserAsync(default)).Name("getUser").Argument("id", a => a.Type<StringType>()).Type<UserType>();

        descriptor.Field(f =>f.GetUsersAsync()).Name("getUsers").Type<ListType<UserType>>();

        descriptor.Field(f => f.GetPostAsync(default)).Name("getPost").Argument("id", a => a.Type<StringType>()).Type<PostType>();

        descriptor.Field(f => f.GetPostsAsync()).Name("getPosts").Type<ListType<PostType>>();    
    }
}

