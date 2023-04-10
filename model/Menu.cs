using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Contoso_Pets.repository;

namespace Contoso_Pets.model
{

    public class Menu
    {
        PetRepository petRepository = new PetRepository();

        public Menu()
        { }
        public static int Options()
        {
            Console.WriteLine("========== Menu de Opções ==========");
            Console.WriteLine("1 - Listar informações de todos os animais");
            Console.WriteLine("2 - Adicionar novo animal");
            Console.WriteLine("3 - Editar idade de um animal");
            Console.WriteLine("4 - Editar descrição de personalidade de um animal");
            Console.WriteLine("5 - Exibir gatos por características físicas");
            Console.WriteLine("6 - Exibir cães por características físicas");
            Console.WriteLine("0 - Sair");
            Console.Write("Digite o número da opção desejada: ");
            int option = Convert.ToInt32(Console.ReadLine());
            return option;
        }

        public void navegar()
        {
            bool exit = false;

            while (!exit)
            {
                int option = Options();

                switch (option)
                {
                    case 1:
                        Console.Write("Digite o número da página: ");
                        int pagina = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Digite o tamanho da página: ");
                        int tamanhoPagina = Convert.ToInt32(Console.ReadLine());
                        petRepository.ListarAnimaisPaginado(pagina, tamanhoPagina);
                        break;
                    case 2:
                        AdicionarNovoAnimal();
                        break;
                    case 3:
                        EditarIdadeAnimal();
                        break;
                    case 4:
                        EditarDescricaoPersonalidade();
                        break;
                    case 5:
                        ExibirGatosPorCaracteristicas();
                        break;
                    case 6:
                        ExibirCaesPorCaracteristicas();
                        break;
                    case 0:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Digite novamente.");
                        break;
                }
            }
        }

        public void AdicionarNovoAnimal()
        {
            Console.WriteLine("========== Adicionar novo animal ==========");
            string tipoAnimal;
            do
            {
                Console.Write("Digite o tipo de animal (cachorro/gato): ");
                tipoAnimal = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(tipoAnimal) || (tipoAnimal.ToLower() != "cachorro" && tipoAnimal.ToLower() != "gato"));

            DateTime dataNascimento;
            do
            {
                Console.Write("Digite a data de nascimento do animal (dd/mm/yyyy): ");
            } while (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataNascimento));

            string condicaoSaude;
            do
            {
                Console.Write("Digite a condição de saúde do animal: ");
                condicaoSaude = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(condicaoSaude));

            string descricaoPersonalidade;
            do
            {
                Console.Write("Digite a descrição de personalidade do animal: ");
                descricaoPersonalidade = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(descricaoPersonalidade));

            string nomeAnimal;
            do
            {
                Console.Write("Digite o nome do animal: ");
                nomeAnimal = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(nomeAnimal));
            int ultimoId = petRepository.ListarAnimais().LastOrDefault()?.PetId ?? 0;
            int novoId = ultimoId + 1;
            Pet novoAnimal = new Pet(novoId, tipoAnimal, dataNascimento, condicaoSaude, descricaoPersonalidade, nomeAnimal);
            petRepository.AdicionarAnimal(novoAnimal);
            Console.WriteLine("Animal adicionado com sucesso!");
        }
        public void EditarIdadeAnimal()
        {
            Console.WriteLine("========== Editar idade de um animal ==========");
            Console.Write("Digite o ID do animal que deseja editar: ");
            int idAnimal = Convert.ToInt32(Console.ReadLine());

            Pet animal = petRepository.BuscarAnimalPorId(idAnimal);

            if (animal == null)
            {
                Console.WriteLine("Animal não encontrado.");
            }
            else
            {
                DateTime novaDataNascimento;
                do
                {
                    Console.Write("Digite a nova data de nascimento do animal (dd/MM/yyyy): ");
                } while (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out novaDataNascimento));

                animal.Age = novaDataNascimento;
                petRepository.AtualizarAnimal(animal.PetId, animal);
                Console.WriteLine("Idade do animal atualizada com sucesso!");
            }
        }
        public void EditarDescricaoPersonalidade()

        {
            Console.WriteLine("========== Editar descrição de personalidade de um animal ==========");
            Console.Write("Digite o ID do animal que deseja editar: ");
            int idAnimal = Convert.ToInt32(Console.ReadLine());

            Pet animal = petRepository.BuscarAnimalPorId(idAnimal);

            if (animal == null)
            {
                Console.WriteLine("Animal não encontrado.");
            }
            else
            {
                string descricaoPersonalidade;
                do
                {
                    Console.Write("Digite a descrição de personalidade do animal: ");
                    descricaoPersonalidade = Console.ReadLine();
                } while (string.IsNullOrWhiteSpace(descricaoPersonalidade));

                animal.Personality = descricaoPersonalidade;
                petRepository.AtualizarAnimal(animal.PetId, animal);
                Console.WriteLine("Descrição de personalidade do animal atualizada com sucesso!");
            }
        }
        public void ExibirGatosPorCaracteristicas()
        {
            Console.WriteLine("Digite as características para busca:");
            string caracteristicas = Console.ReadLine();

            Pet[] gatosEncontrados = petRepository.ExibirPorCaracteristicas("gato", caracteristicas);

            Console.WriteLine("Gatos encontrados:");
            foreach (var gato in gatosEncontrados)
            {
                Console.WriteLine(gato.ToString());
            }
        }

        public void ExibirCaesPorCaracteristicas()

        {
            Console.WriteLine("Digite as características para busca:");
            string caracteristicas = Console.ReadLine();

            Pet[] cachorrosEncontrados = petRepository.ExibirPorCaracteristicas("cachorro", caracteristicas);

            Console.WriteLine("Cachorros encontrados:");
            foreach (var cachorro in cachorrosEncontrados)
            {
                Console.WriteLine(cachorro.ToString());
            }
        }

    }


}