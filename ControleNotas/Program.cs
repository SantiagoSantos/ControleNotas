using System;

namespace ControleNotas
{
    class Program
    {
        static void Main(string[] args)
        {
            int op;
            Aluno[] alunos = new Aluno[5];
            short proximoAluno = 0;

            do
            {            
                op = 0;    
                MontarMenuInicial();

                try
                {
                    op = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("A opção informada é inválida!");
                }

                switch (op)
                {
                    case 1:
                        if (proximoAluno > alunos.Length - 1)
                        {
                            Console.WriteLine("Capacidade de alunos(as) excedida!");
                            Console.ReadKey();
                            break;
                        }
                                                                
                        alunos[proximoAluno] = CadastrarAluno();
                        Console.WriteLine($"Aluno(a) {alunos[proximoAluno].Nome} cadastrado(a) com sucesso!");
                        Console.ReadKey();

                        proximoAluno++;

                        break;
                    case 2:
                        //todo
                        ListarAlunos(alunos);
                        Console.WriteLine("Pressione qualquer tecla para retornar.");
                        Console.ReadKey();
                        break;
                    case 3:
                        //todo 
                        CalcularMedia(alunos);
                        Console.ReadKey();
                        break;  
                    case 9:
                        Console.WriteLine("Encerrando aplicação...");
                        break;                  
                    default:
                        Console.WriteLine("Opção inválida!");
                        Console.ReadKey();
                        break;
                }

            } while (op < 9);

            Console.WriteLine();
            Console.WriteLine("Aplicação encerrada.");
            
        }

        static void MontarMenuInicial()
        {
            Console.Clear();            
            NomeSistema();
            Console.WriteLine("Informe a opção desejada:");
            Console.WriteLine();
            Console.WriteLine("1. Inserir novo(a) aluno(a)");
            Console.WriteLine("2. Listar alunos(as)");
            Console.WriteLine("3. Calcular média da turma");
            Console.WriteLine("9. Encerrar aplicação");

            Console.WriteLine();
        }

        static void NomeSistema()
        {
            Console.WriteLine($":::::::::::::::::::::: Controle de Notas versão {System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()} ::::::::::::::::::::::");
            Console.WriteLine();
        }

        static Aluno CadastrarAluno()
        {
            bool isNotaValida = false;
            bool isNomeValido = false;

            Console.Clear();
            NomeSistema();
            Console.WriteLine();
            Console.WriteLine(":::CADASTRO DE ALUNOS:::");
            Console.WriteLine();

            Aluno aluno = new Aluno();

            do
            {
                Console.WriteLine("Informe o nome do aluno:");                        
                aluno.Nome = Console.ReadLine();

                if (aluno.Nome.Trim().Length == 0 || aluno.Nome.Trim().Length > 15)
                {
                    Console.WriteLine("Nome do aluno deve conter de uma a 15 caracteres.");
                }
                else
                {
                    isNomeValido = true;
                }

            } while (!isNomeValido);

            
            Console.WriteLine("Informe a nota do aluno (com vírgula se necessário):");

            do
            {
                if (decimal.TryParse(Console.ReadLine(), out decimal nota))
                {
                    if (nota >= 0 && nota <= 10)
                    {
                        aluno.Nota = nota;
                        isNotaValida = true;
                    }
                    else
                    {
                        Console.WriteLine("Deve ser digitada uma nota de zero a dez!");
                        Console.WriteLine("Informe a nota do aluno (com vírgula se necessário):");
                    }
                }
                
            } while (!isNotaValida);

            return aluno;
        }

        static void ListarAlunos(Aluno[] alunos)
        {
            Console.Clear();
            NomeSistema();
            Console.WriteLine();
            Console.WriteLine(":::LISTAGEM DE ALUNOS:::");
            Console.WriteLine();
            Console.WriteLine(new String('-', 33));
            Console.WriteLine(String.Format("|{0, -15}|{1, -15}|", "     Aluno     ", "     Nota     "));
            Console.WriteLine(new String('-', 33));

            for (int i = 0; i < alunos.Length; i++)
            { 
                if (!string.IsNullOrEmpty(alunos[i].Nome))
                {                    
                    Console.WriteLine("|{0, -15}|{1, 15}|", alunos[i].Nome, alunos[i].Nota);
                    Console.WriteLine(new String('-', 33));   
                }
                
            }
            
        }

        static void CalcularMedia(Aluno[] alunos)
        {
            decimal media = 0;
            decimal somaTotal = 0;
            short quantidadeAlunos = 0;

            Console.Clear();
            NomeSistema();
            Console.WriteLine();
            Console.WriteLine(":::MÉDIA GERAL DA TURMA:::");
            Console.WriteLine();

            foreach (Aluno a in alunos)
            {
                if (!string.IsNullOrEmpty(a.Nome))
                {                    
                    somaTotal += a.Nota;
                    quantidadeAlunos++;
                }
            }

            if (quantidadeAlunos > 0)
            {
                media = somaTotal / quantidadeAlunos;
            }

            Console.WriteLine(new String('-', 50));
            Console.WriteLine($"|A média geral dos {quantidadeAlunos} alunos cadastrados é {media.ToString("0.##")}.|");
            Console.WriteLine(string.Format("|A média geral de {0} aluno(s) cadastrado(s) é {1}.|", quantidadeAlunos, media.ToString("0.##")));
            Console.WriteLine($"Conceito da turma: {ConceitoFinal(media).PadRight(50, ' ')}");
            Console.WriteLine(new String('-', 50));
        }

        static string ConceitoFinal(decimal nota)
        {
            Conceito con;

            if (nota < 2)
            {
                con = Conceito.E;
            }
            else if (nota < 4)
            {
                con = Conceito.D;
            }
            else if (nota < 4)
            {
                con = Conceito.C;
            }
            else if (nota < 4)
            {
                con = Conceito.B;
            }
            else
            {
                con = Conceito.A;
            }

            return con.ToString();
        }
    }
}
