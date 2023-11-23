# Documentation

Here I will write all of the steps that I took to do this project (lab project)

- Create ASP.NET Core Web App (Model-View-Controller)
- Add login method individual
- Core 6.0

## Connect Database and C.R.U.D

1. Install Entity Framework core
    - location: Tools -> NuGet Package Manager -> Manage NuGet Packages for Solution. 
    - package source : *Package Source*

    - Install Microsoft.EntityFrameworkCore
    - Install Microsoft.EntityFrameworkCore.SqlServer
    - Install Microsoft.EntityFrameworkCore.Design
    - Install Microsoft.EntityFrameworkCore.Tools
2. Create the tables for Article and Category
    - Articles(Id,Title,Content,Date)
    - Category (Id, CategoryName)

commit "Install entity Framework core and create tables (Article and Category)"

-----
## Create the connection with the Database with dependecy injection
1. Create a class named AppDbContext in Models
2. add in program.cs the connection with the databse 
3. Create a new databse in sql server object explorer in mssqllocaldb
4. solution -> explorer -> right click to connected services -> add -> sql server database (sql server express localdb) -> name the connection, connect it to the created datbase and copy the connection string.
5. Add the connection string to the appsetings.json
6. connect the article and categories with the server
7. migrate (Add-Migration + Update-Database)

commit "create connection with the database"

----
