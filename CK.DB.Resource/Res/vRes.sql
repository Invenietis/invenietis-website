-- Version=1.0.0, Requires={ CK.vRes_ParentPrefixes }
create view CK.vRes 
as 
	select  r.ResId,
			r.ResName,
			-- Computing the ChildCount is expensive.
			-- If not needed, this column should be avoided when requesting large number or rows.
			ChildCount = (select count(*) from CK.tRes where ResName like r.ResName + '.%'),
			StringCount = (select count(*) from CK.tResString where ResId = r.ResId),
			-- Null if there is no parent resource (be it direct or not). 
			ParentResId = p.ParentResId,
			-- Null if there is no parent resource (be it direct or not). 
			ParentResName = ParentPrefix,
			-- Null if there is no parent resource, otherwise 
			-- it is the level of the closest parent (1 for a direct parent, 2 for a grand-parent, etc.)  
			p.ParentLevel
		from CK.tRes r
		outer apply (select top(1) ParentResId, ParentPrefix, ParentLevel 
						from CK.vRes_ParentPrefixes 
						where ResId = r.ResId and ParentResId is not null 
						order by ParentLevel) p;