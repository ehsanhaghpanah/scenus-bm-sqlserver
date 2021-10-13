/**
 * Copyright (C) scenüs, 2008.
 * All rights reserved.
 * Ehsan Haghpanah; haghpanah@scenus.com
 */

CREATE TABLE [Samples].[UserBooks]
(
	[UserId]			[bigint]				NOT NULL,
	[BookId]			[bigint]				NOT NULL,
	[CreatedAt]		[datetime]			NOT NULL,
	
	CONSTRAINT [PK_UserBooks] PRIMARY KEY CLUSTERED 
	(
		[UserId]	ASC,
		[BookId]	ASC
	) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Samples].[UserBooks] 
	ADD CONSTRAINT [DF_UserBooks_CreatedAt] 
	DEFAULT (GetDate()) FOR [CreatedAt]
GO

ALTER TABLE [Samples].[UserBooks] 
	ADD CONSTRAINT [FK_UserBooks_Books]
	FOREIGN KEY([BookId]) 
	REFERENCES [Samples].[Books] ([Id])
GO
ALTER TABLE [Samples].[UserBooks] 
	CHECK CONSTRAINT [FK_UserBooks_Books]
GO

ALTER TABLE [Samples].[UserBooks] 
	ADD CONSTRAINT [FK_UserBooks_Users]
	FOREIGN KEY([UserId]) 
	REFERENCES [Samples].[Users] ([Id])
GO
ALTER TABLE [Samples].[UserBooks] 
	CHECK CONSTRAINT [FK_UserBooks_Users]
GO
