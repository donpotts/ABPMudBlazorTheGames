using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Web;
using TheGames.Shared.Blazor.Authentication;
using TheGames.Shared.Blazor.Models;
using TheGames.Shared.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TheGames.Shared.Blazor.Services;

public class AppService
{
    private readonly HttpClient httpClient;
    private readonly JwtAuthenticationStateProvider authenticationStateProvider;

    public AppService(
        HttpClient httpClient,
        AuthenticationStateProvider authenticationStateProvider)
    {
        this.httpClient = httpClient;
        this.authenticationStateProvider
            = authenticationStateProvider as JwtAuthenticationStateProvider
                ?? throw new InvalidOperationException();
    }

    private static async Task HandleResponseErrorsAsync(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode
            && response.StatusCode != HttpStatusCode.Unauthorized
            && response.StatusCode != HttpStatusCode.NotFound)
        {
            string? message = await response.Content.ReadFromJsonAsync<string>();
            throw new Exception(message);
        }

        response.EnsureSuccessStatusCode();
    }

    public async Task RegisterUserAsync(User registerModel)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Post, "api/identity/users");
        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Content = JsonContent.Create(registerModel);

        HttpResponseMessage response = await httpClient.SendAsync(request);// ("/api/Users", registerModel);

        await HandleResponseErrorsAsync(response);
    }

    public async Task<PagedResultDto<User>> ListUsersAsync(string orderby, int skip, int? top)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        Uri uri = new(httpClient.BaseAddress, "api/identity/users");
        uri = GetUri(uri, top, skip, orderby);

        HttpRequestMessage request = new(HttpMethod.Get, uri);
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<PagedResultDto<User>>();
    }

    public async Task<User?> GetUserByIdAsync(string id)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Get, $"api/identity/users/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<User>();
    }

    public async Task UpdateUserAsync(string id, User data)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Put, $"api/identity/users/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Content = JsonContent.Create(data);

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);
    }

    public async Task DeleteUserAsync(string id)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Delete, $"api/identity/users/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);
    }

    public async Task<PagedResultDto<Publisher>> ListPublishersAsync(string orderby, int skip, int? top)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        Uri uri = new(httpClient.BaseAddress, "api/app/publisher");
        uri = GetUri(uri, top, skip, orderby);

        HttpRequestMessage request = new(HttpMethod.Get, uri);
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<PagedResultDto<Publisher>>();
    }

    public async Task<Publisher?> GetPublisherByIdAsync(long id)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Get, $"api/app/publisher/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<Publisher>();
    }

    public async Task UpdatePublisherAsync(long id, Publisher data)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Put, $"api/app/publisher/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Content = JsonContent.Create(data);

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);
    }

    public async Task<Publisher?> InsertPublisherAsync(Publisher data)
    {
        data.Id = 0;
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Post, "api/app/publisher");
        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Content = JsonContent.Create(data);

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<Publisher>();
    }

    public async Task DeletePublisherAsync(long id)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Delete, $"api/app/publisher/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);
    }

    public async Task<PagedResultDto<Game>> ListGamesAsync(string orderby, int skip, int? top)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        Uri uri = new(httpClient.BaseAddress, "api/app/game");
        uri = GetUri(uri, top, skip, orderby);

        HttpRequestMessage request = new(HttpMethod.Get, uri);
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<PagedResultDto<Game>>();
    }

    public async Task<Game?> GetGameByIdAsync(long id)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Get, $"api/app/game/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<Game>();
    }

    public async Task UpdateGameAsync(long id, Game data)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Put, $"api/app/game/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Content = JsonContent.Create(data);

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);
    }

    public async Task<Game?> InsertGameAsync(Game data)
    {
        data.Id = 0;
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Post, "api/app/Game");
        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Content = JsonContent.Create(data);

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<Game>();
    }

    public async Task DeleteGameAsync(long id)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Delete, $"api/app/game/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);
    }

    public Uri GetUri(Uri uri, int? top = null, int? skip = null, string orderby = null)
    {
        UriBuilder uriBuilder = new UriBuilder(uri);
        NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(uriBuilder.Query);

        if (top.HasValue)
        {
            nameValueCollection["MaxResultCount"] = $"{top}";
        }

        if (skip.HasValue)
        {
            nameValueCollection["SkipCount"] = $"{skip}";
        }

        if (!string.IsNullOrEmpty(orderby))
        {
            nameValueCollection["Sorting"] = orderby ?? "";
        }

        uriBuilder.Query = nameValueCollection.ToString();
        return uriBuilder.Uri;
    }
}


//public class AppService(
//    HttpClient httpClient,
//    AuthenticationStateProvider authenticationStateProvider)
//{
//    private readonly IdentityAuthenticationStateProvider authenticationStateProvider
//            = authenticationStateProvider as IdentityAuthenticationStateProvider
//                ?? throw new InvalidOperationException();

