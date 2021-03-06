﻿using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http;
using System.IO;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.AspNetCore.TestHost;

namespace Testing
{
    public class TestFixture : IDisposable
    {
        protected readonly TestServer _server;
        protected readonly HttpClient _client;

        public TestFixture()
        {
            // To avoid hardcoding path to project,
            // see: https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/testing#integration-testing
            // Testing/bin/Debug/netcoreapp2.0
            var integrationTestsPath = ApplicationEnvironment.ApplicationBasePath;
            var applicationPath = Path.GetFullPath(
                Path.Combine(integrationTestsPath, "../../../../dotnet-sqlserver-example"));

            _server = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<TestStartup>()
                .UseContentRoot(applicationPath)
                .UseEnvironment("Development"));
            _client = _server.CreateClient();
        }

        public void Dispose()
        {
            _client.Dispose();
            _server.Dispose();
        }

        // protected static string AUTHENTICATION_COOKIE = ".AspNetCore.Identity.";
        // protected static string ANTIFORGERY_COOKIE = ".AspNetCore.AntiForgery.";
        // protected static string ANTIFORGERY_TOKEN_FORM = "__RequestVerificationToken";
        // protected static string ANTIFORGERTY_TOKEN_HEADER = "XSRF-TOKEN";
        // protected static Regex AntiforgeryFormFieldRegex = new Regex(@"\<input name=""__RequestVerificationToken"" type=""hidden"" value=""([^""]+)"" \/\>");

        // protected SetCookieHeaderValue _authenticationCookie;
        // protected SetCookieHeaderValue _antiforgeryCookie;
        // protected string _antiforgeryToken;

        // public async Task<string> EnsureAntiforgeryToken()
        // {
        //     if (_antiforgeryToken != null) return _antiforgeryToken;

        //     var response = await _client.GetAsync("/Account/Login");
        //     response.EnsureSuccessStatusCode();
        //     if (response.Headers.TryGetValues("Set-Cookie", out IEnumerable<string> values))
        //     {
        //         _antiforgeryCookie = SetCookieHeaderValue.ParseList(values.ToList()).SingleOrDefault(c => c.Name.StartsWith(ANTIFORGERY_COOKIE, StringComparison.InvariantCultureIgnoreCase));
        //     }
        //     Assert.NotNull(_antiforgeryCookie);
        //     _client.DefaultRequestHeaders.Add("Cookie", new CookieHeaderValue(_antiforgeryCookie.Name, _antiforgeryCookie.Value).ToString());

        //     var responseHtml = await response.Content.ReadAsStringAsync();
        //     var match = AntiforgeryFormFieldRegex.Match(responseHtml);
        //     _antiforgeryToken = match.Success ? match.Groups[1].Captures[0].Value : null;
        //     Assert.NotNull(_antiforgeryToken);

        //     return _antiforgeryToken;
        // }

        // public async Task<Dictionary<string, string>> EnsureAntiforgeryTokenForm(Dictionary<string, string> formData = null)
        // {
        //     if (formData == null) formData = new Dictionary<string, string>();

        //     formData.Add(ANTIFORGERY_TOKEN_FORM, await EnsureAntiforgeryToken());
        //     return formData;
        // }

        // public async Task EnsureAntiforgeryTokenHeader()
        // {
        //     _client.DefaultRequestHeaders.Add(ANTIFORGERTY_TOKEN_HEADER, await EnsureAntiforgeryToken());
        // }

    }
}
