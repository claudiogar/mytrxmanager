# Transaction Manager

The transaction manager is an application that can be used to manage transactions directly from a web page.
The solution consists of the following components:
 - a React + Bootstrap front-end layer (published on a NGINX server when executed in Docker)
 - a .Net 5.0 WebApi app that exposes a REST API to manage transactions
   - EF Core with code-first approach is used to interact with the DB
 - a PostgresDB instance to persist the records
 - a PGAdmin instance to navigate the content of the DB (for troubleshooting purposes)
 
 
## How to run the solution (the Docker-compose way)
The whole solution can be built and executed via Docker Compose:

`docker compose build`

`docker compose up -d`

Once started and up and running, the application can be accessed on port `80`.

Since my development machine is ARM-based (Mac M1 processor), I had to use "non-standard" images (and this is the reason why I did not choose MS SQL Server as DBMS).
Thus, the base images within the Docker files may need to be adapted to your target architecture.


## How to run the solution (the Visual Studio way)
This solution can also be executed also locally and without a DBMS installed. When launched in Development mode, the application falls back on a local SQLite database.
`Prerequisites: you need .Net 5.0 SDK, VisualStudio 2019, Node + NPM installed in order to run the solution directly on your machine.`

### Step 1: launch the WebAPI
Open the `/app.sln` VS solution, compile and run it: the REST API will be exposed on port `1234`.

`Note: when executed in development mode, a Swagger documentation page can be accessed on the same port`

## Step 2: run the front-end
Open a console window and, from the solution root folder, navigate to `/frontend/vontobel-ui`.
Execute `npm build`, followed by `npm run start`

At this point you you should be able to access the application by navigating to `http://localhost:3000`