//    private static async Task HandleResponseErrorsAsync(HttpResponseMessage response)
//    {
//        if (!response.IsSuccessStatusCode
//            && response.StatusCode != HttpStatusCode.Unauthorized
//            && response.StatusCode != HttpStatusCode.NotFound)
//        {
//            var message = await response.Content.ReadAsStringAsync();
//            throw new Exception(message);
//        }

//        response.EnsureSuccessStatusCode();
//    }

//    public class ODataResult<T>
//    {
//        [JsonPropertyName("@odata.count")]
//        public int? Count { get; set; }

//        public IEnumerable<T>? Value { get; set; }
//    }

//    public async Task<ODataResult<T>?> GetODataAsync<T>(
//            string entity,
//            int? top = null,
//            int? skip = null,
//            string? orderby = null,
//            string? filter = null,
//            bool count = false,
//            string? expand = null)
//    {
//        var token = await authenticationStateProvider.GetBearerTokenAsync()
//            ?? throw new Exception("Not authorized");

//        var queryString = HttpUtility.ParseQueryString(string.Empty);

//        if (top.HasValue)
//        {
//            queryString.Add("$top", top.ToString());
//        }

//        if (skip.HasValue)
//        {
//            queryString.Add("$skip", skip.ToString());
//        }

//        if (!string.IsNullOrEmpty(orderby))
//        {
//            queryString.Add("$orderby", orderby);
//        }

//        if (!string.IsNullOrEmpty(filter))
//        {
//            queryString.Add("$filter", filter);
//        }

//        if (count)
//        {
//            queryString.Add("$count", "true");
//        }

//        if (!string.IsNullOrEmpty(expand))
//        {
//            queryString.Add("$expand", expand);
//        }

//        var uri = $"/odata/{entity}?{queryString}";

//        HttpRequestMessage request = new(HttpMethod.Get, uri);
//        request.Headers.Authorization = new("Bearer", token);

//        var response = await httpClient.SendAsync(request);

//        await HandleResponseErrorsAsync(response);

//        return await response.Content.ReadFromJsonAsync<ODataResult<T>>();
//    }


//    public async Task<Dictionary<string, List<string>>> RegisterUserAsync(RegisterModel registerModel)
//    {
//        var response = await httpClient.PostAsJsonAsync(
//            "/identity/register",
//            new { registerModel.Email, registerModel.Password });

//        if (response.StatusCode == HttpStatusCode.BadRequest)
//        {
//            var json = await response.Content.ReadAsStringAsync();

//            var problemDetails = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();

//            return problemDetails?.Errors != null
//                ? problemDetails.Errors
//                : throw new Exception("Bad Request");
//        }

//        response.EnsureSuccessStatusCode();

//        response = await httpClient.PostAsJsonAsync(
//            "/identity/login",
//            new { registerModel.Email, registerModel.Password });

//        response.EnsureSuccessStatusCode();

//        var accessTokenResponse = await response.Content.ReadFromJsonAsync<AccessTokenResponse>()
//            ?? throw new Exception("Failed to authenticate");

//        HttpRequestMessage request = new(HttpMethod.Put, "/api/user/@me");
//        request.Headers.Authorization = new("Bearer", accessTokenResponse.AccessToken);
//        request.Content = JsonContent.Create(new UpdateApplicationUserDto
//        {
//            FirstName = registerModel.FirstName,
//            LastName = registerModel.LastName,
//            Title = registerModel.Title,
//            CompanyName = registerModel.CompanyName,
//            Photo = registerModel.Photo,
//        });

//        response = await httpClient.SendAsync(request);

//        response.EnsureSuccessStatusCode();

//        return [];
//    }

//    public async Task<ApplicationUserDto[]?> ListUserAsync()
//    {
//        var token = await authenticationStateProvider.GetBearerTokenAsync()
//            ?? throw new Exception("Not authorized");

//        HttpRequestMessage request = new(HttpMethod.Get, "/api/user");
//        request.Headers.Authorization = new("Bearer", token);

//        var response = await httpClient.SendAsync(request);

//        await HandleResponseErrorsAsync(response);

//        return await response.Content.ReadFromJsonAsync<ApplicationUserDto[]>();
//    }

//    public Task<ODataResult<ApplicationUserDto>?> ListUserODataAsync(
//        int? top = null,
//        int? skip = null,
//        string? orderby = null,
//        string? filter = null,
//        bool count = false,
//        string? expand = null)
//    {
//        return GetODataAsync<ApplicationUserDto>("User", top, skip, orderby, filter, count, expand);
//    }

