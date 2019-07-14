# Domain vs Application
Nicholas Ventimiglia
___

The domain defines the conceptual model and business rules. 
- Data Structures (State)
- Data Services (Procedures)
- Data Events
- Plugin contracts
- No / Limited async

The application defines the run-time and platform integration.
- Startup
- Plugin implementations
- Platform integration
- Centralized async


The domain should interface with the application through service contracts (e.g. the plugin pattern). These contracts should be strict and limited, as this is technically an inversion of flow. Valid examples include IFileService (interface with platform specific file storage) or IDataService (platform specific data access layer).

In most cases, the application should observe the domain model in a state-full synchronous manner. e.g. The application view can post a change to the domain, and whenever change happens the delta can be observed and the view can be updated in a very structured way.

Views and navigation are part of the application context. Because of this, navigation can get complicated. e.g. I completed a purchase, how do I navigate to the receipt view from my domain service? For this I suggest a few solutions. 

1) Implement a transaction contract, let something in the application context decide what to do.

2) Implement a navigation model, a state machine that a navigation view can observe.