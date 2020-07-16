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
