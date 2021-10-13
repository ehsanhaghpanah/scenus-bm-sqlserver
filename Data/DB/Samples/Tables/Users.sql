/**
 * Copyright (C) scenüs, 2008.
 * All rights reserved.
 * Ehsan Haghpanah; haghpanah@scenus.com
 */

CREATE TABLE [Samples].[Users]
(
	[Id]				[bigint] IDENTITY(1, 1)	NOT NULL,
	[NameFrst]		[nvarchar](64)			NULL,
	[NameLast]		[nvarchar](64)			NULL,
	[MobileNumber]		[nvarchar](16)			NOT NULL,
	[EmailAddress]		[nvarchar](256)		NULL,
	[CreatedAt]		[datetime]			NOT NULL,
	[RI]				[uniqueidentifier]		NOT NULL,		-- Replication Index

	CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE UNIQUE NONCLUSTERED INDEX 
	[IN_Users_MobileNumber] ON [Samples].[Users] (
	[MobileNumber] ASC
) ON [PRIMARY]
GO

ALTER TABLE [Samples].[Users] 
	ADD CONSTRAINT [DF_Users_CreatedAt] 
	DEFAULT (GetDate()) FOR [CreatedAt]
GO

-- 
-- User RI
ALTER TABLE [Samples].[Users] 
	ADD CONSTRAINT [DF_Users_RI] 
	DEFAULT (NewID()) FOR [RI]
GO