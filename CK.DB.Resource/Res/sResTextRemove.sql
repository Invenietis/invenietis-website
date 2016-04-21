-- Version = 1.0.0
--
-- Removes a big text value for a resource in a given culture.
--
create procedure CK.sResTextRemove
(
	@ResId int,
	@LCID smallint
)
as
begin
	set nocount on;
	delete v from CK.tResText v where v.ResId = @ResId and LCID = @LCID;
	return 0;
end