# Domain vs Application
Nicholas Ventimiglia
___

The domain defines the conceptual model and business rules. 
- Data structures
- Data Events
- Procedures
- Plugin contracts
- No / Limited async

The application defines the run-time and platform integration.
- Startup
- Plugin implementations
- Platform integration
- Centralized async


The domain should interface with the application through service contracts (e.g. plugin pattern). These contracts should be strict and limited, as this is technically an inversion of flow. Valid examples include IFileService (interface with platform specific file storage) or IDataService (platform specific data access layer).

In most cases, the application should observe the domain model in a state-full synchronous manner. e.g. The application view can post a change to the domain, and whenever change happens the delta can be observed and the view can be updated in a very structured way.

Views and navigation are part of the application context. Because of this, navigation can get complicated. e.g. I completed a purchase, how do I navigate to the receipt view from my domain service? For this I suggest 2 solutions. 

1) Dont, just let the owner view of the transaction decide what to do from within the application layer. 

2) Implement a navigation model, a glorified state machine that a navigation view controller can observe.