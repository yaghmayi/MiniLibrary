DROP TABLE IF EXISTS Item;
DROP TABLE IF EXISTS Category;

CREATE TABLE [Category] ( 
	Code		[int]				NOT NULL, 
	Name		[varchar]	(50)	NOT NULL, 
	
	PRIMARY KEY (Code) 
);

CREATE TABLE [Item] ( 
	Id				[int]				NOT NULL, 
	Name			[varchar]	(200)	NOT NULL, 
	CategoryCode	[int]				NOT NULL,
	Author			[varchar]	(200)	NULL, 
	Description		[varchar]	(1000)	NULL,

	PRIMARY KEY (Id) 
);
