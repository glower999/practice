// TODO:
// 1. Реализовать просмотр каталога деталей и станков
// 2. Реализовать прием и распределение заказов
// 3. Реализовать управление производством и отчеты

using System;

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
            // Инициализация деталей
            manager.AddDetail(new Detail(1, "Фланец", "ЧЕРТ-001", 1200, "сталь", "100x100x20", 0.02m));
            manager.AddDetail(new Detail(2, "Корпус", "ЧЕРТ-002", 3500, "алюминий", "200x150x50", 0.01m));
            manager.AddDetail(new Detail(3, "Шестерня", "ЧЕРТ-003", 800, "сталь", "50x50x10", 0.005m));
            manager.AddDetail(new Detail(4, "Кронштейн", "ЧЕРТ-004", 950, "латунь", "80x60x15", 0.03m));
            manager.AddDetail(new Detail(5, "Втулка", "ЧЕРТ-005", 450, "титан", "30x30x20", 0.008m));
            
            // Инициализация станков
            manager.AddMachine(new Machine(1, "DMU 50", "5-осевой", 2000, 0.003m, "500x400x300"));
            manager.AddMachine(new Machine(2, "Haas VF-2", "3-осевой", 2500, 0.005m, "750x500x400"));
            manager.AddMachine(new Machine(3, "Универсальный фрезерный", "универсальный", 3000, 0.01m, "1000x600x500"));
        }
        
        // TODO 1: Показать все детали
        public void ShowAllDetails()
        {
            Console.WriteLine("=== КАТАЛОГ ДЕТАЛЕЙ ===");
            
            // Получить все детали через manager.GetAllDetails()
            // Для каждой детали вывести:
            // - Название, номер чертежа, цена
            // - Материал, габариты, допуск
            // - Вес, сложность, рекомендуемый станок
        }
        
        // TODO 1: Показать все станки
        public void ShowAllMachines()
        {
            Console.WriteLine("=== СТАНКИ ЦЕХА ===");
            
            // Получить все станки через manager.GetAllMachines()
            // Для каждого станка вызвать ShowMachineInfo()
        }
        
        // TODO 2: Принять новый заказ
        public void CreateNewOrder()
        {
            Console.WriteLine("=== ПРИЕМ НОВОГО ЗАКАЗА ===");
            
            // 1. Запросить название заказчика
            // 2. Запросить желаемый срок (опционально)
            // 3. Создать заказ через manager.CreateOrder()
            // 4. Показать каталог деталей
            // 5. В цикле добавлять детали в заказ
            // 6. После добавления всех деталей:
            //    - Распределить заказ по станкам через manager.DistributeOrder()
            //    - Показать информацию о распределении
            //    - Показать расчетную стоимость и сроки
        }
        
        // TODO 2: Показать активные заказы
        public void ShowActiveOrders()
        {
            Console.WriteLine("=== АКТИВНЫЕ ЗАКАЗЫ ===");
            
            // Получить активные заказы через manager.GetActiveOrders()
            // Для каждого заказа вызвать ShowOrderInfo()
            // Если нет активных заказов - сообщить
        }
        
        // TODO 3: Управление производством
        public void ManageProduction()
        {
            Console.WriteLine("=== УПРАВЛЕНИЕ ПРОИЗВОДСТВОМ ===");
            
            // 1. Показать активные заказы
            // 2. Показать станки с задачами
            // 3. Предложить меню:
            //    а) Начать выполнение задачи на станке
            //    б) Завершить выполнение задачи
            //    в) Проверить готовность заказа
            // 4. Реализовать логику начала/завершения задач
        }
        
        // TODO 3: Зарегистрировать изготовление детали
        public void RegisterDetailCompletion()
        {
            Console.WriteLine("=== РЕГИСТРАЦИЯ ИЗГОТОВЛЕНИЯ ДЕТАЛИ ===");
            
            // 1. Запросить номер заказа
            // 2. Запросить номер детали
            // 3. Запросить номер станка
            // 4. Вызвать manager.CompleteDetailProduction()
            // 5. Сообщить о результате
            // 6. Если заказ полностью готов - поздравить и выдать документы
        }
        
        // TODO 3: Техническое обслуживание
        public void PerformMaintenance()
        {
            Console.WriteLine("=== ТЕХНИЧЕСКОЕ ОБСЛУЖИВАНИЕ ===");
            
            // Получить станки требующие ТО через manager.GetMachinesNeedingMaintenance()
            // Показать список
            // Предложить выполнить ТО на выбранном станке
            // Вызвать machine.PerformMaintenance()
        }
        
        // TODO 3: Показать отчет о производстве
        public void ShowProductionReport()
        {
            Console.WriteLine("=== ОТЧЕТ О ПРОИЗВОДСТВЕ ===");
            
            // Получить статистику через manager.GetProductionStats()
            // Вывести:
            // - Всего заказов за месяц
            // - Выполнено заказов
            // - Выручка за месяц
            // - Загрузка станков
            
            // Показать станки требующие ТО
        }
        
        // TODO 1: Проверить возможность изготовления детали
        public void CheckProductionFeasibility()
        {
            Console.WriteLine("=== ПРОВЕРКА ВОЗМОЖНОСТИ ИЗГОТОВЛЕНИЯ ===");
            
            // 1. Показать каталог деталей
            // 2. Запросить код детали
            // 3. Найти деталь
            // 4. Найти подходящий станок через manager.FindSuitableMachine()
            // 5. Показать результат: можно/нельзя изготовить и на каком станке
        }
        
        // Готовый метод - главное меню
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
                    case "1":
                        ShowAllDetails();
                        break;
                    case "2":
                        ShowAllMachines();
                        break;
                    case "3":
                        CheckProductionFeasibility();
                        break;
                    case "4":
                        CreateNewOrder();
                        break;
                    case "5":
                        ShowActiveOrders();
                        break;
                    case "6":
                        ManageProduction();
                        break;
                    case "7":
                        RegisterDetailCompletion();
                        break;
                    case "8":
                        PerformMaintenance();
                        break;
                    case "9":
                        ShowProductionReport();
                        break;
                    case "10":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор!");
                        break;
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