/****** Object:  Database [GameHub]    Script Date: 1/6/2020 9:53:47 PM ******/
CREATE DATABASE [GameHub]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'GameHub', FILENAME = N'C:\Users\Antho\GameHub.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'GameHub_log', FILENAME = N'C:\Users\Antho\GameHub_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [GameHub] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [GameHub].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [GameHub] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [GameHub] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [GameHub] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [GameHub] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [GameHub] SET ARITHABORT OFF 
GO
ALTER DATABASE [GameHub] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [GameHub] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [GameHub] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [GameHub] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [GameHub] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [GameHub] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [GameHub] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [GameHub] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [GameHub] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [GameHub] SET  DISABLE_BROKER 
GO
ALTER DATABASE [GameHub] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [GameHub] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [GameHub] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [GameHub] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [GameHub] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [GameHub] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [GameHub] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [GameHub] SET RECOVERY FULL 
GO
ALTER DATABASE [GameHub] SET  MULTI_USER 
GO
ALTER DATABASE [GameHub] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [GameHub] SET DB_CHAINING OFF 
GO
ALTER DATABASE [GameHub] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [GameHub] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [GameHub] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [GameHub] SET QUERY_STORE = OFF
GO
USE [GameHub]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [GameHub]
GO
/****** Object:  Table [dbo].[tblAPICallLog]    Script Date: 1/6/2020 9:53:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblAPICallLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[TokenId] [int] NULL,
	[MethodTypeId] [tinyint] NOT NULL,
	[Url] [nvarchar](1000) NOT NULL,
	[Data] [nvarchar](max) NULL,
	[Response] [nvarchar](max) NULL,
	[ResponseStatusCodeId] [int] NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[Modified] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_tblAPICallLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblBrowser]    Script Date: 1/6/2020 9:53:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblBrowser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Version] [nvarchar](50) NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_tblBrowser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblBTBActionType]    Script Date: 1/6/2020 9:53:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblBTBActionType](
	[Id] [tinyint] NOT NULL,
	[Name] [nvarchar](25) NOT NULL,
	[CreatedBy] [nvarchar](20) NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[Modified] [datetimeoffset](7) NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_tblBTBAction] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblBTBCaseAmount]    Script Date: 1/6/2020 9:53:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblBTBCaseAmount](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Amount] [int] NOT NULL,
	[CreatedBy] [nvarchar](20) NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[Modified] [datetimeoffset](7) NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_tblBTBCaseAmount] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblBTBGame]    Script Date: 1/6/2020 9:53:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblBTBGame](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GUID] [nvarchar](50) NOT NULL,
	[UserId] [int] NOT NULL,
	[StatusId] [int] NOT NULL,
	[StartTime] [datetimeoffset](7) NULL,
	[EndTime] [datetimeoffset](7) NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[Modified] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_tblBTBGame] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblBTBGameBankerOffer]    Script Date: 1/6/2020 9:53:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblBTBGameBankerOffer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GameId] [int] NOT NULL,
	[OfferAmount] [int] NOT NULL,
	[StatusId] [int] NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[Modified] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_tblBTBGameBankerOffer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblBTBGameCase]    Script Date: 1/6/2020 9:53:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblBTBGameCase](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GUID] [nvarchar](50) NOT NULL,
	[GameId] [int] NOT NULL,
	[CaseAmountId] [int] NOT NULL,
	[CaseNumber] [int] NOT NULL,
	[StatusId] [int] NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[Modified] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_tblBTBGameCase] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblBTBGameChosenCase]    Script Date: 1/6/2020 9:53:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblBTBGameChosenCase](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GameCaseId] [int] NOT NULL,
	[StatusId] [int] NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[Modified] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_tblBTBGameChoosenCase] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblBTBGameLog]    Script Date: 1/6/2020 9:53:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblBTBGameLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GameId] [int] NOT NULL,
	[ActionId] [tinyint] NOT NULL,
	[UserId] [int] NOT NULL,
	[TableEntityId] [int] NULL,
	[SourceId] [nvarchar](50) NULL,
	[Created] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_tblBTBGameLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblBTBGameResult]    Script Date: 1/6/2020 9:53:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblBTBGameResult](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GameId] [int] NOT NULL,
	[ResultId] [tinyint] NOT NULL,
	[AmountWon] [int] NOT NULL,
	[TableEntityId] [int] NOT NULL,
	[SourceId] [nvarchar](50) NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_tblBTBGameResult] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblBTBResultType]    Script Date: 1/6/2020 9:53:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblBTBResultType](
	[Id] [tinyint] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[Modified] [datetimeoffset](7) NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_tblBTBResultType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblClient]    Script Date: 1/6/2020 9:53:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblClient](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OSId] [int] NOT NULL,
	[BrowserId] [int] NOT NULL,
	[DeviceTypeId] [int] NOT NULL,
	[IP] [nvarchar](100) NOT NULL,
	[UserAgent] [nvarchar](500) NOT NULL,
	[Crawler] [bit] NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_tblClient] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblDeviceType]    Script Date: 1/6/2020 9:53:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblDeviceType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_tblDeviceType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblErrorLog]    Script Date: 1/6/2020 9:53:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblErrorLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Exception] [nvarchar](max) NOT NULL,
	[Message] [nvarchar](2000) NOT NULL,
	[Info] [nvarchar](max) NOT NULL,
	[SeverityCode] [int] NOT NULL,
	[UserId] [int] NULL,
	[Created] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_tblErrorLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblHttpMethodType]    Script Date: 1/6/2020 9:53:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblHttpMethodType](
	[Id] [tinyint] NOT NULL,
	[Name] [nvarchar](10) NOT NULL,
	[CreatedBy] [nvarchar](20) NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[Modified] [datetimeoffset](7) NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_tblHttpMethodType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblHttpStatusCode]    Script Date: 1/6/2020 9:53:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblHttpStatusCode](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[Modified] [datetimeoffset](7) NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_HttpStatusCode] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblLogin]    Script Date: 1/6/2020 9:53:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblLogin](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ClientId] [int] NOT NULL,
	[MethodId] [int] NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_tblLogin] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblLoginMethod]    Script Date: 1/6/2020 9:53:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblLoginMethod](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_tblLoginMethod] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblOS]    Script Date: 1/6/2020 9:53:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblOS](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Version] [nvarchar](50) NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_tblOS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblPlayFabToken]    Script Date: 1/6/2020 9:53:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblPlayFabToken](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PlayFabUserId] [int] NOT NULL,
	[SessionTicket] [nvarchar](500) NOT NULL,
	[EntityToken] [nvarchar](1000) NOT NULL,
	[TokenExperation] [datetimeoffset](7) NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[Modified] [datetimeoffset](7) NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_tblPlayFabToken] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblPlayFabUser]    Script Date: 1/6/2020 9:53:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblPlayFabUser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[PlayFabId] [nvarchar](50) NOT NULL,
	[Username] [nvarchar](20) NULL,
	[Email] [nvarchar](50) NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[Modified] [datetimeoffset](7) NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_tblPlayFabUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblStatus]    Script Date: 1/6/2020 9:53:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblStatus](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](25) NOT NULL,
	[Description] [nvarchar](150) NULL,
	[TypeId] [tinyint] NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[Modified] [datetimeoffset](7) NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_tblStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblStatusType]    Script Date: 1/6/2020 9:53:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblStatusType](
	[Id] [tinyint] NOT NULL,
	[Name] [nvarchar](25) NOT NULL,
	[Description] [nvarchar](150) NULL,
	[CreatedBy] [nvarchar](20) NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[Modified] [datetimeoffset](7) NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_tblStatusType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblTableEntity]    Script Date: 1/6/2020 9:53:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblTableEntity](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[Modified] [datetimeoffset](7) NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_tblTableEntity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblToken]    Script Date: 1/6/2020 9:53:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblToken](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GUID] [nvarchar](50) NOT NULL,
	[UserId] [int] NOT NULL,
	[AccessToken] [nvarchar](2000) NOT NULL,
	[RefreshToken] [nvarchar](2000) NULL,
	[TypeId] [tinyint] NOT NULL,
	[StatusId] [int] NOT NULL,
	[ExpiresIn] [int] NULL,
	[ExpirationDateTime] [datetimeoffset](7) NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[Modified] [datetimeoffset](7) NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_tblToken] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblTokenType]    Script Date: 1/6/2020 9:53:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblTokenType](
	[Id] [tinyint] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](50) NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[Modified] [datetimeoffset](7) NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_tblTokenType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblTracking]    Script Date: 1/6/2020 9:53:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblTracking](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[TrackingActionId] [int] NOT NULL,
	[ClientId] [int] NULL,
	[OldValue] [nvarchar](2000) NULL,
	[NewValue] [nvarchar](2000) NOT NULL,
	[TableEntityId] [int] NULL,
	[SourceId] [nvarchar](50) NULL,
	[Created] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_tblTracking] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblTrackingAction]    Script Date: 1/6/2020 9:53:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblTrackingAction](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](150) NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[Modified] [datetimeoffset](7) NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_tblTrackingActionType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblUser]    Script Date: 1/6/2020 9:53:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblUser](
	[Id] [int] IDENTITY(8950,1) NOT NULL,
	[GUID] [nvarchar](50) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](150) NOT NULL,
	[StatusId] [int] NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[Modified] [datetimeoffset](7) NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_tblUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[tblBTBActionType] ([Id], [Name], [CreatedBy], [Created], [Modified], [Active]) VALUES (1, N'CaseChosen', N'ATuccitto', CAST(N'2020-01-01T19:24:15.7800000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblBTBActionType] ([Id], [Name], [CreatedBy], [Created], [Modified], [Active]) VALUES (2, N'CaseOpened', N'ATuccitto', CAST(N'2020-01-01T19:24:24.7233333+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblBTBActionType] ([Id], [Name], [CreatedBy], [Created], [Modified], [Active]) VALUES (3, N'BankOffer', N'ATuccitto', CAST(N'2020-01-01T19:24:32.1200000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblBTBActionType] ([Id], [Name], [CreatedBy], [Created], [Modified], [Active]) VALUES (4, N'DeclinedOffer', N'ATuccitto', CAST(N'2020-01-01T19:24:41.9666667+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblBTBActionType] ([Id], [Name], [CreatedBy], [Created], [Modified], [Active]) VALUES (5, N'AcceptedOffer', N'ATuccitto', CAST(N'2020-01-01T19:24:54.9933333+00:00' AS DateTimeOffset), NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[tblBTBCaseAmount] ON 
GO
INSERT [dbo].[tblBTBCaseAmount] ([Id], [Amount], [CreatedBy], [Created], [Modified], [Active]) VALUES (1, 1, N'ATuccitto', CAST(N'2020-01-01T19:27:52.7533333+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblBTBCaseAmount] ([Id], [Amount], [CreatedBy], [Created], [Modified], [Active]) VALUES (2, 10, N'ATuccitto', CAST(N'2020-01-01T19:27:58.0700000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblBTBCaseAmount] ([Id], [Amount], [CreatedBy], [Created], [Modified], [Active]) VALUES (3, 25, N'ATuccitto', CAST(N'2020-01-01T19:28:02.3266667+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblBTBCaseAmount] ([Id], [Amount], [CreatedBy], [Created], [Modified], [Active]) VALUES (4, 50, N'ATuccitto', CAST(N'2020-01-01T19:28:06.8700000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblBTBCaseAmount] ([Id], [Amount], [CreatedBy], [Created], [Modified], [Active]) VALUES (5, 75, N'ATuccitto', CAST(N'2020-01-01T19:28:10.9366667+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblBTBCaseAmount] ([Id], [Amount], [CreatedBy], [Created], [Modified], [Active]) VALUES (6, 100, N'ATuccitto', CAST(N'2020-01-01T19:28:16.0133333+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblBTBCaseAmount] ([Id], [Amount], [CreatedBy], [Created], [Modified], [Active]) VALUES (7, 250, N'ATuccitto', CAST(N'2020-01-01T19:28:20.2533333+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblBTBCaseAmount] ([Id], [Amount], [CreatedBy], [Created], [Modified], [Active]) VALUES (8, 500, N'ATuccitto', CAST(N'2020-01-01T19:28:35.0100000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblBTBCaseAmount] ([Id], [Amount], [CreatedBy], [Created], [Modified], [Active]) VALUES (9, 750, N'ATuccitto', CAST(N'2020-01-01T19:28:41.5700000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblBTBCaseAmount] ([Id], [Amount], [CreatedBy], [Created], [Modified], [Active]) VALUES (10, 1000, N'ATuccitto', CAST(N'2020-01-01T19:28:47.8500000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblBTBCaseAmount] ([Id], [Amount], [CreatedBy], [Created], [Modified], [Active]) VALUES (11, 5000, N'ATuccitto', CAST(N'2020-01-01T19:29:00.0433333+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblBTBCaseAmount] ([Id], [Amount], [CreatedBy], [Created], [Modified], [Active]) VALUES (12, 10000, N'ATuccitto', CAST(N'2020-01-01T19:29:17.4000000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblBTBCaseAmount] ([Id], [Amount], [CreatedBy], [Created], [Modified], [Active]) VALUES (13, 25000, N'ATuccitto', CAST(N'2020-01-01T19:29:24.6400000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblBTBCaseAmount] ([Id], [Amount], [CreatedBy], [Created], [Modified], [Active]) VALUES (14, 50000, N'ATuccitto', CAST(N'2020-01-01T19:29:36.4333333+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblBTBCaseAmount] ([Id], [Amount], [CreatedBy], [Created], [Modified], [Active]) VALUES (15, 75000, N'ATuccitto', CAST(N'2020-01-01T19:29:44.0800000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblBTBCaseAmount] ([Id], [Amount], [CreatedBy], [Created], [Modified], [Active]) VALUES (16, 100000, N'ATuccitto', CAST(N'2020-01-01T19:29:50.5433333+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblBTBCaseAmount] ([Id], [Amount], [CreatedBy], [Created], [Modified], [Active]) VALUES (17, 250000, N'ATuccitto', CAST(N'2020-01-01T19:29:57.2700000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblBTBCaseAmount] ([Id], [Amount], [CreatedBy], [Created], [Modified], [Active]) VALUES (18, 500000, N'ATuccitto', CAST(N'2020-01-01T19:30:04.6300000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblBTBCaseAmount] ([Id], [Amount], [CreatedBy], [Created], [Modified], [Active]) VALUES (19, 750000, N'ATuccitto', CAST(N'2020-01-01T19:30:13.4066667+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblBTBCaseAmount] ([Id], [Amount], [CreatedBy], [Created], [Modified], [Active]) VALUES (20, 1000000, N'ATuccitto', CAST(N'2020-01-01T19:30:19.7466667+00:00' AS DateTimeOffset), NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[tblBTBCaseAmount] OFF
GO
INSERT [dbo].[tblBTBResultType] ([Id], [Name], [CreatedBy], [Created], [Modified], [Active]) VALUES (1, N'BankOfferAccepted', N'ATuccitto', CAST(N'2020-01-01T19:49:59.8766667+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblBTBResultType] ([Id], [Name], [CreatedBy], [Created], [Modified], [Active]) VALUES (2, N'KeptCase', N'ATuccitto', CAST(N'2020-01-01T19:50:42.9633333+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpMethodType] ([Id], [Name], [CreatedBy], [Created], [Modified], [Active]) VALUES (1, N'GET', N'ATuccitto', CAST(N'2019-12-29T20:40:17.9866667+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpMethodType] ([Id], [Name], [CreatedBy], [Created], [Modified], [Active]) VALUES (2, N'POST', N'ATuccitto', CAST(N'2019-12-29T20:40:25.1033333+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpMethodType] ([Id], [Name], [CreatedBy], [Created], [Modified], [Active]) VALUES (3, N'PATCH', N'ATuccitto', CAST(N'2019-12-29T20:40:32.7566667+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpMethodType] ([Id], [Name], [CreatedBy], [Created], [Modified], [Active]) VALUES (4, N'PUT', N'ATuccitto', CAST(N'2019-12-29T20:40:39.4600000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpMethodType] ([Id], [Name], [CreatedBy], [Created], [Modified], [Active]) VALUES (5, N'DELETE', N'ATuccitto', CAST(N'2019-12-29T20:40:45.3200000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (100, N'Continue', CAST(N'2019-12-13T09:43:20.1900000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (101, N'SwitchingProtocols', CAST(N'2019-12-13T09:43:27.7300000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (102, N'Processing', CAST(N'2019-12-13T09:43:35.6170000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (103, N'EarlyHints', CAST(N'2019-12-13T09:43:42.6100000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (200, N'OK', CAST(N'2019-12-13T09:43:48.2030000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (201, N'Created', CAST(N'2019-12-13T09:43:54.9830000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (202, N'Accepted', CAST(N'2019-12-13T09:44:01.4770000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (203, N'NonAuthoritativeInformation', CAST(N'2019-12-13T09:44:09.4670000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (204, N'NoContent', CAST(N'2019-12-13T09:44:21.8470000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (205, N'ResetContent', CAST(N'2019-12-13T09:44:28.0430000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (206, N'PartialContent', CAST(N'2019-12-13T09:44:37.9700000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (207, N'MultiStatus', CAST(N'2019-12-13T09:44:46.8030000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (208, N'AlreadyReported', CAST(N'2019-12-13T09:44:53.8600000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (226, N'IMUsed', CAST(N'2019-12-13T09:45:03.5700000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (300, N'Ambiguous', CAST(N'2019-12-13T09:45:13.0070000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (301, N'Moved', CAST(N'2019-12-13T09:45:23.3230000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (302, N'Found', CAST(N'2019-12-13T09:45:34.0630000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (303, N'RedirectMethod', CAST(N'2019-12-13T09:45:52.0700000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (304, N'NotModified', CAST(N'2019-12-13T09:45:59.7670000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (305, N'UseProxy', CAST(N'2019-12-13T09:46:06.2630000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (306, N'Unused', CAST(N'2019-12-13T09:46:14.7200000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (307, N'RedirectKeepVerb', CAST(N'2019-12-13T09:46:22.5900000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (308, N'PermanentRedirect', CAST(N'2019-12-13T09:46:30.9030000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (400, N'BadRequest', CAST(N'2019-12-13T09:46:43.4400000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (401, N'Unauthorized', CAST(N'2019-12-13T09:46:51.3970000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (402, N'PaymentRequired', CAST(N'2019-12-13T09:46:58.4400000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (403, N'Forbidden', CAST(N'2019-12-13T09:47:07.0170000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (404, N'NotFound', CAST(N'2019-12-13T09:47:13.7230000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (405, N'MethodNotAllowed', CAST(N'2019-12-13T09:47:23.7070000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (406, N'NotAcceptable', CAST(N'2019-12-13T09:47:30.4830000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (407, N'ProxyAuthenticationRequired', CAST(N'2019-12-13T09:47:44.8630000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (408, N'RequestTimeout', CAST(N'2019-12-13T09:47:55.4870000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (409, N'Conflict', CAST(N'2019-12-13T09:48:01.9300000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (410, N'Conflict', CAST(N'2019-12-13T09:48:10.3670000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (411, N'LengthRequired', CAST(N'2019-12-13T09:48:19.2230000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (412, N'PreconditionFailed', CAST(N'2019-12-13T09:48:31.7570000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (413, N'RequestEntityTooLarge', CAST(N'2019-12-13T09:48:51.0870000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (414, N'RequestUriTooLong', CAST(N'2019-12-13T09:48:58.4800000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (415, N'UnsupportedMediaType', CAST(N'2019-12-13T09:49:10.8100000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (416, N'RequestedRangeNotSatisfiable', CAST(N'2019-12-13T09:49:17.9170000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (417, N'ExpectationFailed', CAST(N'2019-12-13T09:49:25.1970000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (421, N'MisdirectedRequest', CAST(N'2019-12-13T09:49:33.3770000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (422, N'UnprocessableEntity', CAST(N'2019-12-13T09:49:46.9300000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (423, N'Locked', CAST(N'2019-12-13T09:49:53.8530000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (424, N'FailedDependency', CAST(N'2019-12-13T09:50:03.2130000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (426, N'UpgradeRequired', CAST(N'2019-12-13T09:50:12.0030000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (428, N'PreconditionRequired', CAST(N'2019-12-13T09:50:25.0000000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (429, N'TooManyRequests', CAST(N'2019-12-13T09:50:32.4500000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (431, N'RequestHeaderFieldsTooLarge', CAST(N'2019-12-13T09:50:40.5870000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (451, N'UnavailableForLegalReasons', CAST(N'2019-12-13T09:50:50.6270000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (500, N'InternalServerError', CAST(N'2019-12-13T09:50:58.0500000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (501, N'NotImplemented', CAST(N'2019-12-13T09:51:10.0770000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (502, N'BadGateway', CAST(N'2019-12-13T09:51:16.5600000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (503, N'ServiceUnavailable', CAST(N'2019-12-13T09:51:22.6470000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (504, N'GatewayTimeout', CAST(N'2019-12-13T09:51:28.1430000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (505, N'HttpVersionNotSupported', CAST(N'2019-12-13T09:51:33.9100000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (506, N'VariantAlsoNegotiates', CAST(N'2019-12-13T09:51:40.8470000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (507, N'InsufficientStorage', CAST(N'2019-12-13T09:51:48.0100000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (508, N'LoopDetected', CAST(N'2019-12-13T09:51:54.7630000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (510, N'NotExtended', CAST(N'2019-12-13T09:52:05.6230000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblHttpStatusCode] ([Id], [Name], [Created], [Modified], [Active]) VALUES (511, N'NetworkAuthenticationRequired', CAST(N'2019-12-13T09:52:13.3200000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblLoginMethod] ([Id], [Name], [Created], [Active]) VALUES (1, N'B2C_1_SUSI', CAST(N'2019-12-29T18:45:23.8570000-05:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[tblStatus] ([Id], [Name], [Description], [TypeId], [CreatedBy], [Created], [Modified], [Active]) VALUES (1, N'New', NULL, 1, N'ATuccitto', CAST(N'2019-12-29T16:03:44.3400000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblStatus] ([Id], [Name], [Description], [TypeId], [CreatedBy], [Created], [Modified], [Active]) VALUES (2, N'Active', NULL, 1, N'ATuccitto', CAST(N'2019-12-29T16:03:51.5733333+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblStatus] ([Id], [Name], [Description], [TypeId], [CreatedBy], [Created], [Modified], [Active]) VALUES (3, N'Inactive', NULL, 1, N'ATuccitto', CAST(N'2019-12-29T16:04:01.5966667+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblStatus] ([Id], [Name], [Description], [TypeId], [CreatedBy], [Created], [Modified], [Active]) VALUES (4, N'Suspended', NULL, 1, N'ATuccitto', CAST(N'2019-12-29T16:04:09.1633333+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblStatus] ([Id], [Name], [Description], [TypeId], [CreatedBy], [Created], [Modified], [Active]) VALUES (5, N'Active', NULL, 2, N'ATuccitto', CAST(N'2019-12-29T20:59:05.1700000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblStatus] ([Id], [Name], [Description], [TypeId], [CreatedBy], [Created], [Modified], [Active]) VALUES (6, N'Revoked', NULL, 2, N'ATuccitto', CAST(N'2019-12-29T20:59:40.6666667+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblStatus] ([Id], [Name], [Description], [TypeId], [CreatedBy], [Created], [Modified], [Active]) VALUES (7, N'Inactive', NULL, 2, N'ATuccitto', CAST(N'2019-12-29T20:59:51.7233333+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblStatus] ([Id], [Name], [Description], [TypeId], [CreatedBy], [Created], [Modified], [Active]) VALUES (8, N'Expired', NULL, 2, N'ATuccitto', CAST(N'2019-12-29T21:00:42.3566667+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblStatus] ([Id], [Name], [Description], [TypeId], [CreatedBy], [Created], [Modified], [Active]) VALUES (9, N'Started', NULL, 3, N'ATuccitto', CAST(N'2020-01-01T19:37:55.2366667+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblStatus] ([Id], [Name], [Description], [TypeId], [CreatedBy], [Created], [Modified], [Active]) VALUES (10, N'Completed', NULL, 3, N'ATuccitto', CAST(N'2020-01-01T19:38:08.2233333+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblStatus] ([Id], [Name], [Description], [TypeId], [CreatedBy], [Created], [Modified], [Active]) VALUES (11, N'Active', NULL, 3, N'ATuccitto', CAST(N'2020-01-01T19:38:16.2366667+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblStatus] ([Id], [Name], [Description], [TypeId], [CreatedBy], [Created], [Modified], [Active]) VALUES (12, N'Swapped', NULL, 3, N'ATuccitto', CAST(N'2020-01-01T19:38:26.7300000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblStatus] ([Id], [Name], [Description], [TypeId], [CreatedBy], [Created], [Modified], [Active]) VALUES (13, N'Accepted', NULL, 3, N'ATuccitto', CAST(N'2020-01-01T19:38:34.7433333+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblStatus] ([Id], [Name], [Description], [TypeId], [CreatedBy], [Created], [Modified], [Active]) VALUES (14, N'Declined', NULL, 3, N'ATuccitto', CAST(N'2020-01-01T19:38:44.7500000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblStatus] ([Id], [Name], [Description], [TypeId], [CreatedBy], [Created], [Modified], [Active]) VALUES (15, N'Pending', NULL, 3, N'ATuccitto', CAST(N'2020-01-01T19:38:51.5733333+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblStatus] ([Id], [Name], [Description], [TypeId], [CreatedBy], [Created], [Modified], [Active]) VALUES (16, N'New', NULL, 3, N'ATuccitto', CAST(N'2020-01-01T19:38:51.5733333+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblStatus] ([Id], [Name], [Description], [TypeId], [CreatedBy], [Created], [Modified], [Active]) VALUES (17, N'Opened', NULL, 3, N'ATuccitto', CAST(N'2020-01-01T19:38:51.5733333+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblStatus] ([Id], [Name], [Description], [TypeId], [CreatedBy], [Created], [Modified], [Active]) VALUES (18, N'Selected', NULL, 3, N'ATuccitto', CAST(N'2020-01-01T19:38:51.5733333+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblStatusType] ([Id], [Name], [Description], [CreatedBy], [Created], [Modified], [Active]) VALUES (1, N'User', NULL, N'ATuccitto', CAST(N'2019-12-29T16:03:08.3333333+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblStatusType] ([Id], [Name], [Description], [CreatedBy], [Created], [Modified], [Active]) VALUES (2, N'Token', NULL, N'ATuccitto', CAST(N'2019-12-29T20:58:09.7166667+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblStatusType] ([Id], [Name], [Description], [CreatedBy], [Created], [Modified], [Active]) VALUES (3, N'BTB', N'Beat The Banker Status Types', N'ATuccitto', CAST(N'2020-01-01T19:37:34.8700000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblTableEntity] ([Id], [Name], [Created], [Modified], [Active]) VALUES (1, N'tblUser', CAST(N'2019-12-29T22:41:29.6733333+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblTableEntity] ([Id], [Name], [Created], [Modified], [Active]) VALUES (2, N'tblToken', CAST(N'2019-12-29T22:41:42.4366667+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblTableEntity] ([Id], [Name], [Created], [Modified], [Active]) VALUES (3, N'tblBTBGame', CAST(N'2020-01-01T19:56:25.2966667+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblTableEntity] ([Id], [Name], [Created], [Modified], [Active]) VALUES (4, N'tblBTBGameCase', CAST(N'2020-01-01T19:56:36.6733333+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblTableEntity] ([Id], [Name], [Created], [Modified], [Active]) VALUES (5, N'tblBTBGameBankerOffer', CAST(N'2020-01-01T19:57:01.1500000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblTableEntity] ([Id], [Name], [Created], [Modified], [Active]) VALUES (6, N'tblBTBGameChoosenCase', CAST(N'2020-01-01T19:58:04.7966667+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblTableEntity] ([Id], [Name], [Created], [Modified], [Active]) VALUES (7, N'tblPlayFabUser', CAST(N'2020-01-06T21:41:35.5166667+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblTableEntity] ([Id], [Name], [Created], [Modified], [Active]) VALUES (8, N'tblLogin', CAST(N'2020-01-06T21:52:12.3900000+00:00' AS DateTimeOffset), NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[tblTrackingAction] ON 
GO
INSERT [dbo].[tblTrackingAction] ([Id], [Name], [Description], [Created], [Modified], [Active]) VALUES (1, N'UserCreated', N'User Creation', CAST(N'2019-12-29T22:37:20.2400000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblTrackingAction] ([Id], [Name], [Description], [Created], [Modified], [Active]) VALUES (2, N'FirstNameChange', N'First Name Change', CAST(N'2019-12-29T22:38:08.6466667+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblTrackingAction] ([Id], [Name], [Description], [Created], [Modified], [Active]) VALUES (3, N'LastNameChange', N'Last Name Change', CAST(N'2019-12-29T22:38:18.2566667+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblTrackingAction] ([Id], [Name], [Description], [Created], [Modified], [Active]) VALUES (4, N'UserEmailChange', N'User Email Change', CAST(N'2019-12-29T22:38:32.6966667+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblTrackingAction] ([Id], [Name], [Description], [Created], [Modified], [Active]) VALUES (5, N'UsernameChange', N'Username Change', CAST(N'2019-12-29T22:38:50.8833333+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblTrackingAction] ([Id], [Name], [Description], [Created], [Modified], [Active]) VALUES (6, N'PlayFabLogin', N'PlayFab Login', CAST(N'2019-12-29T22:39:53.0000000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblTrackingAction] ([Id], [Name], [Description], [Created], [Modified], [Active]) VALUES (7, N'TokenAcquired', N'Token Acquired', CAST(N'2019-12-29T22:40:40.2566667+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblTrackingAction] ([Id], [Name], [Description], [Created], [Modified], [Active]) VALUES (8, N'TokenRevoked', N'Token Revoked', CAST(N'2019-12-29T22:40:51.6400000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblTrackingAction] ([Id], [Name], [Description], [Created], [Modified], [Active]) VALUES (9, N'BTBCaseChosen', NULL, CAST(N'2020-01-01T20:00:35.6766667+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblTrackingAction] ([Id], [Name], [Description], [Created], [Modified], [Active]) VALUES (10, N'BTBCaseOpened', NULL, CAST(N'2020-01-01T20:01:02.3366667+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblTrackingAction] ([Id], [Name], [Description], [Created], [Modified], [Active]) VALUES (11, N'BTBBankOffer', NULL, CAST(N'2020-01-01T20:01:11.8100000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblTrackingAction] ([Id], [Name], [Description], [Created], [Modified], [Active]) VALUES (12, N'BTBDeclinedBankOffer', NULL, CAST(N'2020-01-01T20:01:25.5633333+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblTrackingAction] ([Id], [Name], [Description], [Created], [Modified], [Active]) VALUES (13, N'BTBAcceptedBankOffer', NULL, CAST(N'2020-01-01T20:01:32.4100000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblTrackingAction] ([Id], [Name], [Description], [Created], [Modified], [Active]) VALUES (14, N'CurrencyAdded', NULL, CAST(N'2020-01-06T21:31:47.6666667+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblTrackingAction] ([Id], [Name], [Description], [Created], [Modified], [Active]) VALUES (15, N'PlayFabUpdateUserStats', NULL, CAST(N'2020-01-06T21:32:19.8400000+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblTrackingAction] ([Id], [Name], [Description], [Created], [Modified], [Active]) VALUES (16, N'PlayFabGetLeaderboard', NULL, CAST(N'2020-01-06T21:32:58.9066667+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblTrackingAction] ([Id], [Name], [Description], [Created], [Modified], [Active]) VALUES (17, N'PlayFabUserCreated', NULL, CAST(N'2020-01-06T21:38:40.4633333+00:00' AS DateTimeOffset), NULL, 1)
GO
INSERT [dbo].[tblTrackingAction] ([Id], [Name], [Description], [Created], [Modified], [Active]) VALUES (18, N'Login', NULL, CAST(N'2020-01-06T21:51:53.4366667+00:00' AS DateTimeOffset), NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[tblTrackingAction] OFF
GO
ALTER TABLE [dbo].[tblBTBActionType] ADD  CONSTRAINT [DF_tblBTBAction_Created]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[tblBTBActionType] ADD  CONSTRAINT [DF_tblBTBAction_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[tblBTBCaseAmount] ADD  CONSTRAINT [DF_tblBTBCaseAmount_Created]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[tblBTBResultType] ADD  CONSTRAINT [DF_tblBTBResultType_Created]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[tblBTBResultType] ADD  CONSTRAINT [DF_tblBTBResultType_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[tblHttpMethodType] ADD  CONSTRAINT [DF_tblHttpMethodType_Created]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[tblHttpMethodType] ADD  CONSTRAINT [DF_tblHttpMethodType_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[tblHttpStatusCode] ADD  CONSTRAINT [DF_HttpStatusCode_Created]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[tblHttpStatusCode] ADD  CONSTRAINT [DF_HttpStatusCode_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[tblStatus] ADD  CONSTRAINT [DF_tblStatus_Created]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[tblStatus] ADD  CONSTRAINT [DF_tblStatus_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[tblStatusType] ADD  CONSTRAINT [DF_tblStatusType_Created]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[tblStatusType] ADD  CONSTRAINT [DF_tblStatusType_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[tblTableEntity] ADD  CONSTRAINT [DF_tblTableEntity_Created]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[tblTableEntity] ADD  CONSTRAINT [DF_tblTableEntity_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[tblTokenType] ADD  CONSTRAINT [DF_tblTokenType_Created]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[tblTokenType] ADD  CONSTRAINT [DF_tblTokenType_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[tblTrackingAction] ADD  CONSTRAINT [DF_tblTrackingActionType_Created]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[tblTrackingAction] ADD  CONSTRAINT [DF_tblTrackingActionType_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[tblAPICallLog]  WITH CHECK ADD  CONSTRAINT [FK_tblAPICallLog_tblHttpMethodType] FOREIGN KEY([MethodTypeId])
REFERENCES [dbo].[tblHttpMethodType] ([Id])
GO
ALTER TABLE [dbo].[tblAPICallLog] CHECK CONSTRAINT [FK_tblAPICallLog_tblHttpMethodType]
GO
ALTER TABLE [dbo].[tblAPICallLog]  WITH CHECK ADD  CONSTRAINT [FK_tblAPICallLog_tblHttpStatusCode] FOREIGN KEY([ResponseStatusCodeId])
REFERENCES [dbo].[tblHttpStatusCode] ([Id])
GO
ALTER TABLE [dbo].[tblAPICallLog] CHECK CONSTRAINT [FK_tblAPICallLog_tblHttpStatusCode]
GO
ALTER TABLE [dbo].[tblAPICallLog]  WITH CHECK ADD  CONSTRAINT [FK_tblAPICallLog_tblToken] FOREIGN KEY([TokenId])
REFERENCES [dbo].[tblToken] ([Id])
GO
ALTER TABLE [dbo].[tblAPICallLog] CHECK CONSTRAINT [FK_tblAPICallLog_tblToken]
GO
ALTER TABLE [dbo].[tblAPICallLog]  WITH CHECK ADD  CONSTRAINT [FK_tblAPICallLog_tblUser] FOREIGN KEY([UserId])
REFERENCES [dbo].[tblUser] ([Id])
GO
ALTER TABLE [dbo].[tblAPICallLog] CHECK CONSTRAINT [FK_tblAPICallLog_tblUser]
GO
ALTER TABLE [dbo].[tblBTBGame]  WITH CHECK ADD  CONSTRAINT [FK_tblBTBGame_tblStatus] FOREIGN KEY([StatusId])
REFERENCES [dbo].[tblStatus] ([Id])
GO
ALTER TABLE [dbo].[tblBTBGame] CHECK CONSTRAINT [FK_tblBTBGame_tblStatus]
GO
ALTER TABLE [dbo].[tblBTBGame]  WITH CHECK ADD  CONSTRAINT [FK_tblBTBGame_tblUser] FOREIGN KEY([UserId])
REFERENCES [dbo].[tblUser] ([Id])
GO
ALTER TABLE [dbo].[tblBTBGame] CHECK CONSTRAINT [FK_tblBTBGame_tblUser]
GO
ALTER TABLE [dbo].[tblBTBGameBankerOffer]  WITH CHECK ADD  CONSTRAINT [FK_tblBTBGameBankerOffer_tblBTBGame] FOREIGN KEY([GameId])
REFERENCES [dbo].[tblBTBGame] ([Id])
GO
ALTER TABLE [dbo].[tblBTBGameBankerOffer] CHECK CONSTRAINT [FK_tblBTBGameBankerOffer_tblBTBGame]
GO
ALTER TABLE [dbo].[tblBTBGameBankerOffer]  WITH CHECK ADD  CONSTRAINT [FK_tblBTBGameBankerOffer_tblStatus] FOREIGN KEY([StatusId])
REFERENCES [dbo].[tblStatus] ([Id])
GO
ALTER TABLE [dbo].[tblBTBGameBankerOffer] CHECK CONSTRAINT [FK_tblBTBGameBankerOffer_tblStatus]
GO
ALTER TABLE [dbo].[tblBTBGameCase]  WITH CHECK ADD  CONSTRAINT [FK_tblBTBGameCase_tblBTBCaseAmount] FOREIGN KEY([CaseAmountId])
REFERENCES [dbo].[tblBTBCaseAmount] ([Id])
GO
ALTER TABLE [dbo].[tblBTBGameCase] CHECK CONSTRAINT [FK_tblBTBGameCase_tblBTBCaseAmount]
GO
ALTER TABLE [dbo].[tblBTBGameCase]  WITH CHECK ADD  CONSTRAINT [FK_tblBTBGameCase_tblBTBGame] FOREIGN KEY([GameId])
REFERENCES [dbo].[tblBTBGame] ([Id])
GO
ALTER TABLE [dbo].[tblBTBGameCase] CHECK CONSTRAINT [FK_tblBTBGameCase_tblBTBGame]
GO
ALTER TABLE [dbo].[tblBTBGameCase]  WITH CHECK ADD  CONSTRAINT [FK_tblBTBGameCase_tblStatus] FOREIGN KEY([StatusId])
REFERENCES [dbo].[tblStatus] ([Id])
GO
ALTER TABLE [dbo].[tblBTBGameCase] CHECK CONSTRAINT [FK_tblBTBGameCase_tblStatus]
GO
ALTER TABLE [dbo].[tblBTBGameChosenCase]  WITH CHECK ADD  CONSTRAINT [FK_tblBTBGameChoosenCase_tblBTBGameCase] FOREIGN KEY([GameCaseId])
REFERENCES [dbo].[tblBTBGameCase] ([Id])
GO
ALTER TABLE [dbo].[tblBTBGameChosenCase] CHECK CONSTRAINT [FK_tblBTBGameChoosenCase_tblBTBGameCase]
GO
ALTER TABLE [dbo].[tblBTBGameChosenCase]  WITH CHECK ADD  CONSTRAINT [FK_tblBTBGameChoosenCase_tblStatus] FOREIGN KEY([StatusId])
REFERENCES [dbo].[tblStatus] ([Id])
GO
ALTER TABLE [dbo].[tblBTBGameChosenCase] CHECK CONSTRAINT [FK_tblBTBGameChoosenCase_tblStatus]
GO
ALTER TABLE [dbo].[tblBTBGameLog]  WITH CHECK ADD  CONSTRAINT [FK_tblBTBGameLog_tblBTBActionType] FOREIGN KEY([ActionId])
REFERENCES [dbo].[tblBTBActionType] ([Id])
GO
ALTER TABLE [dbo].[tblBTBGameLog] CHECK CONSTRAINT [FK_tblBTBGameLog_tblBTBActionType]
GO
ALTER TABLE [dbo].[tblBTBGameLog]  WITH CHECK ADD  CONSTRAINT [FK_tblBTBGameLog_tblBTBGame] FOREIGN KEY([GameId])
REFERENCES [dbo].[tblBTBGame] ([Id])
GO
ALTER TABLE [dbo].[tblBTBGameLog] CHECK CONSTRAINT [FK_tblBTBGameLog_tblBTBGame]
GO
ALTER TABLE [dbo].[tblBTBGameLog]  WITH CHECK ADD  CONSTRAINT [FK_tblBTBGameLog_tblTableEntity] FOREIGN KEY([TableEntityId])
REFERENCES [dbo].[tblTableEntity] ([Id])
GO
ALTER TABLE [dbo].[tblBTBGameLog] CHECK CONSTRAINT [FK_tblBTBGameLog_tblTableEntity]
GO
ALTER TABLE [dbo].[tblBTBGameLog]  WITH CHECK ADD  CONSTRAINT [FK_tblBTBGameLog_tblUser] FOREIGN KEY([UserId])
REFERENCES [dbo].[tblUser] ([Id])
GO
ALTER TABLE [dbo].[tblBTBGameLog] CHECK CONSTRAINT [FK_tblBTBGameLog_tblUser]
GO
ALTER TABLE [dbo].[tblBTBGameResult]  WITH CHECK ADD  CONSTRAINT [FK_tblBTBGameResult_tblBTBGame] FOREIGN KEY([GameId])
REFERENCES [dbo].[tblBTBGame] ([Id])
GO
ALTER TABLE [dbo].[tblBTBGameResult] CHECK CONSTRAINT [FK_tblBTBGameResult_tblBTBGame]
GO
ALTER TABLE [dbo].[tblBTBGameResult]  WITH CHECK ADD  CONSTRAINT [FK_tblBTBGameResult_tblBTBResultType] FOREIGN KEY([ResultId])
REFERENCES [dbo].[tblBTBResultType] ([Id])
GO
ALTER TABLE [dbo].[tblBTBGameResult] CHECK CONSTRAINT [FK_tblBTBGameResult_tblBTBResultType]
GO
ALTER TABLE [dbo].[tblBTBGameResult]  WITH CHECK ADD  CONSTRAINT [FK_tblBTBGameResult_tblTableEntity] FOREIGN KEY([TableEntityId])
REFERENCES [dbo].[tblTableEntity] ([Id])
GO
ALTER TABLE [dbo].[tblBTBGameResult] CHECK CONSTRAINT [FK_tblBTBGameResult_tblTableEntity]
GO
ALTER TABLE [dbo].[tblClient]  WITH CHECK ADD  CONSTRAINT [FK_tblClient_tblBrowser] FOREIGN KEY([BrowserId])
REFERENCES [dbo].[tblBrowser] ([Id])
GO
ALTER TABLE [dbo].[tblClient] CHECK CONSTRAINT [FK_tblClient_tblBrowser]
GO
ALTER TABLE [dbo].[tblClient]  WITH CHECK ADD  CONSTRAINT [FK_tblClient_tblDeviceType] FOREIGN KEY([DeviceTypeId])
REFERENCES [dbo].[tblDeviceType] ([Id])
GO
ALTER TABLE [dbo].[tblClient] CHECK CONSTRAINT [FK_tblClient_tblDeviceType]
GO
ALTER TABLE [dbo].[tblClient]  WITH CHECK ADD  CONSTRAINT [FK_tblClient_tblOS] FOREIGN KEY([OSId])
REFERENCES [dbo].[tblOS] ([Id])
GO
ALTER TABLE [dbo].[tblClient] CHECK CONSTRAINT [FK_tblClient_tblOS]
GO
ALTER TABLE [dbo].[tblErrorLog]  WITH CHECK ADD  CONSTRAINT [FK_tblErrorLog_tblUser] FOREIGN KEY([UserId])
REFERENCES [dbo].[tblUser] ([Id])
GO
ALTER TABLE [dbo].[tblErrorLog] CHECK CONSTRAINT [FK_tblErrorLog_tblUser]
GO
ALTER TABLE [dbo].[tblLogin]  WITH CHECK ADD  CONSTRAINT [FK_tblLogin_tblClient] FOREIGN KEY([ClientId])
REFERENCES [dbo].[tblClient] ([Id])
GO
ALTER TABLE [dbo].[tblLogin] CHECK CONSTRAINT [FK_tblLogin_tblClient]
GO
ALTER TABLE [dbo].[tblLogin]  WITH CHECK ADD  CONSTRAINT [FK_tblLogin_tblLoginMethod] FOREIGN KEY([MethodId])
REFERENCES [dbo].[tblLoginMethod] ([Id])
GO
ALTER TABLE [dbo].[tblLogin] CHECK CONSTRAINT [FK_tblLogin_tblLoginMethod]
GO
ALTER TABLE [dbo].[tblLogin]  WITH CHECK ADD  CONSTRAINT [FK_tblLogin_tblUser] FOREIGN KEY([UserId])
REFERENCES [dbo].[tblUser] ([Id])
GO
ALTER TABLE [dbo].[tblLogin] CHECK CONSTRAINT [FK_tblLogin_tblUser]
GO
ALTER TABLE [dbo].[tblPlayFabToken]  WITH CHECK ADD  CONSTRAINT [FK_tblPlayFabToken_tblPlayFabUser] FOREIGN KEY([PlayFabUserId])
REFERENCES [dbo].[tblPlayFabUser] ([Id])
GO
ALTER TABLE [dbo].[tblPlayFabToken] CHECK CONSTRAINT [FK_tblPlayFabToken_tblPlayFabUser]
GO
ALTER TABLE [dbo].[tblPlayFabUser]  WITH CHECK ADD  CONSTRAINT [FK_tblPlayFabUser_tblUser] FOREIGN KEY([UserId])
REFERENCES [dbo].[tblUser] ([Id])
GO
ALTER TABLE [dbo].[tblPlayFabUser] CHECK CONSTRAINT [FK_tblPlayFabUser_tblUser]
GO
ALTER TABLE [dbo].[tblStatus]  WITH CHECK ADD  CONSTRAINT [FK_tblStatus_tblStatusType] FOREIGN KEY([TypeId])
REFERENCES [dbo].[tblStatusType] ([Id])
GO
ALTER TABLE [dbo].[tblStatus] CHECK CONSTRAINT [FK_tblStatus_tblStatusType]
GO
ALTER TABLE [dbo].[tblToken]  WITH CHECK ADD  CONSTRAINT [FK_tblToken_tblStatus] FOREIGN KEY([StatusId])
REFERENCES [dbo].[tblStatus] ([Id])
GO
ALTER TABLE [dbo].[tblToken] CHECK CONSTRAINT [FK_tblToken_tblStatus]
GO
ALTER TABLE [dbo].[tblToken]  WITH CHECK ADD  CONSTRAINT [FK_tblToken_tblTokenType] FOREIGN KEY([TypeId])
REFERENCES [dbo].[tblTokenType] ([Id])
GO
ALTER TABLE [dbo].[tblToken] CHECK CONSTRAINT [FK_tblToken_tblTokenType]
GO
ALTER TABLE [dbo].[tblToken]  WITH CHECK ADD  CONSTRAINT [FK_tblToken_tblUser] FOREIGN KEY([UserId])
REFERENCES [dbo].[tblUser] ([Id])
GO
ALTER TABLE [dbo].[tblToken] CHECK CONSTRAINT [FK_tblToken_tblUser]
GO
ALTER TABLE [dbo].[tblTracking]  WITH CHECK ADD  CONSTRAINT [FK_tblTracking_tblClient] FOREIGN KEY([ClientId])
REFERENCES [dbo].[tblClient] ([Id])
GO
ALTER TABLE [dbo].[tblTracking] CHECK CONSTRAINT [FK_tblTracking_tblClient]
GO
ALTER TABLE [dbo].[tblTracking]  WITH CHECK ADD  CONSTRAINT [FK_tblTracking_tblTableEntity] FOREIGN KEY([TableEntityId])
REFERENCES [dbo].[tblTableEntity] ([Id])
GO
ALTER TABLE [dbo].[tblTracking] CHECK CONSTRAINT [FK_tblTracking_tblTableEntity]
GO
ALTER TABLE [dbo].[tblTracking]  WITH CHECK ADD  CONSTRAINT [FK_tblTracking_tblTrackingAction] FOREIGN KEY([TrackingActionId])
REFERENCES [dbo].[tblTrackingAction] ([Id])
GO
ALTER TABLE [dbo].[tblTracking] CHECK CONSTRAINT [FK_tblTracking_tblTrackingAction]
GO
ALTER TABLE [dbo].[tblTracking]  WITH CHECK ADD  CONSTRAINT [FK_tblTracking_tblUser] FOREIGN KEY([UserId])
REFERENCES [dbo].[tblUser] ([Id])
GO
ALTER TABLE [dbo].[tblTracking] CHECK CONSTRAINT [FK_tblTracking_tblUser]
GO
ALTER TABLE [dbo].[tblUser]  WITH CHECK ADD  CONSTRAINT [FK_tblUser_tblStatus] FOREIGN KEY([StatusId])
REFERENCES [dbo].[tblStatus] ([Id])
GO
ALTER TABLE [dbo].[tblUser] CHECK CONSTRAINT [FK_tblUser_tblStatus]
GO
USE [master]
GO
ALTER DATABASE [GameHub] SET  READ_WRITE 
GO
