# Pokemon Review App

Pokemon Review App is a simple `RESTful` web API built using .NET Core version 8.0. It allows users to review Pokemon, utilizing six main entities: Pokemon, Reviewer, Review, Owner, Category, and Country. The database is created using `Entity Framework Core`'s Code First approach, with `SQL Server` as the database provider. 
Building this project helped me to get a grasp on crucial concepts and technologies such as `Repository Design Pattern`, `Dependency Injection`, and EF Core Object Relational Mapper.

#### Notes:
- Implements a Controller-based API
- Utilizes AutoMapper for mapping between domain models and DTOs
- Includes a class for seeding the database with fake data

## Prerequisites

Before running the project, ensure you have the following installed:

- .NET SDK 8.0
  - [Installation link](https://dotnet.microsoft.com/en-us/download)

## Installation

1. Clone the repository:
    ```
    git clone git@github.com:obay-ismaeel/PokemonReviewApp.git
    ```

2. Navigate to the project directory:
    ```
    cd [project directory]
    ```

3. Run the project:
    ```
    dotnet run
    ```

4. Once the project is built, navigate to the URL displayed and append "/swagger" to the end of the URL to access the Swagger UI for testing the API. Ensure to configure the connection string in the `appsettings.json` file.

## Project Dependencies

- AutoMapper.Extensions.Microsoft.DependencyInjection Version=12.0.1
- Microsoft.AspNetCore.OpenApi Version=8.0.1
- Microsoft.EntityFrameworkCore.SqlServer Version=8.0.2
- Microsoft.EntityFrameworkCore.Tools Version=8.0.2
- Swashbuckle.AspNetCore Version=6.4.0

## Database Provider

It's worth noting that you can use any database provider of your choice by installing the corresponding EF Core package and configuring the database connection.

## Contribution

Feel free to clone the repository, experiment with the code, and contribute by sending pull requests. Your contributions are welcome!

---

Let me know if you have any additional information or changes you'd like to make!
## Authors
- [@Obay-Ismaeel](https://github.com/obay-ismaeel)

