using GraphQL_API_X_clone.Models.Schemas;

namespace GraphQL_API_X_clone.Models.Types;

public class UserType : ObjectType<User>
{
    protected override void Configure(IObjectTypeDescriptor<User> descriptor)
    {
        descriptor.Field(f => f.Id).Type<StringType>();

        descriptor.Field(f => f.UserName).Type<StringType>();

        descriptor.Field(f => f.Image).Type<StringType>();

        descriptor.Field(f => f.Description).Type<StringType>();

        descriptor.Field(f => f.CreatedAt).Type<DateTimeType>();

        descriptor.Field(f => f.userIds).Type<ListType<StringType>>().Name("followers");
    }
}

