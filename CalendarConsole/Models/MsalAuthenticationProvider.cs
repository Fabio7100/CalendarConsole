using System.Net.Http.Headers;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using Microsoft.Graph.Core;


namespace CalendarConsole.Models;

public class MsalAuthenticationProvider : IAuthenticationProvider
{
    private IConfidentialClientApplication _clientApplication;
    private string[] _scopes;
    private IAuthenticationProvider _authenticationProviderImplementation;

    public MsalAuthenticationProvider(IConfidentialClientApplication clientApplication, string[] scopes)
    {
        _clientApplication = clientApplication;
        _scopes = scopes;
    }


    public async Task AuthenticateRequestAsync(HttpRequestMessage request)
    {
        var token = await GetTokenAsync();
        request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);
    }

    public async Task<string> GetTokenAsync()
    {
        AuthenticationResult authResult = null;
        authResult = await _clientApplication.AcquireTokenForClient(_scopes).ExecuteAsync();
        return authResult.AccessToken;
    }
    
   
}