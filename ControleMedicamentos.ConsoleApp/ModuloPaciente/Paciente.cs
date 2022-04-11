using ControleMedicamentos.ConsoleApp.Compartilhado;
using System;

namespace ControleMedicamentos.ConsoleApp.ModuloPaciente
{
    public class Paciente : EntidadeBase
    {
        public string nome, cpf, endereco;

        public Paciente(string nome, string cpf, string endereco)
        {
            this.nome = nome;
            this.cpf = cpf;
            this.endereco = endereco;
        }
        public override string ToString()
        {
            return "ID: " + id + Environment.NewLine +
                   "Nome: " + nome + Environment.NewLine +
                   "CPF: " + cpf + Environment.NewLine +
                   "Endereço: " + endereco + Environment.NewLine;
                   
             
        }

    }
}
