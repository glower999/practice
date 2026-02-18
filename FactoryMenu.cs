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
                Console.WriteLine($"  Вес: {detail.CalculateWeight()} г");
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
            Console.WriteLine("=== ПРИЕМ НОВОГО ЗАКАЗА ===\n");

            Console.Write("Введите имя заказчика: ");
            string customerName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(customerName))
            {
                Console.WriteLine("Имя заказчика не может быть пустым!");
                return;
            }

            Console.Write("Указать срок исполнения? (да/нет): ");
            string deadlineChoice = Console.ReadLine();
            DateTime? deadline = null;

            if (deadlineChoice?.ToLower() == "да")
            {
                Console.Write("Введите дату (дд.мм.гггг): ");
                string dateInput = Console.ReadLine();
                if (DateTime.TryParse(dateInput, out DateTime parsedDate))
                {
                    deadline = parsedDate;
                }
                else
                {
                    Console.WriteLine("Некорректная дата. Срок не установлен.");
                }
            }

            Order order = manager.CreateOrder(customerName, deadline);

            Console.Write("Выберите приоритет (1-обычный, 2-срочный, 3-сверхсрочный): ");
            string priorityInput = Console.ReadLine();
            switch (priorityInput)
            {
                case "2": order.Priority = "срочный"; break;
                case "3": order.Priority = "сверхсрочный"; break;
                default: order.Priority = "обычный"; break;
            }

            Console.WriteLine("\n--- Доступные детали ---");
            ShowAllDetails();

            bool addingDetails = true;
            while (addingDetails)
            {
                Console.Write("\nВведите ID детали (0 - завершить): ");
                string detailInput = Console.ReadLine();

                if (int.TryParse(detailInput, out int detailId))
                {
                    if (detailId == 0)
                    {
                        addingDetails = false;
                        continue;
                    }

                    Detail detail = manager.GetDetailById(detailId);
                    if (detail != null)
                    {
                        Console.Write($"Количество деталей '{detail.Name}': ");
                        string qtyInput = Console.ReadLine();

                        if (int.TryParse(qtyInput, out int quantity) && quantity > 0)
                        {
                            order.AddDetail(detail, quantity);
                            Console.WriteLine($"Добавлено: {detail.Name} x{quantity}");
                        }
                        else
                        {
                            Console.WriteLine("Некорректное количество!");
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

            if (order.GetItems().Count == 0)
            {
                Console.WriteLine("Заказ пуст. Отменён.");
                return;
            }

            Console.WriteLine("\n--- Распределение по станкам ---");
            bool distributed = manager.DistributeOrder(order);

            if (distributed)
            {
                Console.WriteLine("Заказ успешно распределён по станкам!");
            }
            else
            {
                Console.WriteLine("Не удалось распределить все детали. Проверьте доступность станков.");
            }

            Console.WriteLine("\n--- Информация о заказе ---");
            order.ShowOrderInfo();
        }

        public void ShowActiveOrders()
        {
            Console.WriteLine("=== АКТИВНЫЕ ЗАКАЗЫ ===\n");

            var activeOrders = manager.GetActiveOrders();

            if (activeOrders.Count == 0)
            {
                Console.WriteLine("Нет активных заказов.");
                return;
            }

            foreach (var order in activeOrders)
            {
                order.ShowOrderInfo();
                Console.WriteLine(new string('=', 50));
            }
        }

        public void ManageProduction()
        {
            Console.WriteLine("=== УПРАВЛЕНИЕ ПРОИЗВОДСТВОМ ===\n");

            var activeOrders = manager.GetActiveOrders();
            if (activeOrders.Count == 0)
            {
                Console.WriteLine("Нет активных заказов для управления.");
                return;
            }

            Console.WriteLine("Активные заказы:");
            foreach (var order in activeOrders)
            {
                var status = order.GetCompletionStatus();
                Console.WriteLine($"  Заказ #{order.Id} ({order.CustomerName}) - {status.completed}/{status.total} деталей готово");
            }

            Console.WriteLine("\nСтанки:");
            var machines = manager.GetAllMachines();
            foreach (var machine in machines)
            {
                Console.WriteLine($"  Станок #{machine.Id} {machine.Name} - задач: {machine.GetTaskCount()}, исправен: {(machine.IsOperational ? "ДА" : "НЕТ")}");
            }

            Console.WriteLine("\nДействия:");
            Console.WriteLine("1. Начать выполнение задачи на станке");
            Console.WriteLine("2. Завершить задачу на станке");
            Console.WriteLine("3. Назад");
            Console.Write("Выберите: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Введите номер станка: ");
                    if (int.TryParse(Console.ReadLine(), out int startMachineId))
                    {
                        Machine machine = manager.GetMachineById(startMachineId);
                        if (machine != null && machine.GetTaskCount() > 0)
                        {
                            Console.WriteLine("Задачи на станке:");
                            var tasks = machine.GetTasks();
                            for (int i = 0; i < tasks.Count; i++)
                            {
                                Console.WriteLine($"  {i + 1}. Заказ #{tasks[i].Order.Id}: {tasks[i].Detail.Name} x{tasks[i].Quantity}");
                            }

                            Console.Write("Номер задачи для запуска: ");
                            if (int.TryParse(Console.ReadLine(), out int taskNum) && machine.StartTask(taskNum - 1))
                            {
                                Console.WriteLine("Задача запущена!");
                            }
                            else
                            {
                                Console.WriteLine("Не удалось запустить задачу.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Станок не найден или нет задач.");
                        }
                    }
                    break;

                case "2":
                    Console.Write("Введите номер станка: ");
                    if (int.TryParse(Console.ReadLine(), out int completeMachineId))
                    {
                        Machine machine = manager.GetMachineById(completeMachineId);
                        if (machine != null && machine.GetTaskCount() > 0)
                        {
                            Console.WriteLine("Задачи на станке:");
                            var tasks = machine.GetTasks();
                            for (int i = 0; i < tasks.Count; i++)
                            {
                                string status = tasks[i].ActualStart.HasValue ? "В РАБОТЕ" : "ОЖИДАЕТ";
                                Console.WriteLine($"  {i + 1}. Заказ #{tasks[i].Order.Id}: {tasks[i].Detail.Name} x{tasks[i].Quantity} [{status}]");
                            }

                            Console.Write("Номер задачи для завершения: ");
                            if (int.TryParse(Console.ReadLine(), out int taskNum) && taskNum > 0 && taskNum <= tasks.Count)
                            {
                                var task = tasks[taskNum - 1];
                                bool result = manager.CompleteDetailProduction(task.Order.Id, task.Detail.Id, machine.Id);
                                if (result)
                                {
                                    Console.WriteLine("Задача завершена! Деталь изготовлена.");

                                    var orderCompletion = task.Order.GetCompletionStatus();
                                    if (orderCompletion.isFullyCompleted)
                                    {
                                        Console.WriteLine($"\n*** ЗАКАЗ #{task.Order.Id} ПОЛНОСТЬЮ ВЫПОЛНЕН! ***");
                                        Console.WriteLine($"Стоимость: {task.Order.CalculateTotalCost()} руб.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Ошибка при завершении задачи.");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Станок не найден или нет задач.");
                        }
                    }
                    break;
            }
        }

        public void RegisterDetailCompletion()
        {
            Console.WriteLine("=== РЕГИСТРАЦИЯ ИЗГОТОВЛЕНИЯ ДЕТАЛИ ===\n");

            Console.Write("Введите номер заказа: ");
            if (!int.TryParse(Console.ReadLine(), out int orderId))
            {
                Console.WriteLine("Некорректный ввод!");
                return;
            }

            Console.Write("Введите ID детали: ");
            if (!int.TryParse(Console.ReadLine(), out int detailId))
            {
                Console.WriteLine("Некорректный ввод!");
                return;
            }

            Console.Write("Введите номер станка: ");
            if (!int.TryParse(Console.ReadLine(), out int machineId))
            {
                Console.WriteLine("Некорректный ввод!");
                return;
            }

            bool result = manager.CompleteDetailProduction(orderId, detailId, machineId);

            if (result)
            {
                Console.WriteLine("Деталь успешно зарегистрирована как изготовленная!");

                var allOrders = manager.GetAllOrders();
                foreach (var order in allOrders)
                {
                    if (order.Id == orderId)
                    {
                        var completion = order.GetCompletionStatus();
                        Console.WriteLine($"Готовность заказа: {completion.completed}/{completion.total}");

                        if (completion.isFullyCompleted)
                        {
                            Console.WriteLine($"\n*** ПОЗДРАВЛЯЕМ! Заказ #{orderId} полностью выполнен! ***");
                            Console.WriteLine($"Общая стоимость: {order.CalculateTotalCost()} руб.");
                            Console.WriteLine("Документы на отгрузку подготовлены.");
                        }
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Ошибка! Проверьте номер заказа, детали и станка.");
            }
        }

        public void PerformMaintenance()
        {
            Console.WriteLine("=== ТЕХНИЧЕСКОЕ ОБСЛУЖИВАНИЕ ===\n");

            var needingMaintenance = manager.GetMachinesNeedingMaintenance();

            if (needingMaintenance.Count == 0)
            {
                Console.WriteLine("Все станки в норме. ТО не требуется.");

                Console.WriteLine("\nТекущее состояние станков:");
                foreach (var machine in manager.GetAllMachines())
                {
                    int percent = (machine.MaxWorkHours > 0)
                        ? (int)((double)machine.CurrentWorkHours / machine.MaxWorkHours * 100)
                        : 0;
                    Console.WriteLine($"  Станок #{machine.Id} {machine.Name}: {machine.CurrentWorkHours}/{machine.MaxWorkHours} ч. ({percent}% ресурса)");
                }
                return;
            }

            Console.WriteLine("Станки, требующие ТО:");
            foreach (var machine in needingMaintenance)
            {
                int percent = (machine.MaxWorkHours > 0)
                    ? (int)((double)machine.CurrentWorkHours / machine.MaxWorkHours * 100)
                    : 0;
                Console.WriteLine($"  Станок #{machine.Id} {machine.Name} - наработка: {machine.CurrentWorkHours}/{machine.MaxWorkHours} ч. ({percent}%)");
            }

            Console.Write("\nВведите номер станка для проведения ТО (0 - отмена): ");
            if (int.TryParse(Console.ReadLine(), out int machineId) && machineId > 0)
            {
                Machine mach = manager.GetMachineById(machineId);
                if (mach != null)
                {
                    mach.PerformMaintenance();
                    Console.WriteLine($"ТО станка #{machineId} '{mach.Name}' выполнено!");
                    Console.WriteLine($"Наработка сброшена. Станок исправен.");
                }
                else
                {
                    Console.WriteLine("Станок не найден!");
                }
            }
        }

        public void ShowProductionReport()
        {
            Console.WriteLine("=== ОТЧЕТ О ПРОИЗВОДСТВЕ ===\n");

            var stats = manager.GetProductionStats();

            Console.WriteLine($"Всего заказов:      {stats.ordersCount}");
            Console.WriteLine($"Выполнено заказов:  {stats.completedOrders}");
            Console.WriteLine($"Выручка за месяц:   {stats.revenue} руб.");
            Console.WriteLine($"Занятых станков:     {stats.busyMachines}/{manager.GetAllMachines().Count}");

            Console.WriteLine("\n--- Состояние станков ---");
            foreach (var machine in manager.GetAllMachines())
            {
                int percent = (machine.MaxWorkHours > 0)
                    ? (int)((double)machine.CurrentWorkHours / machine.MaxWorkHours * 100)
                    : 0;
                string status = machine.IsOperational ? "исправен" : "НЕИСПРАВЕН";
                Console.WriteLine($"  #{machine.Id} {machine.Name}: {status}, задач: {machine.GetTaskCount()}, наработка: {percent}%");
            }

            var needingMaintenance = manager.GetMachinesNeedingMaintenance();
            if (needingMaintenance.Count > 0)
            {
                Console.WriteLine("\nСтанки, требующие ТО:");
                foreach (var machine in needingMaintenance)
                {
                    Console.WriteLine($"  #{machine.Id} {machine.Name}");
                }
            }

            Console.WriteLine("\n--- Активные заказы ---");
            var activeOrders = manager.GetActiveOrders();
            if (activeOrders.Count == 0)
            {
                Console.WriteLine("  Нет активных заказов.");
            }
            else
            {
                foreach (var order in activeOrders)
                {
                    var completion = order.GetCompletionStatus();
                    Console.WriteLine($"  Заказ #{order.Id} ({order.CustomerName}): {completion.completed}/{completion.total} деталей, стоимость: {order.CalculateTotalCost()} руб.");
                }
            }
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
                    Console.WriteLine($"Материал: {detail.Material}");
                    Console.WriteLine($"Габариты: {detail.Dimensions}");
                    Console.WriteLine($"Допуск: {detail.Tolerance} мм");
                    Console.WriteLine($"Сложность: {detail.GetComplexityLevel()}");
                    Console.WriteLine($"Вес: {detail.CalculateWeight()} г");
                    Console.WriteLine($"Рекомендуемый станок: {detail.GetRecommendedMachine()}");

                    Machine machine = manager.FindSuitableMachine(detail);
                    if (machine != null)
                    {
                        Console.WriteLine($"\nПодходящий станок найден: {machine.Name} (#{machine.Id})");
                        Console.WriteLine($"  Тип: {machine.Type}, Точность: {machine.Accuracy} мм");
                        TimeSpan time = machine.CalculateTaskTime(detail, 1);
                        Console.WriteLine($"  Время на 1 деталь: {time.TotalHours:F1} ч.");
                    }
                    else
                    {
                        Console.WriteLine("\nПодходящий станок не найден!");
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