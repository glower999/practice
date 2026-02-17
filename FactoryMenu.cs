using System;
using System.Collections.Generic;

namespace MillingFactory
{
    public class FactoryMenu
    {
        private FactoryManager manager;

        public FactoryMenu()
        {
            manager = new FactoryManager();
            InitializeData();
        }

        private void InitializeData()
        {
            manager.AddDetail(new Detail(1, "Фланец", "ЧЕРТ-001", 1200, "сталь", "100x100x20", 0.02m));
            manager.AddDetail(new Detail(2, "Корпус", "ЧЕРТ-002", 3500, "алюминий", "200x150x50", 0.01m));
            manager.AddDetail(new Detail(3, "Шестерня", "ЧЕРТ-003", 800, "сталь", "50x50x10", 0.005m));
            manager.AddDetail(new Detail(4, "Кронштейн", "ЧЕРТ-004", 950, "латунь", "80x60x15", 0.03m));
            manager.AddDetail(new Detail(5, "Втулка", "ЧЕРТ-005", 450, "титан", "30x30x20", 0.008m));

            manager.AddMachine(new Machine(1, "DMU 50", "5-осевой", 2000, 0.003m, "500x400x300"));
            manager.AddMachine(new Machine(2, "Haas VF-2", "3-осевой", 2500, 0.005m, "750x500x400"));
            manager.AddMachine(new Machine(3, "Универсальный фрезерный", "универсальный", 3000, 0.01m, "1000x600x500"));
        }

        public void ShowAllDetails()
        {
            Console.WriteLine("=== КАТАЛОГ ДЕТАЛЕЙ ===\n");

            var details = manager.GetAllDetails();
            if (details.Count == 0)
            {
                Console.WriteLine("Каталог пуст.");
                return;
            }

            foreach (var detail in details)
            {
                Console.WriteLine($"  ID: {detail.Id}");
                Console.WriteLine($"  Название: {detail.Name}");
                Console.WriteLine($"  Чертеж: {detail.DrawingNumber}");
                Console.WriteLine($"  Цена: {detail.Price} руб.");
                Console.WriteLine($"  Материал: {detail.Material}");
                Console.WriteLine($"  Габариты: {detail.Dimensions} мм");
                Console.WriteLine($"  Допуск: {detail.Tolerance} мм");
                Console.WriteLine($"  Сложность: {detail.GetComplexityLevel()}");
                Console.WriteLine($"  Рекомендуемый станок: {detail.GetRecommendedMachine()}");
                Console.WriteLine(new string('-', 40));
            }
        }

        public void ShowAllMachines()
        {
            Console.WriteLine("=== СТАНКИ ЦЕХА ===\n");

            var machines = manager.GetAllMachines();
            if (machines.Count == 0)
            {
                Console.WriteLine("Станков нет.");
                return;
            }

            foreach (var machine in machines)
            {
                machine.ShowMachineInfo();
                Console.WriteLine(new string('-', 40));
            }
        }

        public void CreateNewOrder()
        {
            Console.WriteLine("=== ПРИЕМ НОВОГО ЗАКАЗА ===");
            Console.WriteLine("(Функция будет реализована позже)");
        }

        public void ShowActiveOrders()
        {
            Console.WriteLine("=== АКТИВНЫЕ ЗАКАЗЫ ===");
            Console.WriteLine("(Функция будет реализована позже)");
        }

        public void ManageProduction()
        {
            Console.WriteLine("=== УПРАВЛЕНИЕ ПРОИЗВОДСТВОМ ===");
            Console.WriteLine("(Функция будет реализована позже)");
        }

        public void RegisterDetailCompletion()
        {
            Console.WriteLine("=== РЕГИСТРАЦИЯ ИЗГОТОВЛЕНИЯ ДЕТАЛИ ===");
            Console.WriteLine("(Функция будет реализована позже)");
        }

        public void PerformMaintenance()
        {
            Console.WriteLine("=== ТЕХНИЧЕСКОЕ ОБСЛУЖИВАНИЕ ===");
            Console.WriteLine("(Функция будет реализована позже)");
        }

        public void ShowProductionReport()
        {
            Console.WriteLine("=== ОТЧЕТ О ПРОИЗВОДСТВЕ ===");
            Console.WriteLine("(Функция будет реализована позже)");
        }

        public void CheckProductionFeasibility()
        {
            Console.WriteLine("=== ПРОВЕРКА ВОЗМОЖНОСТИ ИЗГОТОВЛЕНИЯ ===\n");

            ShowAllDetails();

            Console.Write("\nВведите ID детали: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int detailId))
            {
                Detail detail = manager.GetDetailById(detailId);
                if (detail != null)
                {
                    Console.WriteLine($"\nДеталь: {detail.Name}");
                    Console.WriteLine($"Сложность: {detail.GetComplexityLevel()}");
                    Console.WriteLine($"Рекомендуемый станок: {detail.GetRecommendedMachine()}");

                    Machine machine = manager.FindSuitableMachine(detail);
                    if (machine != null)
                    {
                        Console.WriteLine($"Подходящий станок найден: {machine.Name} (#{machine.Id})");
                    }
                    else
                    {
                        Console.WriteLine("Подходящий станок не найден. Поиск станков будет реализован позже.");
                    }
                }
                else
                {
                    Console.WriteLine("Деталь не найдена!");
                }
            }
            else
            {
                Console.WriteLine("Некорректный ввод!");
            }
        }

        public void ShowMainMenu()
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("=== ЗАВОД 'ТОЧНЫЙ ФРЕЗЕР' ===");
                Console.WriteLine("1. Каталог деталей");
                Console.WriteLine("2. Станки цеха");
                Console.WriteLine("3. Проверить возможность изготовления");
                Console.WriteLine("4. Принять новый заказ");
                Console.WriteLine("5. Активные заказы");
                Console.WriteLine("6. Управление производством");
                Console.WriteLine("7. Регистрация изготовления");
                Console.WriteLine("8. Техническое обслуживание");
                Console.WriteLine("9. Отчет о производстве");
                Console.WriteLine("10. Выход");
                Console.Write("Выберите: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": ShowAllDetails(); break;
                    case "2": ShowAllMachines(); break;
                    case "3": CheckProductionFeasibility(); break;
                    case "4": CreateNewOrder(); break;
                    case "5": ShowActiveOrders(); break;
                    case "6": ManageProduction(); break;
                    case "7": RegisterDetailCompletion(); break;
                    case "8": PerformMaintenance(); break;
                    case "9": ShowProductionReport(); break;
                    case "10": running = false; break;
                    default: Console.WriteLine("Неверный выбор!"); break;
                }

                if (running)
                {
                    Console.WriteLine("\nНажмите Enter...");
                    Console.ReadLine();
                }
            }
        }
    }
}