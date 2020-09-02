using BonfireEvents.Api.Adapters;
using BonfireEvents.Api.Domain;
using BonfireEvents.Api.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace BonfireEvents.Api
{
  static internal class Dependencies
  {
    public static void RegisterDependencies(IServiceCollection services)
    {
      services.AddScoped<IAuthenticationAdapter, FakeAuthenticationAdapter>();
      services.AddScoped<IOrganizersAdapter, FakeOrganizersAdapter>();
      services.AddScoped<ICreateEventCommand, CreateEventCommand>();
      services.AddScoped<IEventRepository, FakeEventRepository>();
      services.AddScoped<IEventListingAdapter, FakeEventListingAdapter>();
    }
  }
}