//    public async Task<ApplicationUserWithRolesDto?> GetUserByIdAsync(string id)
//    {
//        var token = await authenticationStateProvider.GetBearerTokenAsync()
//            ?? throw new Exception("Not authorized");

//        HttpRequestMessage request = new(HttpMethod.Get, $"/api/user/{id}");
//        request.Headers.Add("Authorization", $"Bearer {token}");

//        var response = await httpClient.SendAsync(request);

//        await HandleResponseErrorsAsync(response);

//        return await response.Content.ReadFromJsonAsync<ApplicationUserWithRolesDto>();
//    }

//    public async Task UpdateUserAsync(string id, UpdateApplicationUserDto data)
//    {
//        var token = await authenticationStateProvider.GetBearerTokenAsync()
//            ?? throw new Exception("Not authorized");

//        HttpRequestMessage request = new(HttpMethod.Put, $"/api/user/{id}");
//        request.Headers.Add("Authorization", $"Bearer {token}");
//        request.Content = JsonContent.Create(data);

//        var response = await httpClient.SendAsync(request);

//        await HandleResponseErrorsAsync(response);
//    }

//    public async Task DeleteUserAsync(string id)
//    {
//        var token = await authenticationStateProvider.GetBearerTokenAsync()
//            ?? throw new Exception("Not authorized");

//        HttpRequestMessage request = new(HttpMethod.Delete, $"/api/user/{id}");
//        request.Headers.Add("Authorization", $"Bearer {token}");

//        var response = await httpClient.SendAsync(request);

//        await HandleResponseErrorsAsync(response);
//    }

//    public async Task<Game[]?> ListGameAsync()
//    {
//        var token = await authenticationStateProvider.GetBearerTokenAsync()
//            ?? throw new Exception("Not authorized");

//        HttpRequestMessage request = new(HttpMethod.Get, "/api/game");
//        request.Headers.Authorization = new("Bearer", token);

//        var response = await httpClient.SendAsync(request);

//        await HandleResponseErrorsAsync(response);

//        return await response.Content.ReadFromJsonAsync<Game[]>();
//    }

//    public Task<ODataResult<Game>?> ListGameODataAsync(
//        int? top = null,
//        int? skip = null,
//        string? orderby = null,
//        string? filter = null,
//        bool count = false,
//        string? expand = null)
//    {
//        return GetODataAsync<Game>("Game", top, skip, orderby, filter, count, expand);
//    }

//    public async Task<Game?> GetGameByIdAsync(long key)
//    {
//        var token = await authenticationStateProvider.GetBearerTokenAsync()
//            ?? throw new Exception("Not authorized");

//        HttpRequestMessage request = new(HttpMethod.Get, $"/api/game/{key}");
//        request.Headers.Authorization = new("Bearer", token);

//        var response = await httpClient.SendAsync(request);

//        await HandleResponseErrorsAsync(response);

//        return await response.Content.ReadFromJsonAsync<Game>();
//    }

//    public async Task UpdateGameAsync(long key, Game data)
//    {
//        var token = await authenticationStateProvider.GetBearerTokenAsync()
//            ?? throw new Exception("Not authorized");

//        HttpRequestMessage request = new(HttpMethod.Put, $"/api/game/{key}");
//        request.Headers.Authorization = new("Bearer", token);
//        request.Content = JsonContent.Create(data);

//        var response = await httpClient.SendAsync(request);

//        await HandleResponseErrorsAsync(response);
//    }

//    public async Task<Game?> InsertGameAsync(Game data)
//    {
//        var token = await authenticationStateProvider.GetBearerTokenAsync()
//            ?? throw new Exception("Not authorized");

//        HttpRequestMessage request = new(HttpMethod.Post, "/api/game");
//        request.Headers.Authorization = new("Bearer", token);
//        request.Content = JsonContent.Create(data);

//        var response = await httpClient.SendAsync(request);

//        await HandleResponseErrorsAsync(response);

//        return await response.Content.ReadFromJsonAsync<Game>();
//    }

//    public async Task DeleteGameAsync(long key)
//    {
//        var token = await authenticationStateProvider.GetBearerTokenAsync()
//            ?? throw new Exception("Not authorized");

//        HttpRequestMessage request = new(HttpMethod.Delete, $"/api/game/{key}");
//        request.Headers.Authorization = new("Bearer", token);

//        var response = await httpClient.SendAsync(request);

//        await HandleResponseErrorsAsync(response);
//    }

//    public async Task<Publisher[]?> ListPublisherAsync()
//    {
//        var token = await authenticationStateProvider.GetBearerTokenAsync()
//            ?? throw new Exception("Not authorized");

//        HttpRequestMessage request = new(HttpMethod.Get, "/api/publisher");
//        request.Headers.Authorization = new("Bearer", token);

