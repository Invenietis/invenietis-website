-- Version = 1.0.0
--
-- Sets a String Data ressource
--
create procedure CK.sResDataStringSetByResName
(
	@ResName varchar(96), 
	@Val nvarchar(400)
)
as begin
	--[beginsp]
	declare @ResId int;
	select @ResId = ResId from CK.tRes where ResName = @ResName;
	if @@ROWCOUNT = 0
	begin
		exec CK.sResCreate @ResName, @ResId output;
	end
	exec CK.sResDataStringSet @ResId, @Val;
	--[endsp]
end