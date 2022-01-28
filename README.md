### Turning on app for the first time
To run this app, you will need clone project via VisualStudio Code and turn on Microsoft's SQL Server Management Studio (SSMS).

While connecting to server in SSMS, copy server's name from popout window and paste it in [appsetings.json](https://gitlab.com/scll/wsei/2022-01-csharp-rottenpotatoes/-/blob/main/appsettings.json) file (replace `THINKBAD` with your server's name in 2 Connection Strings).
After it's done, save appsettings.json file and connect to server in SSMS.

To set up database, execute 2 CLI commands from project's main directory (make sure you have [EF Tools](https://docs.microsoft.com/en-us/ef/core/cli/dotnet) installed. If you don't, just type `dotnet tool install --global dotnet-ef` in your CLI). The commands are:

`dotnet ef database update --context AppIdentityDbContext`

`dotnet ef database update --context AppDbContext`

### Using the app

 To run app, type one command:

`dotnet watch run`

You can log in to Admin account with nickname `Admin` and `Secret123$` password.

There are also 5 base user accounts: `userX` (where `X` stands for numbers from 1 to 5). All of that users have the same password: `Haslo12#`

### Using the API

You can access Api endpoints when the app is running. They are available by default by sending requests to `http://localhost:5001/Api/ENDPOINT`, where ENDPOINT stands for:
- `ListTitles` - get list of all movie titles that are currently in database
- `GetTitlesWithIds`- get list of all titles with their coresponding IDs
- `GetTitlesWithVotes` - get list of all movies with their user votes
- `GetMovieDetailsById?id=X` - get details of selected movie
- `GetMovieDescriptionById?id=X` - get description of selected movie

(`X` represents primary keys (IDs) of movies from table `Movies` in `rotten_potatoes` database)
