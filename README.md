# ObjectManagerBackend
This is the backend (API with .net core 6) for the object manager application.

## Architecture
This application uses a clean architecture (hexagonal). There are mainly three layers:
- Domain: It is the main layer and contains all the business logic. It also contains the models, custom domain exceptions, constans and some contracts like the repositories contracts. However, this layer is persistence agnostic because it only define the contracts but not the implementation. In fact, this layer does not depend on any technology or framework.
- Application: This layer does the choreography or orchestration for the use cases. It depends on domain layer and have access to it. This layer is responsible to persist the data because it has access to the repositories contracts but it is agnostic os persistance technology.
- Infrastructure: This layer is the more external layer and implements the technologies and external services like the persistence or the REST Api. Obviously this layer can depends on frameworks and ORMs.

## Projects
- Domain: To implement the domain layer.
- Application: To implement the application layer.
- Infrastructure: To implement the infrastructure layer.
- API: To expose a REST Api with ASP.Net API.
- Unit tests: To test the app components isolated.
- Integration tests: To tests the app components working together.