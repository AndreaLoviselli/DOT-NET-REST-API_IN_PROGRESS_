using SamuraiApp.Core.Model;
using SamuraiApp.Core.Service;
using SamuraiApp.Core.Service.Storage;

namespace SamuraiApp.ConsoleApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string action_exit = "0";
            const string action_print_samurai_list = "1";
            const string action_create_samurai = "2";
            const string action_update_samurai = "3";
            const string action_delete_samurai = "4";

            var samuraiStorageService = new InMemorySamuraiStorageService();
            var manager = new SamuraiApplicationManager(samuraiStorageService);
 
            var action ="";
            do
            {
                // Pulizia console
                Console.Clear();

                // Stampa Menu Principale
                Console.WriteLine("Menu Principale");
                Console.WriteLine($" {action_print_samurai_list}. Visualizzare lista samurai");
                Console.WriteLine($" {action_create_samurai}. Creare un nuovo samurai");
                Console.WriteLine($" {action_update_samurai}. Aggiornare un samurai");
                Console.WriteLine($" {action_delete_samurai}. Cancellare un samurai");
                Console.WriteLine($" {action_exit}. Esci dal programma");
                Console.WriteLine("------------------------------------");

                // Richiesta scelta utente
                Console.Write("Selezionare un'opzione: ");
                action = Console.ReadLine();

                switch (action)
                {
                    case action_print_samurai_list:
                        var samuraiList = manager.GetSamurais();
                        if(samuraiList.Count() == 0)
                        {
                            Console.WriteLine("Nessun samurai in lista");
                        } else
                        {
                            foreach(var samuraiToPrint in samuraiStorageService.GetSamurais())
                            {
                                Console.WriteLine($" Samurai #{samuraiToPrint.Id}");
                                Console.WriteLine($" Nome: {samuraiToPrint.Name}");
                                Console.WriteLine($" Punti Vita: {samuraiToPrint.LifePoints}");
                            }
                        }
                        break;

                    case action_create_samurai:                        
                        Console.Write("Inserire il nome del Samurai: ");
                        string name = Console.ReadLine()!;

                        Console.Write("Inserire i punti vita: ");
                        int lifePoints = int.Parse(Console.ReadLine()!);

                        manager.CreateSamurai(name, lifePoints);                        

                        break;

                    case action_update_samurai:
                        Console.WriteLine("Inserire l'id del samurai da aggiornare: ");
                        var idToUpdate = int.Parse(Console.ReadLine()!);
                        
                        
                        if (manager.Exists(idToUpdate))
                            {
                            Console.WriteLine("Inserire il nuovo nome: ");
                            var newName = Console.ReadLine()!;

                            Console.WriteLine("Inserire i nuovi punti vita: ");
                            var newLifePoints = int.Parse(Console.ReadLine()!);

                            manager.SaveSamurai(idToUpdate, newName, newLifePoints);                                                            
                        }
                        
                        break;

                    case action_delete_samurai:
                        Console.WriteLine("Inserire l'id del samurai da eliminare: ");
                        var idToDelete = int.Parse(Console.ReadLine()!);
                        manager.DeleteSamurai(idToDelete);

                        break;

                    case action_exit:
                        break;
               }

               if (action != "0")
               {
                   Console.WriteLine("------------------------------------");

                   Console.Write("Premere un tasto per continuare: ");
                   Console.ReadLine();
               }
           }while (action != "0");
                }
            }
}
