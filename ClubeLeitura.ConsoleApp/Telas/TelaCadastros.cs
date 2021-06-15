using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClubeLeitura.ConsoleApp.Dominio;
using ClubeLeitura.ConsoleApp.Controladores;


namespace ClubeLeitura.ConsoleApp.Telas
{
    public abstract class TelaCadastros<T>: TelaBase where T : EntidadeBase
    {
        protected Controlador<T> controlador;

        public TelaCadastros(string titulo, Controlador<T> controlador) : base(titulo)
        {
            this.controlador = controlador;
        }

        public void InserirNovoRegistro()
        {
            ConfigurarTela("Inserindo um novo registro...");

            T registro = ObterRegistro(TipoAcao.Inserindo);

            string resultadoValidacao = controlador.Adicionar(registro);

            if (resultadoValidacao == "VALIDO")
                ApresentarMensagem("Registro inserido com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                InserirNovoRegistro();
            }
        }

        public void EditarRegistro()
        {
            ConfigurarTela("Editando um registro...");

            bool temRegistros = VisualizarRegistros(TipoVisualizacao.Pesquisando);

            if (temRegistros == false)
                return;

            Console.Write("\nDigite o número do registro que deseja editar: ");
            int id = Convert.ToInt32(Console.ReadLine());

            bool numeroEncontrado = controlador.ExisteRegistro(id);
            if (numeroEncontrado == false)
            {
                ApresentarMensagem("Nenhum registro foi encontrado com este número: " + id, TipoMensagem.Erro);
                EditarRegistro();
                return;
            }

            T registro = ObterRegistro(TipoAcao.Editando);

            string resultadoValidacao = controlador.Editar(id, registro);

            if (resultadoValidacao == "VALIDO")
                ApresentarMensagem("registro editado com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                EditarRegistro();
            }
        }

        public void ExcluirRegistro()
        {
            ConfigurarTela("Excluindo um registro...");

            bool temRegistros = VisualizarRegistros(TipoVisualizacao.Pesquisando);

            if (temRegistros == false)
                return;

            Console.Write("\nDigite o número do registro que deseja excluir: ");
            int id = Convert.ToInt32(Console.ReadLine());

            bool numeroEncontrado = controlador.ExisteRegistro(id);
            if (numeroEncontrado == false)
            {
                ApresentarMensagem("Nenhum registro foi encontrado com este número: " + id, TipoMensagem.Erro);
                ExcluirRegistro();
                return;
            }

            bool conseguiuExcluir = controlador.ExcluirRegistro(id);

            if (conseguiuExcluir)
                ApresentarMensagem("registro excluído com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar excluir o registro", TipoMensagem.Erro);
                ExcluirRegistro();
            }
        }

        public  bool VisualizarRegistros(TipoVisualizacao tipo)
        {
            if (tipo == TipoVisualizacao.VisualizandoTela)
                ConfigurarTela("Visualizando registros...");

            List<T> registros = controlador.SelecionarTodosRegistros();

            if (registros.Count == 0)
            {
                ApresentarMensagem("Nenhum registro cadastrado!", TipoMensagem.Atencao);
                return false;
            }

           // string configuracaoColunasTabela = "{0,-10} | {1,-55} | {2,-35}";

            //MontarCabecalhoTabela(configuracaoColunasTabela, "Id", "Nome", "Local");

            foreach (T registro in registros)
            {
                Console.WriteLine(registro.ToString());
            }

            return true;
        }

        public abstract  T ObterRegistro(TipoAcao acao);
    }
}
