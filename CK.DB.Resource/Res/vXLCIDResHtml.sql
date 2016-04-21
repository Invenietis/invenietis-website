-- Version = *
create view CK.vXLCIDResHtml
as
	select  r.ResId, 
			r.ResName,
			x.XLCID,
			rh.Value
		from CK.tXLCID x
		cross join CK.tRes r
		left join CK.tResHtml rh on rh.ResId = r.ResId and rh.LCID = x.XLCID
		where x.XLCID > 0