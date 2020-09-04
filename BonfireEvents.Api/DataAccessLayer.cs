using System;
using BonfireEvents.Api.Models;

namespace BonfireEvents.Api.Controllers
{
  public class DataAccessLayer
  {
    public void Save(CreateEventDto createEventDto)
    {
      throw new DbConnectionTimeoutException();
    }
  }

  public class DbConnectionTimeoutException : Exception
  {
  }
}