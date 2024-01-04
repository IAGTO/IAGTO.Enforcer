using Rsk.Enforcer.PIP;
using Rsk.Enforcer.PolicyModels;

using System.Reflection;

namespace IAGTO.Enforcer.Api;
public class UserMemberIdsValueProvider : RecordAttributeValueProvider<UserMember>
{
    private readonly ILogger<UserMemberIdsValueProvider> _logger;
    public UserMemberIdsValueProvider(ILogger<UserMemberIdsValueProvider> logger)
    {
        _logger = logger;
        try
        {
            foreach (PropertyInfo property in typeof(UserMember).GetProperties())
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

    protected override async Task<UserMember> GetRecordValue(IAttributeResolver attributeResolver, CancellationToken ct)
    {
        try
        {
            _logger.LogDebug("GetRecordValue");

            var result = await Task.FromResult(new UserMember());
            
            result.AllowedMemberIds = new string[1];
            var i = 0;

            result.AllowedMemberIds[0] = "d4880bdf-9d5d-4c42-adb7-70351274b183";

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in User Member Ids provider");
            throw;
        }
    }

}
