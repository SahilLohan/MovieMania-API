# MovieMania Project Setup Guide

This guide will walk you through the steps to set up and run the MovieMania project on your local machine.

## Prerequisites

Before you begin, ensure you have the following installed on your system:

- .NET SDK (version 5.0.1)
- Visual Studio (version 2022 or higher)
- Git
- MSSQL Server (version 16 or higher)

## Getting Started

1. Clone the repository to your local machine using Git:

   ```bash
   git clone https://github.com/your-username/MovieMania.git
   
2. Open Visual Studio and navigate to the folder where you cloned the repository.

3. Double-click on the MovieMania.csproj file to open the project in Visual Studio.

## Database Setup
Before running the project, you need to set up the MSSQL database:

- Open MSSQL Server Management Studio (SSMS) or any SQL tool of your choice.

- Connect to your local MSSQL Server instance.

- Run the .script.sql file provided to create all the necessary tables, stored procedures, and database schema.

## Configuration

Before running the project, ensure you configure the necessary settings:

- Open the appsettings.json file located in the project's root directory.

- Update the connection strings, API keys, or any other configuration settings as needed for your environment.

## Running the Project

Once you have configured the project settings, you can run it using Visual Studio:

- Set the startup project to MovieMania if it's not already selected.

- Press F5 or click on the "Start" button in Visual Studio to build and run the project.

- The MovieMania application should now be running locally on your machine.

## Additional Notes
If you encounter any issues during setup or running the project, refer to the Issues section of this repository or reach out to the project maintainers for assistance.

For more detailed documentation or guides, refer to the Wiki section of this repository.

Feel free to customize this README template based on your specific project requirements and add any additional instructions or links as needed.
