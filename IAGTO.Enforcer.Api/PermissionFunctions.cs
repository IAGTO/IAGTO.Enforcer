using Rsk.Enforcer.PAP.Functions;

namespace IAGTO.Enforcer.Api;

public class PermissionFunctions
{
    [PolicyFunction("com.iagto:permission-key")]
    public static string PermissionKey(IReadOnlyCollection<string> resourceTypes, IReadOnlyCollection<string> actions, IReadOnlyCollection<string> permissions)
    {
        var resourceType = resourceTypes.First();
        var action = actions.FirstOrDefault();

        //TODO: Remove this
        if (permissions != null && permissions.Any())
        {
            Console.WriteLine($"Test Data {Environment.NewLine} {string.Join(" ", permissions)}");
        }

        return $"{resourceType}:{action}".ToLower();
    }
}
