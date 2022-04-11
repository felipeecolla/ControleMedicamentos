﻿using ControleMedicamentos.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.ConsoleApp.ModuloPaciente
{
    public class TelaCadastroPaciente : TelaBase, ITelaCadastravel
    {
        private readonly RepositorioPaciente _repositorioPaciente;
        private readonly Notificador _notificador;

        public TelaCadastroPaciente(RepositorioPaciente repositorioFuncionario, Notificador notificador)
            : base("Cadastro de Funcionários")
        {
            _repositorioPaciente = repositorioFuncionario;
            _notificador = notificador;
        }

        public void Editar()
        {
            MostrarTitulo("Editando Paciente");

            bool temPacientesCadastrados = VisualizarRegistros("Pesquisando");

            if (temPacientesCadastrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum paciente cadastrado para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroPaciente = ObterNumeroPaciente();

            Paciente pacienteAtualizado = ObterPaciente();

            bool conseguiuEditar = _repositorioPaciente.Editar(numeroPaciente, pacienteAtualizado);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Paciente editado com sucesso!", TipoMensagem.Sucesso);
        }

        public int ObterNumeroPaciente()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID do Paciente que deseja editar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = _repositorioPaciente.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("ID do Paciente não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Paciente");

            bool temPacientesRegistrados = VisualizarRegistros("Pesquisando");

            if (temPacientesRegistrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum paciente cadastrado para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroPaciente = ObterNumeroRegistro();

            bool conseguiuExcluir = _repositorioPaciente.Excluir(numeroPaciente);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Funcionário excluído com sucesso!", TipoMensagem.Sucesso);
        }

        private int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID do paciente que deseja editar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = _repositorioPaciente.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("ID do paciente não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }

        public void Inserir()
        {
            MostrarTitulo("Cadastro de Paciente");

            Paciente novoPaciente = ObterPaciente();

            _repositorioPaciente.Inserir(novoPaciente);

            _notificador.ApresentarMensagem("Paciente cadastrado com sucesso!", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Paciente");

            List<Paciente> pacientes = _repositorioPaciente.SelecionarTodos();

            if (pacientes.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhum paciente disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Paciente paciente in pacientes)
                Console.WriteLine(paciente.ToString());

            Console.ReadLine();

            return true;
        }

        private Paciente ObterPaciente()
        {
            Console.WriteLine("Digite o nome do paciente: ");
            string nome = Console.ReadLine();

            Console.WriteLine("Digite o CPF do paciente: ");
            string cpf = Console.ReadLine();

            Console.WriteLine("Digite o endereço do paciente: ");
            string endereco = Console.ReadLine();

            return new Paciente(nome, cpf, endereco);
        }
    }
}
