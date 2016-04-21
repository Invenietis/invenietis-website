-- Version = 1.0.0
--
-- Registers a new Culture. The @ParentLCID must already be registered.
--
create procedure CK.sResCultureRegister
(
	@LCID smallint,
	@Name varchar(20),
	@EnglishName varchar(50),
	@NativeName nvarchar(50),
	@ParentLCID smallint
)
as
begin
	if @LCID <= 0 or @LCID = 127 raiserror( 'Res.LCIDMustBePositiveAndNot127', 16, 1 );
	if @ParentLCID = 127 set @ParentLCID = 0;

	--[beginsp]
	
	declare @NewOne bit = 0;
	declare @LCIDCount int;
	-- Current number of cultures (without the 0).
	select @LCIDCount = count(*)-1 from CK.tLCID;
	if not exists( select * from CK.tXLCID where XLCID = @LCID )
	begin
		set @NewOne = 1;
		insert into CK.tXLCID( XLCID ) values( @LCID );
	end
	merge CK.tLCID as target
		using ( select LCID = @LCID ) 
		as source on source.LCID = target.LCID
		when matched then update set Name = @Name, EnglishName = @EnglishName, NativeName = @NativeName, ParentLCID = @ParentLCID
		when not matched by target then insert( LCID, Name, EnglishName, NativeName, ParentLCID ) values( source.LCID, @Name, @EnglishName, @NativeName, @ParentLCID );
	if @NewOne = 1 
	begin
		insert into CK.tXLCIDMap( XLCID, Idx, LCID )
			select @LCID, 0, @LCID
			union all
			select @LCID, en.Idx+1, en.LCID
				from CK.tXLCIDMap en
				where en.XLCID = 9
			union all
			select other.LCID, @LCIDCount, @LCID
				from CK.tLCID other
				where other.LCID > 0 and other.LCID <> @LCID;
	end
	--[endsp]
end
