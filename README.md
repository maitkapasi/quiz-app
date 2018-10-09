# quiz-app
Project locations / ports:
--------------------------

- WebApi .net core project - .\additup\WebApi\WebApi - http://localhost:56000/api/
- Angular 6 Website (generated using Angular CLI) - .\additup\WebApiClient\additup-app\additup-ui - http://localhost:4200
- Typescript client library - .\additup\WebApiClient\common

State management:
-----------------

Application state is stored in memory using a static class "State" in the Web API. Ideally it should be persisted in a database so it does not get lost on IIS Restart/App pool recycle. Some of the other options for storing application state without a database were:

- Entity Framework In Memory DB
- Session variables (not recommended)
- Store state in client side and pass them with JWT token. 

I have chosen static class to make it simple. I have also included an API endpoint \api\state\active to view active exercises and sessions for debugging. It is commented out, please uncomment it if required.  

Unit tests:
-----------

I have added various unit tests (Angular website, common library, .Net Core API) to get quality confidence for basic test cases. However, there is still a room to add more test scenarios.

Another interesting approach is to do integration testing by calling APIs using a node based client and write BDD style test cases. I have not included it due to time constraints. 


Swagger file / Models / API documentation:
------------------------------------------
Swagger tools allow to document API and data transfer models definition. It can auto generate models (and APIs) using the provided definitions to make sure client and server models are always in sync. It can also generate documentation. 

File: \WebApi\swagger\swagger.yaml

View documentation: Copy contents of swagger.yaml file and paste it to http://editor.swagger.io/ (Swagger Online Editor).


Known Issue (and solution):
-----------------------------
- Issue: Quiz restarts on browser refresh. 
- Solution: Quiz data can be stored in local web browser storage to prevent this issue. It can be implemented along with redux pattern for client side state management. A user can also be warned of browser refresh using window.onbeforeunload event. 

Note: Folder structure can be improved, however this is just a quick test app. 