//        var response = await httpClient.SendAsync(request);

//        await HandleResponseErrorsAsync(response);

//        return await response.Content.ReadFromJsonAsync<Publisher[]>();
//    }

//    public Task<ODataResult<Publisher>?> ListPublisherODataAsync(
//        int? top = null,
//        int? skip = null,
//        string? orderby = null,
//        string? filter = null,
//        bool count = false,
//        string? expand = null)
//    {
//        return GetODataAsync<Publisher>("Publisher", top, skip, orderby, filter, count, expand);
//    }

//    public async Task<Publisher?> GetPublisherByIdAsync(long key)
//    {
//        var token = await authenticationStateProvider.GetBearerTokenAsync()
//            ?? throw new Exception("Not authorized");

//        HttpRequestMessage request = new(HttpMethod.Get, $"/api/publisher/{key}");
//        request.Headers.Authorization = new("Bearer", token);

//        var response = await httpClient.SendAsync(request);

//        await HandleResponseErrorsAsync(response);

//        return await response.Content.ReadFromJsonAsync<Publisher>();
//    }

//    public async Task UpdatePublisherAsync(long key, Publisher data)
//    {
//        var token = await authenticationStateProvider.GetBearerTokenAsync()
//            ?? throw new Exception("Not authorized");

//        HttpRequestMessage request = new(HttpMethod.Put, $"/api/publisher/{key}");
//        request.Headers.Authorization = new("Bearer", token);
//        request.Content = JsonContent.Create(data);

//        var response = await httpClient.SendAsync(request);

//        await HandleResponseErrorsAsync(response);
//    }

//    public async Task<Publisher?> InsertPublisherAsync(Publisher data)
//    {
//        var token = await authenticationStateProvider.GetBearerTokenAsync()
//            ?? throw new Exception("Not authorized");

//        HttpRequestMessage request = new(HttpMethod.Post, "/api/publisher");
//        request.Headers.Authorization = new("Bearer", token);
//        request.Content = JsonContent.Create(data);

//        var response = await httpClient.SendAsync(request);

//        await HandleResponseErrorsAsync(response);

//        return await response.Content.ReadFromJsonAsync<Publisher>();
//    }

//    public async Task DeletePublisherAsync(long key)
//    {
//        var token = await authenticationStateProvider.GetBearerTokenAsync()
//            ?? throw new Exception("Not authorized");

//        HttpRequestMessage request = new(HttpMethod.Delete, $"/api/publisher/{key}");
//        request.Headers.Authorization = new("Bearer", token);

//        var response = await httpClient.SendAsync(request);

//        await HandleResponseErrorsAsync(response);
//    }

//    public async Task<string?> UploadImageAsync(Stream stream, int bufferSize, string contentType)
//    {
//        var token = await authenticationStateProvider.GetBearerTokenAsync()
//            ?? throw new Exception("Not authorized");

//        MultipartFormDataContent content = [];
//        StreamContent fileContent = new(stream, bufferSize);
//        fileContent.Headers.ContentType = new(contentType);
//        content.Add(fileContent, "image", "image");

//        HttpRequestMessage request = new(HttpMethod.Post, $"/api/image");
//        request.Headers.Add("Authorization", $"Bearer {token}");
//        request.Content = content;

//        var response = await httpClient.SendAsync(request);

//        await HandleResponseErrorsAsync(response);

//        return await response.Content.ReadFromJsonAsync<string>();
//    }

//    public async Task<string?> UploadImageAsync(IBrowserFile image)
//    {
//        using var stream = image.OpenReadStream(image.Size);

//        return await UploadImageAsync(stream, Convert.ToInt32(image.Size), image.ContentType);
//    }

//    public async Task ChangePasswordAsync(string oldPassword, string newPassword)
//    {
//        var token = await authenticationStateProvider.GetBearerTokenAsync()
//            ?? throw new Exception("Not authorized");

//        HttpRequestMessage request = new(HttpMethod.Post, $"/identity/manage/info");
//        request.Headers.Authorization = new("Bearer", token);
//        request.Content = JsonContent.Create(new { oldPassword, newPassword });

//        var response = await httpClient.SendAsync(request);

//        await HandleResponseErrorsAsync(response);
//    }

//    public async Task ModifyRolesAsync(string key, IEnumerable<string> roles)
//    {
//        var token = await authenticationStateProvider.GetBearerTokenAsync()
//            ?? throw new Exception("Not authorized");

//        HttpRequestMessage request = new(HttpMethod.Put, $"/api/user/{key}/roles");
//        request.Headers.Authorization = new("Bearer", token);
//        request.Content = JsonContent.Create(roles);

//        var response = await httpClient.SendAsync(request);

//        await HandleResponseErrorsAsync(response);
//    }
//}
