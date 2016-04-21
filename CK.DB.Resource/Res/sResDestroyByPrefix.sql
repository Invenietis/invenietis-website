-- Version = 1.0.0
--
-- Destroys all ressources that starts with @ResNamePrefix + '.'.
--
create procedure CK.sResDestroyByPrefix
(
	@ResNamePrefix varchar(128)
)
as begin
	--[beginsp]

	--<Extension Name="Res.PreDestroyByPrefix" />

	declare @ChildResId int;
	declare @CRes cursor;
	set @CRes = cursor local fast_forward for 
		select r.ResId 
			from CK.tRes r
			where r.ResName like @ResNamePrefix + '.%';
	open @CRes;
	fetch from @CRes into @ChildResId;
	while @@FETCH_STATUS = 0
	begin
		exec CK.sResDestroy @ChildResId;
		fetch next from @CRes into @ChildResId;
	end
	deallocate @CRes;

	--<Extension Name="Res.PostDestroyByPrefix" />

	--[endsp]
end
