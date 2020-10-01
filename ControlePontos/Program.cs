using ControlePontos.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlePontos
{
    class Program
    {
        static void Main(string[] args)
        {
            int jogos;
            Console.Write("quantos jogos serão informados?");
            jogos = int.Parse(Console.ReadLine());
            List<Jogo> list = new List<Jogo>();
            int resultado;
            int pior = 0;
            int melhor = 0;
            int mudouBom = 0;
            int mudouRuim = 0;

            for (int i = 1; i <= jogos; i++)
            {
                Console.WriteLine();
                Console.WriteLine($"Jogo #{i}:");
                Console.Write("Resultado: ");
                resultado = int.Parse(Console.ReadLine());
                list.Add(new Jogo(i, resultado));
                Jogo j = list.Find(x => x.Id == i);
                j.ResultadoRuim(pior);
                j.MudouResultadoRuim(pior,mudouRuim);
                mudouRuim = j.MudouPior;
                pior = j.PiorResultado;
                j.ResultadoBom(melhor);
                j.MudouResultadoBom(melhor,mudouBom);
                mudouBom = j.MudouMelhor;
                melhor = j.MelhorResultado;
            }

            Console.WriteLine("Id, Resultado, Pior, Melhor, Piorou, Melhorou");
            foreach (Jogo item in list)
            {
                Console.WriteLine(item);
            }

            Console.ReadLine();

        }
    }
}
