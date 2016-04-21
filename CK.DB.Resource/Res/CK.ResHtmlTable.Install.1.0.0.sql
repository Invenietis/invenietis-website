--[beginscript]

create table CK.tResHtml 
(
	ResId int not null,
	LCID smallint not null,
	Value nvarchar(max) not null,
	constraint PK_CK_ResHtml primary key (ResId,LCID),
	constraint FK_CK_ResHtml_LCID foreign key( LCID ) references CK.tLCID( LCID )
);

insert into CK.tResHtml( ResId, LCID, Value ) values( 0, 9, N'' );
insert into CK.tResHtml( ResId, LCID, Value ) values( 1, 9, N'<strong>System</strong>' );
insert into CK.tResHtml( ResId, LCID, Value ) values( 0, 12, N'' );
insert into CK.tResHtml( ResId, LCID, Value ) values( 1, 12, N'<strong>Syst&egrave;me</strong>' );

--[endscript]