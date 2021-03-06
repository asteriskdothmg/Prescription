USE [master]
GO
/****** Object:  Database [PrescriptionDatabase]    Script Date: 03/13/2019 3:03:30 PM ******/
CREATE DATABASE [PrescriptionDatabase]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PrescriptionDatabase', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLDEV\MSSQL\DATA\PrescriptionDatabase.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'PrescriptionDatabase_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLDEV\MSSQL\DATA\PrescriptionDatabase_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [PrescriptionDatabase] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PrescriptionDatabase].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PrescriptionDatabase] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PrescriptionDatabase] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PrescriptionDatabase] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PrescriptionDatabase] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PrescriptionDatabase] SET ARITHABORT OFF 
GO
ALTER DATABASE [PrescriptionDatabase] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PrescriptionDatabase] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PrescriptionDatabase] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PrescriptionDatabase] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PrescriptionDatabase] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PrescriptionDatabase] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PrescriptionDatabase] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PrescriptionDatabase] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PrescriptionDatabase] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PrescriptionDatabase] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PrescriptionDatabase] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PrescriptionDatabase] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PrescriptionDatabase] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PrescriptionDatabase] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PrescriptionDatabase] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PrescriptionDatabase] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PrescriptionDatabase] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PrescriptionDatabase] SET RECOVERY FULL 
GO
ALTER DATABASE [PrescriptionDatabase] SET  MULTI_USER 
GO
ALTER DATABASE [PrescriptionDatabase] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PrescriptionDatabase] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PrescriptionDatabase] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PrescriptionDatabase] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [PrescriptionDatabase] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'PrescriptionDatabase', N'ON'
GO
USE [PrescriptionDatabase]
GO
/****** Object:  Table [dbo].[ApplicationKey]    Script Date: 03/13/2019 3:03:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ApplicationKey](
	[AppKey] [uniqueidentifier] NOT NULL,
	[AppName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_ApplicationKey] PRIMARY KEY CLUSTERED 
(
	[AppKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Patient]    Script Date: 03/13/2019 3:03:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Patient](
	[PatientId] [int] IDENTITY(1,1) NOT NULL,
	[LastName] [varchar](100) NOT NULL,
	[FirstName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Patient] PRIMARY KEY CLUSTERED 
(
	[PatientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Prescription]    Script Date: 03/13/2019 3:03:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Prescription](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ExpirationDate] [date] NOT NULL,
	[ProductName] [varchar](max) NOT NULL,
	[UsesLeft] [int] NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[PatientId] [int] NOT NULL,
 CONSTRAINT [PK_Prescription] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[ApplicationKey] ([AppKey], [AppName]) VALUES (N'994a6ab3-5cd1-4049-897e-6e4cec039b9b', N'Prescription')
GO
SET IDENTITY_INSERT [dbo].[Patient] ON 

GO
INSERT [dbo].[Patient] ([PatientId], [LastName], [FirstName]) VALUES (1, N'Dela Cruz', N'Juan')
GO
INSERT [dbo].[Patient] ([PatientId], [LastName], [FirstName]) VALUES (3, N'Rizal', N'Jose')
GO
INSERT [dbo].[Patient] ([PatientId], [LastName], [FirstName]) VALUES (4, N'Bonifacio', N'Andres')
GO
INSERT [dbo].[Patient] ([PatientId], [LastName], [FirstName]) VALUES (5, N'Del Pilar', N'Gregorio')
GO
INSERT [dbo].[Patient] ([PatientId], [LastName], [FirstName]) VALUES (6, N'Luna', N'Juan')
GO
SET IDENTITY_INSERT [dbo].[Patient] OFF
GO
SET IDENTITY_INSERT [dbo].[Prescription] ON 

GO
INSERT [dbo].[Prescription] ([Id], [ExpirationDate], [ProductName], [UsesLeft], [Description], [IsActive], [PatientId]) VALUES (1, CAST(N'2020-02-20' AS Date), N'Biogesic', 2, N'Test Description', 1, 1)
GO
INSERT [dbo].[Prescription] ([Id], [ExpirationDate], [ProductName], [UsesLeft], [Description], [IsActive], [PatientId]) VALUES (9, CAST(N'2020-02-20' AS Date), N'Alaxan-FR2', 2, N'Test Description', 1, 3)
GO
INSERT [dbo].[Prescription] ([Id], [ExpirationDate], [ProductName], [UsesLeft], [Description], [IsActive], [PatientId]) VALUES (10, CAST(N'2020-02-20' AS Date), N'Alaxan-FR22', 2, N'Test Description', 1, 3)
GO
INSERT [dbo].[Prescription] ([Id], [ExpirationDate], [ProductName], [UsesLeft], [Description], [IsActive], [PatientId]) VALUES (11, CAST(N'2020-02-20' AS Date), N'Alaxan-FR22', 2, N'Test Description2', 1, 3)
GO
INSERT [dbo].[Prescription] ([Id], [ExpirationDate], [ProductName], [UsesLeft], [Description], [IsActive], [PatientId]) VALUES (12, CAST(N'2020-02-20' AS Date), N'Alaxan-FR22', 2, N'Test Description222', 1, 3)
GO
SET IDENTITY_INSERT [dbo].[Prescription] OFF
GO
ALTER TABLE [dbo].[Prescription]  WITH CHECK ADD  CONSTRAINT [FK_Prescription_Patient] FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patient] ([PatientId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Prescription] CHECK CONSTRAINT [FK_Prescription_Patient]
GO
USE [master]
GO
ALTER DATABASE [PrescriptionDatabase] SET  READ_WRITE 
GO
