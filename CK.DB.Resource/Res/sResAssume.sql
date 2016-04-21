-- Version = 1.0.0
--
-- Finds or creates a resource by name.
--
create procedure CK.sResAssume
(
	@ResName varchar(128),
	@ResIdResult int output
)
as 
begin

	--[beginsp]

	select @ResIdResult = r.ResId from CK.tRes r where r.ResName = @ResName;
	if @@RowCount = 0 
	begin
		insert into CK.tRes( ResName ) values( @ResName );
		select @ResIdResult = SCOPE_IDENTITY();
	end	
	
	--[endsp]
end