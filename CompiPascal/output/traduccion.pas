program anidadas;
begin
PROCEDURE p1( a : INTEGER;  var b : INTEGER);
Var
	w : INTEGER:=11;
	x : INTEGER:=12;
	y : INTEGER:=13;
begin
end;
PROCEDURE p1_p11( a : INTEGER;  var b : INTEGER);
Var
	w : INTEGER:=21;
	x : INTEGER:=22;
	y : INTEGER:=13;
begin
end;
PROCEDURE p1_p11_p111( a : INTEGER;  var b : INTEGER);
Var
	w : INTEGER:=31;
	x : INTEGER:=22;
	y : INTEGER:=13;
begin
writeln('Local 31 = ',w);
writeln('Ambito Padre 22 = ',x);
writeln('Ambito Padre de Padre 13 = ',y);
end;
PROCEDURE p11();
begin
writeln('Aqui no debe entrar');
end;
Var
	w : INTEGER:=1;
	x : INTEGER:=2;
	y : INTEGER:=3;
	z : INTEGER:=4;
writeln('Valor Antes 2 = ',x);
p1(1, x);
writeln('Valor Despues 1000 = ',x);
end.