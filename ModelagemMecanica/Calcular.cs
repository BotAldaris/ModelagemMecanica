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
    public double CalcularMV2()
    {
        string formulaInicial = $"(m{eixo}.{eixo}^2)/2 =";
        CalcularFormula(formulaInicial, entidades, entidade => $"({entidade.EixoMassaRepresentativo}.{entidade.eixo}^2)/2");


        string formulaInicialSem2 = $"m{eixo}.{eixo}^2 =";
        CalcularFormula(formulaInicialSem2, entidades, entidade => $"{entidade.EixoMassaRepresentativo}.{entidade.eixo}^2");


        string formulaComAnguloEixoAjustado = $"m{eixo}.{eixo}^2 =";
        CalcularFormula(formulaComAnguloEixoAjustado, entidades, entidade =>
        $"({entidade.EixoMassaRepresentativo}.{eixo}^2)/({entidade.ratio}{(entidade.EAngulo ? $"{entidade.RaioRepresentativo}^2" : "")})");

        string formulaSemEixo = $"m{eixo} =";
        CalcularFormula(formulaSemEixo, entidades, entidade =>
        $"{entidade.EixoMassaRepresentativo}/({entidade.ratio}{(entidade.EAngulo ? $"{entidade.RaioRepresentativo}^2" : "")})");

        bool alguemAjustado = entidades.Any(entidade => entidade.ajustado);
        if (alguemAjustado)
        {
            string formulaAjustada = $"m{eixo} =";
            for (int i = 0; i < entidades.Length; i++)
            {
                var entidade = entidades[i];
                string bases = $"{entidade.EixoMassaRepresentativo}/({entidade.ratio}{(entidade.EAngulo ? $"{entidade.RaioRepresentativo}^2" : "")}){(entidade.ajustado ? $" * {entidade.formula})" : "")}";
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
        return sum;

    }
    public double CalcularK()
    {
        var entidadesK = entidades.Where(x => x.k != 0).ToArray();
        string formulaInicial = $"(k{eixo}.{eixo}^2)/2 =";
        for (int i = 0; i < entidadesK.Length; i++)
        {
            var entidade = entidadesK[i];
            var bases = $"(k{entidade.EixoRepresentativo}.{entidade.eixo}^2)/2";
            formulaInicial += FormatString(bases, i);
        }
        Console.WriteLine(formulaInicial);
        string formulaInicialSem2 = $"k{eixo}.{eixo}^2 =";
        for (int i = 0; i < entidadesK.Length; i++)
        {
            var entidade = entidadesK[i];
            string bases = $"k{entidade.EixoRepresentativo}.{entidade.eixo}^2";
            formulaInicialSem2 += FormatString(bases, i);

        }
        Console.WriteLine(formulaInicialSem2);
        string formulaComAnguloEixoAjustado = $"k{eixo}.{eixo}^2 =";
        for (int i = 0; i < entidadesK.Length; i++)
        {
            var entidade = entidadesK[i];
            string bases = $"(k{entidade.EixoRepresentativo}.{eixo}^2)/({entidade.ratio}{(entidade.EAngulo ? $"{entidade.RaioRepresentativo}^2" : "")})";
            formulaComAnguloEixoAjustado += FormatString(bases, i);
        }
        Console.WriteLine(formulaComAnguloEixoAjustado);
        string formulaSemEixo = $"k{eixo} =";
        for (int i = 0; i < entidadesK.Length; i++)
        {
            var entidade = entidadesK[i];
            string bases = $"k{entidade.EixoRepresentativo}/({entidade.ratio}{(entidade.EAngulo ? $"{entidade.RaioRepresentativo}^2" : "")})";
            formulaSemEixo += FormatString(bases, i);
        }
        Console.WriteLine(formulaSemEixo);
        string formuamComAnguloSubistituido = $"k{eixo} =";
        for (int i = 0; i < entidadesK.Length; i++)
        {
            var entidade = entidadesK[i];
            string bases = $"{entidade.KReal}{eixo}";
            formuamComAnguloSubistituido += FormatString(bases, i);
        }
        Console.WriteLine(formuamComAnguloSubistituido);
        double sum = entidadesK.Aggregate(0.0, (acc, x) => acc + x.KReal);
        Console.WriteLine($"k = {sum}");
        return sum;
    }
    public double CalcularC()
    {
        var entidadesK = entidades.Where(x => x.c != 0).ToArray();
        string formulaInicialSem2 = $"c{eixo}.{eixo}^2 =";
        for (int i = 0; i < entidadesK.Length; i++)
        {
            var entidade = entidadesK[i];
            string bases = $"c{entidade.EixoRepresentativo}.{entidade.eixo}^2";
            formulaInicialSem2 += FormatString(bases, i);

        }
        Console.WriteLine(formulaInicialSem2);
        string formulaComAnguloEixoAjustado = $"c{eixo}.{eixo}^2 =";
        for (int i = 0; i < entidadesK.Length; i++)
        {
            var entidade = entidadesK[i];
            string bases = $"(c{entidade.EixoRepresentativo}.{eixo}^2)/({entidade.ratio}{(entidade.EAngulo ? $"{entidade.RaioRepresentativo}^2" : "")})";
            formulaComAnguloEixoAjustado += FormatString(bases, i);
        }
        Console.WriteLine(formulaComAnguloEixoAjustado);
        string formulaSemEixo = $"c{eixo} =";
        for (int i = 0; i < entidadesK.Length; i++)
        {
            var entidade = entidadesK[i];
            string bases = $"c{entidade.EixoRepresentativo}/({entidade.ratio}{(entidade.EAngulo ? $"{entidade.RaioRepresentativo}^2" : "")})";
            formulaSemEixo += FormatString(bases, i);
        }
        Console.WriteLine(formulaSemEixo);
        string formuamComAnguloSubistituido = $"c{eixo} =";
        for (int i = 0; i < entidadesK.Length; i++)
        {
            var entidade = entidadesK[i];
            string bases = $"{entidade.CReal}{eixo}";
            formuamComAnguloSubistituido += FormatString(bases, i);
        }
        Console.WriteLine(formuamComAnguloSubistituido);
        double sum = entidadesK.Aggregate(0.0, (acc, x) => acc + x.CReal);
        Console.WriteLine($"c = {sum}");
        return sum;
    }

    public double CalcularWn()
    {
        var k = CalcularK();
        Console.WriteLine();
        var wn = Math.Sqrt(k);
        Console.WriteLine($"wn = {wn}");
        return wn;
    }
    public double CalcularZ()
    {
        var c = CalcularC();
        Console.WriteLine();
        var wn =  CalcularWn();
        var z = c / (wn * 2);
        Console.WriteLine();
        Console.WriteLine($"z = {z}");
        return z;
    }
    private string FormatString(string text, int num)
    {
        if (num == 0)
        {
            return $" {text}";
        }

        return $" + {text}";
    }
    private void CalcularFormula(string eixo, Entidade[] entidades, Func<Entidade, string> formulaSelector)
    {
        for (int i = 0; i < entidades.Length; i++)
        {
            var entidade = entidades[i];
            string bases = formulaSelector(entidade);
            eixo += FormatString(bases, i);
        }
        Console.WriteLine(eixo);
    }
}
