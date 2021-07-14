create trigger TIngreso
on TipoIngreso
after insert
as
	begin
DECLARE @variable int;
	select @variable = D.IdIngreso
	from INSERTED as D;

insert into REL_TIPOS(
	IdIngreso
)
Values(
@variable
);
	end

