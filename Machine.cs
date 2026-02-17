using System;
using System.Collections.Generic;

namespace MillingFactory
{
    public class Machine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int MaxWorkHours { get; set; }
        public int CurrentWorkHours { get; set; }
        public bool IsOperational { get; set; }
        public decimal Accuracy { get; set; }
        public string MaxDimension { get; set; }

        private List<MachineTask> tasks = new List<MachineTask>();

        public class MachineTask
        {
            public Order Order { get; set; }
            public Detail Detail { get; set; }
            public int Quantity { get; set; }
            public DateTime PlannedStart { get; set; }
            public DateTime? ActualStart { get; set; }
            public DateTime? CompletionTime { get; set; }
        }

        public Machine(int id, string name, string type, int maxHours, decimal accuracy, string maxDimension)
        {
            Id = id;
            Name = name;
            Type = type;
            MaxWorkHours = maxHours;
            CurrentWorkHours = 0;
            IsOperational = true;
            Accuracy = accuracy;
            MaxDimension = maxDimension;
        }

        private int[] ParseDimensions(string dimensions)
        {
            try
            {
                string[] parts = dimensions.Split('x');
                if (parts.Length == 3)
                {
                    return new int[]
                    {
                        int.Parse(parts[0]),
                        int.Parse(parts[1]),
                        int.Parse(parts[2])
                    };
                }
            }
            catch { }
            return new int[] { 0, 0, 0 };
        }

        public bool CanProduceDetail(Detail detail)
        {
            if (!IsOperational)
                return false;

            if (Accuracy > detail.Tolerance)
                return false;

            int[] detailDims = ParseDimensions(detail.Dimensions);
            int[] machineDims = ParseDimensions(MaxDimension);

            for (int i = 0; i < 3; i++)
            {
                if (detailDims[i] > machineDims[i])
                    return false;
            }

            return true;
        }

        public void AddTask(Order order, Detail detail, int quantity)
        {
        }

        public TimeSpan CalculateTaskTime(Detail detail, int quantity)
        {
            return TimeSpan.Zero;
        }

        public bool StartTask(int taskIndex)
        {
            return false;
        }

        public bool CompleteTask(int taskIndex)
        {
            return false;
        }

        public bool NeedsMaintenance()
        {
            return CurrentWorkHours >= (int)(MaxWorkHours * 0.9);
        }

        public void PerformMaintenance()
        {
            CurrentWorkHours = 0;
            IsOperational = true;
        }

        public int GetTaskCount()
        {
            return tasks.Count;
        }

        public List<MachineTask> GetTasks()
        {
            return tasks;
        }

        public void ShowMachineInfo()
        {
            Console.WriteLine($"Станок #{Id}: {Name}");
            Console.WriteLine($"Тип: {Type}");
            Console.WriteLine($"Точность: {Accuracy} мм");
            Console.WriteLine($"Макс. габариты: {MaxDimension}");
            Console.WriteLine($"Исправен: {(IsOperational ? "ДА" : "НЕТ")}");
            Console.WriteLine($"Наработка: {CurrentWorkHours}/{MaxWorkHours} часов");

            if (NeedsMaintenance())
                Console.WriteLine("ВНИМАНИЕ: Требуется техническое обслуживание!");

            Console.WriteLine($"\nЗадач в очереди: {tasks.Count}");
            if (tasks.Count > 0)
            {
                Console.WriteLine("Ближайшие задачи:");
                for (int i = 0; i < Math.Min(tasks.Count, 3); i++)
                {
                    Console.WriteLine($"  {i + 1}. {tasks[i].Detail.Name} x{tasks[i].Quantity}");
                }
            }
        }
    }
}