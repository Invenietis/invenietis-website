-- Version = 1.0.0
--
-- Removes a html value for a resource in a given culture.
--
create procedure CK.sResHtmlRemove
(
	@ResId int,
	@LCID smallint
)
as
begin
	set nocount on;
	delete v from CK.tResHtml v where v.ResId = @ResId and LCID = @LCID;
	return 0;
end