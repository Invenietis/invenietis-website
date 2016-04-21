-- Version = 1.0.0
--
-- Sets a Html value for a resource in a given culture.
--
create procedure CK.sResHtmlSet
(
	@ResId int,
	@LCID smallint,
	@Value nvarchar(max)
)
as
begin
	set nocount on;
	merge CK.tResHtml as target
		using ( select ResId = @ResId, LCID = @LCID ) 
		as source on source.ResId = target.ResId and source.LCID = target.LCID
		when matched then update set Value = @Value
		when not matched by target then insert( ResId, LCID, Value ) values( source.ResId, source.LCID, @Value );	
end