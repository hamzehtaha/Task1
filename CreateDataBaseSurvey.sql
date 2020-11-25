USE [master]
GO

/****** Object:  Database [Survey]    Script Date: 11/25/2020 1:50:39 PM ******/
IF OBJECT_ID('Survey') is null
begin 
CREATE DATABASE [Survey]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Survey', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\Survey.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Survey_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\Survey_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
end
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Survey].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [Survey] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [Survey] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [Survey] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [Survey] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [Survey] SET ARITHABORT OFF 
GO

ALTER DATABASE [Survey] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [Survey] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [Survey] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [Survey] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [Survey] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [Survey] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [Survey] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [Survey] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [Survey] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [Survey] SET  DISABLE_BROKER 
GO

ALTER DATABASE [Survey] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [Survey] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [Survey] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [Survey] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [Survey] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [Survey] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [Survey] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [Survey] SET RECOVERY FULL 
GO

ALTER DATABASE [Survey] SET  MULTI_USER 
GO

ALTER DATABASE [Survey] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [Survey] SET DB_CHAINING OFF 
GO

ALTER DATABASE [Survey] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [Survey] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [Survey] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [Survey] SET QUERY_STORE = OFF
GO

USE [Survey]
GO

ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO

ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO

ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO

ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO

ALTER DATABASE [Survey] SET  READ_WRITE 
GO


