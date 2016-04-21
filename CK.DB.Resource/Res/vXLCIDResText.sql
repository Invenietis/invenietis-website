-- Version = *
create view CK.vXLCIDResText
as
	select  r.ResId, 
			r.ResName,
			x.XLCID,
			rt.Value
		from CK.tXLCID x
		cross join CK.tRes r
		left join CK.tResText rt on rt.ResId = r.ResId and rt.LCID = x.XLCID
		where x.XLCID > 0