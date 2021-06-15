using ClubeLeitura.ConsoleApp.Controladores;
using ClubeLeitura.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;

namespace ClubeLeitura.ConsoleApp.Telas
{
    public class TelaAmigo : TelaCadastros<Amigo>, ICadastravel
    {
        public TelaAmigo(Controlador<Amigo> controlador)
            : base("Cadastro de Amigos", controlador)
        {
        }
        public override Amigo ObterRegistro(TipoAcao acao)
        {
            Console.Write("Digite o nome do amiguinho: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o nome do responsável: ");
            string nomeResponsavel = Console.ReadLine();

            Console.Write("Digite o telefone do amiguinho: ");
            string telefone = Console.ReadLine();

            Console.Write("Digite da onde é o amiguinho: ");
            string deOndeEh = Console.ReadLine();

            return new Amigo(nome, nomeResponsavel, telefone, deOndeEh);
        }
    }
}
