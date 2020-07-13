# world-zero

An ARG inspired by sf0.org

## Development Notes

### Projects and Solutions

The solution `WorldZero.sln` is the only solution for this project.

This application uses Layered architecture with Hexagonal architecture.

- `Common/` references nothing and is referenced by everything.
- `Data/` references nothing except `Common/` and is referenced by `Service/`.
- `Service/` references `Common/` and `Data/` and is referenced by nothing.

Additionally, the test projects reference things as relevant.

### Testing

`dotnet test` in the root directory will start tests written using NUnit.
However, there are some integration tests that cannot be turned into unit tests
via dependency injection - a prime example of this is making sure that the EF
Core DbContext is correct. This will require being manually started as needed.
To do this, run `dotnet run -p test/ManualIntegration/` from the root directory
to run all manually ran integration tests; this should also print out any
relevant information you will need to know. That said, this test will not need
to be ran frequently unless the models or DbContext are being edited.
