// TODO:
// 1. Реализовать хранение информации о станках
// 2. Реализовать учет загрузки станков
// 3. Реализовать планирование работ на станках

using System;
using System.Collections.Generic;

namespace MillingFactory
{
    public class Machine
    {
        public int Id { get; set; }               // Номер станка
        public string Name { get; set; }          // Название/модель
        public string Type { get; set; }          // Тип (3-осевой, 5-осевой, универсальный)
        public int MaxWorkHours { get; set; }     // Максимальная наработка до ТО (часы)
        public int CurrentWorkHours { get; set; } // Текущая наработка (часы)
        public bool IsOperational { get; set; }   // Исправен ли
        
        // TODO 1: Добавить свойство Accuracy (точность в мм, например 0.005)
        // TODO 1: Добавить свойство MaxDimension (максимальные габариты обрабатываемой детали)
        
        private List<MachineTask> tasks = new List<MachineTask>(); // Очередь задач
        
        public class MachineTask
        {
            public Order Order { get; set; }      // Заказ
            public Detail Detail { get; set; }    // Деталь
            public int Quantity { get; set; }     // Количество
            public DateTime PlannedStart { get; set; } // Планируемое начало
            public DateTime? ActualStart { get; set; } // Фактическое начало
            public DateTime? CompletionTime { get; set; } // Время завершения
        }
        
        public Machine(int id, string name, string type, int maxHours, decimal accuracy, string maxDimension)
        {
            Id = id;
            Name = name;
            Type = type;
            MaxWorkHours = maxHours;
            CurrentWorkHours = 0;
            IsOperational = true;
            
            // TODO 1: Сохранить точность и максимальные габариты
        }
        
        // TODO 2: Проверить может ли станок изготовить деталь
        public bool CanProduceDetail(Detail detail)
        {
            // Проверить что точность станка >= требуемого допуска детали
            // Проверить что габариты детали <= максимальным габаритам станка
            // Проверить что станок исправен
            // Вернуть true если все условия выполнены
            return false;
        }
        
        // TODO 2: Добавить задачу в очередь
        public void AddTask(Order order, Detail detail, int quantity)
        {
            // Создать новый MachineTask
            // Установить заказ, деталь и количество
            // Рассчитать PlannedStart (текущее время + время на предыдущие задачи)
            // Добавить в список tasks
        }
        
        // TODO 2: Рассчитать время на задачу
        public TimeSpan CalculateTaskTime(Detail detail, int quantity)
        {
            // Базовое время на настройку: 2 часа
            // Время на 1 деталь:
            // - Простая: 1 час
            // - Средняя: 2 часа
            // - Сложная: 4 часа
            // Умножить на количество
            // Для 5-осевых станков умножить на 0.8 (быстрее)
            // Вернуть общее время
            return TimeSpan.Zero;
        }
        
        // TODO 3: Начать выполнение задачи
        public bool StartTask(int taskIndex)
        {
            // Если taskIndex в пределах списка tasks:
            //   - Установить ActualStart = текущее время
            //   - Вернуть true
            // Иначе:
            //   - Вернуть false
            return false;
        }
        
        // TODO 3: Завершить задачу
        public bool CompleteTask(int taskIndex)
        {
            // Если taskIndex в пределах списка tasks:
            //   - Установить CompletionTime = текущее время
            //   - Увеличить CurrentWorkHours на время выполнения задачи
            //   - Проверить是否需要 ТО (если CurrentWorkHours >= MaxWorkHours)
            //   - Удалить задачу из списка (или переместить в архив)
            //   - Вернуть true
            // Иначе:
            //   - Вернуть false
            return false;
        }
        
        // TODO 3: Проверить необходимость ТО
        public bool NeedsMaintenance()
        {
            // Вернуть true если CurrentWorkHours >= MaxWorkHours * 0.9 (90% ресурса)
            return false;
        }
        
        // TODO 1: Выполнить ТО
        public void PerformMaintenance()
        {
            // Сбросить CurrentWorkHours = 0
            // Установить IsOperational = true
        }
        
        public void ShowMachineInfo()
        {
            Console.WriteLine($"Станок #{Id}: {Name}");
            Console.WriteLine($"Тип: {Type}");
            Console.WriteLine($"Исправен: {(IsOperational ? "ДА" : "НЕТ")}");
            Console.WriteLine($"Наработка: {CurrentWorkHours}/{MaxWorkHours} часов");
            // TODO 1: Вывести точность и максимальные габариты
            
            if (NeedsMaintenance())
                Console.WriteLine("ВНИМАНИЕ: Требуется техническое обслуживание!");
            
            Console.WriteLine($"\nЗадач в очереди: {tasks.Count}");
            if (tasks.Count > 0)
            {
                Console.WriteLine("Ближайшие задачи:");
                for (int i = 0; i < Math.Min(tasks.Count, 3); i++)
                {
                    Console.WriteLine($"  {i+1}. {tasks[i].Detail.Name} x{tasks[i].Quantity}");
                }
            }
        }
    }
}