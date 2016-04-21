-- Version = 1.0.0
create function CK.fResNameLeaf
(
    @S varchar(128)
)
returns varchar(128)
as 
begin
	declare @len int, @i int;
	set @len = len(@S);
	set @i = len(@S)-1;
	while @i > 1
	begin
		if SubString( @S, @i, 1 ) = '.' break;
		set @i = @i-1;
	end
	if @i <= 1 return @S;
	return right(@S,@len-@i);
end