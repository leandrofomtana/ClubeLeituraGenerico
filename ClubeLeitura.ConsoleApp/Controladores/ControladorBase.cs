using ClubeLeitura.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;

namespace ClubeLeitura.ConsoleApp.Controladores
{
    public class Controlador<T> where T : EntidadeBase
    {
        protected List<T> itens = new List<T>();
        private int ultimoId;

       public string Adicionar(T item)
        {
            string resultadoValidacao = item.Validar();

            if (resultadoValidacao == "VALIDO")
            {
                item.id = NovoId();
                itens.Add(item);
            }

            return resultadoValidacao;
        }

        public bool ExisteRegistro(int id)
        {
            return itens.Exists(x => x.id == id);

        }
        public bool ExcluirRegistro(int id)
        {
            bool conseguiuExcluir = false;
            if (ExisteRegistro(id))
            {
                T item = itens.Find(x => x.id == id);
                if (item != null)
                {
                    itens.Remove(item);
                    conseguiuExcluir = true;
                }
            }
            return conseguiuExcluir;
        }

        public List<T> SelecionarTodosRegistros()
        {
            return itens;
        }

        public T SelecionarRegistroPorId(int id)
        {
            T item = null;
            if (ExisteRegistro(id))
                item = itens.Find(x => x.id == id);
            return item;
        }

        protected int QtdRegistrosCadastrados()
        {
            return itens.Count;
        }

        protected int NovoId()
        {
            return ++ultimoId;
        }

        public string Editar(int id, T item)
        {
            int index = itens.FindIndex(x => x.id == id);
            string resultadoValidacao = item.Validar();

            if (resultadoValidacao == "VALIDO") { 
                item.id = id;
                itens.Insert(index, item);
                itens.RemoveAt(index+1);
            }

            return resultadoValidacao;


        }
    }
}