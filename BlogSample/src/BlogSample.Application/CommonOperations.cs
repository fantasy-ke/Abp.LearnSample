using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace BlogSample;

public class CommonOperations
{
    public static OperationAuthorizationRequirement Update = new() { Name = nameof(Update) };
    public static OperationAuthorizationRequirement Delete = new() { Name = nameof(Delete) };
}