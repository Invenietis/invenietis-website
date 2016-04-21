--[beginscript]
create table Inv.tProjectCategory
(
	ProjectCategoryId int not null constraint PK_tProjectCategory primary key,
	ResId int not null constraint FK_tProjectCategory_ResId foreign key references CK.tRes(ResId)
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