using IAGTO.Permissions;

namespace IAGTO.Enforcer.Api;

public class HttpUserClient
{
    private readonly HttpClient _httpClient;
    private const string rootUrlPath = "iagto/users";
    private readonly string? _userId;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpUserClient(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;

        _userId = "d3041ef1-d8ab-49ac-9ad4-832fa116e5a3";
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<List<UserMember>> GetCurrentUserMembers()
    {
        //TODO: Don't like how this is done. Probably Permissions API need to be a client and request tokens
        //Or maybe use MassTransit Request/Response to get the data needed

        //var accessTokenTask = _httpContextAccessor.HttpContext?.GetUserAccessTokenAsync();

        //if (accessTokenTask == null)
        //{
        //    throw new UnauthorizedAccessException("Cannot aquire access token");
        //}

        //Task.WaitAll(accessTokenTask);

        //var accessToken = accessTokenTask.Result;

        var response = await _httpClient.GetAsync($"{rootUrlPath}/{_userId}/members");



        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            var deserializedResponse = JsonSerializerExtensions.DeserializeCamelCase<List<UserMember>>(result) ?? new List<UserMember>();
            return deserializedResponse;
        }

        return new List<UserMember>();
    }
}
