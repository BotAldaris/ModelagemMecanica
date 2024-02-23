using ModelagemMecanica;

var disco = new Entidade(3, 2, "y", 45, 35);
var disco0 = new Entidade(3, 2, "0d", raio: 0.25);
disco0.ReajustarMassaThinDisk();
var polia = new Entidade(0.075, 2, "0p", raio: 0.2);
var quadrado = new Entidade(4, 1, "x", 45);
Entidade[] entidades = { disco, disco0, polia, quadrado };


foreach (var item in entidades)
{
    Console.WriteLine(item.ratioReal);
}
Calcular calcular = new Calcular(entidades, "x");
calcular.CalcularMV2();