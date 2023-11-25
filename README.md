# Documentation

Here I will write all of the steps that I took to do this project (lab project)

- Create ASP.NET Core Web App (Model-View-Controller)
- Add login method individual
- Core 6.0

## Connect Database and create tables

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
## Create C.R.U.D
1. In articles view I added Edit, Index, New, Show
2. In ArticlesController I added a function for each view, some of them only acessible on post method

commit "create C.R.U.D and solved database problem"

---
## Add Comments
New database:  

Article   
• Id (int – primary key)   
• Title (string – titlul este obligatoriu)   
• Content (string – continutul este obligatoriu)  
• Date (DateTime)  
• CategoryId (int – cheie externa – categoria din care face parte 
articolul)  

Category:   
• Id (int – primary key)   
• CategoryName (string – numele este obligatoriu)

Comment:   
• Id (int – primary key)   
• Content (string – continutul comentariului este obligatoriu)  
• Date (DateTime – data la care a fost postat comentariul)  
• ArticleId (int – cheie externa – articolul caruia ii apartine 
comentariul)  

1. Change in this way
2. Add comments in ApplicationDbContext
3. Migrate and Update

## Add C.R.U.D for categories and comments 

## Optimize the current code with functions

1. Change ViewBag with model when I could
2. Add a dropdown using @Html.DropDownListFor when adding a new article
    - create a function of type IEnumerable< SelectListItem > which takes all categories
    - use in new function the previous function to add all categories
    - add in article class a list of all categories
    - add in html new (in view) @Html.DropDownListFor

## Add confirmation messages
Example with delete articles
1. create a temp data in delete action with a name.
2. In redirect route from delete verify if a temp data is there. If so, add it in the viewBag.
3. print it on View