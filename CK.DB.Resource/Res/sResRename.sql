-- Version = 1.0.0
--
-- Renames a resource.
--
create procedure CK.sResRename
(
    @ResId int,
    @NewName varchar(128),
	@WithChildren bit = 1
)
as begin

	if @ResId <= 1 raiserror( 'Res.', 16, 1 );
	set @NewName = RTrim( LTrim(@NewName) );
	
	--[beginsp]

	declare @OldName varchar(128);
	declare @LenPrefix int;
	select @OldName = ResName, 
		   @LenPrefix = len(ResName)+1
		from CK.tRes 
		where ResId = @ResId;

	if @OldName is not null 
	begin

		--<Extension Name="Res.PreRename" />

		if @WithChildren = 1
		begin
		-- Updates child names first.
			update CK.tRes set ResName = @NewName + substring( ResName, @LenPrefix, 128 )
				where ResName like @OldName + '.%';
		end
		-- Updates the resource itself.
		update CK.tRes set ResName = @NewName where ResId = @ResId;

		--<Extension Name="Res.PostRename" />
	
	end

	--[endsp]
end