using GraphQL_API_X_clone.Models.Schemas;

namespace GraphQL_API_X_clone.Models.Types;

public class PostType : ObjectType<Post>
{
    protected override void Configure(IObjectTypeDescriptor<Post> descriptor)
    {
        descriptor.Field(f => f.Id).Type<StringType>();

        descriptor.Field(f => f.Content).Type<StringType>();

        descriptor.Field(f => f.Media).Type<StringType>();

        descriptor.Field(f => f.userId).Type<StringType>();

        descriptor.Field(f => f.userIds).Type<ListType<StringType>>().Name("likes");

        descriptor.Field(f => f.postsIds).Type<ListType<StringType>>().Name("replies"); 

        descriptor.Field(f => f.CreatedAt).Type<DateTimeType>();    
    }
}

