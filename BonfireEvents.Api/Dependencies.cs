using BonfireEvents.Api.Adapters;
using BonfireEvents.Api.Storage;
using BonfireEvents.Domain.Event;
using BonfireEvents.Domain.Event.Adapters;
using BonfireEvents.Domain.Event.Commands;
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