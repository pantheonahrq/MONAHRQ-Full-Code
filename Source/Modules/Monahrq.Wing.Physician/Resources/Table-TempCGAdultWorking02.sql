

--  Delete table if needed.
if(Object_id(N'Temp_CG_Adult_Working_02')) is not null 
	drop table [Temp_CG_Adult_Working_02]
go

/****** Object:  Table [dbo].[Temp_CG_Adult_Working_02]    Script Date: 5/4/2016 11:52:09 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Temp_CG_Adult_Working_02](
	[CGPracticeId] [varchar](50) NULL,
	[Adult_Samp] [varchar](50) NULL,
	[AV_06_Comp] [decimal](23, 13) NULL,
	[AV_08_Comp] [decimal](23, 13) NULL,
	[AV_10_Comp] [decimal](23, 13) NULL,
	[AV_12_Comp] [decimal](23, 13) NULL,
	[AV_13_Comp] [decimal](23, 13) NULL,
	[AV_16_Comp] [decimal](23, 13) NULL,
	[AV_17_Comp] [decimal](23, 13) NULL,
	[AV_19_Comp] [decimal](23, 13) NULL,
	[AV_20_Comp] [decimal](23, 13) NULL,
	[AV_21_Comp] [decimal](23, 13) NULL,
	[AV_22_Comp] [decimal](23, 13) NULL,
	[AV_24_Comp] [decimal](23, 13) NULL,
	[AV_25_Comp] [decimal](23, 13) NULL,
	[AV_26_Comp] [decimal](23, 13) NULL,
	[AV_27_Comp] [decimal](23, 13) NULL,
	[AV_28_Comp] [decimal](23, 13) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO