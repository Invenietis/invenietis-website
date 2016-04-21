--[beginscript]

UPDATE CK.tLCID 
SET NativeName = UPPER(LEFT(NativeName, 1)) + LOWER(SUBSTRING(NativeName, 2, LEN(NativeName)));

--[endscript]