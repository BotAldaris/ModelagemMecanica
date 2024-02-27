using ModelagemMecanica;

//var disco = new Entidade(3, 2, "y", 45, 35);
//var disco0 = new Entidade(3, 2, "0d", raio: 0.25);
//disco0.ReajustarMassaThinDisk();
//var polia = new Entidade(0.075, 2, "0p", raio: 0.2);
//var quadrado = new Entidade(4, 1, "x", 45);
var disco = new Entidade(2, 2, "y", 5000, 500);
var disco0 = new Entidade(2, 2, "0d", raio: 0.2);
disco0.ReajustarMassaThinDisk();
var polia = new Entidade(0.08, 2, "0p", raio: 0.15);
var quadrado = new Entidade(4, 1, "x", 5000, 500);
Entidade[] entidades = { disco, disco0, polia, quadrado };
Calcular calcular = new(entidades, "x");
calcular.CalcularMV2();
Console.WriteLine();
calcular.CalcularZ();
