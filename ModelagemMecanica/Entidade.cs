using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelagemMecanica;

public class Entidade
{
    public bool ajustado = false;
    public double massa;
    public double k;
    public double c;
    public double ratio;
    public double raio;
    public string eixo;
    public string formula = "";
    public double ratioReal => 1/(raio*ratio);
    public double MassaReal => massa*ratioReal;
    public double KReal => k*ratioReal;
    public double CReal => c*ratioReal;
    public Entidade(double massa, double ratio, string eixo, double k =0, double c = 0, double raio = 1)
    {
        this.massa = massa;
        this.eixo = eixo;
        this.k = k;
        this.c = c;
        this.ratio = Math.Pow(ratio,2);
        this.raio = Math.Pow(raio,2);
    }
    public void ReajustarMassaThinDisk()
    {
        massa = massa * raio / 2;
        ajustado = true;
        formula = $"(m{eixo[1..]}.{RaioRepresentativo}^2)/2";
    }
    public bool EAngulo => eixo[0] == '0';
    public string EixoMassaRepresentativo => EAngulo ? $"J{eixo[1..]}" :  $"m{eixo}"; 
    public string EixoRepresentativo => EAngulo ? eixo[1..] :  eixo; 
    public string RaioRepresentativo => $"r{eixo[1..]}"; 
}
