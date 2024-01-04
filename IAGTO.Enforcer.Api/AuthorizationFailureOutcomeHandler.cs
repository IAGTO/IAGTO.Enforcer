using Rsk.Enforcer.PEP;
using Rsk.Enforcer.PolicyModels;
using Rsk.Enforcer.Services.Logging;

using System.Text.Json;

namespace IAGTO.Enforcer.Api;

public class AuthorizationFailureOutcomeHandler : OutcomeActionHandler
{
    public override string Name => "iagto:AuthorizationFailureOutcomeAction";

    public override Task Execute(IEnumerable<PolicyAttributeValue> parameters, IEnforcerLogger evaluationLogger)
    {
        foreach (var param in parameters)
        {
            evaluationLogger.LogInformation($"{param.Category} {JsonSerializer.Serialize(param.GetValue<object>())} {param.Type}");
        }

        return Task.CompletedTask;
    }
}
