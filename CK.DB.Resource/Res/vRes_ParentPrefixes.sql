-- Version=1.0.0, Requires={ CK.fResNamePrefixes }
create view CK.vRes_ParentPrefixes 
as 
	select  r.ResId,
			r.ResName,
			-- Null if the ParentPrefix in not an existing resource.
			ParentResId = pExist.ResId,
			-- Parent prefix that may exist as an actual resource or not.
			p.ParentPrefix,
			-- Level of the ParentPrefix. First parent has level 1.
			p.ParentLevel
		from CK.tRes r
		cross apply CK.fResNamePrefixes( r.ResName ) p
		left outer join CK.tRes pExist on pExist.ResName = p.ParentPrefix;

