# Domain vs Application
Nicholas Ventimiglia
___

It is a best practice to split a solution into separate domains. What binds them together is the application which is responsible for startup and other platform specific concerns. 

#### Domain Context
- aka : business logic, domain model
- Declaritive logic (What)
- Conceptual model and rules. 
- Data Structures and Procedures.
- Talks to the application indirectly via contracts.
- No / limited async behaviours.
- Unit Tests.
- Shared Code

#### Application Context
- aka : engine, runtime
- Procedural logic (How)
- Application and platform concerns
- Views, Services, Infrastructure
- Observes the domain
- Async behaviours
- Integration Tests
- Unshared Code

#### Interfaces
It is important that the domain model stays concise and conceptual. It should not dirty itself with integration directly. If the domain  needs to call into the application, it should do so through contracts. Good candidates include rendering (views), file I/O, and networking.

#### Observable
As a better option to interfaces, the application could observe the domain. If domain state changes, the application has the responsibility of updating itself.

#### Example : Navigation
Navigation is an interesting integration point that can overlap both the Domain and Application. The application is concerned because rendering logic is a platform concern. However, the domain might want to know 'which view is currently presented' and/ or begin a navigation request. e.g. On purchase, route to the receipt view.

Here are some recommendations.

- Implement a navigation model, then let the application observe and handle the act of navigation.

- Implement a completion contract, then let the application level handler implement navigation.

- Dont do anything. We are muddling our domain with view specific concerns and procedural logic.