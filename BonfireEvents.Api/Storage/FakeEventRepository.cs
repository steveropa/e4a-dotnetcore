using System.Threading.Tasks;
using BonfireEvents.Api.Domain;

namespace BonfireEvents.Api.Storage
{
  /// <summary>
  /// Since we haven't decided on a persistence technology yet, we'll
  /// rely on a fake for exploratory testing purposes (testing through
  /// browser, PostMan, etc. w/ API running).
  /// </summary>
  public class FakeEventRepository : IEventRepository
  {
    public Event Find(int id)
    {
      return new Event("Fake", "Fake");
    }
  }
}