using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelagemMecanica;

public class Calcular
{
    public Entidade[] entidades;
    public string eixo;
    public Calcular(Entidade[] entidades, string eixo)
    {
        this.entidades = entidades;
        this.eixo = eixo;
    }

    public void CalcularMV1()
    {
        string formulaInicial = $"m{eixo} =";
        foreach (var entidade in entidades)
        {
            formulaInicial += $" +{entidade.massa}{entidade.eixo}";
        }
        Console.WriteLine(formulaInicial);
        string formuamComAnguloSubistituido = $"m{eixo} =";
        foreach (var entidade in entidades)
        {
            formuamComAnguloSubistituido += $" +{entidade.MassaReal}{eixo}";
        }
        Console.WriteLine(formuamComAnguloSubistituido);
        double sum = entidades.Aggregate(0.0, (acc, x) => acc + x.MassaReal);
        Console.WriteLine($"m = {sum}");
    }
    public void CalcularMV2()
    {
        string formulaInicial = $"(m{eixo}.{eixo}^2)/2 =";
        for (int i = 0; i < entidades.Length; i++)
        {
            var entidade = entidades[i];
            var bases = $"({entidade.EixoRepresentativo}.{entidade.eixo}^2)/2";
            formulaInicial += FormatString(bases, i);
        }
        Console.WriteLine(formulaInicial);
        string formulaInicialSem2 = $"m{eixo}.{eixo}^2 =";
        for (int i = 0; i < entidades.Length; i++)
        {
            var entidade = entidades[i];
            string bases = $"{entidade.EixoRepresentativo}.{entidade.eixo}^2";
            formulaInicialSem2 += FormatString(bases, i); 

        }
        Console.WriteLine(formulaInicialSem2);
        string formulaComAnguloEixoAjustado = $"m{eixo}.{eixo}^2 =";
        for (int i = 0; i < entidades.Length; i++)
        {
            var entidade = entidades[i];
            string bases = $"({entidade.EixoRepresentativo}.{eixo}^2)/({entidade.ratio}{(entidade.EAngulo ? $"{entidade.RaioRepresentativo}^2": "")})";
            formulaComAnguloEixoAjustado += FormatString(bases, i);
        }
        Console.WriteLine(formulaComAnguloEixoAjustado);
        string formulaSemEixo = $"m{eixo} =";
        for (int i = 0; i < entidades.Length; i++)
        {
            var entidade = entidades[i];
            string bases = $"{entidade.EixoRepresentativo}/({entidade.ratio}{(entidade.EAngulo ? $"{entidade.RaioRepresentativo}^2" : "")})";
            formulaSemEixo += FormatString(bases, i);
        }
        Console.WriteLine(formulaSemEixo);
        bool alguemAjustado = entidades.Any(entidade => entidade.ajustado);
        if (alguemAjustado)
        {
            string formulaAjustada = $"m{eixo} =";
            for (int i = 0; i < entidades.Length; i++)
            {
                var entidade = entidades[i];
                string bases = $"{entidade.EixoRepresentativo}/({entidade.ratio}{(entidade.EAngulo ? $"{entidade.RaioRepresentativo}^2" : "")}){(entidade.ajustado ? $" * {entidade.formula})" : "")}";
                formulaAjustada += FormatString(bases, i);
            }
           Console.WriteLine(formulaAjustada);
        }
        string formuamComAnguloSubistituido = $"m{eixo} =";
        for (int i = 0; i < entidades.Length; i++)
        {
            var entidade = entidades[i];
            string bases = $"{entidade.MassaReal}{eixo}";
            formuamComAnguloSubistituido += FormatString(bases, i);
        }
        Console.WriteLine(formuamComAnguloSubistituido);
        double sum = entidades.Aggregate(0.0, (acc, x) => acc + x.MassaReal);
        Console.WriteLine($"m = {sum}");

    }
    private string FormatString(string text, int num)
    {
        if (num == 0)
        {
            return $" {text}";
        }

        return $" + {text}";
    }
}
