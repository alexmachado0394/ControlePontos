using ControlePontos.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace ControlePontos
{
    class Program
    {
        static readonly string ConnectionString = "server=localhost;userid=Alexandre;password=159753;database=pontosdb";
        static void Main(string[] args)
        {
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
                        Cadastrar();
                        break;
                    case 2:
                        Visualizar();
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

        static void Cadastrar()
        {
            Jogo jogo = new Jogo();
            int pior = 0;
            int melhor = 0;
            int mudouBom = 0;
            int mudouRuim = 0;
            int id = 0;
            bool continua = true;


            var connString = ConnectionString;
            var connection = new MySqlConnection(connString);

            try
            {
                connection.Open();
                var command = new MySqlCommand("SELECT * FROM pontosdb.jogo", connection);
                command.Parameters.Clear();

                //command.CommandType = CommandType.Text();

                MySqlDataReader data = command.ExecuteReader();
                while (data.Read())
                {
                    id = data.GetInt32(0);
                    pior = data.GetInt32(2);
                    melhor = data.GetInt32(3);
                    mudouBom = data.GetInt32(5);
                    mudouRuim = data.GetInt32(4);
                }
                connection.Close();
            }
            catch (Exception)
            {
                Console.WriteLine("Não entrou no Db!");
                Console.ReadLine();
            }


            do
            {
                id++;
                bool passou;
                do
                {
                    try
                    {
                        Console.WriteLine($"Jogo #{id}:");
                        Console.Write("Resultado: ");
                        jogo.Placar = int.Parse(Console.ReadLine());
                        if (jogo.Placar < 1000)
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

                try
                {
                    connection.Open();
                    var command = new MySqlCommand("INSERT INTO jogo(placar,piorResultado,melhorResultado,mudouPior,MudouMelhor) VALUES(@PLACAR,0,0,0,0)", connection);
                    command.Parameters.AddWithValue("@PLACAR", jogo.Placar);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception)
                {
                    Console.WriteLine("Não entrou no Db!");
                    Console.ReadLine();
                    continua = false;
                }
                
                try
                {
                    connection.Open();
                    var command = new MySqlCommand("SELECT * FROM pontosdb.jogo where jogo.id = @ID", connection);
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@ID", id);

                    //command.CommandType = CommandType.Text();

                    MySqlDataReader data = command.ExecuteReader();
                    data.Read();

                    jogo.Id = data.GetInt32(0);
                    jogo.Placar = data.GetInt32(1);

                    connection.Close();
                    
                    
                    jogo.ResultadoRuim(pior);
                    jogo.MudouResultadoRuim(pior, mudouRuim);
                    mudouRuim = jogo.MudouPior;
                    pior = jogo.PiorResultado;
                    jogo.ResultadoBom(melhor);
                    jogo.MudouResultadoBom(melhor, mudouBom);
                    mudouBom = jogo.MudouMelhor;
                    melhor = jogo.MelhorResultado;

                    connection.Open();

                    var command2 = new MySqlCommand("UPDATE jogo SET piorResultado = @PIOR, melhorResultado = @MELHOR, mudouPior = @MUDOUP, mudouMelhor = @MUDOUM  WHERE jogo.id = @ID", connection);
                    command2.Parameters.AddWithValue("@PIOR", jogo.PiorResultado);
                    command2.Parameters.AddWithValue("@MELHOR", jogo.MelhorResultado);
                    command2.Parameters.AddWithValue("@MUDOUP", jogo.MudouPior);
                    command2.Parameters.AddWithValue("@MUDOUM", jogo.MudouMelhor);
                    command2.Parameters.AddWithValue("@ID", jogo.Id);

                    command2.ExecuteNonQuery();

                    connection.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Falha ao Atualizar "+ e.Message);
                    Console.ReadLine();
                    continua = false;
                }


                if (continua)
                {

                    string s;
                    do
                    {
                        Console.Write("Deseja cadastrar mais um resultado? (S/N)");
                        s = Console.ReadLine();
                        if (s.ToUpper() == "S")
                        {
                            continua = true;
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
                    } while (s.ToUpper() != "S" && s.ToUpper() != "N");
                }
            } while (continua);
            Console.Clear();
        }

        static void Visualizar()
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

            var connString = ConnectionString;
            var connection = new MySqlConnection(connString);

            connection.Open();

            var command = new MySqlCommand("SELECT * FROM pontosdb.jogo", connection);
            command.Parameters.Clear();

            //command.CommandType = CommandType.Text();

            MySqlDataReader data = command.ExecuteReader();
            while (data.Read())
            {
                Console.Write("| ");
                Console.Write(data.GetInt32(0));
                Console.Write("  |  ");
                Console.Write(data.GetInt32(1));
                Console.Write("  | ");
                Console.Write(data.GetInt32(2));
                Console.Write(" | ");
                Console.Write(data.GetInt32(3));
                Console.Write(" |   ");
                Console.Write(data.GetInt32(4));
                Console.Write("   |   ");
                Console.Write(data.GetInt32(5));
                Console.WriteLine("   |");
                Console.WriteLine("---------------------------------------");
            }

            connection.Close();
            Console.ReadLine();
            Console.Clear();
        }
    }
}
