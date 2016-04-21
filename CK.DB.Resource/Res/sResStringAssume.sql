-- Version = 1.0.1
--
-- Sets a string value for a resource in a given culture, creating the named resource if it does not exist.
--
create procedure CK.sResStringAssume
(
	@ResName varchar(128),
	@LCID smallint,
	@Value nvarchar(max),
	@ResIdResult int output
)
as
begin
	set nocount on;
	exec CK.sResAssume @ResName, @ResIdResult output;
	merge CK.tResString as target
		using ( select ResId = @ResIdResult, LCID = @LCID ) 
		as source on source.ResId = target.ResId and source.LCID = target.LCID
		when matched then update set Value = @Value
		when not matched by target then insert( ResId, LCID, Value ) values( source.ResId, source.LCID, @Value );	
end