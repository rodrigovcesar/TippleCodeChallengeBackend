# CodeChallengeBackend
The Tipple Backend Code Challenge

## Changes to the original project structure

In order to add other projects to the solution, the original project was moved to the directory `CodeChallengeBackend`. As a consequence please `cd` into the directory before executing `dotnet run`:

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

## Obeservations

The tests were made to mock the external calls the cocktail api. 
This was possible after refactoring the project to use depency injection of the IHttpClient
Ideally, this project also needs test for the mappers and WebApi. 