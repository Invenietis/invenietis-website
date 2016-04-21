-- Version = *
create view CK.vXLCIDResString
as
	select  r.ResId, 
			r.ResName,
			x.XLCID,
			rs.Value
		from CK.tXLCID x
		cross join CK.tRes r
		left join CK.tResString rs on rs.ResId = r.ResId and rs.LCID = x.XLCID
		where x.XLCID > 0