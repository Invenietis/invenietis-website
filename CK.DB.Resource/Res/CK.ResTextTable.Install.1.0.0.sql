--[beginscript]

create table CK.tResText
(
	ResId int not null,
	LCID smallint not null,
	Value nvarchar(max) not null,
	constraint PK_CK_ResText primary key (ResId,LCID),
	constraint FK_CK_ResText_LCID foreign key( LCID ) references CK.tLCID( LCID )
);

insert into CK.tResText( ResId, LCID, Value ) values( 0, 9, N'' );
insert into CK.tResText( ResId, LCID, Value ) values( 0, 12, N'' );

--[endscript]