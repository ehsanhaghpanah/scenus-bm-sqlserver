/**
 * Copyright (C) scenüs, 2008.
 * All rights reserved.
 * Ehsan Haghpanah; haghpanah@scenus.com
 */

CREATE TABLE [Samples].[Books]
(
	[Id]				[bigint] IDENTITY(1, 1)	NOT NULL,
	[ISBN]			[nvarchar](32)			NOT NULL,
	[Name]			[nvarchar](512)		NOT NULL,
	[Description]		[nvarchar](max)		NULL,
	[CreatedAt]		[datetime]			NOT NULL,
	[RI]				[uniqueidentifier]		NOT NULL,		-- Replication Index

	CONSTRAINT [PK_Books] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE UNIQUE NONCLUSTERED INDEX 
	[IN_Books_ISBN] ON [Samples].[Books] (
	[ISBN] ASC
) ON [PRIMARY]
GO

CREATE UNIQUE NONCLUSTERED INDEX 
	[IN_Books_Name] ON [Samples].[Books] (
	[Name] ASC
) ON [PRIMARY]
GO

ALTER TABLE [Samples].[Books] 
	ADD CONSTRAINT [DF_Books_CreatedAt] 
	DEFAULT (GetDate()) FOR [CreatedAt]
GO

-- 
-- Book RI
ALTER TABLE [Samples].[Books] 
	ADD CONSTRAINT [DF_Books_RI] 
	DEFAULT (NewID()) FOR [RI]
GO