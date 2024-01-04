using Rsk.Enforcer;
using Rsk.Enforcer.Remote.Hosting;
using Rsk.Enforcer.PEP;
using Rsk.Enforcer.AspNetCore;
using System.Security.Claims;
using IAGTO.Enforcer.Api;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddEnforcer(this IServiceCollection services, string licensee, string licenseKey)
    {
        services
                .AddEnforcer("IAGTO.Global", options =>
                {
                    options.Licensee = licensee;
                    options.LicenseKey = licenseKey;
                    options.AddFunctionAssembly(typeof(PermissionFunctions).Assembly);
                })
                .AddPolicyEnforcementPoint(o => o.Bias = PepBias.Deny)
                .AddClaimsAttributeValueProvider(o => {
                    o.NonSensitiveClaims = new[] { ClaimTypes.Role };
                })
                .AddDefaultAdviceHandling()
                .AddFileSystemPolicyStore("policies")
                .WithRemoteHosting(options =>
                {
                    options.AuthenticationState = EnforcerAuthenticationState.None;
                    options.BaseUrl = "pdp";
                })
                .AddHttpRequestAttributeValueProvider()
                .AddPolicyAttributeProvider<UserMemberIdsValueProvider>()
                .AddOutcomeActionHandler<AuthorizationFailureOutcomeHandler>();

        return services;
    }
}
