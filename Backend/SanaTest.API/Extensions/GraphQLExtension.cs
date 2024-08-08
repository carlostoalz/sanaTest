namespace SanaTest.API
{
    internal static class GraphQLExtension
    {
        internal static void AddUserGraphQL(this IServiceCollection services) => services.AddGraphQLServer().AddQueryType<ProductQuery>();
        internal static void UseGraphQL(this WebApplication app) => app.MapGraphQL("/graphql");
    }
}
