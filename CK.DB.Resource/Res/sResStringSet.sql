-- Version = 1.0.0
--
-- Sets a string value for a resource in a given culture.
--
create procedure CK.sResStringSet
(
	@ResId int,
	@LCID smallint,
	@Value nvarchar(512)
)
as
begin
	set nocount on;
	merge CK.tResString as target
		using ( select ResId = @ResId, LCID = @LCID ) 
		as source on source.ResId = target.ResId and source.LCID = target.LCID
		when matched then update set Value = @Value
		when not matched by target then insert( ResId, LCID, Value ) values( source.ResId, source.LCID, @Value );	
end