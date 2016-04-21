--[beginscript]
create table Inv.tLearningCategory
(
	LearningCategoryId int not null constraint PK_tLearningCategory primary key,
	ResId int not null
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