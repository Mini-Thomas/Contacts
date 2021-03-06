USE [master]
GO
/****** Object:  Database [Contact]    Script Date: 06-07-2018 19:45:37 ******/
CREATE DATABASE [Contact]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Contact', FILENAME = N'C:\Users\thomami1\Contact.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Contact_log', FILENAME = N'C:\Users\thomami1\Contact_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Contact].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Contact] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Contact] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Contact] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Contact] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Contact] SET ARITHABORT OFF 
GO
ALTER DATABASE [Contact] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [Contact] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [Contact] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Contact] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Contact] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Contact] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Contact] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Contact] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Contact] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Contact] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Contact] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Contact] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Contact] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Contact] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Contact] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Contact] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Contact] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Contact] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Contact] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Contact] SET  MULTI_USER 
GO
ALTER DATABASE [Contact] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Contact] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Contact] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Contact] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
USE [Contact]
GO
/****** Object:  StoredProcedure [dbo].[spDeleteContact]    Script Date: 06-07-2018 19:45:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spDeleteContact] @ContactID int, @Date datetime

AS

SET NOCOUNT ON

-- 1 - Declare variables

-- 2 - Initialize variables

-- 3 - Execute UPDATE command
	UPDATE [dbo].[Contact]
	       SET   [IsDeleted]=1
				,[ModifyDate]=@Date
				,[Status]=0
		   WHERE [ContactID]=@ContactID


GO
/****** Object:  StoredProcedure [dbo].[spGetContactDetails]    Script Date: 06-07-2018 19:45:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE  PROCEDURE [dbo].[spGetContactDetails] @ContactID int
AS

SET NOCOUNT ON
IF (@ContactID=0) 
BEGIN
SELECT  [ContactID]
       ,[FirstName]
	   ,[LastName]
       ,[PhoneNumber]
       ,[Email]
       ,[Status]
       ,[CreateDate]
	   ,[ModifyDate]
		FROM [dbo].[Contact]
		where [IsDeleted]=0
END
ELSE
BEGIN
	SELECT  [ContactID]
       ,[FirstName]
	   ,[LastName]
       ,[PhoneNumber]
       ,[Email]
       ,[Status]
       ,[CreateDate]
	   ,[ModifyDate]
		FROM [dbo].[Contact]
		WHERE [ContactID]=@ContactID 
END 
    RETURN  


GO
/****** Object:  StoredProcedure [dbo].[spSaveContact]    Script Date: 06-07-2018 19:45:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spSaveContact] @ContactID int,@FirstName varchar(25), @LastName varchar(25), 
@PhoneNumber varchar(15), @Email varchar(25), @Status bit, @Date datetime

AS

SET NOCOUNT ON

-- 1 - Declare variables

-- 2 - Initialize variables

-- 3 - Execute INSERT command
IF (@ContactID=0) 
BEGIN
INSERT INTO [dbo].[Contact]
           ([FirstName]
           ,[LastName]
           ,[PhoneNumber]
           ,[Email]
           ,[Status]
           ,[CreateDate])
     VALUES
           (@FirstName
           ,@LastName
           ,@PhoneNumber
           ,@Email
           ,@Status
           ,@Date)
END
ELSE
BEGIN
	UPDATE [dbo].[Contact]
	       SET  [FirstName]=@FirstName
				,[LastName]=@LastName
				,[PhoneNumber]=@PhoneNumber
				,[Email]=@Email
				,[Status]=@Status
				,[ModifyDate]=@Date
		   WHERE [ContactID]=@ContactID
END 

GO
/****** Object:  Table [dbo].[Contact]    Script Date: 06-07-2018 19:45:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contact](
	[ContactID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[PhoneNumber] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Status] [bit] NOT NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[ModifyDate] [datetime2](7) NULL,
	[IsDeleted] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[ContactID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Contact] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
USE [master]
GO
ALTER DATABASE [Contact] SET  READ_WRITE 
GO
