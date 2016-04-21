-- Version = 1.0.0
--
-- Creates a resource by name.
-- The name must not already exist. Use sResAssume to find or create a resource by name.
--
create procedure CK.sResCreate 
(
	@ResName varchar(128),
	@ResIdResult int output
)
as 
begin
	--[beginsp]

	insert into CK.tRes( ResName ) values( @ResName );
	select @ResIdResult = SCOPE_IDENTITY();
	
	--[endsp]
end