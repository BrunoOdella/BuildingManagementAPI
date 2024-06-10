USE [master]
GO
/****** Object:  Database [BuildingManagementDB]    Script Date: 10/6/2024 17:44:25 ******/
CREATE DATABASE [BuildingManagementDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BuildingManagementDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\BuildingManagementDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BuildingManagementDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\BuildingManagementDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [BuildingManagementDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BuildingManagementDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BuildingManagementDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BuildingManagementDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BuildingManagementDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BuildingManagementDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BuildingManagementDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [BuildingManagementDB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [BuildingManagementDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BuildingManagementDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BuildingManagementDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BuildingManagementDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BuildingManagementDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BuildingManagementDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BuildingManagementDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BuildingManagementDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BuildingManagementDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [BuildingManagementDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BuildingManagementDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BuildingManagementDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BuildingManagementDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BuildingManagementDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BuildingManagementDB] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [BuildingManagementDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BuildingManagementDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BuildingManagementDB] SET  MULTI_USER 
GO
ALTER DATABASE [BuildingManagementDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BuildingManagementDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BuildingManagementDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BuildingManagementDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BuildingManagementDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BuildingManagementDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [BuildingManagementDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [BuildingManagementDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [BuildingManagementDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 10/6/2024 17:44:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Admins]    Script Date: 10/6/2024 17:44:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admins](
	[AdminID] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Admins] PRIMARY KEY CLUSTERED 
(
	[AdminID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Apartments]    Script Date: 10/6/2024 17:44:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Apartments](
	[ApartmentId] [uniqueidentifier] NOT NULL,
	[Floor] [int] NOT NULL,
	[Number] [int] NOT NULL,
	[NumberOfBathrooms] [int] NOT NULL,
	[HasTerrace] [bit] NOT NULL,
	[BuildingId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Apartments] PRIMARY KEY CLUSTERED 
(
	[ApartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Buildings]    Script Date: 10/6/2024 17:44:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Buildings](
	[BuildingId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[ConstructionCompanyId] [uniqueidentifier] NOT NULL,
	[CommonExpenses] [int] NOT NULL,
	[ManagerId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Buildings] PRIMARY KEY CLUSTERED 
(
	[BuildingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 10/6/2024 17:44:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ConstructionCompanies]    Script Date: 10/6/2024 17:44:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConstructionCompanies](
	[ConstructionCompanyId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[ConstructionCompanyAdminId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_ConstructionCompanies] PRIMARY KEY CLUSTERED 
(
	[ConstructionCompanyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ConstructionCompanyAdmins]    Script Date: 10/6/2024 17:44:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConstructionCompanyAdmins](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_ConstructionCompanyAdmins] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invitations]    Script Date: 10/6/2024 17:44:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invitations](
	[InvitationId] [uniqueidentifier] NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[ExpirationDate] [datetime2](7) NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
	[Role] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Invitations] PRIMARY KEY CLUSTERED 
(
	[InvitationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Locations]    Script Date: 10/6/2024 17:44:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Locations](
	[BuildingId] [uniqueidentifier] NOT NULL,
	[Latitude] [float] NOT NULL,
	[Longitude] [float] NOT NULL,
 CONSTRAINT [PK_Locations] PRIMARY KEY CLUSTERED 
(
	[BuildingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MaintenanceStaff]    Script Date: 10/6/2024 17:44:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaintenanceStaff](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_MaintenanceStaff] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Managers]    Script Date: 10/6/2024 17:44:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Managers](
	[ManagerId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NULL,
 CONSTRAINT [PK_Managers] PRIMARY KEY CLUSTERED 
(
	[ManagerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Owners]    Script Date: 10/6/2024 17:44:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Owners](
	[OwnerId] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Owners] PRIMARY KEY CLUSTERED 
(
	[OwnerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Requests]    Script Date: 10/6/2024 17:44:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Requests](
	[Id] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Status] [int] NOT NULL,
	[CategoryID] [int] NULL,
	[CreationTime] [datetime2](7) NOT NULL,
	[StartTime] [datetime2](7) NOT NULL,
	[EndTime] [datetime2](7) NOT NULL,
	[TotalCost] [real] NOT NULL,
	[MaintenanceStaffId] [uniqueidentifier] NOT NULL,
	[ApartmentId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Requests] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240530223141_v2', N'8.0.4')
GO
INSERT [dbo].[Admins] ([AdminID], [FirstName], [LastName], [Email], [Password]) VALUES (N'e3973fca-cb19-4691-28f5-08dc88ee5d60', N'Jamey', N'Kulas', N'Elian.Collins@hotmail.com', N'grey')
INSERT [dbo].[Admins] ([AdminID], [FirstName], [LastName], [Email], [Password]) VALUES (N'c2cbdb88-b367-4fbd-28f6-08dc88ee5d60', N'Jena', N'Gerhold', N'Cordie.Gaylord60@hotmail.com', N'fuchsia')
INSERT [dbo].[Admins] ([AdminID], [FirstName], [LastName], [Email], [Password]) VALUES (N'a9dabb32-3ef8-4af7-28f7-08dc88ee5d60', N'Korbin', N'Yundt', N'Jeremie.Lind11@yahoo.com', N'lime')
INSERT [dbo].[Admins] ([AdminID], [FirstName], [LastName], [Email], [Password]) VALUES (N'e9bccd7d-c7b6-4ece-28f8-08dc88ee5d60', N'August', N'Block', N'Brittany_Kihn50@hotmail.com', N'white')
INSERT [dbo].[Admins] ([AdminID], [FirstName], [LastName], [Email], [Password]) VALUES (N'8bf27613-c0b0-4e7e-28f9-08dc88ee5d60', N'Birdie', N'Donnelly', N'Demarcus_Weber30@gmail.com', N'black')
INSERT [dbo].[Admins] ([AdminID], [FirstName], [LastName], [Email], [Password]) VALUES (N'94dc648a-43a4-4361-28fa-08dc88ee5d60', N'Paula', N'Weissnat', N'Beatrice.Jones@yahoo.com', N'yellow')
INSERT [dbo].[Admins] ([AdminID], [FirstName], [LastName], [Email], [Password]) VALUES (N'9146861f-e0d7-4f3f-28fb-08dc88ee5d60', N'Kenyatta', N'Murphy', N'Kenya56@hotmail.com', N'pink')
INSERT [dbo].[Admins] ([AdminID], [FirstName], [LastName], [Email], [Password]) VALUES (N'f8a79c62-0c8d-45c5-28fc-08dc88ee5d60', N'Palma', N'Maggio', N'Alfredo40@gmail.com', N'lavender')
INSERT [dbo].[Admins] ([AdminID], [FirstName], [LastName], [Email], [Password]) VALUES (N'74306210-2b8e-418d-28fd-08dc88ee5d60', N'Jermey', N'Feest', N'Juanita32@hotmail.com', N'blue')
INSERT [dbo].[Admins] ([AdminID], [FirstName], [LastName], [Email], [Password]) VALUES (N'3d166499-f1a4-455d-28fe-08dc88ee5d60', N'Jaunita', N'Morar', N'Kristian.Ritchie19@yahoo.com', N'purple')
GO
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'87415f65-29ab-4222-2c86-08dc88f1a80a', 5, 1, 1, 1, N'8f653617-033b-4a94-743f-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'0c057c32-a7d9-4ce2-2c87-08dc88f1a80a', 4, 2, 2, 0, N'8f653617-033b-4a94-743f-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'ef7f0b36-fb18-4d5e-2c88-08dc88f1a80a', 4, 3, 1, 0, N'8f653617-033b-4a94-743f-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'a4bc014c-dc3f-4baf-2c89-08dc88f1a80a', 3, 4, 2, 0, N'8f653617-033b-4a94-743f-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'639322d2-5fc6-4887-2c8a-08dc88f1a80a', 2, 5, 1, 1, N'8f653617-033b-4a94-743f-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'e52de014-7861-4102-2c8b-08dc88f1a80a', 2, 6, 3, 0, N'8f653617-033b-4a94-743f-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'0bb102a9-85d5-4650-2c8c-08dc88f1a80a', 13, 7, 3, 0, N'8f653617-033b-4a94-743f-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'eafced6d-b011-4d4f-2c8d-08dc88f1a80a', 1, 8, 2, 0, N'8f653617-033b-4a94-743f-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'0c66dd77-e02b-4122-2c8e-08dc88f1a80a', 8, 9, 3, 1, N'8f653617-033b-4a94-743f-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'b0e2ba02-6fef-4fb1-2c8f-08dc88f1a80a', 3, 10, 3, 1, N'8f653617-033b-4a94-743f-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'77cdd124-537f-4421-2c90-08dc88f1a80a', 11, 11, 2, 1, N'8f653617-033b-4a94-743f-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'f84f1df8-391a-4dc7-2c91-08dc88f1a80a', 0, 12, 2, 1, N'8f653617-033b-4a94-743f-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'b13bbd5e-ea7e-4314-2c92-08dc88f1a80a', 2, 13, 2, 1, N'8f653617-033b-4a94-743f-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'8ee336f0-dc9a-4bc5-2c93-08dc88f1a80a', 9, 14, 3, 1, N'8f653617-033b-4a94-743f-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'3aa75ed5-04eb-4da2-2c94-08dc88f1a80a', 1, 15, 3, 1, N'8f653617-033b-4a94-743f-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'cb5b6783-751e-47eb-2c95-08dc88f1a80a', 14, 16, 1, 1, N'8f653617-033b-4a94-743f-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'b67b60fe-673f-4e66-2c96-08dc88f1a80a', 1, 17, 3, 1, N'8f653617-033b-4a94-743f-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'614b78a1-c9cc-413f-2c97-08dc88f1a80a', 4, 18, 3, 1, N'8f653617-033b-4a94-743f-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'6fd5fe4b-1f71-4888-2c98-08dc88f1a80a', 2, 19, 3, 0, N'8f653617-033b-4a94-743f-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'd635789c-2242-4b05-2c99-08dc88f1a80a', 11, 20, 3, 0, N'8f653617-033b-4a94-743f-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'394ba2b4-04ff-4801-2c9a-08dc88f1a80a', 4, 21, 3, 1, N'8f653617-033b-4a94-743f-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'2bd51d2b-d0b5-4150-2c9b-08dc88f1a80a', 13, 22, 2, 1, N'8f653617-033b-4a94-743f-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'ca478e43-67ec-46d8-2c9c-08dc88f1a80a', 6, 23, 2, 1, N'8f653617-033b-4a94-743f-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'fbdd5dc6-c265-40a8-2c9d-08dc88f1a80a', 10, 24, 2, 0, N'8f653617-033b-4a94-743f-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'0182c18e-4c79-4ef0-2c9e-08dc88f1a80a', 14, 25, 2, 1, N'8f653617-033b-4a94-743f-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'4abb6808-88ee-4084-2c9f-08dc88f1a80a', 7, 26, 1, 0, N'8f653617-033b-4a94-743f-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'c5be47dc-0473-464e-2ca0-08dc88f1a80a', 0, 27, 1, 0, N'8f653617-033b-4a94-743f-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'da935df8-f640-45ba-2ca1-08dc88f1a80a', 4, 28, 1, 1, N'8f653617-033b-4a94-743f-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'08977937-369c-496b-2ca2-08dc88f1a80a', 14, 29, 3, 0, N'8f653617-033b-4a94-743f-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'b4cfcddd-04bf-4749-2ca3-08dc88f1a80a', 4, 30, 1, 0, N'8f653617-033b-4a94-743f-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'fc4183de-80ff-4af1-2ca4-08dc88f1a80a', 4, 31, 3, 1, N'8f653617-033b-4a94-743f-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'15a04be8-3fcc-4311-2ca5-08dc88f1a80a', 0, 32, 2, 1, N'8f653617-033b-4a94-743f-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'882e5101-5041-4318-2ca6-08dc88f1a80a', 7, 33, 3, 0, N'8f653617-033b-4a94-743f-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'f47a9763-dca6-44c4-2ca7-08dc88f1a80a', 10, 34, 3, 1, N'8f653617-033b-4a94-743f-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'c2f62e70-b716-4d6c-2ca8-08dc88f1a80a', 5, 35, 3, 1, N'8f653617-033b-4a94-743f-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'f1a8cddd-0af0-43d7-2ca9-08dc88f1a80a', 10, 36, 3, 0, N'8f653617-033b-4a94-743f-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'967a0c62-dee0-42a7-2caa-08dc88f1a80a', 8, 37, 1, 0, N'8f653617-033b-4a94-743f-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'6e02b154-11da-41b8-2cab-08dc88f1a80a', 13, 38, 3, 1, N'8f653617-033b-4a94-743f-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'b89129c2-4e5c-4b2f-2cac-08dc88f1a80a', 1, 39, 1, 0, N'8f653617-033b-4a94-743f-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'49d6772d-7d5b-4da6-2cad-08dc88f1a80a', 0, 40, 3, 0, N'8f653617-033b-4a94-743f-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'd922e8f3-39b6-4ded-2cae-08dc88f1a80a', 14, 41, 2, 0, N'8f653617-033b-4a94-743f-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'1fb8cc80-5223-40b5-2caf-08dc88f1a80a', 0, 42, 3, 0, N'8f653617-033b-4a94-743f-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'3d3230ac-394b-4694-2cb0-08dc88f1a80a', 7, 43, 3, 0, N'8f653617-033b-4a94-743f-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'c992e8f7-c65f-440f-2cb1-08dc88f1a80a', 5, 44, 1, 0, N'8f653617-033b-4a94-743f-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'611cbfed-64da-4bd0-2cb2-08dc88f1a80a', 8, 45, 3, 0, N'8f653617-033b-4a94-743f-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'104241c3-3826-41a0-2cb3-08dc88f1a80a', 11, 46, 1, 1, N'8f653617-033b-4a94-743f-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'13a13390-f518-4bd7-2cb4-08dc88f1a80a', 10, 47, 2, 0, N'8f653617-033b-4a94-743f-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'9aea52d7-e6cf-4556-2cb5-08dc88f1a80a', 12, 48, 1, 0, N'8f653617-033b-4a94-743f-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'7d218ac9-2ee2-405d-2cb6-08dc88f1a80a', 11, 49, 2, 1, N'8f653617-033b-4a94-743f-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'4de8ce86-3a93-4bcd-2cb7-08dc88f1a80a', 4, 50, 3, 1, N'8f653617-033b-4a94-743f-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'41d0f208-d127-4c74-2cb8-08dc88f1a80a', 14, 1, 1, 1, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'348f63d1-d429-4306-2cb9-08dc88f1a80a', 8, 2, 1, 1, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'739229e1-3ae8-4e48-2cba-08dc88f1a80a', 13, 3, 3, 0, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'07ba7a0c-0061-424b-2cbb-08dc88f1a80a', 0, 4, 2, 0, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'a00ba6d5-f628-43e8-2cbc-08dc88f1a80a', 5, 5, 3, 1, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'6745bf5f-a56d-47d0-2cbd-08dc88f1a80a', 2, 6, 1, 1, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'40d99608-1149-49b5-2cbe-08dc88f1a80a', 11, 7, 3, 0, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'd1da9aaf-5fdd-4c66-2cbf-08dc88f1a80a', 10, 8, 3, 1, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'74aba731-a195-44e4-2cc0-08dc88f1a80a', 9, 9, 3, 0, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'8d4aee0f-9591-4540-2cc1-08dc88f1a80a', 8, 10, 1, 0, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'bf712e90-a03d-4499-2cc2-08dc88f1a80a', 2, 11, 1, 0, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'8ea55b46-eb54-4954-2cc3-08dc88f1a80a', 8, 12, 3, 1, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'31c83e30-862c-4fe0-2cc4-08dc88f1a80a', 11, 13, 2, 1, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'b5cc6aa0-2c35-4934-2cc5-08dc88f1a80a', 14, 14, 3, 0, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'73ae9d4c-247e-458f-2cc6-08dc88f1a80a', 3, 15, 1, 1, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'bf96616c-2968-4468-2cc7-08dc88f1a80a', 11, 16, 2, 0, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'5fab9163-e573-484f-2cc8-08dc88f1a80a', 4, 17, 2, 1, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'1e7a7084-f8a7-4607-2cc9-08dc88f1a80a', 12, 18, 1, 1, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'0c8b5543-1fdd-439c-2cca-08dc88f1a80a', 1, 19, 3, 0, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'5e256427-f982-43bd-2ccb-08dc88f1a80a', 9, 20, 1, 0, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'518ea94b-2828-480d-2ccc-08dc88f1a80a', 1, 21, 1, 0, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'8ae7d4dd-43ac-4282-2ccd-08dc88f1a80a', 3, 22, 2, 0, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'301f2e33-eaff-4de5-2cce-08dc88f1a80a', 7, 23, 3, 0, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'2091b43d-17c7-4c2e-2ccf-08dc88f1a80a', 7, 24, 3, 0, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'8263ddd4-7665-4da9-2cd0-08dc88f1a80a', 5, 25, 2, 0, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'9a5ddd3b-eb48-4893-2cd1-08dc88f1a80a', 9, 26, 1, 1, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'63fdc10c-7dff-4694-2cd2-08dc88f1a80a', 13, 27, 3, 1, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'01282ead-5836-4951-2cd3-08dc88f1a80a', 1, 28, 1, 0, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'f918d2fc-b01f-45fe-2cd4-08dc88f1a80a', 5, 29, 2, 0, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'46c1c42e-d44e-4549-2cd5-08dc88f1a80a', 10, 30, 1, 0, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'ae47f365-83c5-4d93-2cd6-08dc88f1a80a', 3, 31, 3, 0, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'6b70f9f8-5d9a-4f2d-2cd7-08dc88f1a80a', 8, 32, 1, 1, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'444d86b0-b5ba-4bb1-2cd8-08dc88f1a80a', 6, 33, 3, 1, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'240bba45-dc29-41d6-2cd9-08dc88f1a80a', 4, 34, 3, 0, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'c833e34d-1f71-4986-2cda-08dc88f1a80a', 0, 35, 2, 1, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'9d39f175-feaa-4610-2cdb-08dc88f1a80a', 6, 36, 3, 1, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'58072ff5-c6e5-4ba2-2cdc-08dc88f1a80a', 12, 37, 3, 0, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'27e2eb92-8293-4397-2cdd-08dc88f1a80a', 11, 38, 1, 1, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'3b7cdb02-cd2f-4987-2cde-08dc88f1a80a', 7, 39, 1, 1, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'accc982e-3fac-4a64-2cdf-08dc88f1a80a', 2, 40, 1, 0, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'ec876b0d-7c9c-4c62-2ce0-08dc88f1a80a', 0, 41, 1, 1, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'f99aeb17-2afb-4924-2ce1-08dc88f1a80a', 10, 42, 2, 1, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'36ea633a-2759-4aed-2ce2-08dc88f1a80a', 12, 43, 1, 0, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'fb3c16bc-9543-4a24-2ce3-08dc88f1a80a', 2, 44, 3, 0, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'0948a67c-2d4c-4545-2ce4-08dc88f1a80a', 4, 45, 1, 0, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'3782a62d-1e03-4974-2ce5-08dc88f1a80a', 12, 46, 1, 1, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'9f1ee4f6-32b4-4a24-2ce6-08dc88f1a80a', 12, 47, 1, 1, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'504d1c9b-67d3-4211-2ce7-08dc88f1a80a', 6, 48, 3, 0, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'5bf9925e-8017-405d-2ce8-08dc88f1a80a', 10, 49, 3, 1, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'2c5d54cb-2cc9-44bb-2ce9-08dc88f1a80a', 8, 50, 2, 0, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
GO
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'ef10ec10-452a-4fa3-2cea-08dc88f1a80a', 13, 51, 2, 0, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'36d9eb5b-acc5-4dfd-2ceb-08dc88f1a80a', 1, 52, 1, 0, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'f1a5579f-5167-4a3f-2cec-08dc88f1a80a', 2, 53, 3, 0, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'1d196d70-37e5-42b3-2ced-08dc88f1a80a', 6, 54, 1, 0, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'9ed9f769-25ff-4bf8-2cee-08dc88f1a80a', 0, 55, 3, 1, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'0e3d89c3-985d-4350-2cef-08dc88f1a80a', 11, 56, 2, 0, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'1ee329a2-bc72-42e6-2cf0-08dc88f1a80a', 5, 57, 2, 0, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'dea0f32c-1dd7-42af-2cf1-08dc88f1a80a', 0, 58, 2, 0, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'0771096f-9792-42a3-2cf2-08dc88f1a80a', 11, 59, 1, 0, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'1894968c-39e5-4f8d-2cf3-08dc88f1a80a', 14, 60, 1, 1, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'48253341-5e4f-4b14-2cf4-08dc88f1a80a', 1, 61, 1, 1, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'303b43b3-b8a8-452b-2cf5-08dc88f1a80a', 5, 62, 3, 1, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'8a4746d0-db0a-433e-2cf6-08dc88f1a80a', 5, 63, 2, 0, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'f6a74ab9-4c8b-4e60-2cf7-08dc88f1a80a', 14, 64, 2, 0, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'ba8adc39-c60d-447d-2cf8-08dc88f1a80a', 10, 65, 2, 0, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'13260b18-9629-4f28-2cf9-08dc88f1a80a', 11, 66, 1, 0, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'06c50d97-e036-478f-2cfa-08dc88f1a80a', 12, 67, 2, 0, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'cd671b1b-077d-4634-2cfb-08dc88f1a80a', 2, 68, 2, 1, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'16478acb-df92-4e59-2cfc-08dc88f1a80a', 7, 69, 3, 0, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'1d637b40-9b1b-4059-2cfd-08dc88f1a80a', 10, 70, 3, 0, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'c520aa33-813e-402a-2cfe-08dc88f1a80a', 14, 71, 3, 1, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'd34ac49b-f31a-4da4-2cff-08dc88f1a80a', 1, 72, 3, 1, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'37e8d7b2-d340-48e7-2d00-08dc88f1a80a', 11, 73, 3, 0, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'fdf7bcd3-1479-4dc1-2d01-08dc88f1a80a', 7, 74, 2, 0, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'5a2af60d-1594-442b-2d02-08dc88f1a80a', 8, 75, 2, 0, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'1d270b2a-b714-4e0f-2d03-08dc88f1a80a', 12, 76, 3, 1, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'06339072-302e-4982-2d04-08dc88f1a80a', 13, 77, 1, 1, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'ac4bf8ee-0fed-4f1d-2d05-08dc88f1a80a', 14, 78, 2, 1, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'f32cc94e-1c5c-499e-2d06-08dc88f1a80a', 5, 79, 1, 1, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'66423cce-0cc9-4d69-2d07-08dc88f1a80a', 10, 80, 3, 0, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'ee5c4527-ed6c-4c2b-2d08-08dc88f1a80a', 11, 81, 1, 0, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'a7bcc9bb-3a5c-4cc9-2d09-08dc88f1a80a', 13, 82, 2, 1, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'3fd2fc24-3a03-42ef-2d0a-08dc88f1a80a', 5, 83, 3, 1, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'd53c8ff3-f87d-458d-2d0b-08dc88f1a80a', 10, 84, 1, 1, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'98989cf2-c81c-45bd-2d0c-08dc88f1a80a', 3, 85, 2, 0, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'8f68e198-f8e1-4b70-2d0d-08dc88f1a80a', 1, 86, 1, 0, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'c2fe484d-05a3-4e5a-2d0e-08dc88f1a80a', 10, 87, 3, 0, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'8ad38002-a9da-46a4-2d0f-08dc88f1a80a', 3, 88, 3, 1, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'13c301b6-fa36-438b-2d10-08dc88f1a80a', 3, 89, 2, 1, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'dbb2a881-91d6-47a3-2d11-08dc88f1a80a', 1, 90, 1, 0, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'8cd46e72-f96d-4d28-2d12-08dc88f1a80a', 9, 91, 2, 0, N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'b78c7ced-1e08-451a-2d13-08dc88f1a80a', 0, 1, 3, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'9c17a72c-b6cc-4b7a-2d14-08dc88f1a80a', 9, 2, 3, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'8ab3aa28-d5f7-4477-2d15-08dc88f1a80a', 10, 3, 1, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'cce59525-5044-4189-2d16-08dc88f1a80a', 4, 4, 1, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'c81f3464-c09e-4ca8-2d17-08dc88f1a80a', 4, 5, 1, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'80961b98-1eca-46b0-2d18-08dc88f1a80a', 1, 6, 1, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'3eac232f-9350-4b8d-2d19-08dc88f1a80a', 9, 7, 1, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'874bf6fe-0836-4505-2d1a-08dc88f1a80a', 1, 8, 2, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'490ccf1c-cb4c-4a26-2d1b-08dc88f1a80a', 5, 9, 3, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'81b64b41-2231-4d7b-2d1c-08dc88f1a80a', 12, 10, 2, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'b5a5979b-2bf1-4c54-2d1d-08dc88f1a80a', 5, 11, 1, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'2ca0a4c2-0334-4b69-2d1e-08dc88f1a80a', 14, 12, 2, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'1543bba8-7208-4c1f-2d1f-08dc88f1a80a', 10, 13, 2, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'5f64fdba-2706-4099-2d20-08dc88f1a80a', 2, 14, 2, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'38e0b08b-b3dd-46a5-2d21-08dc88f1a80a', 9, 15, 1, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'7790ad88-d8a1-4248-2d22-08dc88f1a80a', 3, 16, 1, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'70c2fb6a-911b-4680-2d23-08dc88f1a80a', 0, 17, 2, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'4b55bfc4-0bec-4d73-2d24-08dc88f1a80a', 13, 18, 1, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'e2898757-da0a-4638-2d25-08dc88f1a80a', 13, 19, 1, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'3762ae4f-0633-4da5-2d26-08dc88f1a80a', 8, 20, 3, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'e03cb08c-45b9-4e53-2d27-08dc88f1a80a', 5, 21, 1, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'6c46952c-8044-4cf6-2d28-08dc88f1a80a', 12, 22, 1, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'2b33b151-dec2-441c-2d29-08dc88f1a80a', 10, 23, 1, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'9a9e442d-e0bd-41f0-2d2a-08dc88f1a80a', 2, 24, 2, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'ad4b4b99-5443-4939-2d2b-08dc88f1a80a', 1, 25, 1, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'7ef0ac94-3c94-41b1-2d2c-08dc88f1a80a', 2, 26, 1, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'046c0860-c426-428c-2d2d-08dc88f1a80a', 1, 27, 2, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'efca1403-45ef-464b-2d2e-08dc88f1a80a', 4, 28, 2, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'baad9194-2ab3-4c00-2d2f-08dc88f1a80a', 1, 29, 2, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'2f8e9625-57a4-4110-2d30-08dc88f1a80a', 9, 30, 1, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'63b0af2d-5370-4617-2d31-08dc88f1a80a', 7, 31, 3, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'9385fc32-4ac5-47e4-2d32-08dc88f1a80a', 0, 32, 1, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'29605517-76cf-4b71-2d33-08dc88f1a80a', 7, 33, 1, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'94cebda7-9fcd-4fca-2d34-08dc88f1a80a', 13, 34, 3, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'c90e01ef-d25f-4f28-2d35-08dc88f1a80a', 1, 35, 3, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'0ebcefbf-0dea-486d-2d36-08dc88f1a80a', 14, 36, 2, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'297330f4-1c0a-4487-2d37-08dc88f1a80a', 5, 37, 3, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'3e2fcb74-44a3-4205-2d38-08dc88f1a80a', 6, 38, 1, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'2bd42c5d-e58d-4bea-2d39-08dc88f1a80a', 6, 39, 3, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'24c505c5-2a9d-4676-2d3a-08dc88f1a80a', 4, 40, 1, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'd2d069f0-e0db-4fa2-2d3b-08dc88f1a80a', 11, 41, 2, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'3115572a-3e6f-40d5-2d3c-08dc88f1a80a', 1, 42, 1, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'634d5bff-6f97-4910-2d3d-08dc88f1a80a', 8, 43, 3, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'c79898a8-819c-4cee-2d3e-08dc88f1a80a', 6, 44, 3, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'51d922e7-186c-43c7-2d3f-08dc88f1a80a', 13, 45, 2, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'85c7afe0-7750-4dfa-2d40-08dc88f1a80a', 13, 46, 1, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'61e22f57-7526-4821-2d41-08dc88f1a80a', 8, 47, 2, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'36c3c6db-9014-49d0-2d42-08dc88f1a80a', 14, 48, 1, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'243ddfc4-2d2b-4827-2d43-08dc88f1a80a', 4, 49, 3, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'845d52b7-b75d-4162-2d44-08dc88f1a80a', 7, 50, 1, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'e8d45c1a-a50c-4293-2d45-08dc88f1a80a', 3, 51, 2, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'68c0f2fd-24a6-47db-2d46-08dc88f1a80a', 10, 52, 2, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'404df143-1ed8-43fb-2d47-08dc88f1a80a', 3, 53, 1, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'eac7fb33-0346-40c9-2d48-08dc88f1a80a', 0, 54, 1, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'd4c381e3-b0ae-47d7-2d49-08dc88f1a80a', 13, 55, 2, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'b43daa6b-6eee-4853-2d4a-08dc88f1a80a', 2, 56, 2, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'1356bb25-3856-4b7e-2d4b-08dc88f1a80a', 9, 57, 3, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'da363c06-e21f-4fad-2d4c-08dc88f1a80a', 5, 58, 2, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'83fd3d3a-eaed-43f9-2d4d-08dc88f1a80a', 14, 59, 2, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
GO
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'e2221866-9b1d-46e6-2d4e-08dc88f1a80a', 1, 60, 1, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'e93524bd-d1e4-4781-2d4f-08dc88f1a80a', 14, 61, 2, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'd8fa008c-080f-471d-2d50-08dc88f1a80a', 0, 62, 2, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'431e854f-2bc1-41dc-2d51-08dc88f1a80a', 14, 63, 1, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'ae2e216c-5d35-47de-2d52-08dc88f1a80a', 11, 64, 3, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'1479ee80-c359-41c0-2d53-08dc88f1a80a', 1, 65, 1, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'5ad2c739-0227-4526-2d54-08dc88f1a80a', 14, 66, 2, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'5dbb4d0a-cada-4755-2d55-08dc88f1a80a', 2, 67, 3, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'f31b9965-4657-487f-2d56-08dc88f1a80a', 0, 68, 3, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'fdffbcf9-7e6e-4e51-2d57-08dc88f1a80a', 1, 69, 3, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'25a891dc-3369-4830-2d58-08dc88f1a80a', 9, 70, 3, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'49d0c28f-4543-427e-2d59-08dc88f1a80a', 10, 71, 1, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'7fed96f4-4fb7-4f46-2d5a-08dc88f1a80a', 5, 72, 1, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'c3eefcdf-abc1-46ca-2d5b-08dc88f1a80a', 10, 73, 3, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'4df32444-8c2a-4c75-2d5c-08dc88f1a80a', 7, 74, 1, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'6874b8ea-751a-4456-2d5d-08dc88f1a80a', 1, 75, 2, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'6e3640d1-1404-4ab4-2d5e-08dc88f1a80a', 11, 76, 3, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'97fc70b6-c6d4-45ac-2d5f-08dc88f1a80a', 5, 77, 2, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'1d6fa71d-c164-48b0-2d60-08dc88f1a80a', 9, 78, 1, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'6833b726-2731-454d-2d61-08dc88f1a80a', 11, 79, 3, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'4d3a01c8-8250-4644-2d62-08dc88f1a80a', 11, 80, 2, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'766abdac-af4b-4a20-2d63-08dc88f1a80a', 10, 81, 1, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'6fae3ef5-7c3f-44db-2d64-08dc88f1a80a', 12, 82, 1, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'd1cf443e-1587-4533-2d65-08dc88f1a80a', 2, 83, 3, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'217e33a3-8775-40b2-2d66-08dc88f1a80a', 8, 84, 2, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'1cd9cd35-6d0c-4153-2d67-08dc88f1a80a', 1, 85, 1, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'd8952788-d36c-444f-2d68-08dc88f1a80a', 14, 86, 3, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'8704e324-a175-431e-2d69-08dc88f1a80a', 10, 87, 1, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'37fe03ba-ff66-4aad-2d6a-08dc88f1a80a', 10, 88, 1, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'77b390e1-be20-461b-2d6b-08dc88f1a80a', 14, 89, 2, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'71c1cbd9-e29b-4051-2d6c-08dc88f1a80a', 4, 90, 1, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'f67cd09e-e1bb-4c14-2d6d-08dc88f1a80a', 12, 91, 3, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'01675a78-5fd5-41eb-2d6e-08dc88f1a80a', 13, 92, 2, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'40813df2-d3c6-4e87-2d6f-08dc88f1a80a', 4, 93, 2, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'8516a6f5-2a9a-4127-2d70-08dc88f1a80a', 4, 94, 3, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'26799535-4d65-4637-2d71-08dc88f1a80a', 11, 95, 1, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'4a0b2b29-10bb-4d2c-2d72-08dc88f1a80a', 10, 96, 1, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'85db4be6-3886-48b7-2d73-08dc88f1a80a', 8, 97, 3, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'527f19ff-b71a-43d6-2d74-08dc88f1a80a', 14, 98, 2, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'cbe1247a-b290-4b6c-2d75-08dc88f1a80a', 2, 99, 3, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'9e9476ba-ed97-4900-2d76-08dc88f1a80a', 2, 100, 2, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'b3beaaab-d2fc-49f1-2d77-08dc88f1a80a', 1, 101, 3, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'd9896692-c9bb-4220-2d78-08dc88f1a80a', 4, 102, 1, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'2518d023-e2f1-44bb-2d79-08dc88f1a80a', 3, 103, 3, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'59703ecb-f85a-410d-2d7a-08dc88f1a80a', 12, 104, 1, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'140e71ec-7f19-4d09-2d7b-08dc88f1a80a', 5, 105, 3, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'f95114c7-5036-4b3f-2d7c-08dc88f1a80a', 4, 106, 2, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'918eb56d-b0d4-4026-2d7d-08dc88f1a80a', 0, 107, 2, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'2465b2d7-ffa8-417b-2d7e-08dc88f1a80a', 0, 108, 1, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'f5f9104f-ca27-44b3-2d7f-08dc88f1a80a', 5, 109, 3, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'802bbbd6-3caf-4923-2d80-08dc88f1a80a', 11, 110, 2, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'ac2038ed-8467-4e84-2d81-08dc88f1a80a', 9, 111, 3, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'9744718b-317f-4542-2d82-08dc88f1a80a', 1, 112, 1, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'94899472-008e-45c9-2d83-08dc88f1a80a', 11, 113, 1, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'ddb8a604-2d20-46b4-2d84-08dc88f1a80a', 13, 114, 2, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'fd1435f9-591c-42e2-2d85-08dc88f1a80a', 8, 115, 1, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'02ab9e95-c3fe-4e3e-2d86-08dc88f1a80a', 6, 116, 2, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'5c2d8a76-7ebc-45c7-2d87-08dc88f1a80a', 12, 117, 1, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'c570b620-2e2d-4b99-2d88-08dc88f1a80a', 12, 118, 1, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'efeead58-bf7e-424e-2d89-08dc88f1a80a', 1, 119, 2, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'3c953705-8b04-405e-2d8a-08dc88f1a80a', 1, 120, 2, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'b5c7ed94-4c37-412f-2d8b-08dc88f1a80a', 12, 121, 2, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'17ccc73e-1405-4646-2d8c-08dc88f1a80a', 14, 122, 2, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'c465352a-1179-468f-2d8d-08dc88f1a80a', 11, 123, 2, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'f026e525-d433-4ae6-2d8e-08dc88f1a80a', 2, 124, 3, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'ec1920fd-7d1c-4945-2d8f-08dc88f1a80a', 11, 125, 3, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'e77ae466-2bdc-4ef4-2d90-08dc88f1a80a', 11, 126, 2, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'c3aa5a6e-1533-4a3c-2d91-08dc88f1a80a', 14, 127, 3, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'd5589914-5744-48a2-2d92-08dc88f1a80a', 3, 128, 1, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'00fffca8-b619-4cca-2d93-08dc88f1a80a', 12, 129, 3, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'168c4b7f-a056-4f89-2d94-08dc88f1a80a', 0, 130, 3, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'2cfb9cab-ab50-499f-2d95-08dc88f1a80a', 1, 131, 3, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'5223e521-46ed-410c-2d96-08dc88f1a80a', 0, 132, 3, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'1a5fb668-0a5c-44f6-2d97-08dc88f1a80a', 6, 133, 1, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'338377ab-0b6b-4ea8-2d98-08dc88f1a80a', 13, 134, 2, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'a21057c4-1060-4892-2d99-08dc88f1a80a', 4, 135, 1, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'2152780b-b9ce-4408-2d9a-08dc88f1a80a', 3, 136, 1, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'e6b3ddbd-b747-4947-2d9b-08dc88f1a80a', 0, 137, 1, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'fbbc8a8a-db7b-4e1f-2d9c-08dc88f1a80a', 1, 138, 3, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'37516788-acfd-4762-2d9d-08dc88f1a80a', 5, 139, 3, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'3b1eaf58-b749-4a42-2d9e-08dc88f1a80a', 9, 140, 1, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'13a4dac4-d1ca-4815-2d9f-08dc88f1a80a', 8, 141, 1, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'cf5806e4-9cce-4480-2da0-08dc88f1a80a', 8, 142, 1, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'bc46af7f-79d6-484c-2da1-08dc88f1a80a', 13, 143, 2, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'3c0bcedc-289a-481e-2da2-08dc88f1a80a', 4, 144, 1, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'd8b80e4e-cd91-43c1-2da3-08dc88f1a80a', 3, 145, 3, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'5471d320-3e15-48d8-2da4-08dc88f1a80a', 7, 146, 3, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'307690eb-b4f5-4d73-2da5-08dc88f1a80a', 8, 147, 1, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'405799c7-ae28-4567-2da6-08dc88f1a80a', 4, 148, 2, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'5e2d86ce-10a0-427a-2da7-08dc88f1a80a', 12, 149, 1, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'de21c9ec-c4cc-4ea7-2da8-08dc88f1a80a', 4, 150, 2, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'139bdc2b-f82b-4673-2da9-08dc88f1a80a', 14, 151, 3, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'c07a5d64-3665-4456-2daa-08dc88f1a80a', 0, 152, 2, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'22067cc0-e653-40bb-2dab-08dc88f1a80a', 14, 153, 2, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'e4cdfced-1567-4160-2dac-08dc88f1a80a', 4, 154, 1, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'cb9bd841-ef47-45b6-2dad-08dc88f1a80a', 1, 155, 2, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'40cdb25b-375b-41cb-2dae-08dc88f1a80a', 11, 156, 2, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'9d41a540-e8cd-4214-2daf-08dc88f1a80a', 9, 157, 2, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'eb5fdfff-5f6e-4bda-2db0-08dc88f1a80a', 12, 158, 2, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'cbe2d7c1-7c12-46f7-2db1-08dc88f1a80a', 9, 159, 2, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
GO
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'aa8afaaf-6029-41aa-2db2-08dc88f1a80a', 8, 160, 1, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'acd73c46-64d9-4b3c-2db3-08dc88f1a80a', 14, 161, 3, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'8b445651-1cf2-44bf-2db4-08dc88f1a80a', 14, 162, 3, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'9524f1ea-a2ad-467b-2db5-08dc88f1a80a', 2, 163, 3, 0, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'051b82b3-c520-4000-2db6-08dc88f1a80a', 9, 164, 3, 1, N'1b686dcc-b618-4221-7441-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'7b7d2792-0745-4abb-2db7-08dc88f1a80a', 1, 1, 3, 1, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'9bbe3266-7e36-489c-2db8-08dc88f1a80a', 8, 2, 2, 1, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'3c16023a-124e-40b6-2db9-08dc88f1a80a', 11, 3, 2, 0, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'c061ff3d-00ac-47f4-2dba-08dc88f1a80a', 11, 4, 2, 1, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'27464946-dfca-4f8a-2dbb-08dc88f1a80a', 7, 5, 3, 0, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'0c7cf475-0f21-4a37-2dbc-08dc88f1a80a', 4, 6, 3, 1, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'c32ceec1-d2da-4dbd-2dbd-08dc88f1a80a', 4, 7, 1, 1, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'381a66a8-0b8f-4375-2dbe-08dc88f1a80a', 12, 8, 3, 0, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'1e33a82e-2e07-49a7-2dbf-08dc88f1a80a', 14, 9, 2, 0, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'55033a25-cb52-4694-2dc0-08dc88f1a80a', 8, 10, 2, 1, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'b2cbeef5-a344-4650-2dc1-08dc88f1a80a', 10, 11, 2, 0, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'9332e4be-a6e0-4b5d-2dc2-08dc88f1a80a', 10, 12, 1, 1, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'6416ce67-e216-44ab-2dc3-08dc88f1a80a', 12, 13, 2, 0, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'5704047d-1794-40b9-2dc4-08dc88f1a80a', 8, 14, 2, 0, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'db0c3ac6-a512-4eaa-2dc5-08dc88f1a80a', 5, 15, 1, 0, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'ffc2ebcb-10c7-4312-2dc6-08dc88f1a80a', 12, 16, 2, 1, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'fc0ad6c5-d800-4294-2dc7-08dc88f1a80a', 6, 17, 2, 0, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'353e33e6-4eac-47e5-2dc8-08dc88f1a80a', 14, 18, 2, 1, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'8550efdf-41fa-4435-2dc9-08dc88f1a80a', 2, 19, 1, 1, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'103d5d8d-a0e7-4e77-2dca-08dc88f1a80a', 2, 20, 3, 1, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'48aa05e3-6a2e-41c0-2dcb-08dc88f1a80a', 12, 21, 3, 0, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'e1caff94-d30f-421d-2dcc-08dc88f1a80a', 14, 22, 3, 1, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'868292f2-7d31-418d-2dcd-08dc88f1a80a', 1, 23, 2, 0, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'daa93915-e72c-4cea-2dce-08dc88f1a80a', 6, 24, 1, 1, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'4e4685f4-6492-40e7-2dcf-08dc88f1a80a', 2, 25, 2, 0, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'4fe00b29-4c42-45b7-2dd0-08dc88f1a80a', 7, 26, 1, 0, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'd10833c9-a312-4085-2dd1-08dc88f1a80a', 4, 27, 3, 1, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'0931fe69-40e5-4fe6-2dd2-08dc88f1a80a', 8, 28, 3, 1, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'8237825d-f381-46dd-2dd3-08dc88f1a80a', 8, 29, 3, 0, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'0e766ae7-b1d9-472b-2dd4-08dc88f1a80a', 11, 30, 3, 1, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'296bb7ef-3d94-4de8-2dd5-08dc88f1a80a', 7, 31, 1, 0, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'7be206c2-4308-41a4-2dd6-08dc88f1a80a', 10, 32, 2, 1, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'e6c4d951-46c0-4413-2dd7-08dc88f1a80a', 1, 33, 3, 0, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'7687bacf-bed1-4e3e-2dd8-08dc88f1a80a', 4, 34, 1, 1, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'34232c63-423d-4fa6-2dd9-08dc88f1a80a', 2, 35, 3, 1, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'af032b83-e6ec-452d-2dda-08dc88f1a80a', 8, 36, 3, 0, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'42fe628c-21d0-49fa-2ddb-08dc88f1a80a', 6, 37, 3, 1, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'5b06bcb9-8de4-465d-2ddc-08dc88f1a80a', 1, 38, 1, 1, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'd0d2bc2a-33c3-4197-2ddd-08dc88f1a80a', 2, 39, 1, 1, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'1bfec848-230b-457b-2dde-08dc88f1a80a', 2, 40, 2, 0, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'284e5f44-511d-419e-2ddf-08dc88f1a80a', 14, 41, 3, 0, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'5acb9949-bb12-438f-2de0-08dc88f1a80a', 9, 42, 1, 1, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'99a0bc89-c1ae-4e90-2de1-08dc88f1a80a', 0, 43, 3, 0, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'6bc85940-9de5-46d6-2de2-08dc88f1a80a', 7, 44, 1, 1, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'602d9a68-c19d-47c4-2de3-08dc88f1a80a', 3, 45, 3, 0, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'd6d40892-f93c-4934-2de4-08dc88f1a80a', 3, 46, 3, 1, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'718714a5-f023-40f3-2de5-08dc88f1a80a', 5, 47, 3, 0, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'61aa0426-cf70-4ec1-2de6-08dc88f1a80a', 1, 48, 3, 0, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'8f3c1324-70bf-4d74-2de7-08dc88f1a80a', 13, 49, 2, 0, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'9e24c5f9-00bc-44bf-2de8-08dc88f1a80a', 9, 50, 1, 1, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'301215fe-620c-42c8-2de9-08dc88f1a80a', 13, 51, 2, 1, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'6e6b0021-14ad-4276-2dea-08dc88f1a80a', 9, 52, 1, 1, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'fc35ba4c-b248-4d53-2deb-08dc88f1a80a', 1, 53, 3, 0, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'537ce295-a66b-4369-2dec-08dc88f1a80a', 7, 54, 1, 1, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'a2ceecf0-1b7e-4771-2ded-08dc88f1a80a', 4, 55, 2, 0, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'110c633c-3761-4128-2dee-08dc88f1a80a', 9, 56, 2, 1, N'74458afd-cc2c-485b-7442-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'6ea9f6a5-82be-4656-2def-08dc88f1a80a', 10, 1, 3, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'eabd4f81-bbed-4352-2df0-08dc88f1a80a', 13, 2, 3, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'32b1d2d5-76b2-4628-2df1-08dc88f1a80a', 9, 3, 1, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'56e7c731-3cc3-4fe9-2df2-08dc88f1a80a', 0, 4, 2, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'1cfa4886-c57a-40bd-2df3-08dc88f1a80a', 9, 5, 1, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'3a9d0c06-6865-4503-2df4-08dc88f1a80a', 1, 6, 3, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'48904e36-1b39-48ab-2df5-08dc88f1a80a', 4, 7, 2, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'386e7b72-54d7-4c88-2df6-08dc88f1a80a', 11, 8, 2, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'99eb19d9-a266-472c-2df7-08dc88f1a80a', 12, 9, 3, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'783c221f-8ac9-4390-2df8-08dc88f1a80a', 7, 10, 3, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'a7d5e27b-51fa-4712-2df9-08dc88f1a80a', 0, 11, 1, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'd584ce5f-96ca-49e1-2dfa-08dc88f1a80a', 0, 12, 1, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'dacf12c1-b49e-4dab-2dfb-08dc88f1a80a', 0, 13, 1, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'5358bdb8-0947-45b0-2dfc-08dc88f1a80a', 11, 14, 1, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'd8203a38-08cb-42a0-2dfd-08dc88f1a80a', 2, 15, 2, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'ce15f278-9e48-4ea7-2dfe-08dc88f1a80a', 13, 16, 1, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'decc8d76-90e6-4223-2dff-08dc88f1a80a', 11, 17, 2, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'aca7ff34-450b-4227-2e00-08dc88f1a80a', 10, 18, 2, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'8cea8e7d-691e-446e-2e01-08dc88f1a80a', 3, 19, 1, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'7b980307-652d-4d82-2e02-08dc88f1a80a', 4, 20, 2, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'a1084d3e-aa91-4a05-2e03-08dc88f1a80a', 6, 21, 3, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'88d5e423-1979-4bb8-2e04-08dc88f1a80a', 8, 22, 1, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'799ba0e4-3716-4d54-2e05-08dc88f1a80a', 8, 23, 1, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'7273937a-4278-4afe-2e06-08dc88f1a80a', 11, 24, 3, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'5c591926-ab34-471e-2e07-08dc88f1a80a', 14, 25, 3, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'c03c0812-dd67-400e-2e08-08dc88f1a80a', 3, 26, 2, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'9fe0af0d-c66b-47c6-2e09-08dc88f1a80a', 12, 27, 2, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'82736289-e517-4639-2e0a-08dc88f1a80a', 0, 28, 2, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'252fba42-30c8-4b20-2e0b-08dc88f1a80a', 12, 29, 1, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'faa7197a-48af-4f87-2e0c-08dc88f1a80a', 8, 30, 3, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'c8779997-bce3-4467-2e0d-08dc88f1a80a', 4, 31, 2, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'895e52ac-0218-44fa-2e0e-08dc88f1a80a', 1, 32, 3, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'904b77ea-92b0-43f0-2e0f-08dc88f1a80a', 3, 33, 1, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'e639ab9b-ceb8-4937-2e10-08dc88f1a80a', 4, 34, 3, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'05fbd012-23b3-4216-2e11-08dc88f1a80a', 0, 35, 1, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'9efaa770-d17d-4b87-2e12-08dc88f1a80a', 9, 36, 1, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'd3feb78e-a4bd-4cf6-2e13-08dc88f1a80a', 7, 37, 2, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'ed37c56c-bf61-46e9-2e14-08dc88f1a80a', 8, 38, 3, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'75f65270-f3a8-4f1d-2e15-08dc88f1a80a', 14, 39, 3, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
GO
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'a9638563-b678-4565-2e16-08dc88f1a80a', 4, 40, 2, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'7c7cc25d-84d8-4d17-2e17-08dc88f1a80a', 6, 41, 2, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'621a7813-8591-4571-2e18-08dc88f1a80a', 8, 42, 2, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'9afa677c-7542-4139-2e19-08dc88f1a80a', 6, 43, 3, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'4f0ef1ff-bd1f-4ce3-2e1a-08dc88f1a80a', 12, 44, 1, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'd51712d1-da3b-4944-2e1b-08dc88f1a80a', 0, 45, 1, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'259f04e9-43b4-4042-2e1c-08dc88f1a80a', 12, 46, 3, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'e425f81d-d231-4d18-2e1d-08dc88f1a80a', 13, 47, 2, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'96a7b834-09b0-4180-2e1e-08dc88f1a80a', 1, 48, 1, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'4fa63734-935e-4708-2e1f-08dc88f1a80a', 14, 49, 1, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'dd852feb-0ae9-4798-2e20-08dc88f1a80a', 4, 50, 3, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'34cc9bba-8202-4f24-2e21-08dc88f1a80a', 9, 51, 1, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'e0c1f022-49f5-42e3-2e22-08dc88f1a80a', 13, 52, 3, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'1b93df19-95b8-43c0-2e23-08dc88f1a80a', 7, 53, 2, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'0dcd7e8a-7bdf-40e1-2e24-08dc88f1a80a', 7, 54, 1, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'1ba6c92b-7a54-4f11-2e25-08dc88f1a80a', 14, 55, 1, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'744e4bd6-e386-445f-2e26-08dc88f1a80a', 14, 56, 1, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'bbfedea6-82d9-4432-2e27-08dc88f1a80a', 11, 57, 1, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'fb2e4156-0777-42d9-2e28-08dc88f1a80a', 0, 58, 2, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'17be064e-12e5-4042-2e29-08dc88f1a80a', 2, 59, 3, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'507580b0-392d-4836-2e2a-08dc88f1a80a', 6, 60, 2, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'110e57f7-603b-4293-2e2b-08dc88f1a80a', 5, 61, 1, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'01e0b7a9-420e-4044-2e2c-08dc88f1a80a', 7, 62, 1, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'76cca4d5-6102-418e-2e2d-08dc88f1a80a', 1, 63, 1, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'4f0dd4db-7b29-403e-2e2e-08dc88f1a80a', 1, 64, 1, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'd6cd0b30-b89d-4e06-2e2f-08dc88f1a80a', 13, 65, 2, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'1fd10b01-f919-4470-2e30-08dc88f1a80a', 11, 66, 2, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'36695cfb-6de3-49db-2e31-08dc88f1a80a', 13, 67, 1, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'dc7db456-7446-4a0c-2e32-08dc88f1a80a', 6, 68, 2, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'a996bd11-abca-4fb2-2e33-08dc88f1a80a', 10, 69, 1, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'60bc5647-6822-43b5-2e34-08dc88f1a80a', 11, 70, 1, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'175834cb-da7b-4ea0-2e35-08dc88f1a80a', 3, 71, 3, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'0ff40adf-b46b-40ae-2e36-08dc88f1a80a', 0, 72, 2, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'4b151bf0-ba19-4cdb-2e37-08dc88f1a80a', 10, 73, 3, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'fe5ed81f-298d-467b-2e38-08dc88f1a80a', 1, 74, 3, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'2e681296-c396-4017-2e39-08dc88f1a80a', 5, 75, 2, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'8257bb8a-4c5d-4afb-2e3a-08dc88f1a80a', 4, 76, 3, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'5abc778d-6e24-4b82-2e3b-08dc88f1a80a', 4, 77, 3, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'95fa0a2c-4874-42a4-2e3c-08dc88f1a80a', 10, 78, 2, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'32e1a5f0-9976-4e5d-2e3d-08dc88f1a80a', 14, 79, 1, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'342e9762-cf49-45db-2e3e-08dc88f1a80a', 12, 80, 3, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'4881a7b5-4726-4cfe-2e3f-08dc88f1a80a', 2, 81, 1, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'98438cbc-1c03-4356-2e40-08dc88f1a80a', 1, 82, 1, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'457a668b-b8b8-40db-2e41-08dc88f1a80a', 10, 83, 2, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'1a522e16-0f2d-4a25-2e42-08dc88f1a80a', 2, 84, 2, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'15e452b1-29c8-4d83-2e43-08dc88f1a80a', 13, 85, 1, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'e3695943-2a17-4e57-2e44-08dc88f1a80a', 6, 86, 2, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'ecccf9c6-8fb5-4aaf-2e45-08dc88f1a80a', 6, 87, 2, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'a587e675-1a36-4dbc-2e46-08dc88f1a80a', 9, 88, 2, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'b2ddfef5-313a-4023-2e47-08dc88f1a80a', 2, 89, 3, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'94100dd0-507b-4d6c-2e48-08dc88f1a80a', 10, 90, 1, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'baca9476-1781-49b1-2e49-08dc88f1a80a', 7, 91, 3, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'f1f9a23f-7c9f-4955-2e4a-08dc88f1a80a', 0, 92, 3, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'6e74899e-0a9f-4f87-2e4b-08dc88f1a80a', 14, 93, 2, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'6bf2a0e3-7928-4b7e-2e4c-08dc88f1a80a', 0, 94, 1, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'e33ac336-9232-48d6-2e4d-08dc88f1a80a', 11, 95, 1, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'8e3432b6-99b4-4a4f-2e4e-08dc88f1a80a', 8, 96, 2, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'902c1a63-f182-474a-2e4f-08dc88f1a80a', 4, 97, 3, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'2d59d182-bbd3-4256-2e50-08dc88f1a80a', 2, 98, 2, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'd55f65a6-f98a-4e8c-2e51-08dc88f1a80a', 3, 99, 1, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'18fe9b96-41fd-423a-2e52-08dc88f1a80a', 14, 100, 2, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'395c4528-2eea-4e44-2e53-08dc88f1a80a', 4, 101, 1, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'ebad53a3-e423-4ebb-2e54-08dc88f1a80a', 0, 102, 2, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'ac197e8f-72ad-4152-2e55-08dc88f1a80a', 11, 103, 1, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'3ddefe8e-e899-4521-2e56-08dc88f1a80a', 4, 104, 2, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'1da76e20-df95-4809-2e57-08dc88f1a80a', 12, 105, 2, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'7715c264-d5af-4eca-2e58-08dc88f1a80a', 7, 106, 2, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'e0217216-27ac-4999-2e59-08dc88f1a80a', 6, 107, 2, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'48663a55-49b6-45c2-2e5a-08dc88f1a80a', 6, 108, 3, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'22abd527-2d37-4e6d-2e5b-08dc88f1a80a', 14, 109, 2, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'fd3d9ce8-3911-4b79-2e5c-08dc88f1a80a', 0, 110, 2, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'7a4e2ffc-97e1-4aac-2e5d-08dc88f1a80a', 7, 111, 2, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'62dc8f73-53b9-4958-2e5e-08dc88f1a80a', 12, 112, 3, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'981f5656-4d7b-4f7e-2e5f-08dc88f1a80a', 11, 113, 2, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'57d07e0b-826b-4508-2e60-08dc88f1a80a', 2, 114, 3, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'f03c09b8-0448-429d-2e61-08dc88f1a80a', 14, 115, 2, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'b167c5ad-9f68-4d34-2e62-08dc88f1a80a', 3, 116, 2, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'94bb95f6-c3f4-47fb-2e63-08dc88f1a80a', 14, 117, 3, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'cf36475a-6630-4fe1-2e64-08dc88f1a80a', 13, 118, 3, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'b0a3e617-0ef3-46d7-2e65-08dc88f1a80a', 6, 119, 2, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'f54bcfbd-a90f-4126-2e66-08dc88f1a80a', 4, 120, 2, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'aebab0a0-bf49-4bde-2e67-08dc88f1a80a', 4, 121, 2, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'f7d305c2-520a-4b5d-2e68-08dc88f1a80a', 4, 122, 1, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'763abcf0-eecd-41cf-2e69-08dc88f1a80a', 11, 123, 1, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'1a45a133-be0d-4592-2e6a-08dc88f1a80a', 3, 124, 3, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'c1a5681b-6bf6-4ee2-2e6b-08dc88f1a80a', 1, 125, 2, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'c79e2bca-1b59-47c9-2e6c-08dc88f1a80a', 14, 126, 2, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'0637a246-3cea-448c-2e6d-08dc88f1a80a', 11, 127, 3, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'b15edd53-3141-427e-2e6e-08dc88f1a80a', 13, 128, 3, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'dd89cc39-8dac-4d88-2e6f-08dc88f1a80a', 13, 129, 3, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'257a9199-cb90-4cb3-2e70-08dc88f1a80a', 2, 130, 1, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'4218e617-d3f6-49f9-2e71-08dc88f1a80a', 12, 131, 1, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'99173236-bba2-4ca5-2e72-08dc88f1a80a', 13, 132, 1, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'b27b3e14-d8e7-4335-2e73-08dc88f1a80a', 14, 133, 2, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'273ac8d4-914e-4e09-2e74-08dc88f1a80a', 0, 134, 1, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'0636ff68-e19c-4628-2e75-08dc88f1a80a', 1, 135, 1, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'3bc53830-807a-48ea-2e76-08dc88f1a80a', 6, 136, 2, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'1d667100-46f8-4fa4-2e77-08dc88f1a80a', 7, 137, 2, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'3bd1258a-3c95-44cb-2e78-08dc88f1a80a', 13, 138, 2, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'ed7d6ce5-6b70-4d0a-2e79-08dc88f1a80a', 14, 139, 3, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
GO
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'cf75afdf-7fe3-46a8-2e7a-08dc88f1a80a', 12, 140, 2, 1, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'bc1000d3-e651-4681-2e7b-08dc88f1a80a', 14, 141, 2, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'09f3fceb-e18b-4c33-2e7c-08dc88f1a80a', 3, 142, 1, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'138cc2d9-8057-4130-2e7d-08dc88f1a80a', 10, 143, 1, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'43b0ab7f-15ea-40f2-2e7e-08dc88f1a80a', 1, 144, 1, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
INSERT [dbo].[Apartments] ([ApartmentId], [Floor], [Number], [NumberOfBathrooms], [HasTerrace], [BuildingId]) VALUES (N'0569066e-56b8-4c64-2e7f-08dc88f1a80a', 13, 145, 1, 0, N'919fb8d8-7e31-4c3a-7443-08dc88f1a806')
GO
INSERT [dbo].[Buildings] ([BuildingId], [Name], [Address], [ConstructionCompanyId], [CommonExpenses], [ManagerId]) VALUES (N'8f653617-033b-4a94-743f-08dc88f1a806', N'Movies', N'13480 Finn Highway', N'71e93a03-fd06-48b7-08af-08dc88f0bea2', 844, N'3bc5a1f0-c43a-4a28-14c3-08dc88ef8275')
INSERT [dbo].[Buildings] ([BuildingId], [Name], [Address], [ConstructionCompanyId], [CommonExpenses], [ManagerId]) VALUES (N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806', N'Industrial', N'74661 Leonor Dam', N'71e93a03-fd06-48b7-08af-08dc88f0bea2', 78, NULL)
INSERT [dbo].[Buildings] ([BuildingId], [Name], [Address], [ConstructionCompanyId], [CommonExpenses], [ManagerId]) VALUES (N'1b686dcc-b618-4221-7441-08dc88f1a806', N'solution', N'35911 Wolf Wells', N'9cc160c0-8235-4ec4-08b0-08dc88f0bea2', 51, NULL)
INSERT [dbo].[Buildings] ([BuildingId], [Name], [Address], [ConstructionCompanyId], [CommonExpenses], [ManagerId]) VALUES (N'74458afd-cc2c-485b-7442-08dc88f1a806', N'COM', N'3858 Avis Center', N'9cc160c0-8235-4ec4-08b0-08dc88f0bea2', 327, N'3bc5a1f0-c43a-4a28-14c3-08dc88ef8275')
INSERT [dbo].[Buildings] ([BuildingId], [Name], [Address], [ConstructionCompanyId], [CommonExpenses], [ManagerId]) VALUES (N'919fb8d8-7e31-4c3a-7443-08dc88f1a806', N'Practical', N'737 Harvey Extension', N'9cc160c0-8235-4ec4-08b0-08dc88f0bea2', 106, NULL)
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([ID], [Name], [Description]) VALUES (1, N'Directives', N'Internal')
INSERT [dbo].[Category] ([ID], [Name], [Description]) VALUES (2, N'Operations', N'Direct')
INSERT [dbo].[Category] ([ID], [Name], [Description]) VALUES (3, N'Applications', N'Lead')
INSERT [dbo].[Category] ([ID], [Name], [Description]) VALUES (4, N'Communications', N'Lead')
INSERT [dbo].[Category] ([ID], [Name], [Description]) VALUES (5, N'Web', N'Future')
INSERT [dbo].[Category] ([ID], [Name], [Description]) VALUES (6, N'Assurance', N'Human')
INSERT [dbo].[Category] ([ID], [Name], [Description]) VALUES (7, N'Infrastructure', N'Global')
INSERT [dbo].[Category] ([ID], [Name], [Description]) VALUES (8, N'Intranet', N'Chief')
INSERT [dbo].[Category] ([ID], [Name], [Description]) VALUES (9, N'Security', N'Customer')
INSERT [dbo].[Category] ([ID], [Name], [Description]) VALUES (10, N'Metrics', N'Product')
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
INSERT [dbo].[ConstructionCompanies] ([ConstructionCompanyId], [Name], [ConstructionCompanyAdminId]) VALUES (N'71e93a03-fd06-48b7-08af-08dc88f0bea2', N'Heathcote LLC', N'823dc932-a6f0-4f89-4017-08dc88efedab')
INSERT [dbo].[ConstructionCompanies] ([ConstructionCompanyId], [Name], [ConstructionCompanyAdminId]) VALUES (N'9cc160c0-8235-4ec4-08b0-08dc88f0bea2', N'Kessler - Feest', N'd8b3f144-0163-4ecc-4018-08dc88efedab')
GO
INSERT [dbo].[ConstructionCompanyAdmins] ([Id], [Name], [Email], [Password]) VALUES (N'823dc932-a6f0-4f89-4017-08dc88efedab', N'Verona', N'Anastasia92@gmail.com', N'zIry4zQvXml4ivu')
INSERT [dbo].[ConstructionCompanyAdmins] ([Id], [Name], [Email], [Password]) VALUES (N'd8b3f144-0163-4ecc-4018-08dc88efedab', N'Dovie', N'Nyah35@yahoo.com', N'7MXZdMmsJvF8u3K')
GO
INSERT [dbo].[Invitations] ([InvitationId], [Email], [Name], [ExpirationDate], [Status], [Role]) VALUES (N'7401a525-1ca3-44d9-98c6-08dc88eeb073', N'Austyn_Tillman8@yahoo.com', N'Patience', CAST(N'2024-07-06T00:00:00.0000000' AS DateTime2), N'No aceptada', N'manager')
INSERT [dbo].[Invitations] ([InvitationId], [Email], [Name], [ExpirationDate], [Status], [Role]) VALUES (N'0ee3941c-9b10-4145-98c7-08dc88eeb073', N'Marques24@gmail.com', N'Anika', CAST(N'2024-07-06T00:00:00.0000000' AS DateTime2), N'No aceptada', N'manager')
INSERT [dbo].[Invitations] ([InvitationId], [Email], [Name], [ExpirationDate], [Status], [Role]) VALUES (N'2fe8bc78-158c-4bd6-98c8-08dc88eeb073', N'Loma4@hotmail.com', N'Alena', CAST(N'2024-07-06T00:00:00.0000000' AS DateTime2), N'No aceptada', N'manager')
INSERT [dbo].[Invitations] ([InvitationId], [Email], [Name], [ExpirationDate], [Status], [Role]) VALUES (N'0f828680-acd2-4f23-98c9-08dc88eeb073', N'Floyd_Wisoky@hotmail.com', N'Garth', CAST(N'2024-07-06T00:00:00.0000000' AS DateTime2), N'No aceptada', N'manager')
INSERT [dbo].[Invitations] ([InvitationId], [Email], [Name], [ExpirationDate], [Status], [Role]) VALUES (N'489e5771-523c-4fc8-98ca-08dc88eeb073', N'Derrick.Hintz@gmail.com', N'Rosamond', CAST(N'2024-07-06T00:00:00.0000000' AS DateTime2), N'Aceptada', N'manager')
INSERT [dbo].[Invitations] ([InvitationId], [Email], [Name], [ExpirationDate], [Status], [Role]) VALUES (N'ee8ad198-9247-442b-98cb-08dc88eeb073', N'Nakia79@gmail.com', N'Ansley', CAST(N'2024-07-06T00:00:00.0000000' AS DateTime2), N'Aceptada', N'manager')
INSERT [dbo].[Invitations] ([InvitationId], [Email], [Name], [ExpirationDate], [Status], [Role]) VALUES (N'43b34b85-99e0-4918-98cc-08dc88eeb073', N'Markus26@yahoo.com', N'Dortha', CAST(N'2024-07-06T00:00:00.0000000' AS DateTime2), N'No aceptada', N'manager')
INSERT [dbo].[Invitations] ([InvitationId], [Email], [Name], [ExpirationDate], [Status], [Role]) VALUES (N'f18b6177-601e-45b6-98cd-08dc88eeb073', N'Faye.Doyle39@yahoo.com', N'Alison', CAST(N'2024-06-06T00:00:00.0000000' AS DateTime2), N'No aceptada', N'manager')
INSERT [dbo].[Invitations] ([InvitationId], [Email], [Name], [ExpirationDate], [Status], [Role]) VALUES (N'd61a19bb-9b5b-47d0-98ce-08dc88eeb073', N'Bettye_Lueilwitz@hotmail.com', N'Micaela', CAST(N'2024-06-06T00:00:00.0000000' AS DateTime2), N'No aceptada', N'manager')
INSERT [dbo].[Invitations] ([InvitationId], [Email], [Name], [ExpirationDate], [Status], [Role]) VALUES (N'ecdc3037-86ff-4fb3-98cf-08dc88eeb073', N'Bessie56@gmail.com', N'Easter', CAST(N'2024-06-06T00:00:00.0000000' AS DateTime2), N'No aceptada', N'manager')
INSERT [dbo].[Invitations] ([InvitationId], [Email], [Name], [ExpirationDate], [Status], [Role]) VALUES (N'ec1a97fd-dbdf-429e-98d0-08dc88eeb073', N'Luna_Bosco66@hotmail.com', N'Idell', CAST(N'2024-06-06T00:00:00.0000000' AS DateTime2), N'No aceptada', N'constructioncompanyadmin')
INSERT [dbo].[Invitations] ([InvitationId], [Email], [Name], [ExpirationDate], [Status], [Role]) VALUES (N'f66aa7f4-454d-475e-98d1-08dc88eeb073', N'Coleman.Lubowitz@hotmail.com', N'Amparo', CAST(N'2024-06-06T00:00:00.0000000' AS DateTime2), N'No aceptada', N'constructioncompanyadmin')
INSERT [dbo].[Invitations] ([InvitationId], [Email], [Name], [ExpirationDate], [Status], [Role]) VALUES (N'3348b43c-354e-4b23-98d2-08dc88eeb073', N'Hilda_Howe2@gmail.com', N'Jerald', CAST(N'2024-07-06T00:00:00.0000000' AS DateTime2), N'No aceptada', N'constructioncompanyadmin')
INSERT [dbo].[Invitations] ([InvitationId], [Email], [Name], [ExpirationDate], [Status], [Role]) VALUES (N'74c2b108-c7ee-4999-98d3-08dc88eeb073', N'Evan53@yahoo.com', N'Adela', CAST(N'2024-07-06T00:00:00.0000000' AS DateTime2), N'No aceptada', N'constructioncompanyadmin')
INSERT [dbo].[Invitations] ([InvitationId], [Email], [Name], [ExpirationDate], [Status], [Role]) VALUES (N'25053f28-eeba-4bf7-98d4-08dc88eeb073', N'Anastasia92@gmail.com', N'Verona', CAST(N'2024-07-06T00:00:00.0000000' AS DateTime2), N'Aceptada', N'constructioncompanyadmin')
INSERT [dbo].[Invitations] ([InvitationId], [Email], [Name], [ExpirationDate], [Status], [Role]) VALUES (N'28393bbb-43df-4e72-98d5-08dc88eeb073', N'Nyah35@yahoo.com', N'Dovie', CAST(N'2024-07-06T00:00:00.0000000' AS DateTime2), N'Aceptada', N'constructioncompanyadmin')
INSERT [dbo].[Invitations] ([InvitationId], [Email], [Name], [ExpirationDate], [Status], [Role]) VALUES (N'5b9891e6-61de-4938-98d6-08dc88eeb073', N'Anderson91@hotmail.com', N'Lexus', CAST(N'2024-07-06T00:00:00.0000000' AS DateTime2), N'No aceptada', N'constructioncompanyadmin')
INSERT [dbo].[Invitations] ([InvitationId], [Email], [Name], [ExpirationDate], [Status], [Role]) VALUES (N'906b2154-1068-4dfe-98d7-08dc88eeb073', N'Milton.Will@gmail.com', N'Meggie', CAST(N'2024-07-06T00:00:00.0000000' AS DateTime2), N'No aceptada', N'constructioncompanyadmin')
INSERT [dbo].[Invitations] ([InvitationId], [Email], [Name], [ExpirationDate], [Status], [Role]) VALUES (N'8c163ea3-871c-4ab3-98d8-08dc88eeb073', N'Larue85@hotmail.com', N'Madalyn', CAST(N'2024-07-06T00:00:00.0000000' AS DateTime2), N'No aceptada', N'constructioncompanyadmin')
INSERT [dbo].[Invitations] ([InvitationId], [Email], [Name], [ExpirationDate], [Status], [Role]) VALUES (N'f371a3f2-b2d9-4ae5-98d9-08dc88eeb073', N'Hassan_Labadie@yahoo.com', N'Josiah', CAST(N'2024-07-06T00:00:00.0000000' AS DateTime2), N'No aceptada', N'constructioncompanyadmin')
GO
INSERT [dbo].[Locations] ([BuildingId], [Latitude], [Longitude]) VALUES (N'1b686dcc-b618-4221-7441-08dc88f1a806', -74.3479, 73.3834)
INSERT [dbo].[Locations] ([BuildingId], [Latitude], [Longitude]) VALUES (N'8f653617-033b-4a94-743f-08dc88f1a806', -52.6413, -121.342)
INSERT [dbo].[Locations] ([BuildingId], [Latitude], [Longitude]) VALUES (N'74458afd-cc2c-485b-7442-08dc88f1a806', 28.5038, -88.7659)
INSERT [dbo].[Locations] ([BuildingId], [Latitude], [Longitude]) VALUES (N'919fb8d8-7e31-4c3a-7443-08dc88f1a806', 36.0897, 69.3804)
INSERT [dbo].[Locations] ([BuildingId], [Latitude], [Longitude]) VALUES (N'4a97e3a9-8f67-4c5d-7440-08dc88f1a806', 48.0655, -117.9123)
GO
INSERT [dbo].[MaintenanceStaff] ([ID], [Name], [LastName], [Email], [Password]) VALUES (N'd9966fa0-98d7-4573-a654-55de500b45e5', N'Karson', N'Becker', N'Hiram_Borer@hotmail.com', N'salmon')
INSERT [dbo].[MaintenanceStaff] ([ID], [Name], [LastName], [Email], [Password]) VALUES (N'df13da4a-51e3-42e0-a089-64ed5da59d71', N'Destany', N'Ankunding', N'Juliana_Schamberger@hotmail.com', N'maroon')
INSERT [dbo].[MaintenanceStaff] ([ID], [Name], [LastName], [Email], [Password]) VALUES (N'0c9d062c-b552-469b-8f91-965d6ec5380e', N'Sonny', N'Runolfsdottir', N'Dewayne_Stokes75@yahoo.com', N'silver')
INSERT [dbo].[MaintenanceStaff] ([ID], [Name], [LastName], [Email], [Password]) VALUES (N'cf22af3d-e8e8-4445-9bd7-976acd5f74b1', N'Nelle', N'Schmidt', N'Demetrius_Heaney35@hotmail.com', N'orchid')
INSERT [dbo].[MaintenanceStaff] ([ID], [Name], [LastName], [Email], [Password]) VALUES (N'91056360-28fd-4d81-9bb4-a37eab670638', N'Rick', N'Kuhic', N'Evie.Kutch@hotmail.com', N'violet')
INSERT [dbo].[MaintenanceStaff] ([ID], [Name], [LastName], [Email], [Password]) VALUES (N'019b19c6-f5db-4a0b-92ef-a7f701cfff75', N'Raheem', N'Hand', N'Bailee47@gmail.com', N'maroon')
INSERT [dbo].[MaintenanceStaff] ([ID], [Name], [LastName], [Email], [Password]) VALUES (N'0bae43ad-8b5d-46ad-a4f6-b06d8b974d83', N'Renee', N'Cremin', N'Tomasa_Auer17@yahoo.com', N'silver')
INSERT [dbo].[MaintenanceStaff] ([ID], [Name], [LastName], [Email], [Password]) VALUES (N'3035305a-8544-4c68-9803-d2e253615b85', N'Ernest', N'Strosin', N'Jonatan49@gmail.com', N'lavender')
GO
INSERT [dbo].[Managers] ([ManagerId], [Name], [Email], [Password]) VALUES (N'3bc5a1f0-c43a-4a28-14c3-08dc88ef8275', N'Rosamond', N'Derrick.Hintz@gmail.com', N'NFpxP5FWcB3cjqk')
INSERT [dbo].[Managers] ([ManagerId], [Name], [Email], [Password]) VALUES (N'39bf5921-3ecc-4684-14c4-08dc88ef8275', N'Ansley', N'Nakia79@gmail.com', N'sV8ERYMp0OFiUMD')
GO
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'87415f65-29ab-4222-2c86-08dc88f1a80a', N'Javon', N'Metz', N'Adolfo_Ernser@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'0c057c32-a7d9-4ce2-2c87-08dc88f1a80a', N'Sheridan', N'Orn', N'Lori.Kertzmann27@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'ef7f0b36-fb18-4d5e-2c88-08dc88f1a80a', N'Hope', N'Keeling', N'Laney_Huel60@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'a4bc014c-dc3f-4baf-2c89-08dc88f1a80a', N'Princess', N'Mante', N'Augustine_Kreiger52@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'639322d2-5fc6-4887-2c8a-08dc88f1a80a', N'Ova', N'Harber', N'Dortha.Jacobson@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'e52de014-7861-4102-2c8b-08dc88f1a80a', N'Lillie', N'Bernier', N'Jameson82@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'0bb102a9-85d5-4650-2c8c-08dc88f1a80a', N'Loyce', N'Feeney', N'Antoinette.Wiza@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'eafced6d-b011-4d4f-2c8d-08dc88f1a80a', N'Wayne', N'Monahan', N'Hattie_Kohler@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'0c66dd77-e02b-4122-2c8e-08dc88f1a80a', N'Fabiola', N'Nienow', N'Hayden_Ferry@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'b0e2ba02-6fef-4fb1-2c8f-08dc88f1a80a', N'Cleo', N'Sauer', N'Maudie.Gislason@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'77cdd124-537f-4421-2c90-08dc88f1a80a', N'Ibrahim', N'Anderson', N'Bradford48@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'f84f1df8-391a-4dc7-2c91-08dc88f1a80a', N'Joel', N'Barrows', N'Granville45@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'b13bbd5e-ea7e-4314-2c92-08dc88f1a80a', N'Vivianne', N'Wuckert', N'Alejandrin_Erdman12@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'8ee336f0-dc9a-4bc5-2c93-08dc88f1a80a', N'Hector', N'Bosco', N'Skye79@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'3aa75ed5-04eb-4da2-2c94-08dc88f1a80a', N'Julie', N'Lowe', N'Armando.Doyle@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'cb5b6783-751e-47eb-2c95-08dc88f1a80a', N'Hope', N'Grant', N'Alexandrea_Fritsch68@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'b67b60fe-673f-4e66-2c96-08dc88f1a80a', N'Mitchell', N'Zemlak', N'Dessie_Kuvalis@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'614b78a1-c9cc-413f-2c97-08dc88f1a80a', N'Candido', N'Bauch', N'Jarred_Kautzer@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'6fd5fe4b-1f71-4888-2c98-08dc88f1a80a', N'Jaylan', N'Dach', N'Clint.Lueilwitz@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'd635789c-2242-4b05-2c99-08dc88f1a80a', N'Jasper', N'Larkin', N'Sheridan_Robel94@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'394ba2b4-04ff-4801-2c9a-08dc88f1a80a', N'Billie', N'Sanford', N'Melyna_Huel@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'2bd51d2b-d0b5-4150-2c9b-08dc88f1a80a', N'Flo', N'Gutmann', N'Martina_Mohr@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'ca478e43-67ec-46d8-2c9c-08dc88f1a80a', N'Terrence', N'Bradtke', N'Rogelio.Jones76@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'fbdd5dc6-c265-40a8-2c9d-08dc88f1a80a', N'Helen', N'Haag', N'Dave45@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'0182c18e-4c79-4ef0-2c9e-08dc88f1a80a', N'Alphonso', N'Armstrong', N'Christy.Trantow@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'4abb6808-88ee-4084-2c9f-08dc88f1a80a', N'Haleigh', N'Greenfelder', N'Johnathon_Crist28@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'c5be47dc-0473-464e-2ca0-08dc88f1a80a', N'Catharine', N'Paucek', N'Penelope43@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'da935df8-f640-45ba-2ca1-08dc88f1a80a', N'Mafalda', N'Smitham', N'Scarlett30@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'08977937-369c-496b-2ca2-08dc88f1a80a', N'Carley', N'Abshire', N'Lucious.Dare28@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'b4cfcddd-04bf-4749-2ca3-08dc88f1a80a', N'Velda', N'Schulist', N'Stone.Adams@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'fc4183de-80ff-4af1-2ca4-08dc88f1a80a', N'Edyth', N'Reichel', N'Lorenzo_Weber87@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'15a04be8-3fcc-4311-2ca5-08dc88f1a80a', N'Rolando', N'McCullough', N'Price16@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'882e5101-5041-4318-2ca6-08dc88f1a80a', N'Tyra', N'Bartell', N'Burnice_Funk@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'f47a9763-dca6-44c4-2ca7-08dc88f1a80a', N'Isaiah', N'Zieme', N'Ashton14@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'c2f62e70-b716-4d6c-2ca8-08dc88f1a80a', N'Ola', N'Hills', N'Wendy1@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'f1a8cddd-0af0-43d7-2ca9-08dc88f1a80a', N'Dejon', N'Bradtke', N'Rey_Koepp95@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'967a0c62-dee0-42a7-2caa-08dc88f1a80a', N'Consuelo', N'Gaylord', N'Miguel_Schroeder@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'6e02b154-11da-41b8-2cab-08dc88f1a80a', N'Domingo', N'Schultz', N'Winifred34@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'b89129c2-4e5c-4b2f-2cac-08dc88f1a80a', N'Asa', N'Klein', N'Avery49@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'49d6772d-7d5b-4da6-2cad-08dc88f1a80a', N'Eli', N'Herman', N'Maximillian29@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'd922e8f3-39b6-4ded-2cae-08dc88f1a80a', N'Alex', N'Kunze', N'Palma.Jakubowski36@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'1fb8cc80-5223-40b5-2caf-08dc88f1a80a', N'Adeline', N'Pfeffer', N'German_Hahn@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'3d3230ac-394b-4694-2cb0-08dc88f1a80a', N'Hester', N'Koelpin', N'Hattie.Morissette@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'c992e8f7-c65f-440f-2cb1-08dc88f1a80a', N'Fabian', N'Stehr', N'Dulce58@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'611cbfed-64da-4bd0-2cb2-08dc88f1a80a', N'Bette', N'Sawayn', N'Grant25@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'104241c3-3826-41a0-2cb3-08dc88f1a80a', N'Nova', N'Gutmann', N'Marge26@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'13a13390-f518-4bd7-2cb4-08dc88f1a80a', N'Nico', N'Mayert', N'Bernice.Schultz40@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'9aea52d7-e6cf-4556-2cb5-08dc88f1a80a', N'Vince', N'Cassin', N'Maryjane83@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'7d218ac9-2ee2-405d-2cb6-08dc88f1a80a', N'Kian', N'Johns', N'Marion85@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'4de8ce86-3a93-4bcd-2cb7-08dc88f1a80a', N'Rocky', N'Gibson', N'Naomie57@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'41d0f208-d127-4c74-2cb8-08dc88f1a80a', N'Rashawn', N'Wintheiser', N'Jerod62@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'348f63d1-d429-4306-2cb9-08dc88f1a80a', N'Jaida', N'Block', N'Hector_Feeney@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'739229e1-3ae8-4e48-2cba-08dc88f1a80a', N'Rachael', N'Leannon', N'Werner.Parker38@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'07ba7a0c-0061-424b-2cbb-08dc88f1a80a', N'Eldora', N'Haag', N'Brennon_Cormier39@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'a00ba6d5-f628-43e8-2cbc-08dc88f1a80a', N'Randal', N'Donnelly', N'Kelley.Runolfsdottir@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'6745bf5f-a56d-47d0-2cbd-08dc88f1a80a', N'Cristopher', N'Blick', N'Edmond_Cole91@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'40d99608-1149-49b5-2cbe-08dc88f1a80a', N'Bailey', N'White', N'Rowan_Kling@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'd1da9aaf-5fdd-4c66-2cbf-08dc88f1a80a', N'Buford', N'Lueilwitz', N'Veda77@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'74aba731-a195-44e4-2cc0-08dc88f1a80a', N'Crystal', N'Bogan', N'Laney.Streich11@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'8d4aee0f-9591-4540-2cc1-08dc88f1a80a', N'Nellie', N'Fay', N'Melvin.Paucek@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'bf712e90-a03d-4499-2cc2-08dc88f1a80a', N'Trystan', N'Howell', N'Jay.Collins@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'8ea55b46-eb54-4954-2cc3-08dc88f1a80a', N'Gay', N'Bogan', N'Vaughn_Stoltenberg12@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'31c83e30-862c-4fe0-2cc4-08dc88f1a80a', N'Reanna', N'Stiedemann', N'Selena_Medhurst78@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'b5cc6aa0-2c35-4934-2cc5-08dc88f1a80a', N'Lenna', N'Dietrich', N'Elisha_Fadel@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'73ae9d4c-247e-458f-2cc6-08dc88f1a80a', N'Carolyn', N'Rath', N'Laverna47@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'bf96616c-2968-4468-2cc7-08dc88f1a80a', N'Adelbert', N'Wilderman', N'Joe35@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'5fab9163-e573-484f-2cc8-08dc88f1a80a', N'Roosevelt', N'O''Kon', N'Cordelia_Schuppe98@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'1e7a7084-f8a7-4607-2cc9-08dc88f1a80a', N'Felicity', N'Lebsack', N'Piper_Bailey54@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'0c8b5543-1fdd-439c-2cca-08dc88f1a80a', N'Niko', N'Huel', N'Cora.Murray45@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'5e256427-f982-43bd-2ccb-08dc88f1a80a', N'Meghan', N'Monahan', N'Hershel51@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'518ea94b-2828-480d-2ccc-08dc88f1a80a', N'Rebekah', N'Schowalter', N'Arely_Mohr39@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'8ae7d4dd-43ac-4282-2ccd-08dc88f1a80a', N'Evert', N'Medhurst', N'Skyla.Koss@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'301f2e33-eaff-4de5-2cce-08dc88f1a80a', N'Bettye', N'Labadie', N'Jamey.Stokes@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'2091b43d-17c7-4c2e-2ccf-08dc88f1a80a', N'Rosalind', N'Schinner', N'Elliot52@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'8263ddd4-7665-4da9-2cd0-08dc88f1a80a', N'Brook', N'Homenick', N'Westley12@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'9a5ddd3b-eb48-4893-2cd1-08dc88f1a80a', N'Madisyn', N'Langosh', N'Constantin43@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'63fdc10c-7dff-4694-2cd2-08dc88f1a80a', N'Delphine', N'Hayes', N'Blaze_Beier@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'01282ead-5836-4951-2cd3-08dc88f1a80a', N'Lura', N'Schiller', N'Wilfrid62@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'f918d2fc-b01f-45fe-2cd4-08dc88f1a80a', N'Audreanne', N'Gislason', N'Thelma.Reinger@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'46c1c42e-d44e-4549-2cd5-08dc88f1a80a', N'Markus', N'Morar', N'Daisy16@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'ae47f365-83c5-4d93-2cd6-08dc88f1a80a', N'Delores', N'Zulauf', N'Otilia_Cummerata66@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'6b70f9f8-5d9a-4f2d-2cd7-08dc88f1a80a', N'Nash', N'Jacobi', N'Zena67@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'444d86b0-b5ba-4bb1-2cd8-08dc88f1a80a', N'Lyric', N'Jacobi', N'Davonte.Walker@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'240bba45-dc29-41d6-2cd9-08dc88f1a80a', N'Quinten', N'Renner', N'Yvonne_Feeney@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'c833e34d-1f71-4986-2cda-08dc88f1a80a', N'Augustus', N'Fahey', N'Zora.Schamberger4@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'9d39f175-feaa-4610-2cdb-08dc88f1a80a', N'Jocelyn', N'Sporer', N'Eloy54@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'58072ff5-c6e5-4ba2-2cdc-08dc88f1a80a', N'Jana', N'Daugherty', N'Emelia61@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'27e2eb92-8293-4397-2cdd-08dc88f1a80a', N'Ashleigh', N'Volkman', N'Osborne_Ondricka85@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'3b7cdb02-cd2f-4987-2cde-08dc88f1a80a', N'Stephany', N'Kassulke', N'Melba_Koch89@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'accc982e-3fac-4a64-2cdf-08dc88f1a80a', N'Israel', N'Stehr', N'Valentina_Franecki54@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'ec876b0d-7c9c-4c62-2ce0-08dc88f1a80a', N'Creola', N'Leffler', N'Jack40@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'f99aeb17-2afb-4924-2ce1-08dc88f1a80a', N'Glenda', N'Christiansen', N'Queen_Hermiston@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'36ea633a-2759-4aed-2ce2-08dc88f1a80a', N'Wellington', N'Padberg', N'Domenica.Stiedemann@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'fb3c16bc-9543-4a24-2ce3-08dc88f1a80a', N'Nestor', N'Moen', N'Rosamond_Steuber39@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'0948a67c-2d4c-4545-2ce4-08dc88f1a80a', N'Joanie', N'Mueller', N'Clotilde.Monahan@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'3782a62d-1e03-4974-2ce5-08dc88f1a80a', N'Larissa', N'Rohan', N'Casimir_Hermann@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'9f1ee4f6-32b4-4a24-2ce6-08dc88f1a80a', N'Cooper', N'Gorczany', N'Zetta_Cassin@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'504d1c9b-67d3-4211-2ce7-08dc88f1a80a', N'Sydnie', N'Hessel', N'Riley20@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'5bf9925e-8017-405d-2ce8-08dc88f1a80a', N'Aryanna', N'Shanahan', N'Carlee93@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'2c5d54cb-2cc9-44bb-2ce9-08dc88f1a80a', N'Alvina', N'Altenwerth', N'Jaime.Witting@gmail.com')
GO
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'ef10ec10-452a-4fa3-2cea-08dc88f1a80a', N'Marina', N'Flatley', N'Robert_Osinski27@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'36d9eb5b-acc5-4dfd-2ceb-08dc88f1a80a', N'Electa', N'Hagenes', N'Marquis_Ortiz35@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'f1a5579f-5167-4a3f-2cec-08dc88f1a80a', N'Elody', N'Barrows', N'Dalton.Pagac77@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'1d196d70-37e5-42b3-2ced-08dc88f1a80a', N'Woodrow', N'Walker', N'Aliyah30@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'9ed9f769-25ff-4bf8-2cee-08dc88f1a80a', N'Gregg', N'Kassulke', N'Rebekah.Kling@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'0e3d89c3-985d-4350-2cef-08dc88f1a80a', N'Cletus', N'Lemke', N'Grant_Watsica@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'1ee329a2-bc72-42e6-2cf0-08dc88f1a80a', N'Rae', N'Bode', N'Norval61@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'dea0f32c-1dd7-42af-2cf1-08dc88f1a80a', N'Edwin', N'Friesen', N'Earnest90@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'0771096f-9792-42a3-2cf2-08dc88f1a80a', N'Payton', N'White', N'Morris.Moen32@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'1894968c-39e5-4f8d-2cf3-08dc88f1a80a', N'Estevan', N'Weimann', N'Skyla7@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'48253341-5e4f-4b14-2cf4-08dc88f1a80a', N'Aurore', N'Heidenreich', N'Cecelia89@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'303b43b3-b8a8-452b-2cf5-08dc88f1a80a', N'Wilfredo', N'Thompson', N'Gabriella_Nikolaus15@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'8a4746d0-db0a-433e-2cf6-08dc88f1a80a', N'Sofia', N'D''Amore', N'Carmel.Dickens47@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'f6a74ab9-4c8b-4e60-2cf7-08dc88f1a80a', N'Lincoln', N'Bruen', N'Erica_Labadie16@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'ba8adc39-c60d-447d-2cf8-08dc88f1a80a', N'Karelle', N'Gerlach', N'Frederic_Haag@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'13260b18-9629-4f28-2cf9-08dc88f1a80a', N'Gage', N'Gaylord', N'Isom.Howell@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'06c50d97-e036-478f-2cfa-08dc88f1a80a', N'Aidan', N'Turner', N'Bo.Kub95@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'cd671b1b-077d-4634-2cfb-08dc88f1a80a', N'Joan', N'Kovacek', N'Jamil.Dare67@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'16478acb-df92-4e59-2cfc-08dc88f1a80a', N'Pamela', N'Baumbach', N'Litzy_Stoltenberg@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'1d637b40-9b1b-4059-2cfd-08dc88f1a80a', N'Millie', N'Larkin', N'Johnathon_Grady16@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'c520aa33-813e-402a-2cfe-08dc88f1a80a', N'Jaydon', N'Beahan', N'Kari.Gottlieb35@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'd34ac49b-f31a-4da4-2cff-08dc88f1a80a', N'Conrad', N'Bednar', N'Bernie_Dickinson70@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'37e8d7b2-d340-48e7-2d00-08dc88f1a80a', N'Adelia', N'Deckow', N'Jaylin43@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'fdf7bcd3-1479-4dc1-2d01-08dc88f1a80a', N'Elwyn', N'Rodriguez', N'Mikel_Upton17@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'5a2af60d-1594-442b-2d02-08dc88f1a80a', N'Gerardo', N'Nolan', N'Conrad.Heathcote@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'1d270b2a-b714-4e0f-2d03-08dc88f1a80a', N'Rossie', N'Lemke', N'Ulises17@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'06339072-302e-4982-2d04-08dc88f1a80a', N'Boyd', N'Hyatt', N'Corrine_Considine53@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'ac4bf8ee-0fed-4f1d-2d05-08dc88f1a80a', N'Johnnie', N'Wunsch', N'Ottilie_Kerluke32@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'f32cc94e-1c5c-499e-2d06-08dc88f1a80a', N'Gregoria', N'Altenwerth', N'Mervin.Yost@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'66423cce-0cc9-4d69-2d07-08dc88f1a80a', N'Stanley', N'Deckow', N'Jennifer82@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'ee5c4527-ed6c-4c2b-2d08-08dc88f1a80a', N'Colin', N'Crooks', N'Darion.Koepp@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'a7bcc9bb-3a5c-4cc9-2d09-08dc88f1a80a', N'Beryl', N'Schmeler', N'Rasheed_Swift@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'3fd2fc24-3a03-42ef-2d0a-08dc88f1a80a', N'Madeline', N'Hagenes', N'Felix_Greenholt56@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'd53c8ff3-f87d-458d-2d0b-08dc88f1a80a', N'Vaughn', N'Pollich', N'Millie_Murray@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'98989cf2-c81c-45bd-2d0c-08dc88f1a80a', N'Rashawn', N'Torp', N'Rex50@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'8f68e198-f8e1-4b70-2d0d-08dc88f1a80a', N'Lonzo', N'Kuphal', N'Magdalen.Padberg@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'c2fe484d-05a3-4e5a-2d0e-08dc88f1a80a', N'Eula', N'Ledner', N'Kaela_Ritchie7@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'8ad38002-a9da-46a4-2d0f-08dc88f1a80a', N'Harry', N'Stiedemann', N'Arvid_Strosin33@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'13c301b6-fa36-438b-2d10-08dc88f1a80a', N'Nikolas', N'Reilly', N'Barry_Gutmann@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'dbb2a881-91d6-47a3-2d11-08dc88f1a80a', N'Mona', N'Wehner', N'Clement88@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'8cd46e72-f96d-4d28-2d12-08dc88f1a80a', N'Solon', N'Wilderman', N'Adonis70@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'b78c7ced-1e08-451a-2d13-08dc88f1a80a', N'Jaylen', N'Johnson', N'Brayan94@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'9c17a72c-b6cc-4b7a-2d14-08dc88f1a80a', N'Hunter', N'Pollich', N'Beatrice_Skiles@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'8ab3aa28-d5f7-4477-2d15-08dc88f1a80a', N'Manuela', N'Haag', N'Trevion_Hahn1@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'cce59525-5044-4189-2d16-08dc88f1a80a', N'Mariana', N'Walter', N'Edgardo_Prohaska@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'c81f3464-c09e-4ca8-2d17-08dc88f1a80a', N'Dominique', N'Hudson', N'Jerrod.Harris@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'80961b98-1eca-46b0-2d18-08dc88f1a80a', N'Marlee', N'Connelly', N'Casimir6@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'3eac232f-9350-4b8d-2d19-08dc88f1a80a', N'Travon', N'Haley', N'Arvel.Windler91@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'874bf6fe-0836-4505-2d1a-08dc88f1a80a', N'Garnett', N'Orn', N'General_Dooley@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'490ccf1c-cb4c-4a26-2d1b-08dc88f1a80a', N'Rhoda', N'Denesik', N'Barton_Daugherty@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'81b64b41-2231-4d7b-2d1c-08dc88f1a80a', N'Winona', N'Lowe', N'Jeffrey6@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'b5a5979b-2bf1-4c54-2d1d-08dc88f1a80a', N'Antwan', N'Kassulke', N'Eleonore.Hane@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'2ca0a4c2-0334-4b69-2d1e-08dc88f1a80a', N'Alena', N'Von', N'Terrence_Pfannerstill54@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'1543bba8-7208-4c1f-2d1f-08dc88f1a80a', N'Jedediah', N'Altenwerth', N'Imani_Hintz85@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'5f64fdba-2706-4099-2d20-08dc88f1a80a', N'Roma', N'Schulist', N'Corbin.Prohaska72@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'38e0b08b-b3dd-46a5-2d21-08dc88f1a80a', N'Johanna', N'Koelpin', N'Magdalena.Bins@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'7790ad88-d8a1-4248-2d22-08dc88f1a80a', N'Imani', N'Wisozk', N'Ernest66@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'70c2fb6a-911b-4680-2d23-08dc88f1a80a', N'Rocky', N'Quigley', N'Era_Champlin44@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'4b55bfc4-0bec-4d73-2d24-08dc88f1a80a', N'Ardith', N'Hauck', N'Kariane.Blick@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'e2898757-da0a-4638-2d25-08dc88f1a80a', N'Charley', N'Carter', N'Liliane0@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'3762ae4f-0633-4da5-2d26-08dc88f1a80a', N'Braden', N'Turcotte', N'Jedediah_Skiles93@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'e03cb08c-45b9-4e53-2d27-08dc88f1a80a', N'Americo', N'Bartoletti', N'Filomena23@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'6c46952c-8044-4cf6-2d28-08dc88f1a80a', N'Vanessa', N'Collins', N'Zelda.Gutkowski@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'2b33b151-dec2-441c-2d29-08dc88f1a80a', N'Zackary', N'Treutel', N'Maverick_Heathcote51@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'9a9e442d-e0bd-41f0-2d2a-08dc88f1a80a', N'Zula', N'Blanda', N'Elyssa0@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'ad4b4b99-5443-4939-2d2b-08dc88f1a80a', N'Maynard', N'Kutch', N'Cristina_Feil51@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'7ef0ac94-3c94-41b1-2d2c-08dc88f1a80a', N'Pattie', N'Halvorson', N'Jasper.Green@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'046c0860-c426-428c-2d2d-08dc88f1a80a', N'Fleta', N'O''Kon', N'Wellington.Schamberger@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'efca1403-45ef-464b-2d2e-08dc88f1a80a', N'Charlie', N'Jones', N'Kelton_Nitzsche@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'baad9194-2ab3-4c00-2d2f-08dc88f1a80a', N'Cyril', N'Marquardt', N'Afton_Kessler@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'2f8e9625-57a4-4110-2d30-08dc88f1a80a', N'Frederique', N'Mosciski', N'Vida_Cronin@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'63b0af2d-5370-4617-2d31-08dc88f1a80a', N'Matilde', N'King', N'Tommie65@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'9385fc32-4ac5-47e4-2d32-08dc88f1a80a', N'Stanford', N'Smitham', N'Richie.Rolfson38@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'29605517-76cf-4b71-2d33-08dc88f1a80a', N'Quinton', N'Ernser', N'Novella.Veum61@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'94cebda7-9fcd-4fca-2d34-08dc88f1a80a', N'Reginald', N'Crist', N'Boris_Harber@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'c90e01ef-d25f-4f28-2d35-08dc88f1a80a', N'Gussie', N'Greenholt', N'Percy_Boehm@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'0ebcefbf-0dea-486d-2d36-08dc88f1a80a', N'Aaliyah', N'Goldner', N'Amie_Deckow@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'297330f4-1c0a-4487-2d37-08dc88f1a80a', N'Adolph', N'Terry', N'Gonzalo60@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'3e2fcb74-44a3-4205-2d38-08dc88f1a80a', N'Amie', N'Kihn', N'Sunny_Lowe56@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'2bd42c5d-e58d-4bea-2d39-08dc88f1a80a', N'Bernadette', N'Marvin', N'Scotty14@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'24c505c5-2a9d-4676-2d3a-08dc88f1a80a', N'Greta', N'Borer', N'Jaquelin59@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'd2d069f0-e0db-4fa2-2d3b-08dc88f1a80a', N'Jazlyn', N'Hodkiewicz', N'Haleigh81@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'3115572a-3e6f-40d5-2d3c-08dc88f1a80a', N'Carlotta', N'Hackett', N'Anibal.Raynor@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'634d5bff-6f97-4910-2d3d-08dc88f1a80a', N'Zakary', N'Adams', N'Jackie.Wisoky17@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'c79898a8-819c-4cee-2d3e-08dc88f1a80a', N'Geraldine', N'Bashirian', N'Isabel76@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'51d922e7-186c-43c7-2d3f-08dc88f1a80a', N'Gregorio', N'Crona', N'Magdalena43@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'85c7afe0-7750-4dfa-2d40-08dc88f1a80a', N'Arnoldo', N'Bartoletti', N'Henriette_Bechtelar@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'61e22f57-7526-4821-2d41-08dc88f1a80a', N'Maximillian', N'O''Kon', N'Ethan_McGlynn@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'36c3c6db-9014-49d0-2d42-08dc88f1a80a', N'Woodrow', N'Keeling', N'Maritza.Rath@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'243ddfc4-2d2b-4827-2d43-08dc88f1a80a', N'Josefa', N'Cruickshank', N'Rahul.Bernier26@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'845d52b7-b75d-4162-2d44-08dc88f1a80a', N'Vita', N'Gutkowski', N'Adolfo.Wintheiser@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'e8d45c1a-a50c-4293-2d45-08dc88f1a80a', N'Rosalinda', N'Cormier', N'Aliza.MacGyver90@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'68c0f2fd-24a6-47db-2d46-08dc88f1a80a', N'Hubert', N'Konopelski', N'Hildegard.Altenwerth31@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'404df143-1ed8-43fb-2d47-08dc88f1a80a', N'Jaron', N'Bernier', N'Emmanuel.Medhurst@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'eac7fb33-0346-40c9-2d48-08dc88f1a80a', N'Makayla', N'Carroll', N'Era60@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'd4c381e3-b0ae-47d7-2d49-08dc88f1a80a', N'Nichole', N'Mills', N'Bridie54@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'b43daa6b-6eee-4853-2d4a-08dc88f1a80a', N'Markus', N'Miller', N'Rubie_Larson34@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'1356bb25-3856-4b7e-2d4b-08dc88f1a80a', N'Nelson', N'Dicki', N'Sterling.Gislason@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'da363c06-e21f-4fad-2d4c-08dc88f1a80a', N'Abbey', N'Ondricka', N'Marcelo.Schmitt@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'83fd3d3a-eaed-43f9-2d4d-08dc88f1a80a', N'Reginald', N'Skiles', N'Trinity.Langworth27@gmail.com')
GO
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'e2221866-9b1d-46e6-2d4e-08dc88f1a80a', N'Otilia', N'Gutkowski', N'Justus.Vandervort@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'e93524bd-d1e4-4781-2d4f-08dc88f1a80a', N'Rebecca', N'Rowe', N'Amanda_Beier@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'd8fa008c-080f-471d-2d50-08dc88f1a80a', N'Alan', N'Spencer', N'Sandy17@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'431e854f-2bc1-41dc-2d51-08dc88f1a80a', N'Marjory', N'Heidenreich', N'Helga.Hansen79@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'ae2e216c-5d35-47de-2d52-08dc88f1a80a', N'Leonel', N'Lueilwitz', N'Lottie.Pfeffer@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'1479ee80-c359-41c0-2d53-08dc88f1a80a', N'Hershel', N'Balistreri', N'Arden34@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'5ad2c739-0227-4526-2d54-08dc88f1a80a', N'Leonardo', N'Jerde', N'Harley_Bosco@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'5dbb4d0a-cada-4755-2d55-08dc88f1a80a', N'Janet', N'Maggio', N'Larissa21@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'f31b9965-4657-487f-2d56-08dc88f1a80a', N'Sadye', N'Stark', N'Edgardo_Zulauf54@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'fdffbcf9-7e6e-4e51-2d57-08dc88f1a80a', N'Ivah', N'Lehner', N'Clint93@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'25a891dc-3369-4830-2d58-08dc88f1a80a', N'Magnolia', N'Denesik', N'Brooke72@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'49d0c28f-4543-427e-2d59-08dc88f1a80a', N'Marguerite', N'Rosenbaum', N'Reanna.Erdman50@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'7fed96f4-4fb7-4f46-2d5a-08dc88f1a80a', N'Brett', N'Yundt', N'Thora14@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'c3eefcdf-abc1-46ca-2d5b-08dc88f1a80a', N'Luisa', N'Schmitt', N'Luisa.Friesen19@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'4df32444-8c2a-4c75-2d5c-08dc88f1a80a', N'Vickie', N'Schmeler', N'Raheem35@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'6874b8ea-751a-4456-2d5d-08dc88f1a80a', N'Ebba', N'Welch', N'Jerad.Heller64@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'6e3640d1-1404-4ab4-2d5e-08dc88f1a80a', N'Gerson', N'Moen', N'Scot.Mosciski@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'97fc70b6-c6d4-45ac-2d5f-08dc88f1a80a', N'Hans', N'Blick', N'Adalberto.Lakin@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'1d6fa71d-c164-48b0-2d60-08dc88f1a80a', N'Bernie', N'Parker', N'Raymundo_Kuphal@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'6833b726-2731-454d-2d61-08dc88f1a80a', N'Mya', N'McGlynn', N'Stacey_Jakubowski80@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'4d3a01c8-8250-4644-2d62-08dc88f1a80a', N'Merle', N'Kassulke', N'Merle_Dooley@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'766abdac-af4b-4a20-2d63-08dc88f1a80a', N'Ralph', N'Koelpin', N'Asa74@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'6fae3ef5-7c3f-44db-2d64-08dc88f1a80a', N'Savanna', N'Turner', N'Janae.Balistreri@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'd1cf443e-1587-4533-2d65-08dc88f1a80a', N'Magali', N'Carroll', N'Hortense23@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'217e33a3-8775-40b2-2d66-08dc88f1a80a', N'Tess', N'Pagac', N'Shayna47@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'1cd9cd35-6d0c-4153-2d67-08dc88f1a80a', N'Jorge', N'Kub', N'Wilburn.Ernser0@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'd8952788-d36c-444f-2d68-08dc88f1a80a', N'Solon', N'Moore', N'Antonietta.Runolfsdottir1@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'8704e324-a175-431e-2d69-08dc88f1a80a', N'Rachael', N'Mraz', N'Palma_Little87@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'37fe03ba-ff66-4aad-2d6a-08dc88f1a80a', N'Davon', N'Schultz', N'Kaelyn5@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'77b390e1-be20-461b-2d6b-08dc88f1a80a', N'Georgianna', N'Hoppe', N'Lura53@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'71c1cbd9-e29b-4051-2d6c-08dc88f1a80a', N'Kaitlyn', N'Welch', N'Jacynthe93@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'f67cd09e-e1bb-4c14-2d6d-08dc88f1a80a', N'Herta', N'Mraz', N'Sabryna_Herman@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'01675a78-5fd5-41eb-2d6e-08dc88f1a80a', N'Camden', N'Walter', N'Ericka.Mertz17@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'40813df2-d3c6-4e87-2d6f-08dc88f1a80a', N'Rosemarie', N'Champlin', N'Blair.Gislason47@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'8516a6f5-2a9a-4127-2d70-08dc88f1a80a', N'Paolo', N'Kilback', N'Teagan_Rodriguez75@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'26799535-4d65-4637-2d71-08dc88f1a80a', N'Judah', N'Denesik', N'Israel61@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'4a0b2b29-10bb-4d2c-2d72-08dc88f1a80a', N'Itzel', N'Okuneva', N'Muriel_Cole11@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'85db4be6-3886-48b7-2d73-08dc88f1a80a', N'Odie', N'Langosh', N'Archibald.Gutmann@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'527f19ff-b71a-43d6-2d74-08dc88f1a80a', N'Holden', N'Padberg', N'Tia_Ortiz77@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'cbe1247a-b290-4b6c-2d75-08dc88f1a80a', N'Jaeden', N'Nicolas', N'Alison_Green55@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'9e9476ba-ed97-4900-2d76-08dc88f1a80a', N'Ari', N'Cole', N'Joannie_Hoppe@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'b3beaaab-d2fc-49f1-2d77-08dc88f1a80a', N'Jessie', N'Koch', N'Bianka31@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'd9896692-c9bb-4220-2d78-08dc88f1a80a', N'Stone', N'Beer', N'Elisha.Thiel@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'2518d023-e2f1-44bb-2d79-08dc88f1a80a', N'Era', N'Hoppe', N'Eda_Cassin33@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'59703ecb-f85a-410d-2d7a-08dc88f1a80a', N'Kamren', N'Raynor', N'Doris_Robel94@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'140e71ec-7f19-4d09-2d7b-08dc88f1a80a', N'Alana', N'Zulauf', N'Liliana24@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'f95114c7-5036-4b3f-2d7c-08dc88f1a80a', N'Christop', N'Bogan', N'Kamryn.Bailey@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'918eb56d-b0d4-4026-2d7d-08dc88f1a80a', N'Esteban', N'Lowe', N'Clay_Quigley@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'2465b2d7-ffa8-417b-2d7e-08dc88f1a80a', N'Misael', N'Schulist', N'Price78@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'f5f9104f-ca27-44b3-2d7f-08dc88f1a80a', N'Eusebio', N'Bednar', N'Shemar54@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'802bbbd6-3caf-4923-2d80-08dc88f1a80a', N'Jamie', N'Mante', N'Kennedi_Luettgen40@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'ac2038ed-8467-4e84-2d81-08dc88f1a80a', N'Maia', N'Glover', N'Aiden76@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'9744718b-317f-4542-2d82-08dc88f1a80a', N'Madonna', N'O''Hara', N'Nadia_Orn@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'94899472-008e-45c9-2d83-08dc88f1a80a', N'Reta', N'Carroll', N'Dina56@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'ddb8a604-2d20-46b4-2d84-08dc88f1a80a', N'Bell', N'Carter', N'Keaton55@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'fd1435f9-591c-42e2-2d85-08dc88f1a80a', N'Waino', N'Swift', N'Una59@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'02ab9e95-c3fe-4e3e-2d86-08dc88f1a80a', N'Marilou', N'Kshlerin', N'Helga34@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'5c2d8a76-7ebc-45c7-2d87-08dc88f1a80a', N'Arvel', N'Schmitt', N'Ada2@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'c570b620-2e2d-4b99-2d88-08dc88f1a80a', N'Desiree', N'Crooks', N'Kody66@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'efeead58-bf7e-424e-2d89-08dc88f1a80a', N'Vicente', N'Harris', N'Chet_OKon@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'3c953705-8b04-405e-2d8a-08dc88f1a80a', N'Justice', N'Veum', N'Pansy_Paucek79@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'b5c7ed94-4c37-412f-2d8b-08dc88f1a80a', N'Myrtie', N'Senger', N'Joanne.Dicki78@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'17ccc73e-1405-4646-2d8c-08dc88f1a80a', N'Grady', N'Lakin', N'Lucy_Yost@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'c465352a-1179-468f-2d8d-08dc88f1a80a', N'River', N'Kozey', N'Patricia_Douglas@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'f026e525-d433-4ae6-2d8e-08dc88f1a80a', N'Ulices', N'Glover', N'Rahsaan.Rosenbaum@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'ec1920fd-7d1c-4945-2d8f-08dc88f1a80a', N'Kianna', N'Baumbach', N'Lucie.Rolfson@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'e77ae466-2bdc-4ef4-2d90-08dc88f1a80a', N'Daija', N'Kassulke', N'Carli.Zulauf39@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'c3aa5a6e-1533-4a3c-2d91-08dc88f1a80a', N'Albertha', N'Romaguera', N'Boris_Hermann@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'd5589914-5744-48a2-2d92-08dc88f1a80a', N'Robb', N'Sauer', N'Terrance_Brakus66@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'00fffca8-b619-4cca-2d93-08dc88f1a80a', N'Tony', N'Miller', N'Zetta.OHara53@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'168c4b7f-a056-4f89-2d94-08dc88f1a80a', N'Emely', N'Glover', N'Kamryn43@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'2cfb9cab-ab50-499f-2d95-08dc88f1a80a', N'Carmela', N'Tremblay', N'Muhammad_Halvorson69@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'5223e521-46ed-410c-2d96-08dc88f1a80a', N'Brennon', N'Boehm', N'Columbus_Borer@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'1a5fb668-0a5c-44f6-2d97-08dc88f1a80a', N'Fredrick', N'Zieme', N'Mable.Smitham@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'338377ab-0b6b-4ea8-2d98-08dc88f1a80a', N'Armando', N'Erdman', N'Kimberly.Feest5@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'a21057c4-1060-4892-2d99-08dc88f1a80a', N'Dorothy', N'Grant', N'Dexter96@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'2152780b-b9ce-4408-2d9a-08dc88f1a80a', N'Niko', N'Hammes', N'Odessa.Wisoky93@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'e6b3ddbd-b747-4947-2d9b-08dc88f1a80a', N'Alanis', N'Gleason', N'Elmore.Bernier@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'fbbc8a8a-db7b-4e1f-2d9c-08dc88f1a80a', N'Maryse', N'Schinner', N'Abel30@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'37516788-acfd-4762-2d9d-08dc88f1a80a', N'Violet', N'Durgan', N'Zack_Koch@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'3b1eaf58-b749-4a42-2d9e-08dc88f1a80a', N'Kiera', N'Jacobson', N'Aliyah.Homenick33@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'13a4dac4-d1ca-4815-2d9f-08dc88f1a80a', N'Clyde', N'Morissette', N'Makenzie24@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'cf5806e4-9cce-4480-2da0-08dc88f1a80a', N'Gerson', N'Cummings', N'Lavon_Denesik94@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'bc46af7f-79d6-484c-2da1-08dc88f1a80a', N'Ryder', N'Walter', N'Troy32@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'3c0bcedc-289a-481e-2da2-08dc88f1a80a', N'Karson', N'Nicolas', N'Mallie.Schumm@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'd8b80e4e-cd91-43c1-2da3-08dc88f1a80a', N'Alessandra', N'Purdy', N'Mose_Towne33@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'5471d320-3e15-48d8-2da4-08dc88f1a80a', N'Dario', N'Kilback', N'Keara.Kuvalis@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'307690eb-b4f5-4d73-2da5-08dc88f1a80a', N'Liam', N'Anderson', N'Carlos90@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'405799c7-ae28-4567-2da6-08dc88f1a80a', N'Ariel', N'Moen', N'Amy60@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'5e2d86ce-10a0-427a-2da7-08dc88f1a80a', N'Cornelius', N'Medhurst', N'Theresia_Prohaska@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'de21c9ec-c4cc-4ea7-2da8-08dc88f1a80a', N'Daphney', N'Hodkiewicz', N'Berry.Kozey@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'139bdc2b-f82b-4673-2da9-08dc88f1a80a', N'Joy', N'Will', N'Berneice_Willms55@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'c07a5d64-3665-4456-2daa-08dc88f1a80a', N'Donnell', N'Emmerich', N'Elyse_Hartmann8@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'22067cc0-e653-40bb-2dab-08dc88f1a80a', N'Nedra', N'Walsh', N'Hettie25@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'e4cdfced-1567-4160-2dac-08dc88f1a80a', N'April', N'Wehner', N'Yadira75@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'cb9bd841-ef47-45b6-2dad-08dc88f1a80a', N'Isaac', N'Ziemann', N'Baby32@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'40cdb25b-375b-41cb-2dae-08dc88f1a80a', N'Hannah', N'Stoltenberg', N'Mylene.Ruecker@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'9d41a540-e8cd-4214-2daf-08dc88f1a80a', N'Sandra', N'O''Keefe', N'Valentine_Jaskolski39@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'eb5fdfff-5f6e-4bda-2db0-08dc88f1a80a', N'Gregorio', N'Becker', N'Mario62@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'cbe2d7c1-7c12-46f7-2db1-08dc88f1a80a', N'Maegan', N'Wuckert', N'Aron.Schaden@gmail.com')
GO
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'aa8afaaf-6029-41aa-2db2-08dc88f1a80a', N'Domenic', N'Schumm', N'Violette.Homenick@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'acd73c46-64d9-4b3c-2db3-08dc88f1a80a', N'Rupert', N'Lind', N'Brianne80@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'8b445651-1cf2-44bf-2db4-08dc88f1a80a', N'Chelsey', N'Paucek', N'Emmy_Hayes2@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'9524f1ea-a2ad-467b-2db5-08dc88f1a80a', N'Garett', N'Rice', N'Anderson.Predovic75@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'051b82b3-c520-4000-2db6-08dc88f1a80a', N'Keanu', N'Koepp', N'Jade2@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'7b7d2792-0745-4abb-2db7-08dc88f1a80a', N'Eli', N'Boyle', N'Jordan.Macejkovic36@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'9bbe3266-7e36-489c-2db8-08dc88f1a80a', N'Freeman', N'Strosin', N'Mavis_Murphy83@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'3c16023a-124e-40b6-2db9-08dc88f1a80a', N'Micaela', N'Smith', N'Burdette6@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'c061ff3d-00ac-47f4-2dba-08dc88f1a80a', N'Liliane', N'Luettgen', N'Roy_Huels@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'27464946-dfca-4f8a-2dbb-08dc88f1a80a', N'Darrick', N'Bernhard', N'Felicita_Bode64@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'0c7cf475-0f21-4a37-2dbc-08dc88f1a80a', N'Ken', N'Greenfelder', N'Emma.Bartell@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'c32ceec1-d2da-4dbd-2dbd-08dc88f1a80a', N'Floyd', N'Carter', N'Peter42@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'381a66a8-0b8f-4375-2dbe-08dc88f1a80a', N'Angela', N'Daugherty', N'Shayne39@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'1e33a82e-2e07-49a7-2dbf-08dc88f1a80a', N'Bobbie', N'Baumbach', N'Josue_Morissette74@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'55033a25-cb52-4694-2dc0-08dc88f1a80a', N'Thora', N'Brekke', N'Maximillian14@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'b2cbeef5-a344-4650-2dc1-08dc88f1a80a', N'Nora', N'Quigley', N'Margarita_Dach69@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'9332e4be-a6e0-4b5d-2dc2-08dc88f1a80a', N'Norbert', N'O''Kon', N'Otha_Mann@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'6416ce67-e216-44ab-2dc3-08dc88f1a80a', N'Jammie', N'Hessel', N'Lulu_Hessel@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'5704047d-1794-40b9-2dc4-08dc88f1a80a', N'Luther', N'Kunze', N'Kassandra59@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'db0c3ac6-a512-4eaa-2dc5-08dc88f1a80a', N'Carter', N'Hilll', N'Antonia76@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'ffc2ebcb-10c7-4312-2dc6-08dc88f1a80a', N'Marques', N'Kuhlman', N'Luna29@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'fc0ad6c5-d800-4294-2dc7-08dc88f1a80a', N'David', N'Krajcik', N'Mikel.Daniel@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'353e33e6-4eac-47e5-2dc8-08dc88f1a80a', N'Chaz', N'Walker', N'Mariah_Collins44@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'8550efdf-41fa-4435-2dc9-08dc88f1a80a', N'Keith', N'Hane', N'Rosa2@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'103d5d8d-a0e7-4e77-2dca-08dc88f1a80a', N'Domenica', N'Braun', N'Stephanie_Bruen79@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'48aa05e3-6a2e-41c0-2dcb-08dc88f1a80a', N'Norene', N'Bauch', N'Katherine_Hills89@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'e1caff94-d30f-421d-2dcc-08dc88f1a80a', N'Mia', N'Koss', N'Herminio67@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'868292f2-7d31-418d-2dcd-08dc88f1a80a', N'Dorcas', N'Buckridge', N'Mia_Johns@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'daa93915-e72c-4cea-2dce-08dc88f1a80a', N'Tomas', N'Altenwerth', N'Cornell68@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'4e4685f4-6492-40e7-2dcf-08dc88f1a80a', N'Mertie', N'Mueller', N'Major_McLaughlin@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'4fe00b29-4c42-45b7-2dd0-08dc88f1a80a', N'Virgil', N'Parisian', N'Asia.Padberg5@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'd10833c9-a312-4085-2dd1-08dc88f1a80a', N'Carlo', N'Johns', N'Addison.Kertzmann@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'0931fe69-40e5-4fe6-2dd2-08dc88f1a80a', N'Dina', N'Orn', N'Corrine26@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'8237825d-f381-46dd-2dd3-08dc88f1a80a', N'Darrick', N'Macejkovic', N'Mackenzie.Schamberger81@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'0e766ae7-b1d9-472b-2dd4-08dc88f1a80a', N'Wava', N'Streich', N'Yadira86@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'296bb7ef-3d94-4de8-2dd5-08dc88f1a80a', N'Dewitt', N'Prohaska', N'Manuela_Fisher67@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'7be206c2-4308-41a4-2dd6-08dc88f1a80a', N'Tess', N'Dare', N'Eldridge44@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'e6c4d951-46c0-4413-2dd7-08dc88f1a80a', N'Geovanny', N'Ruecker', N'Colleen_McLaughlin@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'7687bacf-bed1-4e3e-2dd8-08dc88f1a80a', N'Savanah', N'Bernhard', N'Jermey91@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'34232c63-423d-4fa6-2dd9-08dc88f1a80a', N'Dayna', N'Ankunding', N'Floyd.Spinka39@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'af032b83-e6ec-452d-2dda-08dc88f1a80a', N'Tracey', N'Kovacek', N'Lou.Sipes18@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'42fe628c-21d0-49fa-2ddb-08dc88f1a80a', N'Catalina', N'Franecki', N'General31@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'5b06bcb9-8de4-465d-2ddc-08dc88f1a80a', N'Maurice', N'Durgan', N'Tessie.Shields58@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'd0d2bc2a-33c3-4197-2ddd-08dc88f1a80a', N'Osvaldo', N'Wintheiser', N'Jimmy98@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'1bfec848-230b-457b-2dde-08dc88f1a80a', N'Cielo', N'Wunsch', N'Neha77@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'284e5f44-511d-419e-2ddf-08dc88f1a80a', N'Paris', N'Berge', N'Christy.Huels@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'5acb9949-bb12-438f-2de0-08dc88f1a80a', N'Naomi', N'Rath', N'Sage.Rice@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'99a0bc89-c1ae-4e90-2de1-08dc88f1a80a', N'Alycia', N'Borer', N'Lonie_Schultz@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'6bc85940-9de5-46d6-2de2-08dc88f1a80a', N'Ozella', N'Rutherford', N'Erin.Wunsch65@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'602d9a68-c19d-47c4-2de3-08dc88f1a80a', N'Meggie', N'Balistreri', N'Catharine83@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'd6d40892-f93c-4934-2de4-08dc88f1a80a', N'Lilian', N'Hane', N'Bert.Waters85@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'718714a5-f023-40f3-2de5-08dc88f1a80a', N'Lonie', N'Kuvalis', N'Elyssa.Pollich@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'61aa0426-cf70-4ec1-2de6-08dc88f1a80a', N'Dayana', N'Brekke', N'Magali.Haag93@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'8f3c1324-70bf-4d74-2de7-08dc88f1a80a', N'Ellis', N'Gorczany', N'Elian_Murphy@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'9e24c5f9-00bc-44bf-2de8-08dc88f1a80a', N'Anabelle', N'Lebsack', N'Camille_Abshire@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'301215fe-620c-42c8-2de9-08dc88f1a80a', N'Opal', N'Kuphal', N'Gene_Rodriguez92@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'6e6b0021-14ad-4276-2dea-08dc88f1a80a', N'Oral', N'Grady', N'Maria.Conn@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'fc35ba4c-b248-4d53-2deb-08dc88f1a80a', N'Parker', N'Bechtelar', N'Archibald_Lockman@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'537ce295-a66b-4369-2dec-08dc88f1a80a', N'Keith', N'Frami', N'Jimmie25@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'a2ceecf0-1b7e-4771-2ded-08dc88f1a80a', N'Georgianna', N'Mertz', N'Jolie8@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'110c633c-3761-4128-2dee-08dc88f1a80a', N'April', N'Cole', N'Raphael.Schaefer18@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'6ea9f6a5-82be-4656-2def-08dc88f1a80a', N'Grayson', N'Boehm', N'Maximillia_Kutch73@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'eabd4f81-bbed-4352-2df0-08dc88f1a80a', N'Toy', N'Bergnaum', N'Tillman11@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'32b1d2d5-76b2-4628-2df1-08dc88f1a80a', N'Corbin', N'Erdman', N'Jonathan50@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'56e7c731-3cc3-4fe9-2df2-08dc88f1a80a', N'Makenzie', N'Smith', N'Angie21@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'1cfa4886-c57a-40bd-2df3-08dc88f1a80a', N'Emie', N'Metz', N'Anna.Conn76@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'3a9d0c06-6865-4503-2df4-08dc88f1a80a', N'Florencio', N'Goyette', N'Arlene37@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'48904e36-1b39-48ab-2df5-08dc88f1a80a', N'Eldora', N'Heller', N'Mittie.Borer@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'386e7b72-54d7-4c88-2df6-08dc88f1a80a', N'Stefan', N'Ortiz', N'Flavio.Roberts@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'99eb19d9-a266-472c-2df7-08dc88f1a80a', N'Graham', N'Hegmann', N'Della3@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'783c221f-8ac9-4390-2df8-08dc88f1a80a', N'Brenda', N'Hyatt', N'Edwardo_Yundt6@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'a7d5e27b-51fa-4712-2df9-08dc88f1a80a', N'Roberta', N'Reilly', N'Deangelo9@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'd584ce5f-96ca-49e1-2dfa-08dc88f1a80a', N'Micah', N'Welch', N'Nikolas_Walker@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'dacf12c1-b49e-4dab-2dfb-08dc88f1a80a', N'Raven', N'Stroman', N'Jakayla43@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'5358bdb8-0947-45b0-2dfc-08dc88f1a80a', N'Amani', N'Mante', N'Armand_Romaguera@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'd8203a38-08cb-42a0-2dfd-08dc88f1a80a', N'Barbara', N'Weissnat', N'Oral_West@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'ce15f278-9e48-4ea7-2dfe-08dc88f1a80a', N'Scarlett', N'Schamberger', N'Gerardo12@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'decc8d76-90e6-4223-2dff-08dc88f1a80a', N'Theodore', N'Bechtelar', N'Reymundo.VonRueden81@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'aca7ff34-450b-4227-2e00-08dc88f1a80a', N'Marina', N'Beatty', N'Rusty44@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'8cea8e7d-691e-446e-2e01-08dc88f1a80a', N'Rosalinda', N'Treutel', N'Treva_Ratke@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'7b980307-652d-4d82-2e02-08dc88f1a80a', N'Tad', N'Senger', N'Lizeth.Jenkins9@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'a1084d3e-aa91-4a05-2e03-08dc88f1a80a', N'Devon', N'Daniel', N'Abdiel12@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'88d5e423-1979-4bb8-2e04-08dc88f1a80a', N'Luciano', N'Stehr', N'Jakayla91@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'799ba0e4-3716-4d54-2e05-08dc88f1a80a', N'Lavonne', N'Walter', N'Burnice_Rau98@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'7273937a-4278-4afe-2e06-08dc88f1a80a', N'Brice', N'Gutkowski', N'Juwan39@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'5c591926-ab34-471e-2e07-08dc88f1a80a', N'Josianne', N'Johnston', N'Peggie.Okuneva83@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'c03c0812-dd67-400e-2e08-08dc88f1a80a', N'Vivianne', N'Fay', N'Pinkie.Wiza50@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'9fe0af0d-c66b-47c6-2e09-08dc88f1a80a', N'Annie', N'Rodriguez', N'Emelia20@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'82736289-e517-4639-2e0a-08dc88f1a80a', N'Derrick', N'Schimmel', N'Cecilia.Mohr@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'252fba42-30c8-4b20-2e0b-08dc88f1a80a', N'Elouise', N'McLaughlin', N'Geo_Gusikowski@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'faa7197a-48af-4f87-2e0c-08dc88f1a80a', N'Kaylin', N'Kuhlman', N'Zackary_Walsh@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'c8779997-bce3-4467-2e0d-08dc88f1a80a', N'Estrella', N'Runolfsson', N'Jordane58@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'895e52ac-0218-44fa-2e0e-08dc88f1a80a', N'Florida', N'Renner', N'Carole88@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'904b77ea-92b0-43f0-2e0f-08dc88f1a80a', N'Osborne', N'Mills', N'Dennis_Gusikowski90@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'e639ab9b-ceb8-4937-2e10-08dc88f1a80a', N'Gerda', N'Kutch', N'Claudia.Heidenreich92@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'05fbd012-23b3-4216-2e11-08dc88f1a80a', N'Bruce', N'Veum', N'Jarred95@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'9efaa770-d17d-4b87-2e12-08dc88f1a80a', N'Newell', N'Huels', N'Dave.Greenholt@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'd3feb78e-a4bd-4cf6-2e13-08dc88f1a80a', N'Alyce', N'Homenick', N'Percival0@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'ed37c56c-bf61-46e9-2e14-08dc88f1a80a', N'Tiana', N'Feest', N'Alden_Keeling32@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'75f65270-f3a8-4f1d-2e15-08dc88f1a80a', N'Tressa', N'McLaughlin', N'Abbie_Rempel@hotmail.com')
GO
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'a9638563-b678-4565-2e16-08dc88f1a80a', N'Rosario', N'Kuhn', N'Derrick.Macejkovic@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'7c7cc25d-84d8-4d17-2e17-08dc88f1a80a', N'Vesta', N'Schumm', N'Cristal48@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'621a7813-8591-4571-2e18-08dc88f1a80a', N'Oda', N'Feil', N'Candelario_Kemmer3@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'9afa677c-7542-4139-2e19-08dc88f1a80a', N'Nia', N'Littel', N'Ava10@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'4f0ef1ff-bd1f-4ce3-2e1a-08dc88f1a80a', N'Maryse', N'Braun', N'Hans.Okuneva@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'd51712d1-da3b-4944-2e1b-08dc88f1a80a', N'Gracie', N'O''Kon', N'Dejuan99@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'259f04e9-43b4-4042-2e1c-08dc88f1a80a', N'Joy', N'Bergstrom', N'Effie.Zulauf@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'e425f81d-d231-4d18-2e1d-08dc88f1a80a', N'Emma', N'Langosh', N'Iliana.Glover76@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'96a7b834-09b0-4180-2e1e-08dc88f1a80a', N'Sydni', N'Abernathy', N'Rowland9@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'4fa63734-935e-4708-2e1f-08dc88f1a80a', N'Jarvis', N'Emard', N'Erik.Mitchell91@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'dd852feb-0ae9-4798-2e20-08dc88f1a80a', N'Derek', N'Haag', N'Guy.Miller76@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'34cc9bba-8202-4f24-2e21-08dc88f1a80a', N'Lorena', N'Bahringer', N'Marques.Flatley16@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'e0c1f022-49f5-42e3-2e22-08dc88f1a80a', N'Chloe', N'Berge', N'Ransom96@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'1b93df19-95b8-43c0-2e23-08dc88f1a80a', N'Furman', N'Hudson', N'Estefania.Jaskolski@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'0dcd7e8a-7bdf-40e1-2e24-08dc88f1a80a', N'Zechariah', N'Dietrich', N'Haylee_Harvey@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'1ba6c92b-7a54-4f11-2e25-08dc88f1a80a', N'Cindy', N'Reichert', N'Toy.Hudson@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'744e4bd6-e386-445f-2e26-08dc88f1a80a', N'Alexandrea', N'Sauer', N'Marcelina.Koch@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'bbfedea6-82d9-4432-2e27-08dc88f1a80a', N'Eudora', N'Mertz', N'Betty_Kuvalis82@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'fb2e4156-0777-42d9-2e28-08dc88f1a80a', N'Mikel', N'Franecki', N'Bridget_White@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'17be064e-12e5-4042-2e29-08dc88f1a80a', N'Armani', N'Schinner', N'Lane_Oberbrunner@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'507580b0-392d-4836-2e2a-08dc88f1a80a', N'Jess', N'Toy', N'Keyshawn_Bechtelar@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'110e57f7-603b-4293-2e2b-08dc88f1a80a', N'Kendra', N'Jones', N'Adelle_Greenholt@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'01e0b7a9-420e-4044-2e2c-08dc88f1a80a', N'Sterling', N'Effertz', N'Yasmin.Keebler@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'76cca4d5-6102-418e-2e2d-08dc88f1a80a', N'Lue', N'Cummerata', N'Marion11@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'4f0dd4db-7b29-403e-2e2e-08dc88f1a80a', N'Golden', N'Bernhard', N'Michel.Hills92@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'd6cd0b30-b89d-4e06-2e2f-08dc88f1a80a', N'Mittie', N'Altenwerth', N'Modesta.Durgan@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'1fd10b01-f919-4470-2e30-08dc88f1a80a', N'Orval', N'Bogisich', N'Karson31@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'36695cfb-6de3-49db-2e31-08dc88f1a80a', N'Mallie', N'Rath', N'Orrin53@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'dc7db456-7446-4a0c-2e32-08dc88f1a80a', N'Lupe', N'Murray', N'Anita.Lueilwitz@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'a996bd11-abca-4fb2-2e33-08dc88f1a80a', N'Audrey', N'McGlynn', N'Savion39@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'60bc5647-6822-43b5-2e34-08dc88f1a80a', N'Laisha', N'White', N'Patricia11@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'175834cb-da7b-4ea0-2e35-08dc88f1a80a', N'Sonny', N'Ondricka', N'Jovanny.Kerluke@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'0ff40adf-b46b-40ae-2e36-08dc88f1a80a', N'Bernard', N'Runolfsdottir', N'Shawn16@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'4b151bf0-ba19-4cdb-2e37-08dc88f1a80a', N'Reilly', N'Morissette', N'Micah12@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'fe5ed81f-298d-467b-2e38-08dc88f1a80a', N'Talia', N'Hagenes', N'Ramon23@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'2e681296-c396-4017-2e39-08dc88f1a80a', N'Mallory', N'Fahey', N'Oda64@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'8257bb8a-4c5d-4afb-2e3a-08dc88f1a80a', N'Deanna', N'Schimmel', N'Dana_OReilly@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'5abc778d-6e24-4b82-2e3b-08dc88f1a80a', N'Lamar', N'Kuhlman', N'Gordon.Jacobi73@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'95fa0a2c-4874-42a4-2e3c-08dc88f1a80a', N'Amari', N'Luettgen', N'Carli_Bashirian@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'32e1a5f0-9976-4e5d-2e3d-08dc88f1a80a', N'Betsy', N'Gottlieb', N'Nat.Hilll3@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'342e9762-cf49-45db-2e3e-08dc88f1a80a', N'Andre', N'Wisoky', N'Jude.King@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'4881a7b5-4726-4cfe-2e3f-08dc88f1a80a', N'Donavon', N'Russel', N'Magdalen.Murazik@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'98438cbc-1c03-4356-2e40-08dc88f1a80a', N'Joaquin', N'Ruecker', N'Rosamond.DAmore@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'457a668b-b8b8-40db-2e41-08dc88f1a80a', N'Samanta', N'Lindgren', N'Genesis.Osinski@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'1a522e16-0f2d-4a25-2e42-08dc88f1a80a', N'Michael', N'Emmerich', N'David_Nikolaus@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'15e452b1-29c8-4d83-2e43-08dc88f1a80a', N'Janae', N'Hermann', N'Narciso.Corwin65@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'e3695943-2a17-4e57-2e44-08dc88f1a80a', N'Vicente', N'Flatley', N'Westley.Walker6@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'ecccf9c6-8fb5-4aaf-2e45-08dc88f1a80a', N'Golden', N'Muller', N'Luis79@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'a587e675-1a36-4dbc-2e46-08dc88f1a80a', N'Mary', N'Quitzon', N'Ada.Williamson97@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'b2ddfef5-313a-4023-2e47-08dc88f1a80a', N'Tia', N'Koch', N'Rodolfo.Cole@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'94100dd0-507b-4d6c-2e48-08dc88f1a80a', N'Baron', N'Block', N'Orin73@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'baca9476-1781-49b1-2e49-08dc88f1a80a', N'Marty', N'Langosh', N'Ryann13@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'f1f9a23f-7c9f-4955-2e4a-08dc88f1a80a', N'Daisy', N'Langosh', N'Lafayette.Frami@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'6e74899e-0a9f-4f87-2e4b-08dc88f1a80a', N'Raina', N'Barton', N'Elnora_Donnelly@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'6bf2a0e3-7928-4b7e-2e4c-08dc88f1a80a', N'Soledad', N'Howe', N'Lorenz_Mertz@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'e33ac336-9232-48d6-2e4d-08dc88f1a80a', N'Keyon', N'Lindgren', N'Flavio58@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'8e3432b6-99b4-4a4f-2e4e-08dc88f1a80a', N'Candace', N'Greenholt', N'Karlie90@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'902c1a63-f182-474a-2e4f-08dc88f1a80a', N'Amir', N'Larson', N'Monique_Armstrong88@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'2d59d182-bbd3-4256-2e50-08dc88f1a80a', N'Genesis', N'Sanford', N'Timothy.Padberg68@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'd55f65a6-f98a-4e8c-2e51-08dc88f1a80a', N'Heloise', N'VonRueden', N'Verna_Leannon@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'18fe9b96-41fd-423a-2e52-08dc88f1a80a', N'Amiya', N'Turcotte', N'Annie8@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'395c4528-2eea-4e44-2e53-08dc88f1a80a', N'Jayce', N'Barrows', N'Polly69@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'ebad53a3-e423-4ebb-2e54-08dc88f1a80a', N'Abbigail', N'Champlin', N'Sandrine48@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'ac197e8f-72ad-4152-2e55-08dc88f1a80a', N'Sydni', N'Feeney', N'Chyna.Yundt@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'3ddefe8e-e899-4521-2e56-08dc88f1a80a', N'Serena', N'Moore', N'Susan.Langosh@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'1da76e20-df95-4809-2e57-08dc88f1a80a', N'Candelario', N'Gorczany', N'Ronaldo.McLaughlin@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'7715c264-d5af-4eca-2e58-08dc88f1a80a', N'Christiana', N'Crist', N'Mauricio_Wiza@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'e0217216-27ac-4999-2e59-08dc88f1a80a', N'Sylvan', N'Mohr', N'Jacques48@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'48663a55-49b6-45c2-2e5a-08dc88f1a80a', N'Stacy', N'Lindgren', N'Monica.Padberg@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'22abd527-2d37-4e6d-2e5b-08dc88f1a80a', N'Dario', N'Hessel', N'Clint41@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'fd3d9ce8-3911-4b79-2e5c-08dc88f1a80a', N'Lew', N'Skiles', N'Eda_Bechtelar20@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'7a4e2ffc-97e1-4aac-2e5d-08dc88f1a80a', N'Doyle', N'Walsh', N'Henri.Ernser@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'62dc8f73-53b9-4958-2e5e-08dc88f1a80a', N'Sonny', N'Schmitt', N'Cruz.Waters@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'981f5656-4d7b-4f7e-2e5f-08dc88f1a80a', N'Ruby', N'Pagac', N'Wellington9@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'57d07e0b-826b-4508-2e60-08dc88f1a80a', N'Shawn', N'Gutmann', N'Alek.Schulist28@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'f03c09b8-0448-429d-2e61-08dc88f1a80a', N'Miracle', N'Gusikowski', N'Furman20@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'b167c5ad-9f68-4d34-2e62-08dc88f1a80a', N'Tevin', N'Rosenbaum', N'Alexanne_Langworth@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'94bb95f6-c3f4-47fb-2e63-08dc88f1a80a', N'Alf', N'Hagenes', N'Antonetta74@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'cf36475a-6630-4fe1-2e64-08dc88f1a80a', N'Julien', N'Wehner', N'Brian_Schroeder88@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'b0a3e617-0ef3-46d7-2e65-08dc88f1a80a', N'Brett', N'Ondricka', N'Vanessa.Pagac@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'f54bcfbd-a90f-4126-2e66-08dc88f1a80a', N'Chanelle', N'Schmitt', N'Geovanni_Cruickshank@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'aebab0a0-bf49-4bde-2e67-08dc88f1a80a', N'Sincere', N'Sanford', N'Patience.Jaskolski94@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'f7d305c2-520a-4b5d-2e68-08dc88f1a80a', N'Mauricio', N'Kerluke', N'Maye.Nicolas@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'763abcf0-eecd-41cf-2e69-08dc88f1a80a', N'Jarvis', N'Lemke', N'Eileen_Torp55@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'1a45a133-be0d-4592-2e6a-08dc88f1a80a', N'Ruth', N'Pfeffer', N'Maggie66@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'c1a5681b-6bf6-4ee2-2e6b-08dc88f1a80a', N'Clyde', N'Tromp', N'Vincenza.Goyette@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'c79e2bca-1b59-47c9-2e6c-08dc88f1a80a', N'Annetta', N'Jacobi', N'Johnny.Kulas@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'0637a246-3cea-448c-2e6d-08dc88f1a80a', N'Freddy', N'Mante', N'Kayleigh_Mertz@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'b15edd53-3141-427e-2e6e-08dc88f1a80a', N'Hayden', N'Armstrong', N'Trisha_OKon55@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'dd89cc39-8dac-4d88-2e6f-08dc88f1a80a', N'Hilario', N'Gibson', N'Izabella.Bechtelar@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'257a9199-cb90-4cb3-2e70-08dc88f1a80a', N'Skylar', N'Huels', N'Maverick_Hudson@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'4218e617-d3f6-49f9-2e71-08dc88f1a80a', N'Leon', N'Heidenreich', N'Jessyca.Upton@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'99173236-bba2-4ca5-2e72-08dc88f1a80a', N'Marco', N'McDermott', N'Sigurd23@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'b27b3e14-d8e7-4335-2e73-08dc88f1a80a', N'Izaiah', N'Bode', N'Alexandro56@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'273ac8d4-914e-4e09-2e74-08dc88f1a80a', N'Curt', N'Rowe', N'Norene21@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'0636ff68-e19c-4628-2e75-08dc88f1a80a', N'Camryn', N'Senger', N'Anibal_Mueller23@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'3bc53830-807a-48ea-2e76-08dc88f1a80a', N'Randy', N'Thompson', N'Jesse_Spencer@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'1d667100-46f8-4fa4-2e77-08dc88f1a80a', N'Lincoln', N'Roob', N'Vincenzo.Muller@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'3bd1258a-3c95-44cb-2e78-08dc88f1a80a', N'Jackie', N'Abshire', N'Philip.Sawayn@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'ed7d6ce5-6b70-4d0a-2e79-08dc88f1a80a', N'Marco', N'Boyle', N'Nola_Senger17@yahoo.com')
GO
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'cf75afdf-7fe3-46a8-2e7a-08dc88f1a80a', N'Jerel', N'Boehm', N'Fred39@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'bc1000d3-e651-4681-2e7b-08dc88f1a80a', N'Benton', N'Kling', N'Tatyana_Hyatt83@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'09f3fceb-e18b-4c33-2e7c-08dc88f1a80a', N'Reed', N'Effertz', N'Shanon58@yahoo.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'138cc2d9-8057-4130-2e7d-08dc88f1a80a', N'Rodrick', N'Schamberger', N'Alysson.Kutch@gmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'43b0ab7f-15ea-40f2-2e7e-08dc88f1a80a', N'Shany', N'Spencer', N'Sydnie16@hotmail.com')
INSERT [dbo].[Owners] ([OwnerId], [FirstName], [LastName], [Email]) VALUES (N'0569066e-56b8-4c64-2e7f-08dc88f1a80a', N'Roman', N'Senger', N'Terrence.Jones@yahoo.com')
GO
INSERT [dbo].[Requests] ([Id], [Description], [Status], [CategoryID], [CreationTime], [StartTime], [EndTime], [TotalCost], [MaintenanceStaffId], [ApartmentId]) VALUES (N'0b3e8b3d-b8f0-4cac-a206-080010f681b4', N'Try to generate the COM capacitor, maybe it will hack the haptic transmitter!', 1, 1, CAST(N'2024-04-02T00:00:00.0000000' AS DateTime2), CAST(N'2024-05-02T00:00:00.0000000' AS DateTime2), CAST(N'2024-05-02T21:26:06.2770000' AS DateTime2), 1000, N'91056360-28fd-4d81-9bb4-a37eab670638', N'1e33a82e-2e07-49a7-2dbf-08dc88f1a80a')
INSERT [dbo].[Requests] ([Id], [Description], [Status], [CategoryID], [CreationTime], [StartTime], [EndTime], [TotalCost], [MaintenanceStaffId], [ApartmentId]) VALUES (N'c1020de7-714c-4b84-a42f-142301248edb', N'You can''t connect the bus without programming the online CSS hard drive!', 2, 1, CAST(N'2024-04-02T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'0c9d062c-b552-469b-8f91-965d6ec5380e', N'c32ceec1-d2da-4dbd-2dbd-08dc88f1a80a')
INSERT [dbo].[Requests] ([Id], [Description], [Status], [CategoryID], [CreationTime], [StartTime], [EndTime], [TotalCost], [MaintenanceStaffId], [ApartmentId]) VALUES (N'58812679-9812-4afb-9af2-1901fcf3ab6a', N'Use the mobile RAM monitor, then you can compress the neural program!', 2, 3, CAST(N'2024-04-02T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'0bae43ad-8b5d-46ad-a4f6-b06d8b974d83', N'8550efdf-41fa-4435-2dc9-08dc88f1a80a')
INSERT [dbo].[Requests] ([Id], [Description], [Status], [CategoryID], [CreationTime], [StartTime], [EndTime], [TotalCost], [MaintenanceStaffId], [ApartmentId]) VALUES (N'7e8b332d-f291-47fc-a6f7-2182da61867b', N'The XML circuit is down, hack the 1080p program so we can program the XSS card!', 0, 9, CAST(N'2024-04-02T00:00:00.0000000' AS DateTime2), CAST(N'2024-05-02T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'0bae43ad-8b5d-46ad-a4f6-b06d8b974d83', N'7be206c2-4308-41a4-2dd6-08dc88f1a80a')
INSERT [dbo].[Requests] ([Id], [Description], [Status], [CategoryID], [CreationTime], [StartTime], [EndTime], [TotalCost], [MaintenanceStaffId], [ApartmentId]) VALUES (N'ed6c1bc7-510b-439a-961b-32cb49d6dfce', N'We need to quantify the redundant SQL protocol!', 2, 4, CAST(N'2024-04-02T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'd9966fa0-98d7-4573-a654-55de500b45e5', N'49d6772d-7d5b-4da6-2cad-08dc88f1a80a')
INSERT [dbo].[Requests] ([Id], [Description], [Status], [CategoryID], [CreationTime], [StartTime], [EndTime], [TotalCost], [MaintenanceStaffId], [ApartmentId]) VALUES (N'8e639ea9-fbce-4b5c-aeb6-3871de5d5c5d', N'generating the protocol won''t do anything, we need to bypass the bluetooth AGP application!', 0, 7, CAST(N'2024-04-02T00:00:00.0000000' AS DateTime2), CAST(N'2024-05-02T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'0bae43ad-8b5d-46ad-a4f6-b06d8b974d83', N'4fe00b29-4c42-45b7-2dd0-08dc88f1a80a')
INSERT [dbo].[Requests] ([Id], [Description], [Status], [CategoryID], [CreationTime], [StartTime], [EndTime], [TotalCost], [MaintenanceStaffId], [ApartmentId]) VALUES (N'2245828d-a30e-45ce-ac45-5c915304ea5b', N'If we reboot the circuit, we can get to the SDD sensor through the open-source HTTP card!', 0, 4, CAST(N'2024-04-02T00:00:00.0000000' AS DateTime2), CAST(N'2024-05-02T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'91056360-28fd-4d81-9bb4-a37eab670638', N'4e4685f4-6492-40e7-2dcf-08dc88f1a80a')
INSERT [dbo].[Requests] ([Id], [Description], [Status], [CategoryID], [CreationTime], [StartTime], [EndTime], [TotalCost], [MaintenanceStaffId], [ApartmentId]) VALUES (N'6b1e2e1a-1e35-450d-9968-726cf2f8142e', N'I''ll index the auxiliary HTTP transmitter, that should card the SDD panel!', 1, 6, CAST(N'2024-04-02T00:00:00.0000000' AS DateTime2), CAST(N'2024-05-02T00:00:00.0000000' AS DateTime2), CAST(N'2024-05-02T21:26:06.2770000' AS DateTime2), 1000, N'd9966fa0-98d7-4573-a654-55de500b45e5', N'c992e8f7-c65f-440f-2cb1-08dc88f1a80a')
INSERT [dbo].[Requests] ([Id], [Description], [Status], [CategoryID], [CreationTime], [StartTime], [EndTime], [TotalCost], [MaintenanceStaffId], [ApartmentId]) VALUES (N'ca13ad1a-bc44-4f83-a476-7acf9f60278f', N'If we copy the card, we can get to the RAM protocol through the solid state HTTP matrix!', 2, 3, CAST(N'2024-04-02T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'0c9d062c-b552-469b-8f91-965d6ec5380e', N'7b7d2792-0745-4abb-2db7-08dc88f1a80a')
INSERT [dbo].[Requests] ([Id], [Description], [Status], [CategoryID], [CreationTime], [StartTime], [EndTime], [TotalCost], [MaintenanceStaffId], [ApartmentId]) VALUES (N'c116152c-8211-4b05-9be8-b58bd29faa77', N'We need to connect the optical SSL capacitor!', 0, 6, CAST(N'2024-04-02T00:00:00.0000000' AS DateTime2), CAST(N'2024-05-02T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'd9966fa0-98d7-4573-a654-55de500b45e5', N'394ba2b4-04ff-4801-2c9a-08dc88f1a80a')
INSERT [dbo].[Requests] ([Id], [Description], [Status], [CategoryID], [CreationTime], [StartTime], [EndTime], [TotalCost], [MaintenanceStaffId], [ApartmentId]) VALUES (N'b6a85e2c-45ec-4a11-ac2e-bcc311a498f7', N'Try to generate the SCSI hard drive, maybe it will hack the cross-platform alarm!', 1, 1, CAST(N'2024-04-02T00:00:00.0000000' AS DateTime2), CAST(N'2024-05-02T00:00:00.0000000' AS DateTime2), CAST(N'2024-05-02T21:26:06.2770000' AS DateTime2), 1000, N'd9966fa0-98d7-4573-a654-55de500b45e5', N'b4cfcddd-04bf-4749-2ca3-08dc88f1a80a')
INSERT [dbo].[Requests] ([Id], [Description], [Status], [CategoryID], [CreationTime], [StartTime], [EndTime], [TotalCost], [MaintenanceStaffId], [ApartmentId]) VALUES (N'87bbcb8b-10b7-40a4-89ee-c49675a99197', N'Try to calculate the HDD circuit, maybe it will connect the haptic sensor!', 0, 1, CAST(N'2024-04-02T00:00:00.0000000' AS DateTime2), CAST(N'2024-05-02T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'd9966fa0-98d7-4573-a654-55de500b45e5', N'15a04be8-3fcc-4311-2ca5-08dc88f1a80a')
INSERT [dbo].[Requests] ([Id], [Description], [Status], [CategoryID], [CreationTime], [StartTime], [EndTime], [TotalCost], [MaintenanceStaffId], [ApartmentId]) VALUES (N'bc98a1a1-d90b-47bb-96e6-cf7ddd8f3322', N'I''ll generate the digital USB matrix, that should array the JBOD interface!', 2, 2, CAST(N'2024-04-02T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'0bae43ad-8b5d-46ad-a4f6-b06d8b974d83', N'301215fe-620c-42c8-2de9-08dc88f1a80a')
INSERT [dbo].[Requests] ([Id], [Description], [Status], [CategoryID], [CreationTime], [StartTime], [EndTime], [TotalCost], [MaintenanceStaffId], [ApartmentId]) VALUES (N'539c678d-249f-451a-8dc3-d173c59438c7', N'If we input the firewall, we can get to the IB pixel through the open-source CSS array!', 0, 7, CAST(N'2024-04-02T00:00:00.0000000' AS DateTime2), CAST(N'2024-05-02T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'd9966fa0-98d7-4573-a654-55de500b45e5', N'f47a9763-dca6-44c4-2ca7-08dc88f1a80a')
INSERT [dbo].[Requests] ([Id], [Description], [Status], [CategoryID], [CreationTime], [StartTime], [EndTime], [TotalCost], [MaintenanceStaffId], [ApartmentId]) VALUES (N'c659443b-98f0-4c28-84b6-d34841c7155d', N'If we program the feed, we can get to the PNG circuit through the auxiliary PNG interface!', 0, 4, CAST(N'2024-04-02T00:00:00.0000000' AS DateTime2), CAST(N'2024-05-02T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'0bae43ad-8b5d-46ad-a4f6-b06d8b974d83', N'55033a25-cb52-4694-2dc0-08dc88f1a80a')
INSERT [dbo].[Requests] ([Id], [Description], [Status], [CategoryID], [CreationTime], [StartTime], [EndTime], [TotalCost], [MaintenanceStaffId], [ApartmentId]) VALUES (N'd7390c64-6e40-404a-9d2b-e19343a4295f', N'I''ll generate the back-end PNG hard drive, that should panel the JBOD port!', 2, 4, CAST(N'2024-04-02T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'91056360-28fd-4d81-9bb4-a37eab670638', N'e6c4d951-46c0-4413-2dd7-08dc88f1a80a')
INSERT [dbo].[Requests] ([Id], [Description], [Status], [CategoryID], [CreationTime], [StartTime], [EndTime], [TotalCost], [MaintenanceStaffId], [ApartmentId]) VALUES (N'b8caf9e3-d819-46b2-9a61-ecd1a968c024', N'I''ll quantify the digital JBOD firewall, that should driver the SMS alarm!', 2, 7, CAST(N'2024-04-02T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'0c9d062c-b552-469b-8f91-965d6ec5380e', N'0931fe69-40e5-4fe6-2dd2-08dc88f1a80a')
INSERT [dbo].[Requests] ([Id], [Description], [Status], [CategoryID], [CreationTime], [StartTime], [EndTime], [TotalCost], [MaintenanceStaffId], [ApartmentId]) VALUES (N'99d9877f-ae96-4915-b5e7-eebe12a41787', N'I''ll override the neural SAS protocol, that should firewall the JBOD microchip!', 2, 7, CAST(N'2024-04-02T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'91056360-28fd-4d81-9bb4-a37eab670638', N'61aa0426-cf70-4ec1-2de6-08dc88f1a80a')
INSERT [dbo].[Requests] ([Id], [Description], [Status], [CategoryID], [CreationTime], [StartTime], [EndTime], [TotalCost], [MaintenanceStaffId], [ApartmentId]) VALUES (N'16fd6b1b-cf2f-450e-b0f1-f8d476976ce6', N'If we calculate the bandwidth, we can get to the SMTP capacitor through the auxiliary CSS port!', 2, 7, CAST(N'2024-04-02T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'd9966fa0-98d7-4573-a654-55de500b45e5', N'1fb8cc80-5223-40b5-2caf-08dc88f1a80a')
GO
/****** Object:  Index [IX_Apartments_BuildingId]    Script Date: 10/6/2024 17:44:25 ******/
CREATE NONCLUSTERED INDEX [IX_Apartments_BuildingId] ON [dbo].[Apartments]
(
	[BuildingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Buildings_ConstructionCompanyId]    Script Date: 10/6/2024 17:44:25 ******/
CREATE NONCLUSTERED INDEX [IX_Buildings_ConstructionCompanyId] ON [dbo].[Buildings]
(
	[ConstructionCompanyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Buildings_ManagerId]    Script Date: 10/6/2024 17:44:25 ******/
CREATE NONCLUSTERED INDEX [IX_Buildings_ManagerId] ON [dbo].[Buildings]
(
	[ManagerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ConstructionCompanies_ConstructionCompanyAdminId]    Script Date: 10/6/2024 17:44:25 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_ConstructionCompanies_ConstructionCompanyAdminId] ON [dbo].[ConstructionCompanies]
(
	[ConstructionCompanyAdminId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Locations_Latitude_Longitude]    Script Date: 10/6/2024 17:44:25 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Locations_Latitude_Longitude] ON [dbo].[Locations]
(
	[Latitude] ASC,
	[Longitude] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Requests_ApartmentId]    Script Date: 10/6/2024 17:44:25 ******/
CREATE NONCLUSTERED INDEX [IX_Requests_ApartmentId] ON [dbo].[Requests]
(
	[ApartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Requests_CategoryID]    Script Date: 10/6/2024 17:44:25 ******/
CREATE NONCLUSTERED INDEX [IX_Requests_CategoryID] ON [dbo].[Requests]
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Requests_MaintenanceStaffId]    Script Date: 10/6/2024 17:44:25 ******/
CREATE NONCLUSTERED INDEX [IX_Requests_MaintenanceStaffId] ON [dbo].[Requests]
(
	[MaintenanceStaffId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Apartments]  WITH CHECK ADD  CONSTRAINT [FK_Apartments_Buildings_BuildingId] FOREIGN KEY([BuildingId])
REFERENCES [dbo].[Buildings] ([BuildingId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Apartments] CHECK CONSTRAINT [FK_Apartments_Buildings_BuildingId]
GO
ALTER TABLE [dbo].[Buildings]  WITH CHECK ADD  CONSTRAINT [FK_Buildings_ConstructionCompanies_ConstructionCompanyId] FOREIGN KEY([ConstructionCompanyId])
REFERENCES [dbo].[ConstructionCompanies] ([ConstructionCompanyId])
GO
ALTER TABLE [dbo].[Buildings] CHECK CONSTRAINT [FK_Buildings_ConstructionCompanies_ConstructionCompanyId]
GO
ALTER TABLE [dbo].[Buildings]  WITH CHECK ADD  CONSTRAINT [FK_Buildings_Managers_ManagerId] FOREIGN KEY([ManagerId])
REFERENCES [dbo].[Managers] ([ManagerId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Buildings] CHECK CONSTRAINT [FK_Buildings_Managers_ManagerId]
GO
ALTER TABLE [dbo].[ConstructionCompanies]  WITH CHECK ADD  CONSTRAINT [FK_ConstructionCompanies_ConstructionCompanyAdmins_ConstructionCompanyAdminId] FOREIGN KEY([ConstructionCompanyAdminId])
REFERENCES [dbo].[ConstructionCompanyAdmins] ([Id])
GO
ALTER TABLE [dbo].[ConstructionCompanies] CHECK CONSTRAINT [FK_ConstructionCompanies_ConstructionCompanyAdmins_ConstructionCompanyAdminId]
GO
ALTER TABLE [dbo].[Locations]  WITH CHECK ADD  CONSTRAINT [FK_Locations_Buildings_BuildingId] FOREIGN KEY([BuildingId])
REFERENCES [dbo].[Buildings] ([BuildingId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Locations] CHECK CONSTRAINT [FK_Locations_Buildings_BuildingId]
GO
ALTER TABLE [dbo].[Owners]  WITH CHECK ADD  CONSTRAINT [FK_Owners_Apartments_OwnerId] FOREIGN KEY([OwnerId])
REFERENCES [dbo].[Apartments] ([ApartmentId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Owners] CHECK CONSTRAINT [FK_Owners_Apartments_OwnerId]
GO
ALTER TABLE [dbo].[Requests]  WITH CHECK ADD  CONSTRAINT [FK_Requests_Apartments_ApartmentId] FOREIGN KEY([ApartmentId])
REFERENCES [dbo].[Apartments] ([ApartmentId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Requests] CHECK CONSTRAINT [FK_Requests_Apartments_ApartmentId]
GO
ALTER TABLE [dbo].[Requests]  WITH CHECK ADD  CONSTRAINT [FK_Requests_Category_CategoryID] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Category] ([ID])
GO
ALTER TABLE [dbo].[Requests] CHECK CONSTRAINT [FK_Requests_Category_CategoryID]
GO
ALTER TABLE [dbo].[Requests]  WITH CHECK ADD  CONSTRAINT [FK_Requests_MaintenanceStaff_MaintenanceStaffId] FOREIGN KEY([MaintenanceStaffId])
REFERENCES [dbo].[MaintenanceStaff] ([ID])
GO
ALTER TABLE [dbo].[Requests] CHECK CONSTRAINT [FK_Requests_MaintenanceStaff_MaintenanceStaffId]
GO
USE [master]
GO
ALTER DATABASE [BuildingManagementDB] SET  READ_WRITE 
GO
