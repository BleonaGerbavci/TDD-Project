# **Art Collection Organizer**
This repository contains the codebase for the Art Collection Organizer, a system designed to manage and organize art pieces, including functionalities for adding, updating, buying, deleting, and searching art pieces. 
The repository also includes a suite of unit tests to ensure the correct functionality of the system.

**Project Structure**
**ArtCollectionOrganizer (Main Project)**
The core project for the application, containing all of the essential logic, including:

Controllers: Handles HTTP requests related to art pieces.
DTOs (Data Transfer Objects): Contains the structure of the data that is transferred between layers.
Services: Contains the core business logic of the application.
Models: Defines the entities used in the system.
Migrations: Contains the database migrations for managing schema changes.
Configurations: Configuration files for setting up the application.

**TDD-project (Test Project)**
This folder contains unit tests for various features of the application. The tests are built using **NUnit** and ensure the following functionalities:

AddArtPiecesTests: Tests for adding new art pieces to the collection.
BuyArtPiecesTests: Tests for buying art pieces from the collection.
DeleteArtPiecesTests: Tests for removing art pieces from the collection.
SearchArtPiecesTests: Tests for searching and filtering art pieces.
UpdateArtPiecesTests: Tests for updating art pieces in the collection.

##  **Setup and Installation**

**Prerequisites**
.NET SDK 6 or later
Visual Studio or your preferred C# IDE
Installation

**Clone this repository:**
git clone https://github.com/yourusername/TDD-project.git

**Navigate into the project directory:**
cd TDD-project

**Restore the dependencies:**
dotnet restore

**Run the application:**
dotnet run --project ArtCollectionOrganizer

**Run the unit tests:**
dotnet test

**Database Setup**
The project uses Entity Framework Core with an in-memory database for testing. For production, you would need to configure a real database in appsettings.json.

**Running Tests**
The test project includes various tests for the functionalities in the application:

AddArtPiecesTests: Ensures that art pieces are correctly added.
BuyArtPiecesTests: Verifies that art pieces can be purchased.
DeleteArtPiecesTests: Confirms that art pieces can be deleted.
SearchArtPiecesTests: Tests the search and filtering functionality.
UpdateArtPiecesTests: Verifies that updates to art pieces are processed correctly.

**You can run all tests with:**
dotnet test

**Running Tests for a Specific Project**
To run tests for a specific functionality, you can specify the test class name like:
dotnet test --filter AddArtPiecesTests
