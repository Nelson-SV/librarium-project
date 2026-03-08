Prerequisites to run the application:
- .NET 10 SDK
- SQL Server (or Docker, see below)
- EF Core CLI tools: dotnet tool install --global dotnet-ef


Database Setup:
The project uses SQL Server. You can run it locally via the docker-compose file or directly with the following command:
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=SuperSecret7!" -p 1450:1433 -d mcr.microsoft.com/mssql/server:2022-latest
(Note: docker-compose file is made to run on macOS or Linux. If you are on Windows, remove the "platform" and "user" lines)

The connection string is configured in src/Librarium.Api/appsettings.Development.json:
Server=localhost,1450;Database=librarium-db;User Id=sa;Password=SuperSecret7!;TrustServerCertificate=True;


Applying Migrations:
You can run the script database-update-scripts.sh. (macOS or Linux), if on Windows, run the following command in the terminal:
dotnet ef database update --project src/Librarium.Data --startup-project src/Librarium.Api


Running the API:
cd src/Librarium.Api
dotnet run

The API will be available at http://localhost:5036 (confirm in your terminal). Swagger UI is available at http://localhost:5036/swagger.


Project Structure:
/src                  → Application source code
/migrations/sql       → SQL migration artifacts (one file per migration)
/migrations/README.md → Migrations log 