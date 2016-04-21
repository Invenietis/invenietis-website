-- Version = 1.0.0
--
-- Removes string value for a resource in a given culture.
--
create procedure CK.sResStringRemove
(
	@ResId int,
	@LCID smallint
)
as
begin
	set nocount on;
	delete v from CK.tResString v where v.ResId = @ResId and LCID = @LCID;
	return 0;
end