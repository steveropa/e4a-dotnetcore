using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;

namespace BonfireEvents.Api.Tests.Api.Framework
{
  public class ApiTestBase : IDisposable
  {
    protected HttpClient Client;
    private IHost _host;

    public ApiTestBase()
    {
      // Arrange
      var hostBuilder = new HostBuilder()
        .ConfigureWebHost(webHost =>
        {
          // Add TestServer
          webHost.UseTestServer();

          // Specify the environment
          webHost.UseEnvironment("Test");

          // Use our Startup
          webHost.UseStartup<Startup>();
        });

      // Create and start up the host
      _host = hostBuilder.Start();

      // Create an HttpClient which is setup for the test host
      Client = _host.GetTestClient();
    }

    private void ReleaseUnmanagedResources()
    {
      // TODO release unmanaged resources here
    }

    private void Dispose(bool disposing)
    {
      ReleaseUnmanagedResources();
      if (disposing)
      {
        Client?.Dispose();
        _host?.Dispose();
      }
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }
  }
}