using BonfireEvents.Api.Storage;
using Xunit;

namespace BonfireEvents.Api.Tests.Api.Framework
{
  public class FakeRepositoryTests
  {
    [Fact]
    public void Add_a_new_event_to_FakeEventRepository()
    {
      FakeEventRepository subject = new FakeEventRepository();
      var anEvent = new BonfireEvents.Api.Domain.Event(title:"a", description:"b");

      subject.Save(anEvent);
      
      Assert.NotEqual(0, anEvent.Id);
    }
    
    [Fact]
    public void Retrieve_events()
    {
      var subject = new FakeEventRepository();
      var anEvent = new BonfireEvents.Api.Domain.Event(title:"a", description:"b");
      subject.Save(anEvent);

      var retrievedEvent = subject.Find(anEvent.Id);
      
      Assert.Equal(anEvent,retrievedEvent);
    } 
  }
}