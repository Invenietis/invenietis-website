-- Version = 1.0.0
--
-- Destroys a Culture. There must not be any specific culture: specific cultures must be destroyed first.
--
create procedure CK.sResCultureDestroy
(
	@LCID smallint
)
as
begin
	if @LCID <= 0 or @LCID = 127 raiserror( 'Res.LCIDMustBePositiveAndNot127', 16, 1 );
	if @LCID = 12 or @LCID = 9 raiserror( 'Res.EnglishAndFrenchNotDestroyable', 16, 1 );

	--[beginsp]

	-- Removes main mapping for the culture.
	delete m from CK.tXLCIDMap m where m.XLCID = @LCID;
	-- Removes all mapping from others.
	delete m from CK.tXLCIDMap m where m.LCID = @LCID;

	-- Removes all string.
	delete s from CK.tResString s where s.LCID = @LCID;

	-- Removes the culture...
	delete c from CK.tLCID c where c.LCID = @LCID;
	-- ...and its XLCID
	delete c from CK.tXLCID c where c.XLCID = @LCID;

	--[endsp]
end