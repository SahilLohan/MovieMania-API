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
mail : sahillohan07@gmail.com

## DB-Script

```sql
﻿CREATE DATABASE [IMDB-Database]
USE [IMDB-Database]
GO
CREATE SCHEMA [Foundation]
GO
CREATE TYPE [Foundation].[ActorsIdsList] AS TABLE(
	[ActorId] [int] NULL
)
GO
CREATE TYPE [Foundation].[MoviesIdsList] AS TABLE(
	[MovieId] [int] NULL
)
GO
CREATE TABLE [Foundation].[Actor_Movies](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ActorId] [int] NOT NULL,
	[MovieId] [int] NOT NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedAt] [datetime] NULL,
 CONSTRAINT [PK_Foundation_ActorsMovies_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [Foundation].[Actors](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Gender] [varchar](20) NOT NULL,
	[DOB] [date] NOT NULL,
	[Bio] [varchar](1000) NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedAt] [datetime] NULL,
 CONSTRAINT [PK_Foundation_Actors_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [Foundation].[Genre_Movies](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GenreId] [int] NOT NULL,
	[MovieId] [int] NOT NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [Foundation].[Genres](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [Foundation].[Movies](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[YearOfRelease] [int] NOT NULL,
	[Plot] [varchar](1000) NULL,
	[CoverImage] [varchar](500) NULL,
	[ProducerId] [int] NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [Foundation].[Producers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Gender] [varchar](20) NULL,
	[DOB] [date] NULL,
	[Bio] [varchar](1000) NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedAt] [datetime] NULL,
 CONSTRAINT [PK_Foundation_Producers_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [Foundation].[Reviews](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Message] [varchar](1000) NOT NULL,
	[MovieId] [int] NOT NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [Foundation].[Actors] ON 

INSERT [Foundation].[Actors] ([Id], [Name], [Gender], [DOB], [Bio], [CreatedAt], [UpdatedAt]) VALUES (3, N'Mitu', N'Male', CAST(N'2024-03-11' AS Date), N'jkbvakjh', NULL, NULL)
INSERT [Foundation].[Actors] ([Id], [Name], [Gender], [DOB], [Bio], [CreatedAt], [UpdatedAt]) VALUES (4, N'Jai', N'Male', CAST(N'2024-03-11' AS Date), N'jkbvakjh', NULL, NULL)
INSERT [Foundation].[Actors] ([Id], [Name], [Gender], [DOB], [Bio], [CreatedAt], [UpdatedAt]) VALUES (5, N'Sunila', N'f', CAST(N'2002-01-12' AS Date), N'xbfzd', CAST(N'2024-03-14T12:32:29.897' AS DateTime), NULL)
INSERT [Foundation].[Actors] ([Id], [Name], [Gender], [DOB], [Bio], [CreatedAt], [UpdatedAt]) VALUES (6, N'Jiila', N'f', CAST(N'2002-02-12' AS Date), N'xbfjfzd', CAST(N'2024-03-14T12:32:29.897' AS DateTime), NULL)
INSERT [Foundation].[Actors] ([Id], [Name], [Gender], [DOB], [Bio], [CreatedAt], [UpdatedAt]) VALUES (1005, N'string', N'male', CAST(N'2024-04-10' AS Date), N'string', CAST(N'2024-04-10T18:08:02.507' AS DateTime), NULL)
INSERT [Foundation].[Actors] ([Id], [Name], [Gender], [DOB], [Bio], [CreatedAt], [UpdatedAt]) VALUES (1006, N'string', N'male', CAST(N'2024-04-10' AS Date), N'string', CAST(N'2024-04-10T18:28:42.643' AS DateTime), NULL)
INSERT [Foundation].[Actors] ([Id], [Name], [Gender], [DOB], [Bio], [CreatedAt], [UpdatedAt]) VALUES (1007, N'string', N'male', CAST(N'2024-04-10' AS Date), N'string', CAST(N'2024-04-10T18:29:29.070' AS DateTime), NULL)
INSERT [Foundation].[Actors] ([Id], [Name], [Gender], [DOB], [Bio], [CreatedAt], [UpdatedAt]) VALUES (1008, N'Mannu', N'male', CAST(N'2024-04-10' AS Date), N'string', CAST(N'2024-04-10T18:36:47.537' AS DateTime), NULL)
INSERT [Foundation].[Actors] ([Id], [Name], [Gender], [DOB], [Bio], [CreatedAt], [UpdatedAt]) VALUES (1009, N'Testing1', N'male', CAST(N'2024-04-10' AS Date), N'string', CAST(N'2024-04-10T18:40:02.467' AS DateTime), NULL)
INSERT [Foundation].[Actors] ([Id], [Name], [Gender], [DOB], [Bio], [CreatedAt], [UpdatedAt]) VALUES (1010, N'testingIT', N'female', CAST(N'2024-04-10' AS Date), N'string', CAST(N'2024-04-10T18:46:31.937' AS DateTime), NULL)
INSERT [Foundation].[Actors] ([Id], [Name], [Gender], [DOB], [Bio], [CreatedAt], [UpdatedAt]) VALUES (1011, N'string', N'male', CAST(N'2024-04-10' AS Date), N'string', CAST(N'2024-04-10T18:47:58.690' AS DateTime), NULL)
INSERT [Foundation].[Actors] ([Id], [Name], [Gender], [DOB], [Bio], [CreatedAt], [UpdatedAt]) VALUES (1012, N'string', N'male', CAST(N'2024-04-10' AS Date), N'string', CAST(N'2024-04-10T18:51:09.060' AS DateTime), NULL)
INSERT [Foundation].[Actors] ([Id], [Name], [Gender], [DOB], [Bio], [CreatedAt], [UpdatedAt]) VALUES (2005, N'Sahil22', N'male', CAST(N'2024-04-12' AS Date), N'string', CAST(N'2024-04-12T11:09:13.690' AS DateTime), NULL)
INSERT [Foundation].[Actors] ([Id], [Name], [Gender], [DOB], [Bio], [CreatedAt], [UpdatedAt]) VALUES (2007, N'Sahil22', N'male', CAST(N'2024-04-12' AS Date), N'string', CAST(N'2024-04-12T11:18:16.043' AS DateTime), NULL)
INSERT [Foundation].[Actors] ([Id], [Name], [Gender], [DOB], [Bio], [CreatedAt], [UpdatedAt]) VALUES (2008, N'Sahil22', N'male', CAST(N'2024-04-12' AS Date), N'string', CAST(N'2024-04-12T11:51:48.727' AS DateTime), NULL)
INSERT [Foundation].[Actors] ([Id], [Name], [Gender], [DOB], [Bio], [CreatedAt], [UpdatedAt]) VALUES (2009, N'Sahil22', N'male', CAST(N'2024-04-12' AS Date), N'string', CAST(N'2024-04-12T11:53:20.500' AS DateTime), NULL)
INSERT [Foundation].[Actors] ([Id], [Name], [Gender], [DOB], [Bio], [CreatedAt], [UpdatedAt]) VALUES (2010, N'Sahil22', N'male', CAST(N'2024-04-12' AS Date), N'string', CAST(N'2024-04-15T11:12:18.620' AS DateTime), NULL)
INSERT [Foundation].[Actors] ([Id], [Name], [Gender], [DOB], [Bio], [CreatedAt], [UpdatedAt]) VALUES (2011, N'SahilNew', N'male', CAST(N'2024-04-12' AS Date), N'string', CAST(N'2024-04-15T11:31:31.193' AS DateTime), NULL)
INSERT [Foundation].[Actors] ([Id], [Name], [Gender], [DOB], [Bio], [CreatedAt], [UpdatedAt]) VALUES (2012, N'SahilNew', N'male', CAST(N'2024-04-12' AS Date), N'string', CAST(N'2024-04-15T11:36:18.397' AS DateTime), NULL)
INSERT [Foundation].[Actors] ([Id], [Name], [Gender], [DOB], [Bio], [CreatedAt], [UpdatedAt]) VALUES (2013, N'SahilNew', N'male', CAST(N'2024-04-12' AS Date), N'string', CAST(N'2024-04-15T11:36:21.517' AS DateTime), NULL)
INSERT [Foundation].[Actors] ([Id], [Name], [Gender], [DOB], [Bio], [CreatedAt], [UpdatedAt]) VALUES (4010, N'Pankaj', N'male', CAST(N'2024-04-12' AS Date), N'MBA ka DON', CAST(N'2024-04-28T06:56:23.390' AS DateTime), NULL)
SET IDENTITY_INSERT [Foundation].[Actors] OFF
GO
SET IDENTITY_INSERT [Foundation].[Genres] ON 

INSERT [Foundation].[Genres] ([Id], [Name], [CreatedAt], [UpdatedAt]) VALUES (1, N'Action', CAST(N'2024-04-12T12:51:01.613' AS DateTime), NULL)
INSERT [Foundation].[Genres] ([Id], [Name], [CreatedAt], [UpdatedAt]) VALUES (4, N'Religious', CAST(N'2024-04-12T12:51:35.080' AS DateTime), NULL)
INSERT [Foundation].[Genres] ([Id], [Name], [CreatedAt], [UpdatedAt]) VALUES (5, N'Drama', CAST(N'2024-04-12T12:51:43.727' AS DateTime), NULL)
SET IDENTITY_INSERT [Foundation].[Genres] OFF
GO
SET IDENTITY_INSERT [Foundation].[Producers] ON 

INSERT [Foundation].[Producers] ([Id], [Name], [Gender], [DOB], [Bio], [CreatedAt], [UpdatedAt]) VALUES (1, N'Sahil', N'Male', CAST(N'2024-03-11' AS Date), N'jkbvakjh', CAST(N'2024-03-13T13:12:27.150' AS DateTime), NULL)
INSERT [Foundation].[Producers] ([Id], [Name], [Gender], [DOB], [Bio], [CreatedAt], [UpdatedAt]) VALUES (2, N'Husain', N'Male', CAST(N'2024-03-11' AS Date), N'jkbvakjh', CAST(N'2024-03-13T13:12:27.150' AS DateTime), NULL)
INSERT [Foundation].[Producers] ([Id], [Name], [Gender], [DOB], [Bio], [CreatedAt], [UpdatedAt]) VALUES (3, N'Amit', N'Male', CAST(N'2024-03-11' AS Date), N'jkbvakjh', CAST(N'2024-03-13T13:12:27.150' AS DateTime), NULL)
INSERT [Foundation].[Producers] ([Id], [Name], [Gender], [DOB], [Bio], [CreatedAt], [UpdatedAt]) VALUES (4, N'Rohit', N'Male', CAST(N'2024-03-11' AS Date), N'jkbvakjh', CAST(N'2024-03-13T13:12:27.150' AS DateTime), NULL)
INSERT [Foundation].[Producers] ([Id], [Name], [Gender], [DOB], [Bio], [CreatedAt], [UpdatedAt]) VALUES (5, N'Sahil22', N'male', CAST(N'2024-04-12' AS Date), N'string', CAST(N'2024-04-12T12:55:01.600' AS DateTime), NULL)
INSERT [Foundation].[Producers] ([Id], [Name], [Gender], [DOB], [Bio], [CreatedAt], [UpdatedAt]) VALUES (6, N'don updated name', N'female', CAST(N'2024-04-12' AS Date), N'string', CAST(N'2024-04-12T12:56:09.523' AS DateTime), CAST(N'2024-04-15T11:41:35.730' AS DateTime))
INSERT [Foundation].[Producers] ([Id], [Name], [Gender], [DOB], [Bio], [CreatedAt], [UpdatedAt]) VALUES (8, N'Sahil22', N'male', CAST(N'2024-04-12' AS Date), N'string', CAST(N'2024-04-15T11:41:08.000' AS DateTime), NULL)
SET IDENTITY_INSERT [Foundation].[Producers] OFF
GO
ALTER TABLE [Foundation].[Actor_Movies] ADD  CONSTRAINT [DF_Actors_Movies_CreatedAt]  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [Foundation].[Actors] ADD  CONSTRAINT [DF_Actors_CreatedAt]  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [Foundation].[Genre_Movies] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [Foundation].[Genres] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [Foundation].[Movies] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [Foundation].[Producers] ADD  CONSTRAINT [DF_Foundation_Producers_CreatedAt]  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [Foundation].[Reviews] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [Foundation].[Actor_Movies]  WITH CHECK ADD  CONSTRAINT [FK_Foundation_ActorsMovies_ActorId] FOREIGN KEY([ActorId])
REFERENCES [Foundation].[Actors] ([Id])
GO
ALTER TABLE [Foundation].[Actor_Movies] CHECK CONSTRAINT [FK_Foundation_ActorsMovies_ActorId]
GO
ALTER TABLE [Foundation].[Actor_Movies]  WITH CHECK ADD  CONSTRAINT [FK_Foundation_ActorsMovies_MovieId] FOREIGN KEY([MovieId])
REFERENCES [Foundation].[Movies] ([Id])
GO
ALTER TABLE [Foundation].[Actor_Movies] CHECK CONSTRAINT [FK_Foundation_ActorsMovies_MovieId]
GO
ALTER TABLE [Foundation].[Genre_Movies]  WITH CHECK ADD  CONSTRAINT [FK_FoundationGenreMovies_GenreId_FoundationGenres_Id] FOREIGN KEY([GenreId])
REFERENCES [Foundation].[Genres] ([Id])
GO
ALTER TABLE [Foundation].[Genre_Movies] CHECK CONSTRAINT [FK_FoundationGenreMovies_GenreId_FoundationGenres_Id]
GO
ALTER TABLE [Foundation].[Genre_Movies]  WITH CHECK ADD  CONSTRAINT [FK_FoundationGenreMovies_MovieId_FoundationMovies_Id] FOREIGN KEY([MovieId])
REFERENCES [Foundation].[Movies] ([Id])
GO
ALTER TABLE [Foundation].[Genre_Movies] CHECK CONSTRAINT [FK_FoundationGenreMovies_MovieId_FoundationMovies_Id]
GO
ALTER TABLE [Foundation].[Movies]  WITH CHECK ADD FOREIGN KEY([ProducerId])
REFERENCES [Foundation].[Producers] ([Id])
GO
ALTER TABLE [Foundation].[Reviews]  WITH CHECK ADD  CONSTRAINT [FK_FoundationReviews_MovieId_FoundationMovies_Id] FOREIGN KEY([MovieId])
REFERENCES [Foundation].[Movies] ([Id])
GO
ALTER TABLE [Foundation].[Reviews] CHECK CONSTRAINT [FK_FoundationReviews_MovieId_FoundationMovies_Id]
GO
ALTER TABLE [Foundation].[Movies]  WITH CHECK ADD  CONSTRAINT [CK_FoundationMovies_YearOfRelease] CHECK  (([YearOfRelease]>=(1888)))
GO
ALTER TABLE [Foundation].[Movies] CHECK CONSTRAINT [CK_FoundationMovies_YearOfRelease]
GO
CREATE Proc [dbo].[usp_AddMovie]
@Name varchar(50),
@YearOfRelease int,
@Plot varchar(1000),
@CoverImage varchar(500),
@ProducerId int,
@GenresIds nvarchar(max),
@ActorsIds nvarchar(max),
@MovieId int output
As
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO Foundation.Movies (YearOfRelease, [Name], Plot, CoverImage, ProducerId)
        VALUES (@YearOfRelease, @Name, @Plot, @CoverImage, @ProducerId);

        SET @MovieId = SCOPE_IDENTITY();

		INSERT INTO Foundation.Actor_Movies (ActorId, MovieId)
		SELECT CAST(value AS INT), @MovieId
		FROM STRING_SPLIT(@ActorsIds, ',');

		INSERT INTO Foundation.Genre_Movies (GenreId, MovieId)
		SELECT CAST(value AS INT), @MovieId
		FROM STRING_SPLIT(@GenresIds, ',');

		SELECT @MovieId;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        
        PRINT ERROR_MESSAGE();
    END CATCH;
END;
GO
CREATE PROC [dbo].[usp_DeleteActor]
@Id int
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

		-- Delete actors and related movies
		DELETE FROM Foundation.Actor_Movies
		WHERE [ActorId] = @Id;

		DELETE FROM Foundation.Actors
		WHERE [Id] = @Id;

		-- Delete movies with zero actors and their reviews
		DELETE FROM Foundation.Reviews
		WHERE MovieId IN (
			SELECT m.Id
			FROM Foundation.Movies m
			LEFT JOIN Foundation.Actor_Movies am ON m.Id = am.MovieId
			GROUP BY m.Id
			HAVING COUNT(am.MovieId) = 0
		);

		DELETE FROM Foundation.Genre_Movies
		WHERE MovieId IN (
			SELECT m.Id
			FROM Foundation.Movies m
			LEFT JOIN Foundation.Actor_Movies am ON m.Id = am.MovieId
			GROUP BY m.Id
			HAVING COUNT(am.MovieId) = 0
		);

		DELETE FROM Foundation.Movies
		WHERE Id IN (
			SELECT m.Id
			FROM Foundation.Movies m
			LEFT JOIN Foundation.Actor_Movies am ON m.Id = am.MovieId
			GROUP BY m.Id
			HAVING COUNT(am.MovieId) = 0
		);


        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        
        PRINT ERROR_MESSAGE();
    END CATCH;
END;
GO
CREATE PROC [dbo].[usp_DeleteGenre]
@Id int
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

		DELETE FROM Foundation.Genre_Movies
		WHERE [GenreId] = @Id;

		DELETE FROM Foundation.Genres
		WHERE [Id] = @Id;

		DELETE FROM Foundation.Reviews
		WHERE MovieId IN (
			SELECT m.Id
			FROM Foundation.Movies m
			LEFT JOIN Foundation.Genre_Movies gm ON m.Id = gm.MovieId
			GROUP BY m.Id
			HAVING COUNT(gm.MovieId) = 0
		);

		DELETE FROM Foundation.Actor_Movies
		WHERE MovieId IN (
			SELECT m.Id
			FROM Foundation.Movies m
			LEFT JOIN Foundation.Genre_Movies gm ON m.Id = gm.MovieId
			GROUP BY m.Id
			HAVING COUNT(gm.MovieId) = 0
		);

		-- Delete movies with zero genres and their related records
		DELETE FROM Foundation.Movies
		WHERE Id IN (
			SELECT m.Id
			FROM Foundation.Movies m
			LEFT JOIN Foundation.Genre_Movies gm ON m.Id = gm.MovieId
			GROUP BY m.Id
			HAVING COUNT(gm.MovieId) = 0
		);
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        
        PRINT ERROR_MESSAGE();
    END CATCH;
END;
GO
-- Movie
CREATE PROC [dbo].[usp_DeleteMovie]
@Id int
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

		DELETE FROM Foundation.Actor_Movies
		WHERE [MovieId] = @Id;

		DELETE FROM Foundation.Genre_Movies
		WHERE [MovieId] = @Id;

		DELETE FROM Foundation.Reviews
		WHERE [MovieId] = @Id;

		DELETE FROM Foundation.Movies
		WHERE [Id] = @Id;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        
        PRINT ERROR_MESSAGE();
    END CATCH;
END;

GO
-- producer
CREATE PROC [dbo].[usp_DeleteProducer]
@Id int
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

		DELETE FROM Foundation.Movies
		WHERE [ProducerId] = @Id;

		DELETE FROM Foundation.Producers
		WHERE [Id] = @Id;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        
        PRINT ERROR_MESSAGE();
    END CATCH;
END;

GO
-- Review
CREATE PROC [dbo].[usp_DeleteReview]
@Id int
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

		DELETE FROM Foundation.Reviews
		WHERE [Id] = @Id;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        
        PRINT ERROR_MESSAGE();
    END CATCH;
END;
GO
CREATE Proc [dbo].[usp_UpdateMovie]
@Id int,
@Name varchar(50),
@YearOfRelease int,
@Plot varchar(1000),
@CoverImage varchar(500),
@ProducerId int,
@GenresIds nvarchar(max),
@ActorsIds nvarchar(max)
As
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;


        UPDATE Foundation.Movies 
		SET YearOfRelease=@YearOfRelease, 
			[Name]=@Name, 
			Plot=@Plot, 
			CoverImage=@CoverImage, 
			ProducerId=@ProducerId,
			UpdatedAt=GETDATE()
		WHERE Id=@Id;

		DELETE FROM Foundation.Actor_Movies
		WHERE MovieId = @Id;

		DELETE FROM Foundation.Genre_Movies
		WHERE MovieId = @Id;

		INSERT INTO Foundation.Actor_Movies (ActorId, MovieId)
		SELECT CAST(value AS INT), @Id
		FROM STRING_SPLIT(@ActorsIds, ',');

		INSERT INTO Foundation.Genre_Movies (GenreId, MovieId)
		SELECT CAST(value AS INT), @Id
		FROM STRING_SPLIT(@GenresIds, ',');

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        
        PRINT ERROR_MESSAGE();
    END CATCH;
END;
```

