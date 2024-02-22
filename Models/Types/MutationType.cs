using GraphQL_API_X_clone.Models.Mutations;

namespace GraphQL_API_X_clone.Models.Types;

public class MutationType : ObjectType<Mutation>
{
    protected override void Configure(IObjectTypeDescriptor<Mutation> descriptor)
    {
        // Posts

        descriptor.Field(f => f.AddPostAsync(default!,default!)).Name("CreatePost");

        descriptor.Field(f => f.AddReplyAsync(default!, default!, default!)).Name("CreateReply");

        descriptor.Field(f => f.UpdatePostAsync(default!, default!)).Name("UpdatePost");

        descriptor.Field(f => f.RemovePostAsync(default!)).Name("DeletePost");

        descriptor.Field(f => f.UpdateLikesAsync(default!, default!)).Name("UpdateLikes");

        // Users

        descriptor.Field(f => f.AddUserAsync(default!)).Name("CreateUser");

        descriptor.Field(f => f.RemoveUserAsync(default!)).Name("DeleteUser");

        descriptor.Field(f => f.UpdateUserAsync(default!,default!)).Name("UpdateUser");

        descriptor.Field(f => f.UpdateUserFollowersAsync(default!, default!)).Name("UpdateUserFollowers");
    }
}

