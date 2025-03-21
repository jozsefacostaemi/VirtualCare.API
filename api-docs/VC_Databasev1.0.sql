USE [master]
GO
/****** Object:  Database [VirtualCare]    Script Date: 21/02/2025 1:12:01 p. m. ******/
CREATE DATABASE [VirtualCare]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'VirtualCare', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\VirtualCare.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'VirtualCare_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\VirtualCare_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [VirtualCare] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [VirtualCare].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [VirtualCare] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [VirtualCare] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [VirtualCare] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [VirtualCare] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [VirtualCare] SET ARITHABORT OFF 
GO
ALTER DATABASE [VirtualCare] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [VirtualCare] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [VirtualCare] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [VirtualCare] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [VirtualCare] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [VirtualCare] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [VirtualCare] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [VirtualCare] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [VirtualCare] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [VirtualCare] SET  DISABLE_BROKER 
GO
ALTER DATABASE [VirtualCare] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [VirtualCare] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [VirtualCare] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [VirtualCare] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [VirtualCare] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [VirtualCare] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [VirtualCare] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [VirtualCare] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [VirtualCare] SET  MULTI_USER 
GO
ALTER DATABASE [VirtualCare] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [VirtualCare] SET DB_CHAINING OFF 
GO
ALTER DATABASE [VirtualCare] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [VirtualCare] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [VirtualCare] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [VirtualCare] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [VirtualCare] SET QUERY_STORE = ON
GO
ALTER DATABASE [VirtualCare] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [VirtualCare]
GO
/****** Object:  Table [dbo].[AdministrationRoutes]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdministrationRoutes](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Code] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_AdministrationRoutess] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AMD]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AMD](
	[Id] [uniqueidentifier] NOT NULL,
	[MedicalRecordId] [uniqueidentifier] NOT NULL,
	[AMDClasificationId] [uniqueidentifier] NOT NULL,
	[Email] [nvarchar](320) NOT NULL,
	[CellPhone] [nvarchar](15) NOT NULL,
	[Phone] [nvarchar](15) NULL,
	[NeighborhoodId] [uniqueidentifier] NOT NULL,
	[Address] [nvarchar](100) NOT NULL,
	[AdrressReference] [nvarchar](100) NULL,
	[Copay] [decimal](18, 2) NULL,
	[WaitingTime] [bigint] NULL,
	[HasCoverage] [bit] NULL,
	[ConfirmInfoAMD] [bit] NULL,
 CONSTRAINT [PK_AMD] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AMDClasifications]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AMDClasifications](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Code] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_AMDClasifications] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BusinessLineLevelValueQueueConfig]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BusinessLineLevelValueQueueConfig](
	[Id] [uniqueidentifier] NOT NULL,
	[LevelQueueId] [uniqueidentifier] NOT NULL,
	[CountryId] [uniqueidentifier] NULL,
	[DepartmentId] [uniqueidentifier] NULL,
	[CityId] [uniqueidentifier] NULL,
	[ServiceId] [uniqueidentifier] NOT NULL,
	[BusinessLineId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_LevelValue] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BusinessLines]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BusinessLines](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Code] [nvarchar](10) NOT NULL,
	[LogoId] [varbinary](max) NULL,
	[LanguageId] [uniqueidentifier] NOT NULL,
	[CityId] [uniqueidentifier] NOT NULL,
	[Address] [nvarchar](200) NOT NULL,
	[ExperienceCenterLeaderId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_BusinessLines] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BusinessLineSubmenus]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BusinessLineSubmenus](
	[Id] [uniqueidentifier] NOT NULL,
	[BusinessLineId] [uniqueidentifier] NOT NULL,
	[SubmenuId] [uniqueidentifier] NOT NULL,
	[Order] [int] NOT NULL,
	[Required] [bit] NOT NULL,
 CONSTRAINT [PK_BusinessLineSubmenus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cities]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cities](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
	[Code] [nvarchar](30) NOT NULL,
	[Active] [bit] NOT NULL,
	[DepartmentId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Cities] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClinicalRecordGynObstHistories]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClinicalRecordGynObstHistories](
	[Id] [uniqueidentifier] NOT NULL,
	[Abortions] [int] NOT NULL,
	[Caesareans] [int] NULL,
	[Gravidas] [int] NULL,
	[Childbirths] [int] NULL,
	[FPP] [datetime] NULL,
	[FUM] [datetime] NULL,
	[MedicalRecordId] [uniqueidentifier] NOT NULL,
	[IsPregnancy] [bit] NULL,
 CONSTRAINT [PK_GynecologicalObstetricHistories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClinicalRecordHistories]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClinicalRecordHistories](
	[Id] [uniqueidentifier] NULL,
	[ClinicalRecordTypeId] [uniqueidentifier] NULL,
	[Value] [nvarchar](50) NULL,
	[Order] [int] NULL,
	[MedicalRecordId] [uniqueidentifier] NOT NULL,
	[Priority] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClinicalRecordHistoriesTypes]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClinicalRecordHistoriesTypes](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Code] [nvarchar](10) NOT NULL,
	[Order] [int] NOT NULL,
 CONSTRAINT [PK_MedicalHistoryTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Colors]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Colors](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Code] [nvarchar](10) NOT NULL,
	[ColorHex] [nchar](7) NOT NULL,
 CONSTRAINT [PK_Colors] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompensationBoxes]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompensationBoxes](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Code] [nvarchar](10) NOT NULL,
	[Score] [int] NULL,
 CONSTRAINT [PK_CompensationBoxes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Countries]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Countries](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
	[Code] [nvarchar](10) NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Departments]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departments](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
	[Code] [nvarchar](10) NOT NULL,
	[Active] [nchar](10) NOT NULL,
	[CountryId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_States] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DiagnosticImpressions]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DiagnosticImpressions](
	[Id] [uniqueidentifier] NOT NULL,
	[DiagnosticId] [uniqueidentifier] NOT NULL,
	[DiagnosticRelatedId] [uniqueidentifier] NOT NULL,
	[MedicalConcept] [nvarchar](max) NULL,
	[Recomendations] [nvarchar](max) NULL,
	[MedicalRecordId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_DiagnosticImpressions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Diagnostics]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Diagnostics](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](200) NOT NULL,
	[Code] [nvarchar](10) NOT NULL,
	[CodeCie10] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_Diagnostics] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Disabilities]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Disabilities](
	[Id] [uniqueidentifier] NOT NULL,
	[MedicalRecordId] [uniqueidentifier] NOT NULL,
	[Extension] [bit] NOT NULL,
	[RetroactiveDisability] [bit] NOT NULL,
	[ReasonMedicalAttention] [nvarchar](50) NULL,
	[DaysIncapacity] [int] NULL,
	[StartDisability] [datetime] NULL,
	[EndDisability] [datetime] NULL,
 CONSTRAINT [PK_Disabilities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DisabilityResends]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DisabilityResends](
	[Id] [uniqueidentifier] NOT NULL,
	[DisabilitySendId] [uniqueidentifier] NOT NULL,
	[Resend] [bit] NOT NULL,
	[ResendAt] [datetime] NOT NULL,
	[Response] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_DisabilityResends] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DisabilitySends]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DisabilitySends](
	[Id] [uniqueidentifier] NOT NULL,
	[DisabilityId] [uniqueidentifier] NOT NULL,
	[Send] [bit] NOT NULL,
	[SentAt] [datetime] NOT NULL,
	[Response] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_DisabilitySends] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Dosages]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dosages](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Code] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_Dosages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Exams]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Exams](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Code] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_Exams] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Genders]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genders](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Code] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_Genders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GeneratedQueues]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GeneratedQueues](
	[Id] [uniqueidentifier] NOT NULL,
	[QueueConfId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Active] [bit] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
 CONSTRAINT [PK_GeneratedQueue] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Groups]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Groups](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Code] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HealthEntities]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HealthEntities](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](200) NOT NULL,
	[Code] [nvarchar](10) NOT NULL,
	[Score] [int] NULL,
 CONSTRAINT [PK_HealthEntities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HealthPolicies]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HealthPolicies](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Code] [nvarchar](10) NOT NULL,
	[HealthEntityId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_HealthPolicies] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Helps]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Helps](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Code] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_Helps] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Icons]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Icons](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Code] [nvarchar](10) NOT NULL,
	[Value] [varbinary](max) NOT NULL,
 CONSTRAINT [PK_Logos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Languages]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Languages](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Code] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_Languages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LevelQueues]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LevelQueues](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Code] [nvarchar](10) NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Levels] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MedicalRecordCalls]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MedicalRecordCalls](
	[Id] [uniqueidentifier] NOT NULL,
	[PhoneNumber] [nvarchar](15) NOT NULL,
	[MedicalRecordId] [uniqueidentifier] NOT NULL,
	[CallTimeSeconds] [bigint] NOT NULL,
 CONSTRAINT [PK_MedicalCareCalls] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MedicalRecordHistories]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MedicalRecordHistories](
	[Id] [uniqueidentifier] NOT NULL,
	[MedicalRecordId] [uniqueidentifier] NOT NULL,
	[MedicalRecordStateId] [uniqueidentifier] NOT NULL,
	[Comments] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_MedicalCaresHistories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MedicalRecordLinkSends]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MedicalRecordLinkSends](
	[Id] [uniqueidentifier] NOT NULL,
	[Links] [nvarchar](500) NOT NULL,
	[MedicalRecordId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_MedicalCareLinkSends] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MedicalRecords]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MedicalRecords](
	[Id] [uniqueidentifier] NOT NULL,
	[PatientId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NULL,
	[ServiceId] [uniqueidentifier] NULL,
	[MedicalRecordStateId] [uniqueidentifier] NOT NULL,
	[ReferredServiceId] [uniqueidentifier] NULL,
	[AutoNumber] [int] IDENTITY(1,1) NOT NULL,
	[TriageId] [uniqueidentifier] NULL,
	[ParamedicComments] [nvarchar](max) NULL,
	[StartedAt] [datetime] NOT NULL,
	[FinishedAt] [datetime] NULL,
 CONSTRAINT [PK_Appointments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MedicalRecordStates]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MedicalRecordStates](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](100) NOT NULL,
	[Code] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_AttentionStates] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MedicalShifts]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MedicalShifts](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Code] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_MedicalShifts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Medicines]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Medicines](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Code] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_Medicines] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Menus]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menus](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](100) NOT NULL,
	[Order] [int] NOT NULL,
	[Code] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_Modules] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Modalities]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Modalities](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Code] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_Modalities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Neighborhoods]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Neighborhoods](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](200) NULL,
	[CityId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Neighborhoods] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NotificationsDtl]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NotificationsDtl](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](100) NULL,
	[Code] [nvarchar](10) NOT NULL,
	[Seconds] [bigint] NOT NULL,
	[Sound] [varbinary](max) NOT NULL,
	[HexColor] [nchar](7) NOT NULL,
	[NotificationHedId] [uniqueidentifier] NOT NULL,
	[SoundId] [uniqueidentifier] NULL,
	[ColorId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_NotificationsDtl] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NotificationsHead]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NotificationsHead](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Code] [nvarchar](7) NOT NULL,
 CONSTRAINT [PK_NotificationsHead] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ParaclinicalDtls]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ParaclinicalDtls](
	[Id] [uniqueidentifier] NOT NULL,
	[ParaclinicalHedId] [uniqueidentifier] NOT NULL,
	[ExamId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_ParaclinicalDtls] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ParaclinicalHeds]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ParaclinicalHeds](
	[Id] [uniqueidentifier] NOT NULL,
	[MedicalRecordId] [uniqueidentifier] NOT NULL,
	[Recomendations] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_ParaclinicalHeds] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ParaclinicalSends]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ParaclinicalSends](
	[Id] [uniqueidentifier] NOT NULL,
	[ParaclinicalHedId] [uniqueidentifier] NOT NULL,
	[Send] [bit] NOT NULL,
	[SendAt] [datetime] NOT NULL,
	[Response] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_ParaclinicalSends] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PatientAttachments]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PatientAttachments](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[FileType] [nvarchar](100) NOT NULL,
	[FileData] [varbinary](max) NOT NULL,
	[FileSize] [bigint] NOT NULL,
	[MedicalRecordId] [uniqueidentifier] NOT NULL,
	[OriginCode] [nvarchar](10) NULL,
 CONSTRAINT [PK_PatientAttachments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PatientInformations]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PatientInformations](
	[Id] [uniqueidentifier] NOT NULL,
	[ReasonConsultations] [nvarchar](max) NULL,
	[ClinicalPresentation] [nvarchar](max) NULL,
	[CurrentIllness] [nvarchar](max) NOT NULL,
	[MedicalRecordId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_InfoPatients] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Patients]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patients](
	[Id] [uniqueidentifier] NOT NULL,
	[PatientIdSO] [uniqueidentifier] NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[SecondName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[SecondLastName] [nvarchar](50) NULL,
	[Birthday] [datetime] NULL,
	[Identification] [nvarchar](50) NULL,
	[PlanId] [uniqueidentifier] NULL,
	[PatientStateId] [uniqueidentifier] NOT NULL,
	[HealthEntityId] [uniqueidentifier] NULL,
	[CompensationBoxId] [uniqueidentifier] NULL,
	[BusinessLineId] [uniqueidentifier] NOT NULL,
	[AutoNumber] [int] IDENTITY(1,1) NOT NULL,
	[HealthPolicyId] [uniqueidentifier] NULL,
	[RegimenId] [uniqueidentifier] NULL,
	[GenderId] [uniqueidentifier] NULL,
	[Email] [nvarchar](320) NOT NULL,
	[CellPhone] [nvarchar](15) NOT NULL,
	[WAPhone] [nvarchar](15) NULL,
	[SendDocsToEmail] [bit] NOT NULL,
	[SendDocsToWA] [bit] NOT NULL,
	[SendDocsToSMS] [bit] NOT NULL,
	[Phone] [nvarchar](15) NULL,
	[CityId] [uniqueidentifier] NULL,
	[NeighborhoodsId] [uniqueidentifier] NULL,
	[Address] [nvarchar](300) NULL,
	[LastSyncDateSO] [datetime] NULL,
 CONSTRAINT [PK_Patients] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PatientStateHistories]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PatientStateHistories](
	[Id] [uniqueidentifier] NOT NULL,
	[PatientId] [uniqueidentifier] NOT NULL,
	[PatientStateId] [uniqueidentifier] NOT NULL,
	[Comments] [nvarchar](max) NULL,
 CONSTRAINT [PK_PatientStateHistories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PatientStates]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PatientStates](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Code] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_PatientStates] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Plans]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Plans](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[Number] [int] NOT NULL,
 CONSTRAINT [PK_Plan] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PrescriptionHed]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrescriptionHed](
	[Id] [uniqueidentifier] NOT NULL,
	[MedicalRecordId] [uniqueidentifier] NOT NULL,
	[IsPos] [bit] NOT NULL,
	[IsComercial] [bit] NOT NULL,
 CONSTRAINT [PK_PrescriptionHed] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PrescriptionHedResends]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrescriptionHedResends](
	[Id] [uniqueidentifier] NOT NULL,
	[PrescriptionHedSendId] [uniqueidentifier] NOT NULL,
	[Resend] [bit] NOT NULL,
	[ResendAt] [datetime] NOT NULL,
	[Response] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_PrescriptionHedResendHistories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PrescriptionHedSends]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrescriptionHedSends](
	[Id] [uniqueidentifier] NOT NULL,
	[PrescriptionHedId] [uniqueidentifier] NOT NULL,
	[Send] [bit] NOT NULL,
	[SendAt] [datetime] NOT NULL,
	[Response] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_PresctiptionHedSyncHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PrescriptionsDtl]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrescriptionsDtl](
	[Id] [uniqueidentifier] NOT NULL,
	[MedicineId] [uniqueidentifier] NOT NULL,
	[AdministrationRouteId] [uniqueidentifier] NOT NULL,
	[PresentationId] [uniqueidentifier] NOT NULL,
	[DosageId] [uniqueidentifier] NOT NULL,
	[Quantity] [int] NOT NULL,
	[ScaleHours] [bit] NOT NULL,
	[ScaleDays] [bit] NOT NULL,
	[ScaleValue] [int] NOT NULL,
	[FrecuencyHours] [bit] NOT NULL,
	[FrecuencyDays] [bit] NOT NULL,
	[FrecuencyValue] [int] NOT NULL,
	[Recomendations] [nvarchar](max) NULL,
	[PrescriptionHed] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Prescriptions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Presentations]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Presentations](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Code] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_Presentations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProcessMessageErrorLogs]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProcessMessageErrorLogs](
	[Id] [uniqueidentifier] NOT NULL,
	[ProcessMessageId] [uniqueidentifier] NOT NULL,
	[ErrorMessage] [nvarchar](max) NOT NULL,
	[StackTrace] [nvarchar](max) NOT NULL,
	[Reason] [nvarchar](20) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
 CONSTRAINT [PK_ProcessMessageErrorLogs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProcessMessages]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProcessMessages](
	[Id] [uniqueidentifier] NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
	[Published] [bit] NULL,
	[PublishedAt] [datetime] NULL,
	[Consumed] [bit] NULL,
	[ConsumedAt] [datetime] NULL,
	[CreatedAt] [datetime] NOT NULL,
	[MedicalRecordId] [uniqueidentifier] NOT NULL,
	[QueueConfId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_ProcessMessages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QueueConfs]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QueueConfs](
	[ID] [uniqueidentifier] NOT NULL,
	[Durable] [bit] NULL,
	[AutoDelete] [bit] NULL,
	[NProcessor] [int] NULL,
	[Active] [bit] NULL,
	[nOrder] [int] NULL,
	[MaxPriority] [int] NULL,
	[Exclusive] [bit] NULL,
	[MessageLifeTime] [int] NULL,
	[QueueExpireTime] [int] NULL,
	[QueueMode] [nvarchar](10) NULL,
	[QueueDeadLetterExchange] [nvarchar](50) NULL,
	[QueueDeadLetterExchangeRoutingKey] [nvarchar](50) NULL,
	[LevelValueQueueConfId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_ConfQueues] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReferredServices]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReferredServices](
	[Id] [uniqueidentifier] NOT NULL,
	[ServiceId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_ReferredServices] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Regimens]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Regimens](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Regimens] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServicePriorities]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServicePriorities](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Code] [nvarchar](10) NOT NULL,
	[Number] [int] NOT NULL,
 CONSTRAINT [PK_ServicePriorities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Services]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Services](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[ColorId] [uniqueidentifier] NULL,
	[IconId] [uniqueidentifier] NULL,
	[AutoNumber] [int] IDENTITY(1,1) NOT NULL,
	[NextMedicalRecordWaitTimeSecond] [int] NOT NULL,
	[GroupId] [uniqueidentifier] NULL,
	[ModalityId] [uniqueidentifier] NULL,
	[NotificationHedId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Services] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sounds]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sounds](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Sound] [varbinary](max) NOT NULL,
 CONSTRAINT [PK_Sounds] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Submenus]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Submenus](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](100) NOT NULL,
	[Code] [nvarchar](10) NOT NULL,
	[MenuId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Submodules] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TemplateConfigs]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TemplateConfigs](
	[Id] [uniqueidentifier] NOT NULL,
	[TemplateTypeId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Code] [nvarchar](10) NOT NULL,
	[Value] [nvarchar](max) NOT NULL,
	[Order] [int] NOT NULL,
	[Pin] [bit] NOT NULL,
 CONSTRAINT [PK_TemplateConfigs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TemplateTypes]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TemplateTypes](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Code] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_TemplateTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Translations]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Translations](
	[Id] [uniqueidentifier] NOT NULL,
	[Key] [nvarchar](20) NOT NULL,
	[Value] [nvarchar](200) NOT NULL,
	[LanguageId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Translations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Treatments]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Treatments](
	[Id] [uniqueidentifier] NOT NULL,
	[MedicationName] [nvarchar](100) NOT NULL,
	[MedicationDose] [int] NOT NULL,
	[Order] [int] NOT NULL,
	[MedicalRecordId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Treatments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Triages]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Triages](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Code] [nvarchar](10) NOT NULL,
	[IconId] [uniqueidentifier] NOT NULL,
	[Score] [int] NULL,
 CONSTRAINT [PK_Triages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserExpires]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserExpires](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[MinutesExpires] [int] NOT NULL,
 CONSTRAINT [PK_LogUserExpires] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserHelps]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserHelps](
	[Id] [uniqueidentifier] NOT NULL,
	[HelpId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Comments] [nvarchar](100) NOT NULL,
	[Resolve] [bit] NULL,
	[ResolveComments] [nvarchar](max) NULL,
	[ResolveAt] [datetime] NULL,
 CONSTRAINT [PK_UserHelps] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserHistories]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserHistories](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[LogIn] [bit] NULL,
	[LogOut] [bit] NULL,
 CONSTRAINT [PK_LogUserHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserMedicalShifts]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserMedicalShifts](
	[Id] [uniqueidentifier] NOT NULL,
	[MedicalShiftId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_UserMedicalShifts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[UserIdSO] [uniqueidentifier] NOT NULL,
	[Loggued] [bit] NOT NULL,
	[UserStateId] [uniqueidentifier] NOT NULL,
	[UserExpireId] [uniqueidentifier] NOT NULL,
	[Photo] [varbinary](max) NULL,
	[AutoNumber] [int] IDENTITY(1,1) NOT NULL,
	[MedicalShiftId] [uniqueidentifier] NULL,
	[BusinessLineId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_LogUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserServices]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserServices](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[ServiceId] [uniqueidentifier] NOT NULL,
	[ServicePriorityId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_UserServices] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserStateHistories]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserStateHistories](
	[Id] [uniqueidentifier] NOT NULL,
	[UserStateId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Comments] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserStateHistories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserStates]    Script Date: 21/02/2025 1:12:01 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserStates](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[Order] [int] NOT NULL,
	[IsBreak] [bit] NOT NULL,
	[RequiredComment] [bit] NOT NULL,
 CONSTRAINT [PK_UserStates] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AMD]  WITH CHECK ADD  CONSTRAINT [FK_AMD_AMDClasifications] FOREIGN KEY([AMDClasificationId])
REFERENCES [dbo].[AMDClasifications] ([Id])
GO
ALTER TABLE [dbo].[AMD] CHECK CONSTRAINT [FK_AMD_AMDClasifications]
GO
ALTER TABLE [dbo].[AMD]  WITH CHECK ADD  CONSTRAINT [FK_AMD_MedicalRecords] FOREIGN KEY([MedicalRecordId])
REFERENCES [dbo].[MedicalRecords] ([Id])
GO
ALTER TABLE [dbo].[AMD] CHECK CONSTRAINT [FK_AMD_MedicalRecords]
GO
ALTER TABLE [dbo].[AMD]  WITH CHECK ADD  CONSTRAINT [FK_AMD_Neighborhoods] FOREIGN KEY([NeighborhoodId])
REFERENCES [dbo].[Neighborhoods] ([Id])
GO
ALTER TABLE [dbo].[AMD] CHECK CONSTRAINT [FK_AMD_Neighborhoods]
GO
ALTER TABLE [dbo].[BusinessLineLevelValueQueueConfig]  WITH CHECK ADD  CONSTRAINT [FK_BusinessLineLevelValueQueueConfig_BusinessLines] FOREIGN KEY([BusinessLineId])
REFERENCES [dbo].[BusinessLines] ([Id])
GO
ALTER TABLE [dbo].[BusinessLineLevelValueQueueConfig] CHECK CONSTRAINT [FK_BusinessLineLevelValueQueueConfig_BusinessLines]
GO
ALTER TABLE [dbo].[BusinessLineLevelValueQueueConfig]  WITH CHECK ADD  CONSTRAINT [FK_LevelValueQueueConfig_Cities] FOREIGN KEY([CityId])
REFERENCES [dbo].[Cities] ([ID])
GO
ALTER TABLE [dbo].[BusinessLineLevelValueQueueConfig] CHECK CONSTRAINT [FK_LevelValueQueueConfig_Cities]
GO
ALTER TABLE [dbo].[BusinessLineLevelValueQueueConfig]  WITH CHECK ADD  CONSTRAINT [FK_LevelValueQueueConfig_Countries] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Countries] ([ID])
GO
ALTER TABLE [dbo].[BusinessLineLevelValueQueueConfig] CHECK CONSTRAINT [FK_LevelValueQueueConfig_Countries]
GO
ALTER TABLE [dbo].[BusinessLineLevelValueQueueConfig]  WITH CHECK ADD  CONSTRAINT [FK_LevelValueQueueConfig_Departments] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Departments] ([ID])
GO
ALTER TABLE [dbo].[BusinessLineLevelValueQueueConfig] CHECK CONSTRAINT [FK_LevelValueQueueConfig_Departments]
GO
ALTER TABLE [dbo].[BusinessLineLevelValueQueueConfig]  WITH CHECK ADD  CONSTRAINT [FK_LevelValueQueueConfig_LevelQueues] FOREIGN KEY([LevelQueueId])
REFERENCES [dbo].[LevelQueues] ([Id])
GO
ALTER TABLE [dbo].[BusinessLineLevelValueQueueConfig] CHECK CONSTRAINT [FK_LevelValueQueueConfig_LevelQueues]
GO
ALTER TABLE [dbo].[BusinessLineLevelValueQueueConfig]  WITH CHECK ADD  CONSTRAINT [FK_LevelValueQueueConfig_Services] FOREIGN KEY([ServiceId])
REFERENCES [dbo].[Services] ([Id])
GO
ALTER TABLE [dbo].[BusinessLineLevelValueQueueConfig] CHECK CONSTRAINT [FK_LevelValueQueueConfig_Services]
GO
ALTER TABLE [dbo].[BusinessLines]  WITH CHECK ADD  CONSTRAINT [FK_BusinessLines_Languages] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Languages] ([Id])
GO
ALTER TABLE [dbo].[BusinessLines] CHECK CONSTRAINT [FK_BusinessLines_Languages]
GO
ALTER TABLE [dbo].[BusinessLines]  WITH CHECK ADD  CONSTRAINT [FK_BusinessLines_Users] FOREIGN KEY([ExperienceCenterLeaderId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[BusinessLines] CHECK CONSTRAINT [FK_BusinessLines_Users]
GO
ALTER TABLE [dbo].[BusinessLineSubmenus]  WITH CHECK ADD  CONSTRAINT [FK_BusinessLineSubmenus_BusinessLines] FOREIGN KEY([BusinessLineId])
REFERENCES [dbo].[BusinessLines] ([Id])
GO
ALTER TABLE [dbo].[BusinessLineSubmenus] CHECK CONSTRAINT [FK_BusinessLineSubmenus_BusinessLines]
GO
ALTER TABLE [dbo].[BusinessLineSubmenus]  WITH CHECK ADD  CONSTRAINT [FK_BusinessLineSubmenus_Submenus] FOREIGN KEY([SubmenuId])
REFERENCES [dbo].[Submenus] ([Id])
GO
ALTER TABLE [dbo].[BusinessLineSubmenus] CHECK CONSTRAINT [FK_BusinessLineSubmenus_Submenus]
GO
ALTER TABLE [dbo].[ClinicalRecordGynObstHistories]  WITH CHECK ADD  CONSTRAINT [FK_GynecologicalObstetricHistories_Appointments] FOREIGN KEY([MedicalRecordId])
REFERENCES [dbo].[MedicalRecords] ([Id])
GO
ALTER TABLE [dbo].[ClinicalRecordGynObstHistories] CHECK CONSTRAINT [FK_GynecologicalObstetricHistories_Appointments]
GO
ALTER TABLE [dbo].[ClinicalRecordHistories]  WITH CHECK ADD  CONSTRAINT [FK_ClinicalRecordHistories_ClinicalRecordHistoriesTypes] FOREIGN KEY([ClinicalRecordTypeId])
REFERENCES [dbo].[ClinicalRecordHistoriesTypes] ([Id])
GO
ALTER TABLE [dbo].[ClinicalRecordHistories] CHECK CONSTRAINT [FK_ClinicalRecordHistories_ClinicalRecordHistoriesTypes]
GO
ALTER TABLE [dbo].[ClinicalRecordHistories]  WITH CHECK ADD  CONSTRAINT [FK_PatientHistories_Appointments] FOREIGN KEY([MedicalRecordId])
REFERENCES [dbo].[MedicalRecords] ([Id])
GO
ALTER TABLE [dbo].[ClinicalRecordHistories] CHECK CONSTRAINT [FK_PatientHistories_Appointments]
GO
ALTER TABLE [dbo].[DiagnosticImpressions]  WITH CHECK ADD  CONSTRAINT [FK_DiagnosticImpressions_Appointments] FOREIGN KEY([MedicalRecordId])
REFERENCES [dbo].[MedicalRecords] ([Id])
GO
ALTER TABLE [dbo].[DiagnosticImpressions] CHECK CONSTRAINT [FK_DiagnosticImpressions_Appointments]
GO
ALTER TABLE [dbo].[DiagnosticImpressions]  WITH CHECK ADD  CONSTRAINT [FK_DiagnosticImpressions_Diagnostics] FOREIGN KEY([DiagnosticId])
REFERENCES [dbo].[Diagnostics] ([Id])
GO
ALTER TABLE [dbo].[DiagnosticImpressions] CHECK CONSTRAINT [FK_DiagnosticImpressions_Diagnostics]
GO
ALTER TABLE [dbo].[DiagnosticImpressions]  WITH CHECK ADD  CONSTRAINT [FK_DiagnosticImpressions_Diagnostics1] FOREIGN KEY([DiagnosticRelatedId])
REFERENCES [dbo].[Diagnostics] ([Id])
GO
ALTER TABLE [dbo].[DiagnosticImpressions] CHECK CONSTRAINT [FK_DiagnosticImpressions_Diagnostics1]
GO
ALTER TABLE [dbo].[Disabilities]  WITH CHECK ADD  CONSTRAINT [FK_Disabilities_MedicalRecords] FOREIGN KEY([MedicalRecordId])
REFERENCES [dbo].[MedicalRecords] ([Id])
GO
ALTER TABLE [dbo].[Disabilities] CHECK CONSTRAINT [FK_Disabilities_MedicalRecords]
GO
ALTER TABLE [dbo].[DisabilityResends]  WITH CHECK ADD  CONSTRAINT [FK_DisabilityResends_DisabilitySends] FOREIGN KEY([DisabilitySendId])
REFERENCES [dbo].[DisabilitySends] ([Id])
GO
ALTER TABLE [dbo].[DisabilityResends] CHECK CONSTRAINT [FK_DisabilityResends_DisabilitySends]
GO
ALTER TABLE [dbo].[DisabilitySends]  WITH CHECK ADD  CONSTRAINT [FK_DisabilitySends_Disabilities] FOREIGN KEY([DisabilityId])
REFERENCES [dbo].[Disabilities] ([Id])
GO
ALTER TABLE [dbo].[DisabilitySends] CHECK CONSTRAINT [FK_DisabilitySends_Disabilities]
GO
ALTER TABLE [dbo].[GeneratedQueues]  WITH CHECK ADD  CONSTRAINT [FK_GeneratedQueues_QueueConfs] FOREIGN KEY([QueueConfId])
REFERENCES [dbo].[QueueConfs] ([ID])
GO
ALTER TABLE [dbo].[GeneratedQueues] CHECK CONSTRAINT [FK_GeneratedQueues_QueueConfs]
GO
ALTER TABLE [dbo].[HealthPolicies]  WITH CHECK ADD  CONSTRAINT [FK_HealthPolicies_HealthEntities] FOREIGN KEY([HealthEntityId])
REFERENCES [dbo].[HealthEntities] ([Id])
GO
ALTER TABLE [dbo].[HealthPolicies] CHECK CONSTRAINT [FK_HealthPolicies_HealthEntities]
GO
ALTER TABLE [dbo].[MedicalRecordCalls]  WITH CHECK ADD  CONSTRAINT [FK_MedicalCareCalls_MedicalCares] FOREIGN KEY([MedicalRecordId])
REFERENCES [dbo].[MedicalRecords] ([Id])
GO
ALTER TABLE [dbo].[MedicalRecordCalls] CHECK CONSTRAINT [FK_MedicalCareCalls_MedicalCares]
GO
ALTER TABLE [dbo].[MedicalRecordHistories]  WITH CHECK ADD  CONSTRAINT [FK_MedicalCaresHistories_MedicalCares] FOREIGN KEY([MedicalRecordId])
REFERENCES [dbo].[MedicalRecords] ([Id])
GO
ALTER TABLE [dbo].[MedicalRecordHistories] CHECK CONSTRAINT [FK_MedicalCaresHistories_MedicalCares]
GO
ALTER TABLE [dbo].[MedicalRecordHistories]  WITH CHECK ADD  CONSTRAINT [FK_MedicalCaresHistories_MedicalCareStates] FOREIGN KEY([MedicalRecordStateId])
REFERENCES [dbo].[MedicalRecordStates] ([Id])
GO
ALTER TABLE [dbo].[MedicalRecordHistories] CHECK CONSTRAINT [FK_MedicalCaresHistories_MedicalCareStates]
GO
ALTER TABLE [dbo].[MedicalRecordLinkSends]  WITH CHECK ADD  CONSTRAINT [FK_MedicalCareLinkSends_MedicalCares] FOREIGN KEY([MedicalRecordId])
REFERENCES [dbo].[MedicalRecords] ([Id])
GO
ALTER TABLE [dbo].[MedicalRecordLinkSends] CHECK CONSTRAINT [FK_MedicalCareLinkSends_MedicalCares]
GO
ALTER TABLE [dbo].[MedicalRecords]  WITH CHECK ADD  CONSTRAINT [FK_Appointments_AppointmentStates] FOREIGN KEY([MedicalRecordStateId])
REFERENCES [dbo].[MedicalRecordStates] ([Id])
GO
ALTER TABLE [dbo].[MedicalRecords] CHECK CONSTRAINT [FK_Appointments_AppointmentStates]
GO
ALTER TABLE [dbo].[MedicalRecords]  WITH CHECK ADD  CONSTRAINT [FK_Appointments_LogUser] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[MedicalRecords] CHECK CONSTRAINT [FK_Appointments_LogUser]
GO
ALTER TABLE [dbo].[MedicalRecords]  WITH CHECK ADD  CONSTRAINT [FK_Appointments_Patients] FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patients] ([Id])
GO
ALTER TABLE [dbo].[MedicalRecords] CHECK CONSTRAINT [FK_Appointments_Patients]
GO
ALTER TABLE [dbo].[MedicalRecords]  WITH CHECK ADD  CONSTRAINT [FK_Appointments_ReferredServices] FOREIGN KEY([ReferredServiceId])
REFERENCES [dbo].[ReferredServices] ([Id])
GO
ALTER TABLE [dbo].[MedicalRecords] CHECK CONSTRAINT [FK_Appointments_ReferredServices]
GO
ALTER TABLE [dbo].[MedicalRecords]  WITH CHECK ADD  CONSTRAINT [FK_Appointments_Services] FOREIGN KEY([ServiceId])
REFERENCES [dbo].[Services] ([Id])
GO
ALTER TABLE [dbo].[MedicalRecords] CHECK CONSTRAINT [FK_Appointments_Services]
GO
ALTER TABLE [dbo].[MedicalRecords]  WITH CHECK ADD  CONSTRAINT [FK_MedicalCares_Triages] FOREIGN KEY([TriageId])
REFERENCES [dbo].[Triages] ([Id])
GO
ALTER TABLE [dbo].[MedicalRecords] CHECK CONSTRAINT [FK_MedicalCares_Triages]
GO
ALTER TABLE [dbo].[Neighborhoods]  WITH CHECK ADD  CONSTRAINT [FK_Neighborhoods_Cities] FOREIGN KEY([CityId])
REFERENCES [dbo].[Cities] ([ID])
GO
ALTER TABLE [dbo].[Neighborhoods] CHECK CONSTRAINT [FK_Neighborhoods_Cities]
GO
ALTER TABLE [dbo].[NotificationsDtl]  WITH CHECK ADD  CONSTRAINT [FK_NotificationsDtl_Colors] FOREIGN KEY([ColorId])
REFERENCES [dbo].[Colors] ([Id])
GO
ALTER TABLE [dbo].[NotificationsDtl] CHECK CONSTRAINT [FK_NotificationsDtl_Colors]
GO
ALTER TABLE [dbo].[NotificationsDtl]  WITH CHECK ADD  CONSTRAINT [FK_NotificationsDtl_NotificationsHead] FOREIGN KEY([NotificationHedId])
REFERENCES [dbo].[NotificationsHead] ([Id])
GO
ALTER TABLE [dbo].[NotificationsDtl] CHECK CONSTRAINT [FK_NotificationsDtl_NotificationsHead]
GO
ALTER TABLE [dbo].[NotificationsDtl]  WITH CHECK ADD  CONSTRAINT [FK_NotificationsDtl_Sounds] FOREIGN KEY([SoundId])
REFERENCES [dbo].[Sounds] ([Id])
GO
ALTER TABLE [dbo].[NotificationsDtl] CHECK CONSTRAINT [FK_NotificationsDtl_Sounds]
GO
ALTER TABLE [dbo].[ParaclinicalDtls]  WITH CHECK ADD  CONSTRAINT [FK_ParaclinicalDtls_Exams] FOREIGN KEY([ExamId])
REFERENCES [dbo].[Exams] ([Id])
GO
ALTER TABLE [dbo].[ParaclinicalDtls] CHECK CONSTRAINT [FK_ParaclinicalDtls_Exams]
GO
ALTER TABLE [dbo].[ParaclinicalDtls]  WITH CHECK ADD  CONSTRAINT [FK_ParaclinicalDtls_ParaclinicalHeds] FOREIGN KEY([ParaclinicalHedId])
REFERENCES [dbo].[ParaclinicalHeds] ([Id])
GO
ALTER TABLE [dbo].[ParaclinicalDtls] CHECK CONSTRAINT [FK_ParaclinicalDtls_ParaclinicalHeds]
GO
ALTER TABLE [dbo].[ParaclinicalHeds]  WITH CHECK ADD  CONSTRAINT [FK_ParaclinicalHeds_MedicalRecords] FOREIGN KEY([MedicalRecordId])
REFERENCES [dbo].[MedicalRecords] ([Id])
GO
ALTER TABLE [dbo].[ParaclinicalHeds] CHECK CONSTRAINT [FK_ParaclinicalHeds_MedicalRecords]
GO
ALTER TABLE [dbo].[ParaclinicalSends]  WITH CHECK ADD  CONSTRAINT [FK_ParaclinicalSends_ParaclinicalHeds] FOREIGN KEY([ParaclinicalHedId])
REFERENCES [dbo].[ParaclinicalHeds] ([Id])
GO
ALTER TABLE [dbo].[ParaclinicalSends] CHECK CONSTRAINT [FK_ParaclinicalSends_ParaclinicalHeds]
GO
ALTER TABLE [dbo].[PatientAttachments]  WITH CHECK ADD  CONSTRAINT [FK_PatientAttachments_Appointments] FOREIGN KEY([MedicalRecordId])
REFERENCES [dbo].[MedicalRecords] ([Id])
GO
ALTER TABLE [dbo].[PatientAttachments] CHECK CONSTRAINT [FK_PatientAttachments_Appointments]
GO
ALTER TABLE [dbo].[PatientInformations]  WITH CHECK ADD  CONSTRAINT [FK_PatientInformations_Appointments] FOREIGN KEY([MedicalRecordId])
REFERENCES [dbo].[MedicalRecords] ([Id])
GO
ALTER TABLE [dbo].[PatientInformations] CHECK CONSTRAINT [FK_PatientInformations_Appointments]
GO
ALTER TABLE [dbo].[Patients]  WITH CHECK ADD  CONSTRAINT [FK_Patients_BusinessLines] FOREIGN KEY([BusinessLineId])
REFERENCES [dbo].[BusinessLines] ([Id])
GO
ALTER TABLE [dbo].[Patients] CHECK CONSTRAINT [FK_Patients_BusinessLines]
GO
ALTER TABLE [dbo].[Patients]  WITH CHECK ADD  CONSTRAINT [FK_Patients_Cities] FOREIGN KEY([CityId])
REFERENCES [dbo].[Cities] ([ID])
GO
ALTER TABLE [dbo].[Patients] CHECK CONSTRAINT [FK_Patients_Cities]
GO
ALTER TABLE [dbo].[Patients]  WITH CHECK ADD  CONSTRAINT [FK_Patients_CompensationBoxes] FOREIGN KEY([CompensationBoxId])
REFERENCES [dbo].[CompensationBoxes] ([Id])
GO
ALTER TABLE [dbo].[Patients] CHECK CONSTRAINT [FK_Patients_CompensationBoxes]
GO
ALTER TABLE [dbo].[Patients]  WITH CHECK ADD  CONSTRAINT [FK_Patients_Genders] FOREIGN KEY([GenderId])
REFERENCES [dbo].[Genders] ([Id])
GO
ALTER TABLE [dbo].[Patients] CHECK CONSTRAINT [FK_Patients_Genders]
GO
ALTER TABLE [dbo].[Patients]  WITH CHECK ADD  CONSTRAINT [FK_Patients_HealthEntities] FOREIGN KEY([HealthEntityId])
REFERENCES [dbo].[HealthEntities] ([Id])
GO
ALTER TABLE [dbo].[Patients] CHECK CONSTRAINT [FK_Patients_HealthEntities]
GO
ALTER TABLE [dbo].[Patients]  WITH CHECK ADD  CONSTRAINT [FK_Patients_HealthPolicies] FOREIGN KEY([HealthPolicyId])
REFERENCES [dbo].[HealthPolicies] ([Id])
GO
ALTER TABLE [dbo].[Patients] CHECK CONSTRAINT [FK_Patients_HealthPolicies]
GO
ALTER TABLE [dbo].[Patients]  WITH CHECK ADD  CONSTRAINT [FK_Patients_Neighborhoods] FOREIGN KEY([NeighborhoodsId])
REFERENCES [dbo].[Neighborhoods] ([Id])
GO
ALTER TABLE [dbo].[Patients] CHECK CONSTRAINT [FK_Patients_Neighborhoods]
GO
ALTER TABLE [dbo].[Patients]  WITH CHECK ADD  CONSTRAINT [FK_Patients_PatientStates] FOREIGN KEY([PatientStateId])
REFERENCES [dbo].[PatientStates] ([Id])
GO
ALTER TABLE [dbo].[Patients] CHECK CONSTRAINT [FK_Patients_PatientStates]
GO
ALTER TABLE [dbo].[Patients]  WITH CHECK ADD  CONSTRAINT [FK_Patients_Plans] FOREIGN KEY([PlanId])
REFERENCES [dbo].[Plans] ([ID])
GO
ALTER TABLE [dbo].[Patients] CHECK CONSTRAINT [FK_Patients_Plans]
GO
ALTER TABLE [dbo].[Patients]  WITH CHECK ADD  CONSTRAINT [FK_Patients_Regimens] FOREIGN KEY([RegimenId])
REFERENCES [dbo].[Regimens] ([Id])
GO
ALTER TABLE [dbo].[Patients] CHECK CONSTRAINT [FK_Patients_Regimens]
GO
ALTER TABLE [dbo].[PatientStateHistories]  WITH CHECK ADD  CONSTRAINT [FK_PatientStateHistories_Patients] FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patients] ([Id])
GO
ALTER TABLE [dbo].[PatientStateHistories] CHECK CONSTRAINT [FK_PatientStateHistories_Patients]
GO
ALTER TABLE [dbo].[PatientStateHistories]  WITH CHECK ADD  CONSTRAINT [FK_PatientStateHistories_PatientStates] FOREIGN KEY([PatientStateId])
REFERENCES [dbo].[PatientStates] ([Id])
GO
ALTER TABLE [dbo].[PatientStateHistories] CHECK CONSTRAINT [FK_PatientStateHistories_PatientStates]
GO
ALTER TABLE [dbo].[PrescriptionHed]  WITH CHECK ADD  CONSTRAINT [FK_PrescriptionHed_MedicalRecords] FOREIGN KEY([MedicalRecordId])
REFERENCES [dbo].[MedicalRecords] ([Id])
GO
ALTER TABLE [dbo].[PrescriptionHed] CHECK CONSTRAINT [FK_PrescriptionHed_MedicalRecords]
GO
ALTER TABLE [dbo].[PrescriptionHedResends]  WITH CHECK ADD  CONSTRAINT [FK_PrescriptionHedResendHistories_PrescriptionHedSendHistories] FOREIGN KEY([PrescriptionHedSendId])
REFERENCES [dbo].[PrescriptionHedSends] ([Id])
GO
ALTER TABLE [dbo].[PrescriptionHedResends] CHECK CONSTRAINT [FK_PrescriptionHedResendHistories_PrescriptionHedSendHistories]
GO
ALTER TABLE [dbo].[PrescriptionHedSends]  WITH CHECK ADD  CONSTRAINT [FK_PresctiptionHedSyncHistory_PrescriptionHed] FOREIGN KEY([PrescriptionHedId])
REFERENCES [dbo].[PrescriptionHed] ([Id])
GO
ALTER TABLE [dbo].[PrescriptionHedSends] CHECK CONSTRAINT [FK_PresctiptionHedSyncHistory_PrescriptionHed]
GO
ALTER TABLE [dbo].[PrescriptionsDtl]  WITH CHECK ADD  CONSTRAINT [FK_PrescriptionsDtl_AdministrationRoutes] FOREIGN KEY([AdministrationRouteId])
REFERENCES [dbo].[AdministrationRoutes] ([Id])
GO
ALTER TABLE [dbo].[PrescriptionsDtl] CHECK CONSTRAINT [FK_PrescriptionsDtl_AdministrationRoutes]
GO
ALTER TABLE [dbo].[PrescriptionsDtl]  WITH CHECK ADD  CONSTRAINT [FK_PrescriptionsDtl_Dosages] FOREIGN KEY([DosageId])
REFERENCES [dbo].[Dosages] ([Id])
GO
ALTER TABLE [dbo].[PrescriptionsDtl] CHECK CONSTRAINT [FK_PrescriptionsDtl_Dosages]
GO
ALTER TABLE [dbo].[PrescriptionsDtl]  WITH CHECK ADD  CONSTRAINT [FK_PrescriptionsDtl_Medicines] FOREIGN KEY([MedicineId])
REFERENCES [dbo].[Medicines] ([Id])
GO
ALTER TABLE [dbo].[PrescriptionsDtl] CHECK CONSTRAINT [FK_PrescriptionsDtl_Medicines]
GO
ALTER TABLE [dbo].[PrescriptionsDtl]  WITH CHECK ADD  CONSTRAINT [FK_PrescriptionsDtl_PrescriptionHed] FOREIGN KEY([PrescriptionHed])
REFERENCES [dbo].[PrescriptionHed] ([Id])
GO
ALTER TABLE [dbo].[PrescriptionsDtl] CHECK CONSTRAINT [FK_PrescriptionsDtl_PrescriptionHed]
GO
ALTER TABLE [dbo].[PrescriptionsDtl]  WITH CHECK ADD  CONSTRAINT [FK_PrescriptionsDtl_Presentations] FOREIGN KEY([PresentationId])
REFERENCES [dbo].[Presentations] ([Id])
GO
ALTER TABLE [dbo].[PrescriptionsDtl] CHECK CONSTRAINT [FK_PrescriptionsDtl_Presentations]
GO
ALTER TABLE [dbo].[ProcessMessageErrorLogs]  WITH CHECK ADD  CONSTRAINT [FK_ProcessMessageErrorLogs_ProcessMessages] FOREIGN KEY([ProcessMessageId])
REFERENCES [dbo].[ProcessMessages] ([Id])
GO
ALTER TABLE [dbo].[ProcessMessageErrorLogs] CHECK CONSTRAINT [FK_ProcessMessageErrorLogs_ProcessMessages]
GO
ALTER TABLE [dbo].[ProcessMessages]  WITH CHECK ADD  CONSTRAINT [FK_ProcessMessages_Appoinments] FOREIGN KEY([MedicalRecordId])
REFERENCES [dbo].[MedicalRecords] ([Id])
GO
ALTER TABLE [dbo].[ProcessMessages] CHECK CONSTRAINT [FK_ProcessMessages_Appoinments]
GO
ALTER TABLE [dbo].[ProcessMessages]  WITH CHECK ADD  CONSTRAINT [FK_ProcessMessages_QueueConfs] FOREIGN KEY([QueueConfId])
REFERENCES [dbo].[QueueConfs] ([ID])
GO
ALTER TABLE [dbo].[ProcessMessages] CHECK CONSTRAINT [FK_ProcessMessages_QueueConfs]
GO
ALTER TABLE [dbo].[QueueConfs]  WITH CHECK ADD  CONSTRAINT [FK_QueueConfs_LevelValueQueueConfig] FOREIGN KEY([LevelValueQueueConfId])
REFERENCES [dbo].[BusinessLineLevelValueQueueConfig] ([Id])
GO
ALTER TABLE [dbo].[QueueConfs] CHECK CONSTRAINT [FK_QueueConfs_LevelValueQueueConfig]
GO
ALTER TABLE [dbo].[Services]  WITH CHECK ADD  CONSTRAINT [FK_Services_Colors] FOREIGN KEY([ColorId])
REFERENCES [dbo].[Colors] ([Id])
GO
ALTER TABLE [dbo].[Services] CHECK CONSTRAINT [FK_Services_Colors]
GO
ALTER TABLE [dbo].[Services]  WITH CHECK ADD  CONSTRAINT [FK_Services_Groups] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Groups] ([Id])
GO
ALTER TABLE [dbo].[Services] CHECK CONSTRAINT [FK_Services_Groups]
GO
ALTER TABLE [dbo].[Services]  WITH CHECK ADD  CONSTRAINT [FK_Services_Logos] FOREIGN KEY([IconId])
REFERENCES [dbo].[Icons] ([Id])
GO
ALTER TABLE [dbo].[Services] CHECK CONSTRAINT [FK_Services_Logos]
GO
ALTER TABLE [dbo].[Services]  WITH CHECK ADD  CONSTRAINT [FK_Services_Modalities] FOREIGN KEY([ModalityId])
REFERENCES [dbo].[Modalities] ([Id])
GO
ALTER TABLE [dbo].[Services] CHECK CONSTRAINT [FK_Services_Modalities]
GO
ALTER TABLE [dbo].[Services]  WITH CHECK ADD  CONSTRAINT [FK_Services_NotificationsHead] FOREIGN KEY([NotificationHedId])
REFERENCES [dbo].[NotificationsHead] ([Id])
GO
ALTER TABLE [dbo].[Services] CHECK CONSTRAINT [FK_Services_NotificationsHead]
GO
ALTER TABLE [dbo].[Submenus]  WITH CHECK ADD  CONSTRAINT [FK_Submenus_Menus] FOREIGN KEY([MenuId])
REFERENCES [dbo].[Menus] ([Id])
GO
ALTER TABLE [dbo].[Submenus] CHECK CONSTRAINT [FK_Submenus_Menus]
GO
ALTER TABLE [dbo].[TemplateConfigs]  WITH CHECK ADD  CONSTRAINT [FK_TemplateConfigs_TemplateTypes] FOREIGN KEY([TemplateTypeId])
REFERENCES [dbo].[TemplateTypes] ([Id])
GO
ALTER TABLE [dbo].[TemplateConfigs] CHECK CONSTRAINT [FK_TemplateConfigs_TemplateTypes]
GO
ALTER TABLE [dbo].[Translations]  WITH CHECK ADD  CONSTRAINT [FK_Translations_Languages] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Languages] ([Id])
GO
ALTER TABLE [dbo].[Translations] CHECK CONSTRAINT [FK_Translations_Languages]
GO
ALTER TABLE [dbo].[Treatments]  WITH CHECK ADD  CONSTRAINT [FK_Treatments_Appointments] FOREIGN KEY([MedicalRecordId])
REFERENCES [dbo].[MedicalRecords] ([Id])
GO
ALTER TABLE [dbo].[Treatments] CHECK CONSTRAINT [FK_Treatments_Appointments]
GO
ALTER TABLE [dbo].[Triages]  WITH CHECK ADD  CONSTRAINT [FK_Triages_Icons] FOREIGN KEY([IconId])
REFERENCES [dbo].[Icons] ([Id])
GO
ALTER TABLE [dbo].[Triages] CHECK CONSTRAINT [FK_Triages_Icons]
GO
ALTER TABLE [dbo].[UserHelps]  WITH CHECK ADD  CONSTRAINT [FK_UserHelps_Helps] FOREIGN KEY([HelpId])
REFERENCES [dbo].[Helps] ([Id])
GO
ALTER TABLE [dbo].[UserHelps] CHECK CONSTRAINT [FK_UserHelps_Helps]
GO
ALTER TABLE [dbo].[UserHelps]  WITH CHECK ADD  CONSTRAINT [FK_UserHelps_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UserHelps] CHECK CONSTRAINT [FK_UserHelps_Users]
GO
ALTER TABLE [dbo].[UserHistories]  WITH CHECK ADD  CONSTRAINT [FK_LogUserHistory_LogUser] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UserHistories] CHECK CONSTRAINT [FK_LogUserHistory_LogUser]
GO
ALTER TABLE [dbo].[UserMedicalShifts]  WITH CHECK ADD  CONSTRAINT [FK_UserMedicalShifts_MedicalShifts] FOREIGN KEY([MedicalShiftId])
REFERENCES [dbo].[MedicalShifts] ([Id])
GO
ALTER TABLE [dbo].[UserMedicalShifts] CHECK CONSTRAINT [FK_UserMedicalShifts_MedicalShifts]
GO
ALTER TABLE [dbo].[UserMedicalShifts]  WITH CHECK ADD  CONSTRAINT [FK_UserMedicalShifts_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UserMedicalShifts] CHECK CONSTRAINT [FK_UserMedicalShifts_Users]
GO
ALTER TABLE [dbo].[UserMedicalShifts]  WITH CHECK ADD  CONSTRAINT [FK_UserMedicalShifts_Users1] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UserMedicalShifts] CHECK CONSTRAINT [FK_UserMedicalShifts_Users1]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_BusinessLines] FOREIGN KEY([BusinessLineId])
REFERENCES [dbo].[BusinessLines] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_BusinessLines]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_UserExpires] FOREIGN KEY([UserExpireId])
REFERENCES [dbo].[UserExpires] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_UserExpires]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_UserStates] FOREIGN KEY([UserStateId])
REFERENCES [dbo].[UserStates] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_UserStates]
GO
ALTER TABLE [dbo].[UserServices]  WITH CHECK ADD  CONSTRAINT [FK_UserServices_ServicePriorities] FOREIGN KEY([ServicePriorityId])
REFERENCES [dbo].[ServicePriorities] ([Id])
GO
ALTER TABLE [dbo].[UserServices] CHECK CONSTRAINT [FK_UserServices_ServicePriorities]
GO
ALTER TABLE [dbo].[UserServices]  WITH CHECK ADD  CONSTRAINT [FK_UserServices_Services] FOREIGN KEY([ServiceId])
REFERENCES [dbo].[Services] ([Id])
GO
ALTER TABLE [dbo].[UserServices] CHECK CONSTRAINT [FK_UserServices_Services]
GO
ALTER TABLE [dbo].[UserServices]  WITH CHECK ADD  CONSTRAINT [FK_UserServices_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UserServices] CHECK CONSTRAINT [FK_UserServices_Users]
GO
ALTER TABLE [dbo].[UserStateHistories]  WITH CHECK ADD  CONSTRAINT [FK_UserStateHistories_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UserStateHistories] CHECK CONSTRAINT [FK_UserStateHistories_Users]
GO
ALTER TABLE [dbo].[UserStateHistories]  WITH CHECK ADD  CONSTRAINT [FK_UserStateHistories_UserStates] FOREIGN KEY([UserStateId])
REFERENCES [dbo].[UserStates] ([Id])
GO
ALTER TABLE [dbo].[UserStateHistories] CHECK CONSTRAINT [FK_UserStateHistories_UserStates]
GO
USE [master]
GO
ALTER DATABASE [VirtualCare] SET  READ_WRITE 
GO
