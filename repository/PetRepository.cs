using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contoso_Pets.repository
{
    public class PetRepository
    {
        public Pet[][] ourAnimals = new Pet[][]
        {
        new Pet[]
         {
      new Pet(1, "cachorro", new DateTime(2019, 3, 10), "saúde boa", "amigável", "Rex"),
      new Pet(2, "cachorro", new DateTime(2017, 5, 15), "saúde excelente", "brincalhão", "Buddy"),
      new Pet(3, "cachorro", new DateTime(2016, 7, 20), "saúde regular", "leal", "Charlie"),
      new Pet(4, "cachorro", new DateTime(2020, 1, 5), "saúde boa", "energético", "Max"),
      new Pet(5, "cachorro", new DateTime(2015, 10, 25), "saúde excelente", "inteligente", "Cooper"),
      new Pet(6, "cachorro", new DateTime(2017, 8, 12), "saúde boa", "protetor", "Rocky"),
      new Pet(7, "cachorro", new DateTime(2018, 6, 30), "saúde regular", "brincalhão", "Bailey"),
      new Pet(8, "cachorro", new DateTime(2016, 4, 18), "saúde excelente", "obediente", "Lola"),
      new Pet(9, "cachorro", new DateTime(2014, 9, 5), "saúde boa", "amoroso", "Duke"),
      new Pet(10, "cachorro", new DateTime(2013, 11, 22), "saúde regular", "corajoso", "Milo")
         },
         new Pet[]
             {
      new Pet(11, "gato", new DateTime(2020, 4, 8), "saúde boa", "curioso", "Milo"),
      new Pet(12, "gato", new DateTime(2018, 6, 10), "saúde excelente", "ronrona", "Luna"),
      new Pet(13, "gato", new DateTime(2017, 8, 15), "saúde regular", "independente", "Simba"),
      new Pet(14, "gato", new DateTime(2019, 2, 28), "saúde boa", "brincalhão", "Oliver"),
      new Pet(15, "gato", new DateTime(2016, 10, 5), "saúde excelente", "dorminhoco", "Chloe"),
      new Pet(16, "gato", new DateTime(2018, 12, 20), "saúde boa", "meigo", "Whiskers"),
      new Pet(17, "gato", new DateTime(2017, 4, 18), "saúde regular", "esperto", "Salem"),
      new Pet(18, "gato", new DateTime(2015, 11, 10), "saúde excelente", "caçador", "Nala"),
      new Pet(19, "gato", new DateTime(2013, 9, 8), "saúde boa", "manhoso", "Garfield"),
      new Pet(20, "gato", new DateTime(2010, 6, 7), "saúde boa", "manhoso", "Jubileu"),
            }
       };
        public void ListarAnimaisPaginado(int pagina, int tamanhoPagina)
        {
            int totalAnimais = ourAnimals.SelectMany(a => a).Count();
            int totalPaginas = (int)Math.Ceiling((double)totalAnimais / tamanhoPagina);

            if (pagina < 1 || pagina > totalPaginas)
            {
                Console.WriteLine("Página inválida.");
                return;
            }

            int indiceInicial = (pagina - 1) * tamanhoPagina;
            int indiceFinal = indiceInicial + tamanhoPagina - 1;

            for (int i = indiceInicial; i <= indiceFinal && i < totalAnimais; i++)
            {
                Pet animal = ourAnimals.SelectMany(a => a).ElementAt(i);
                Console.WriteLine(animal.ToString());
            }

            Console.WriteLine($"Página {pagina}/{totalPaginas}");
        }

        public List<Pet> ListarAnimais()
        {
            List<Pet> animais = ourAnimals.SelectMany(a => a).ToList();
            return animais;
        }
        public void AdicionarAnimal(Pet animal)
        {
            if (animal == null)
            {
                Console.WriteLine("Animal inválido.");
                return;
            }

            int grupoIndex = -1;
            for (int i = 0; i < ourAnimals.Length; i++)
            {
                if (ourAnimals[i].Length < 10)
                {
                    grupoIndex = i;
                    break;
                }
            }

            if (grupoIndex == -1)
            {
                Console.WriteLine("Não é possível adicionar mais animais.");
                return;
            }

            ourAnimals[grupoIndex] = ourAnimals[grupoIndex].Append(animal).ToArray();
            Console.WriteLine("Animal adicionado com sucesso.");
        }
        public Pet BuscarAnimalPorId(int id)
        {
            foreach (var animais in ourAnimals)
            {
                foreach (var animal in animais)
                {
                    if (animal.PetId == id)
                    {
                        return animal;
                    }
                }
            }
            return null;
        }
        public void AtualizarAnimal(int id, Pet novoAnimal)
        {
            for (int i = 0; i < ourAnimals.Length; i++)
            {
                for (int j = 0; j < ourAnimals[i].Length; j++)
                {
                    if (ourAnimals[i][j].PetId == id)
                    {
                        ourAnimals[i][j] = novoAnimal;
                        return;
                    }
                }
            }
        }

        public Pet[] ExibirPorCaracteristicas(string tipo, string caracteristicas)
        {
            List<Pet> Encontrados = new List<Pet>();

            for (int i = 0; i < ourAnimals.Length; i++)
            {
                for (int j = 0; j < ourAnimals[i].Length; j++)
                {
                    if (ourAnimals[i][j].Species.ToLower() == tipo &&
                        (
                         ourAnimals[i][j].Nickname.ToLower().Contains(caracteristicas.ToLower()) ||
                         ourAnimals[i][j].Personality.ToLower().Contains(caracteristicas.ToLower()) ||
                         ourAnimals[i][j].PhysicalCondition.ToLower().Contains(caracteristicas.ToLower())))
                    {
                        Encontrados.Add(ourAnimals[i][j]);
                    }
                }
            }

            return Encontrados.ToArray();
        }


    }
}