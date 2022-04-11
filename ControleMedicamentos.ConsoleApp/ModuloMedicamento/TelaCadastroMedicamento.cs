using ControleMedicamentos.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.ConsoleApp.ModuloMedicamento
{
    public class TelaCadastroMedicamento : TelaBase, ITelaCadastravel
    {
        private readonly RepositorioMedicamento _repositorioMedicamento;
        private readonly Notificador _notificador;

        public TelaCadastroMedicamento(RepositorioMedicamento repositorioMedicamento, Notificador notificador)
        : base("Cadastro Medicamento")
        {
            _repositorioMedicamento = repositorioMedicamento;
            _notificador = notificador;
        }

        public void Editar()
        {
            MostrarTitulo("Editando Medicamento");

            bool temMedicamentosCadastrados = VisualizarRegistros("Pesquisando");

            if (temMedicamentosCadastrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum medicamento cadastrado para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroMedicamento = ObterNumeroMedicamento();

            Medicamento medicamentoAtualizado = ObterMedicamento();

            bool conseguiuEditar = _repositorioMedicamento.Editar(numeroMedicamento, medicamentoAtualizado);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Medicamento editado com sucesso!", TipoMensagem.Sucesso);
        
        }

        private Medicamento ObterMedicamento()
        {
            Console.WriteLine("Digite o nome do medicamento: ");
            string nome = Console.ReadLine();

            Console.WriteLine("Digite a quantidade do medicamento: ");
            int quantidade = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite a descrição do medicamento: ");
            string descricao = Console.ReadLine();

            return new Medicamento(nome, descricao, quantidade);
        }

        private int ObterNumeroMedicamento()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID do medicamento que deseja editar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = _repositorioMedicamento.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("ID do medicamento não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Medicamento");

            bool temMedicamentoRegistrados = VisualizarRegistros("Pesquisando");

            if (temMedicamentoRegistrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum Medicamento cadastrado para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeromedicamento = ObterNumeroRegistro();

            bool conseguiuExcluir = _repositorioMedicamento.Excluir(numeromedicamento);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Medicamento excluído com sucesso!", TipoMensagem.Sucesso);
        }

        private int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID do medicamento que deseja editar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = _repositorioMedicamento.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("ID do medicamento não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }

        public void Inserir()
        {
            MostrarTitulo("Cadastro de medicamento");

            Medicamento novoMedicamento = ObterMedicamento();

            _repositorioMedicamento.Inserir(novoMedicamento);

            _notificador.ApresentarMensagem("Medicamento cadastrado com sucesso!", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Medicamento");

            List<Medicamento> medicamento = _repositorioMedicamento.SelecionarTodos();

            if (medicamento.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhum Medicamento disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Medicamento med in medicamento)
            {
                Console.WriteLine(med.ToString());
            }

            Console.ReadLine();

            return true;
        }
    }
}
