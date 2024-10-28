using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;

namespace Audit.Api.Tests;

public class TestWebApplication : WebApplicationFactory<MockStartup>
{
    protected override IHostBuilder CreateHostBuilder()
        => Host
            .CreateDefaultBuilder()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<MockStartup>();
            });
}
