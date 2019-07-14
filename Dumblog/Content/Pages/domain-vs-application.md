# Domain vs Application
Nicholas Ventimiglia
___

A solution is comprised of two contexts, the domain context and the application context.

#### Domain Context
- Conceptual model and business rules. 
- Data Structures and Procedures
- Talks to the application via contracts
- No / Limited async
- Unit Tests
- 
#### Application Context
- Run-time implementation
- Views, Services, Infrastructure
- Observes the domain
- Async concerns
- Integration Tests

#### Interfacing
If the domain needs to call into the application, it should do so through contracts (e.g. the plugin pattern). Examples include IFileService (interface with platform specific file storage) or IDataService (platform specific data access layer).

#### Observable
The application is observable by the application. If domain state changes, the application has the responsibility of updating itself.

#### Navigation
Navigation is an interesting integration point that can overlap both Domain and Application. Often the domain needs to know context of 'where it is at', yet navigation is generally a platform specific and async activity.

e.g. I make a purchase into my purchase controller, how do I navigate to the receipt view on completion? Here are some recommendations.

- Implement a completion contract, then let the application implementation decide what to do. 

- Implement a model with a navigation state, then let the application observe and transition internally.