using ControleMedicamentos.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.ConsoleApp.ModuloMedicamento
{
    public class Medicamento : EntidadeBase
    {
        public string nome, descricao;
        public int quantidade; 
        public Medicamento(string nome, string descricao, int quantidade)
        {
            this.nome = nome;
            this.quantidade = quantidade;
            this.descricao = descricao;
        }
        public override string ToString()
        {
            return "ID: " + id + Environment.NewLine +
                   "Nome: " + nome + Environment.NewLine +
                   "Descrição: " + descricao + Environment.NewLine +
                   "Quantidade: " + quantidade + Environment.NewLine;


        }
    }
}
