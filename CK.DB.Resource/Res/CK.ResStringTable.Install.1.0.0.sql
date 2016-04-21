--[beginscript]

create table CK.tResString 
(
	ResId int not null,
	LCID smallint not null,
	Value nvarchar(max) not null,
	constraint PK_CK_ResString primary key (ResId,LCID),
	constraint FK_CK_ResString_LCID foreign key( LCID ) references CK.tLCID( LCID )
);

insert into CK.tResString( ResId, LCID, Value ) values( 0, 9, N'' );
insert into CK.tResString( ResId, LCID, Value ) values( 1, 9, N'System' );
insert into CK.tResString( ResId, LCID, Value ) values( 0, 12, N'' );
insert into CK.tResString( ResId, LCID, Value ) values( 1, 12, N'Système' );

--[endscript]