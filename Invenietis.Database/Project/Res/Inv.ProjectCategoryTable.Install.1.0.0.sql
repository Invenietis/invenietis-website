--[beginscript]
create table Inv.tProjectCategory
(
	ProjectCategoryId int not null constraint PK_tProjectCategory primary key,
	ResId int not null
);

insert into Inv.tProjectCategory
(
	ProjectCategoryId,
	ResId
)
values
(
	0,
	0
);
--[endscript]