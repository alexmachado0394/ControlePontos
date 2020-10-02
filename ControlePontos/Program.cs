﻿using ControlePontos.Classes;
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
            List<Jogo> jogos = new List<Jogo>();
            int opc = 0;
            Console.WriteLine("Bem Vinda, Maria!");
            
            do
            {
                Console.WriteLine("1- Cadastrar Resultados");
                Console.WriteLine("2- Visualizar Resultados");
                Console.WriteLine("9- Sair");


                try
                {
                    Console.Write("Insira uma opção: ");
                    opc = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Insira um número");
                    //throw;
                }
                

                switch (opc)
                {
                    case 1:
                        Cadastrar(jogos);
                        break;
                    case 2:
                        Visualizar(jogos);
                        break;
                    case 9:
                        Console.WriteLine("Tenha um bom dia!");
                        Console.ReadLine();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("não existe essa opção!");
                        break;
                }
            } while (opc != 9);
        }

        static void Cadastrar(List<Jogo> jogos)
        {
            Console.Clear();
            //int jogos;
            //Console.Write("quantos jogos serão informados?");
            //jogos = int.Parse(Console.ReadLine());
            int resultado = 0;
            int pior = 0;
            int melhor = 0;
            int mudouBom = 0;
            int mudouRuim = 0;
            int id = 0;
            bool continua = true;
            
            foreach (Jogo lista in jogos)
            {
                id = lista.Id;
            }

            id++;
            do
            {
                bool passou;
                do
                {
                    try
                    {
                        Console.WriteLine($"Jogo #{id}:");
                        Console.Write("Resultado: ");
                        resultado = int.Parse(Console.ReadLine());
                        if (resultado<1000)
                        {
                            passou = true;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Resultado deve ser menor que 1000 pontos!");
                            passou = false;
                        }
                    }
                    catch (FormatException)
                    {
                        Console.Clear();
                        Console.WriteLine("Insira um número");
                        passou = false;
                    }
                } while (!passou);
                
                jogos.Add(new Jogo(id, resultado));
                Jogo j = jogos.Find(x => x.Id == id);
                j.ResultadoRuim(pior);
                j.MudouResultadoRuim(pior, mudouRuim);
                mudouRuim = j.MudouPior;
                pior = j.PiorResultado;
                j.ResultadoBom(melhor);
                j.MudouResultadoBom(melhor, mudouBom);
                mudouBom = j.MudouMelhor;
                melhor = j.MelhorResultado;
                string s;
                do
                {
                    Console.Write("Deseja cadastrar mais um resultado? (S/N)");
                    s = Console.ReadLine();
                    if (s.ToUpper() == "S")
                    {
                        continua = true;
                        id++;
                        Console.Clear();
                    }
                    else if (s.ToUpper() == "N")
                    {
                        continua = false;
                    }
                    else
                    {
                        Console.WriteLine("Insira uma resposta válida!");
                    }
                } while (s.ToUpper()!="S" && s.ToUpper()!="N");
            } while (continua);
            Console.Clear();
        }

        static void Visualizar(List<Jogo> jogos)
        {
            Console.Clear();
            Console.WriteLine("---------------------------------------");
            Console.Write("|");
            Console.Write("Jogo");
            Console.Write("|");
            Console.Write("Placar");
            Console.Write("|");
            Console.Write("Míni");
            Console.Write("|");
            Console.Write("Máxi");
            Console.Write("|");
            Console.Write("Q. min.");
            Console.Write("|");
            Console.Write("Q. max.");
            Console.WriteLine("|");
            Console.WriteLine("---------------------------------------");


            foreach (Jogo item in jogos)
            {
                Console.WriteLine(item);
                Console.WriteLine("---------------------------------------");
            }
            Console.ReadLine();
            Console.Clear();
        }
    }
}
