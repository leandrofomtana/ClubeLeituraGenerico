using ClubeLeitura.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClubeLeitura.ConsoleApp.Controladores
{
    public class ControladorEmprestimo : Controlador<Emprestimo>
    {
        public string RegistrarEmprestimo(Amigo amigo, Revista revista, DateTime data)
        {
            Emprestimo emprestimo = new Emprestimo(amigo, revista, data);

            string resultadoValidacao = emprestimo.Validar();

            if (resultadoValidacao == "VALIDO")
            {
                string dasdsadsaa = Adicionar(emprestimo);
                amigo.RegistrarEmprestimo(emprestimo);
                revista.RegistrarEmprestimo(emprestimo);
            }

            return resultadoValidacao;
        }

        internal bool RegistrarDevolucao(int idEmprestimo, DateTime data)
        {
            Emprestimo emprestimo = SelecionarRegistroPorId(idEmprestimo);
            emprestimo.Fechar(data);
            return true;
        }

        internal List<Emprestimo> SelecionarEmprestimosEmAberto()
        {
            List<Emprestimo> emprestimosEmAberto = new List<Emprestimo>();
            foreach (var e in SelecionarTodosRegistros())
                if (e.estaAberto)
                    emprestimosEmAberto.Add(e);
            return emprestimosEmAberto;
        }

        internal List<Emprestimo> SelecionarEmprestimosFechados(int mes)
        {
            List<Emprestimo> emprestimosFechados = new List<Emprestimo>();
            foreach (var e in SelecionarTodosRegistros())
                if (!e.estaAberto && e.Mes==mes)
                    emprestimosFechados.Add(e);
            return emprestimosFechados;
        }
    }
}