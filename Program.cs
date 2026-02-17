using System;

namespace MillingFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== ЗАВОД 'ТОЧНЫЙ ФРЕЗЕР' ===\n");
            
            FactoryMenu menu = new FactoryMenu();
            menu.ShowMainMenu();
            
            Console.WriteLine("\nСмена завершена. Качественных деталей!");
            Console.ReadKey();
        }
    }
}