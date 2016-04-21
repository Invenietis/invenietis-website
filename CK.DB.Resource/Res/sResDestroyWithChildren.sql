-- Version = 1.0.0
--
-- Destroys a ressource and its children (calls sResDestroyChildren and sResDestroy).
--
create procedure CK.sResDestroyWithChildren
(
	@ResId int
)
as begin
	if @ResId <= 1 raiserror( 'Res.Undestroyable', 16, 1 );

	--[beginsp]

	exec CK.sResDestroyChildren @ResId;
	exec CK.sResDestroy @ResId;
	
	--[endsp]
end
