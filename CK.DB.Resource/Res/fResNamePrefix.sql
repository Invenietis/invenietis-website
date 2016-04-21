-- Version = 1.0.0
create function CK.fResNamePrefix
(
    @S varchar(128)
)
returns varchar(128)
as
begin
	declare @lenS int;
	set @lenS = len(@S)-1;
	while @lenS > 1
	begin
		if SubString( @S, @lenS, 1 ) = '.' break;
		set @lenS = @lenS-1;
	end
	if @lenS <= 1 return '';
	return left(@S,@lenS-1);
end
