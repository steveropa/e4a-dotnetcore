using System;
using System.Collections.Generic;

namespace BonfireEvents.Api.Exceptions
{
  public class CreateEventException : Exception
  {
    public CreateEventException()
    {
      this.ValidationErrors = new List<string>();
    }
    
    public List<string> ValidationErrors { get; }
  }
}