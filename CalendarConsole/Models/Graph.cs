using System;
using System.Collections.Generic;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using Microsoft.Graph.Core;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using CalendarConsole.Models;



namespace CalendarConsole.Models
{
    public class Graph
    {
        private static GraphServiceClient _graphClient;
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _tenantId;
        
        public Graph(string clientId,string clientSecret,string tenantId)
        {

            _clientId = clientId;
            _clientSecret = clientSecret;
            _tenantId = tenantId;
        }

        private IAuthenticationProvider CreateAuthorizationProvider()
        {
            
            var redirectUri = "https://localhost";
            var authority = $"https://login.microsoftonline.com/{_tenantId}/v2.0";

            List<string> scopes = new List<string>();
            scopes.Add("https://graph.microsoft.com/.default");

            var cca = ConfidentialClientApplicationBuilder.Create(_clientId)
                .WithAuthority(authority)
                .WithRedirectUri(redirectUri)
                .WithClientSecret(_clientSecret)
                .Build();
            return new MsalAuthenticationProvider(cca, scopes.ToArray());
        }
        public GraphServiceClient GetAuthenticatedGraphClient()
        {
            var authenticationProvider = CreateAuthorizationProvider();
            _graphClient = new GraphServiceClient(authenticationProvider);
            return _graphClient;
            
        }
    }
    
    
}