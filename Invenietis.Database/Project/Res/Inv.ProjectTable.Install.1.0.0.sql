--[beginscript]
create table Inv.tProject
(
	ProjectId int not null constraint PK_tProject primary key,
	ResId int not null constraint FK_tProject_ResId foreign key references CK.tRes(ResId),
	CategoryId int not null constraint FK_tProject_CategoryId foreign key references Inv.tProjectCategory(ProjectCategoryId),
	Content xml not null,
	CreationDate datetime2(0) not null,
	LastUpdateDate datetime2(0)
);

insert into Inv.tProject
(
	ProjectId,
	ResId,
	CategoryId,
	Content,
	CreationDate,
	LastUpdateDate
)
values
(
	0,
	0,
	0,
	'',
	'',
	''
);
--[endscript]