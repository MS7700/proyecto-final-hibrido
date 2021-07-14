create trigger TDeduccion
on TipoDeduccion
after insert
as
	begin
DECLARE @variable int;
	select @variable = D.IdDeduccion
	from INSERTED as D;

insert into REL_TIPOS(
	IdDeduccion
)
Values(
@variable
);
	end
