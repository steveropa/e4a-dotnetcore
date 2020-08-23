# Engineering for Agility - C# .NET Core

This project was built with .NET Core 3.1.401 and has been verified to run in a terminal/VIM, with Visual Studio Code (with C# plugin), and with JetBrains Rider 2020.1.

## Installing Dependencies

From the repository root directory, in a terminal, type: `dotnet restore`.

## Running Tests 

For JetBrains rider (what instructors are likely to use), the in-built test runner "just works."

For Visual Studio Code (using the terminal window) and the generally terminal obsessed, you can change directory into any of the projects and use the `dotnet` command:

`$ dotnet test`

If you want to run the actual API through a test web server, say for exploratory testing purposes, the following command will do the trick:

`$ dotnet run --project BonfireEvents.Api`

This should open a browser at the configured API location `https://localhost:5000/event`.

## Navigating the Exercises

Navigating exercies is based on git branches. In the workshop you'll be presented with challenges in the form of user stories. These stories are designed to be completed in order. To get started:

`git checkout origin/start`

Users can switch to a solved state of the story at anytime by checking out the solution cooresponding to the most recently completed story. There are a few use cases where this could come in handy:

- `git checkout origin/04/solved` will checkout the exercies completed <em>through</em> User Story 04. 
- This helps student stay up-to-date with the class's pace and gives them a chance to view how the instructor solved the problem. 
- If they like the instructor's solution better, the student is able to continue on to User Story 05 from that point.
- Instructors may recommend students realign to a known-good state.