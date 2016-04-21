-- Version = 1.0.0
--
-- Removes a ressource
--
create procedure CK.sResDestroyChildren
(
	@ResId int
)
as begin
	--[beginsp]

	--<Extension Name="Res.PreDestroyChidren" />

	declare @ChildResId int;
	declare @CRes cursor;
	set @CRes = cursor local fast_forward for 
		select c.ResId 
			from CK.tRes r
			inner join CK.tRes c on c.ResName like r.ResName + '.%'
			where r.ResId = @ResId;
	open @CRes;
	fetch from @CRes into @ChildResId;
	while @@FETCH_STATUS = 0
	begin
		exec CK.sResDestroy @ChildResId;
		fetch next from @CRes into @ChildResId;
	end
	deallocate @CRes;

	--<Extension Name="Res.PostDestroyChidren" />

	--[endsp]
end
