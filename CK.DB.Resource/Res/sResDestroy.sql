-- Version = 1.0.1
--
-- Destroys a ressource
--
create procedure CK.sResDestroy
(
	@ResId int
)
as begin
	if @ResId <= 1 raiserror( 'Res.Undestroyable', 16, 1 );

	--[beginsp]
	
	--<Extension Name="Res.PreDestroy" />

	delete from CK.tResHtml where ResId = @ResId;
	delete from CK.tResText where ResId = @ResId;
	delete from CK.tResString where ResId = @ResId;
	delete from CK.tRes where ResId = @ResId;
	
	--<Extension Name="Res.PostDestroy" />

	--[endsp]
end
