using Rsk.Enforcer.PIP;
using Rsk.Enforcer.PolicyModels;

namespace IAGTO.Enforcer.Api;

public class EnforcerMember
{
    [PolicyAttributeValue(PolicyAttributeCategories.Resource, "iagto:allowedMemberIds")]
    public string[] AllowedMemberIds { get; set; } = null!;
}

