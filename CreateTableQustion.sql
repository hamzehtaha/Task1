USE [Survey]
GO

/****** Object:  Table [dbo].[Qustions]    Script Date: 11/25/2020 1:44:24 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID('Survey.dbo.Qustions') is null
begin 
CREATE TABLE [dbo].[Qustions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Qustions_text] [nvarchar](100) NULL,
	[Qustion_order] [int] NOT NULL,
	[Type_Of_Qustion] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
end
GO
USE [Survey]
GO

/****** Object:  Table [dbo].[Smily]    Script Date: 11/25/2020 1:46:31 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID('Survey.dbo.Smily') is null
begin 
CREATE TABLE [dbo].[Smily](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Number_of_smily] [int] NOT NULL,
	[Qus_ID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
end
GO

ALTER TABLE [dbo].[Smily]  WITH CHECK ADD FOREIGN KEY([Qus_ID])
REFERENCES [dbo].[Qustions] ([ID])
GO
USE [Survey]
GO

/****** Object:  Table [dbo].[Stars]    Script Date: 11/25/2020 1:47:14 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID('Survey.dbo.Stars') is null
begin 
CREATE TABLE [dbo].[Stars](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Number_Of_Stars] [int] NOT NULL,
	[Qus_ID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
end
GO

ALTER TABLE [dbo].[Stars]  WITH CHECK ADD FOREIGN KEY([Qus_ID])
REFERENCES [dbo].[Qustions] ([ID])
GO

ALTER TABLE [dbo].[Stars]  WITH CHECK ADD CHECK  (([Number_Of_Stars]<=(10)))
GO
USE [Survey]
GO

/****** Object:  Table [dbo].[Slider]    Script Date: 11/25/2020 1:45:32 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID('Survey.dbo.Slider') is null
begin 
CREATE TABLE [dbo].[Slider](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Start_Value] [int] NOT NULL,
	[End_Value] [int] NOT NULL,
	[Start_Value_Cap] [varchar](50) NULL,
	[End_Value_Cap] [varchar](50) NULL,
	[Qus_ID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
end
GO

ALTER TABLE [dbo].[Slider]  WITH CHECK ADD FOREIGN KEY([Qus_ID])
REFERENCES [dbo].[Qustions] ([ID])
GO

ALTER TABLE [dbo].[Slider]  WITH CHECK ADD CHECK  (([End_Value]<=(100)))
GO



