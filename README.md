# CodeChallengeBackend
The Tipple Backend Code Challenge

## Changes to the original project structure

In order to add other projects to the solution, the original project was moved to the directory `CodeChallengeBackend`. As a consequence please `cd` into the directory `CodeChallengeBackend` before executing `dotnet run`:

~~~
C:\TechTest\CodeChallengeBackend>dotnet run
info: Microsoft.Hosting.Lifetime[0]
      Now listening on: https://localhost:5001
info: Microsoft.Hosting.Lifetime[0]
      Now listening on: http://localhost:5000
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: C:\TechTest\CodeChallengeBackend
~~~ 

## Notes

The tests were made to mock the external calls to the Cocktail API. 
This was possible after refactoring the project to use dependency injection of the IHttpClientFactory.
Ideally, this project also needs tests for the mappers and WebApi. 

Another small change was to add a cache from ASP.Net Core for the search by ingredient name method. Ideally, an intermediate layer with a cache for each cocktail id would likely help to increase performance, but I tried to focus on other aspects of the application.
