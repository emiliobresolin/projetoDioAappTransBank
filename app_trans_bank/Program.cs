using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace app_trans_bank
{
    public enum TipoConta
    {
        PessoaFisica = 1,
        PessoaJuridica = 2
    }
    public class Conta    //ATRIBUTOS
    {
        private TipoConta TipoConta { get; set; }  //AS CONTAS SAO PRIVETE PARA NGM CONSEGUIR MODIFICA-LOS APOS CRIACAO
        private double Saldo { get; set; }
        private double Credito { get; set; }
        private string Nome { get; set; }

        public Conta(TipoConta TipoConta, double saldo, double credito, string nome) //DECLARANDO TUDO Q TEM DENTRO DA CONTA
        {
            this.TipoConta = TipoConta; //o THIS serve para deditar somente aquela instancia declara depois do .
            this.Saldo = saldo;
            this.Credito = credito;
            this.Nome = nome;
        }

        public bool Sacar(double valorSaque) // BOLEANO PARA RETORNAR TRUE OR FALSE
        {
            if (this.Saldo - valorSaque < (this.Credito * -1))    //VALIDACAO DE SALDO SUFICIENTE
            {
                Console.WriteLine("\nSaldo Insuficiente\n");
                return false;   //DEPOIS DO FALSE ELE VOLTA PRA QM CHAMOU ESSE METODO
            }
            this.Saldo -= valorSaque; //IGUAL FAZER this.Saldo = this.Saldo - valorSaque
            Console.WriteLine($"Saldo Atual da Conta de {Nome} e {Saldo}", this.Nome, this.Saldo); //CORRECAO DAS VIDEO AULAS ONDE ERAN 0 e 1
            return true; //DEPOIS DE INSERIR O TIPO DE RETORNO O ERRO SAI NO bool Saque(
        }
        public void Depositar(double valorDeposito) //deposito nao precisa de verificacao para entrar na conta
        {
            this.Saldo += valorDeposito;
            Console.WriteLine($"Saldo Atual da Conta de {Nome} e {Saldo}", this.Nome, this.Saldo); //CORRECAO DAS VIDEO AULAS ONDE ERAN 0 e 1
        }
        public void Transferir(double valorTransferencia, Conta contaDestino)
        {
            if (this.Sacar(valorTransferencia))  //reutilizando o methodo sacar para transferencia
            {
                contaDestino.Depositar(valorTransferencia);
            }
        }
        public override string ToString() //toda vez q puxarmos a string ele vai dar esse orver ride para mostrar os dados ao inves de somente a classe 
        {
            string retorno = " ";
            retorno += "TipoConta " + this.TipoConta + " | ";
            retorno += "Saldo " + this.Saldo + " | ";
            retorno += "Credito " + this.Credito + " | ";
            retorno += "Nome " + this.Nome + " | ";
            return retorno;
        }
    }
    class Program
    {
        static List<Conta> ListaContas = new List<Conta>();
        private static string ObterOpcaoUsuario()   //MENU
        {
            Console.WriteLine();
            Console.WriteLine("\nBRESOLIN BANK sempre a frente!");
            Console.WriteLine("\nInforma a Opcao Desejada!");

            Console.WriteLine("1- Listar Contas");
            Console.WriteLine("2- Inserir Conta Nova");
            Console.WriteLine("3- Transferir");
            Console.WriteLine("4- Sacar");
            Console.WriteLine("5- Depositar");
            Console.WriteLine("C- Limpar Tela");
            Console.WriteLine("X- Sair");
            Console.WriteLine();

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;
        }
        static void Main(string[] args)
        {

            string opcaoUsuario = ObterOpcaoUsuario();

            while (opcaoUsuario.ToUpper() != "X") //inserir ToUpper se o usuario digitar minusculo
            {
                switch (opcaoUsuario)
                {
                    case "1":
                        ListarContas();
                        break;
                    case "2":
                        InserirConta();
                        break;
                    case "3":
                        Transferir();
                        break;
                    case "4":
                        Sacar();
                        break;
                    case "5":
                        Depositar();
                        break;
                    case "C":
                        Console.Clear();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                opcaoUsuario = ObterOpcaoUsuario();
            }
            Console.WriteLine("\nObrigado por Ultilizar Nossos Servicos!");
            Console.ReadLine();
        }

        private static void Transferir()
        {
            Console.WriteLine("\nDigite o Numero da Conta de Origem: ");
            int indiceContaOrigem = int.Parse(Console.ReadLine());
            Console.WriteLine("\nDigite o Numero da Conta de Destino: ");
            int indiceContaDestino = int.Parse(Console.ReadLine());
            Console.WriteLine("\nDigite o Valor a ser Transferido: ");
            double valorTransferencia = double.Parse(Console.ReadLine());
            ListaContas[indiceContaOrigem].Transferir(valorTransferencia, ListaContas[indiceContaDestino]);
        }

        private static void Depositar()
        {
            Console.WriteLine("\nDigite o Numero da Conta: ");
            int indiceConta = int.Parse(Console.ReadLine());
            Console.WriteLine("\nDigite o Valor a ser Depositado: ");
            double valorDeposito = double.Parse(Console.ReadLine());
            ListaContas[indiceConta].Depositar(valorDeposito);
        }

        private static void Sacar()
        {
            Console.WriteLine("\nDigite o Numero da Conta: ");
            int indiceConta = int.Parse(Console.ReadLine());
            Console.WriteLine("\nDigite o Valor a ser Sacado: ");
            double valorSaque = double.Parse(Console.ReadLine());
            ListaContas[indiceConta].Sacar(valorSaque); // DEU UM ERRO QUANDO O VALOR DA NEGATIVO, MAS FIZ OUTRO TESTE E FUNCIONOU (!?!??!)
        }

        private static void InserirConta()
        {
            Console.WriteLine("\nInserir Nova Conta\n");
            Console.WriteLine("Digite 1 para Conta Fisica ou 2 para Conta Juridica: ");
            int entradaTipoConta = int.Parse(Console.ReadLine());
            Console.WriteLine("\nDigite o Nome do Cliente: ");
            string entradaNome = Console.ReadLine();
            Console.WriteLine("\nDigite o Saldo Inicial: ");
            double entradaSaldo = double.Parse(Console.ReadLine());
            Console.WriteLine("\nInforme o Valor do Credito do Cliente: ");
            double entradaCredito = double.Parse(Console.ReadLine());
            Conta novaConta = new Conta(TipoConta: (TipoConta)entradaTipoConta, saldo: entradaSaldo, credito: entradaCredito, nome: entradaNome);
            
            ListaContas.Add(novaConta);
        }
        private static void ListarContas()
        {
            Console.WriteLine("\nListas Contas");
            if (ListaContas.Count == 0)
            {
                Console.WriteLine("\nNenhum Conta Cadastrada!");
                return;
            }
            for (int i = 0; i < ListaContas.Count; i++)
            {
                Conta conta = ListaContas[i];
                Console.WriteLine($"#{i} - ", i);   //CORRECAO VIDEO AULA ONDE AO INVES DE I ERA 0
                Console.WriteLine(conta);
            }
        }

    }
}
