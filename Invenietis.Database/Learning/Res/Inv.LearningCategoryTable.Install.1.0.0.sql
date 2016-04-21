--[beginscript]
create table Inv.tLearningCategory
(
	LearningCategoryId int not null constraint PK_tLearningCategory primary key,
	ResId int not null constraint FK_tLearningCategory_ResId foreign key references CK.tRes(ResId)
);

insert into Inv.tLearningCategory
(
	LearningCategoryId,
	ResId
)
values
(
	0,
	0
);
--[endscript]