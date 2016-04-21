--[beginscript]
create table Inv.tLearning
(
	LearningId int not null constraint PK_tLearning primary key,
	ResId int not null,
	CategoryId int not null constraint FK_tLearningCategory_CategoryId foreign key references Inv.tLearningCategory(LearningCategoryId),
	Content xml not null,
	CreationDate datetime2(0) not null,
	LastUpdateDate datetime2(0)
);

insert into Inv.tLearning
(
	LearningId,
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