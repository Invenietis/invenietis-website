-- Version = *
create view CK.vRes_AllChildren
as
select  ResId = r.ResId,
		ResName = r.ResName,
		c.ChildId,
		c.ChildName
	from CK.tRes r
	cross apply (select ChildId = ResId, 
						ChildName = ResName
					from CK.tRes 
					where ResName like r.ResName + '.%') c;
