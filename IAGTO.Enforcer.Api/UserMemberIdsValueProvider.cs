using Rsk.Enforcer.PIP;
using Rsk.Enforcer.PolicyModels;

using System.Reflection;

namespace IAGTO.Enforcer.Api;
public class UserMemberIdsValueProvider : RecordAttributeValueProvider<EnforcerMember>
{
    private readonly ILogger<UserMemberIdsValueProvider> _logger;
    private readonly HttpUserClient _userClient;

    public UserMemberIdsValueProvider(ILogger<UserMemberIdsValueProvider> logger, HttpUserClient userClient)
    {
        _logger = logger;
        _userClient = userClient;
        try
        {
            foreach (PropertyInfo property in typeof(EnforcerMember).GetProperties())
            {
                if (property == null)
                {
                    _logger.LogInformation("Property is null");
                }
                else
                {
                    var pava = property.GetCustomAttribute<PolicyAttributeValueAttribute>();
                    _logger.LogDebug($"Record Attribute defined for property {property.Name} with Category {pava?.Category}");
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString(), ex);
        }


        _logger.LogDebug($"Initialising {nameof(UserMemberIdsValueProvider)}");
        _logger.LogDebug($"Supported Categories {string.Join(" ", SupportedCategories)}");
    }

    protected override async Task<EnforcerMember> GetRecordValue(IAttributeResolver attributeResolver, CancellationToken ct)
    {
        try
        {
            _logger.LogDebug("GetRecordValue");

            var members = await _userClient.GetCurrentUserMembers();

            var result = await Task.FromResult(new EnforcerMember());

            var allowedMemberIds = new string[members.Count];
            var count = 0;
            foreach (var member in members)
            {
                allowedMemberIds[count++] = member.MemberId.ToString();
            }

            result.AllowedMemberIds = allowedMemberIds;

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in User Member Ids provider");
            throw;
        }
    }

}
