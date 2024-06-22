using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http.Json;
using System.Security.Principal;
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

    public async Task AccountRegisterAsync(string userName, string emailAddress, string password, string appName)
    {
        HttpRequestMessage request = new(HttpMethod.Post, "api/account/register");
        request.Content = JsonContent.Create(new { userName, emailAddress, password, appName });
        HttpResponseMessage response = await httpClient.SendAsync(request);
        await HandleResponseErrorsAsync(response);
    }

    public async Task ChangePasswordAsync(string userName, string currentPassword, string newPassword)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Post, $"api/account/my-profile/change-password");
        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Content = JsonContent.Create(new {currentPassword, newPassword });

        var response = await httpClient.SendAsync(request);

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

    public async Task<RoleItemsDto<RoleItems>> GetRolesByUserIdAsync(string id)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Get, $"api/identity/users/{id}/roles");
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<RoleItemsDto<RoleItems>>();
    }

    public async Task<RoleItems> GetRoleByIdAsync(string id)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Get, $"api/identity/roles/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<RoleItems>();
    }

    public async Task<RoleItemsDto<RoleItems>> GetAllRolesAsync()
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Get, $"api/identity/roles/all");
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<RoleItemsDto<RoleItems>>();
    }

    public async Task<RoleItemsDto<RoleItems>> ListRolesAsync(string orderby, int skip, int? top)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        Uri uri = new(httpClient.BaseAddress, "api/identity/roles");
        uri = GetUri(uri, top, skip, orderby);

        HttpRequestMessage request = new(HttpMethod.Get, uri);
        request.Headers.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);

        return await response.Content.ReadFromJsonAsync<RoleItemsDto<RoleItems>>();
    }

    public class RoleNames
    {
        public List<string> roleNames { get; set; }
    }

    public async Task ModifyRolesAsync(string key, IEnumerable<string> roleNames)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Put, $"api/identity/users/{key}/roles");
        request.Headers.Add("Authorization", $"Bearer {token}");

        RoleNames roles = new RoleNames
        {
            roleNames = roleNames.ToList()
        };

        request.Content = JsonContent.Create(roles);

        var response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);
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

    public async Task AddRoleAsync(RoleItems roleModel)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Post, "api/identity/roles");
        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Content = JsonContent.Create(roleModel);

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);
    }

    public async Task UpdateRoleAsync(string id, RoleItems data)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Put, $"api/identity/roles/{id}");
        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Content = JsonContent.Create(data);

        HttpResponseMessage response = await httpClient.SendAsync(request);

        await HandleResponseErrorsAsync(response);
    }

    public async Task DeleteRoleAsync(string id)
    {
        string token = authenticationStateProvider.Token
            ?? throw new Exception("Not authorized");

        HttpRequestMessage request = new(HttpMethod.Delete, $"api/identity/roles/{id}");
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
