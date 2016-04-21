-- Version = 1.0.1
--
-- Creates an automatic resource under a prefix (that defaults to 'R.Auto') suffixed with a newid() (ex: 'R.Auto.168A1D7E-7257-4DC2-AF68-86A809F8ECB2').
--
alter procedure CK.sResCreateAuto
(
	@Prefix varchar(128) = 'R.Auto',
	@ResIdResult int output
)
as 
begin
	set nocount on;
	if @Prefix is null set @Prefix = 'R.Auto.';
	else 
	begin
		set @Prefix = ltrim(rtrim(@Prefix));
		if len(@Prefix) = 0 set @Prefix = 'R.Auto.';
		else if substring(@Prefix,len(@Prefix),1) <> '.' set @Prefix = @Prefix + '.';
	end
	declare @ResName varchar(128);
	set @ResName = @Prefix + replace(cast(newid() as varchar(48)), '-', '');

	insert into CK.tRes( ResName ) values( @ResName );
	select @ResIdResult = SCOPE_IDENTITY();
	return 0;
end