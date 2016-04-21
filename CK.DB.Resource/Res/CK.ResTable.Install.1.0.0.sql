--[beginscript]

create table CK.tRes 
(
	ResId int not null identity (0, 1),
	ResName varchar(128) collate Latin1_General_BIN not null,
	constraint PK_CK_Res primary key nonclustered (ResId)
);

create unique clustered index IX_CK_Res_ResName on CK.tRes( ResName );

insert into CK.tRes( ResName ) values( '' );
insert into CK.tRes( ResName ) values( 'System' );

--[endscript]