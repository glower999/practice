using System;
using System.Collections.Generic;

namespace MillingFactory
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? Deadline { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }

        private List<OrderItem> items = new List<OrderItem>();
        private decimal totalCost = 0;

        public class OrderItem
        {
            public Detail Detail { get; set; }
            public int Quantity { get; set; }
            public bool IsCompleted { get; set; }
        }

        public Order(int id, string customer, DateTime orderDate, DateTime? deadline = null)
        {
            Id = id;
            CustomerName = customer;
            OrderDate = orderDate;
            Deadline = deadline;
            Status = "принят";
            Priority = "обычный";
        }

        public void AddDetail(Detail detail, int quantity)
        {
        }

        public DateTime CalculateProductionDeadline()
        {
            return DateTime.Now.AddDays(5);
        }

        public bool MarkDetailComplete(int detailId)
        {
            return false;
        }

        public (int completed, int total, bool isFullyCompleted) GetCompletionStatus()
        {
            return (0, 0, false);
        }

        public decimal CalculateTotalCost()
        {
            return 0;
        }

        public List<OrderItem> GetItems()
        {
            return items;
        }

        public void ShowOrderInfo()
        {
            Console.WriteLine($"Заказ #{Id}");
            Console.WriteLine($"Заказчик: {CustomerName}");
            Console.WriteLine($"Дата заказа: {OrderDate:dd.MM.yyyy}");
            if (Deadline.HasValue)
                Console.WriteLine($"Срок исполнения: {Deadline:dd.MM.yyyy}");
            Console.WriteLine($"Статус: {Status}");
            Console.WriteLine($"Приоритет: {Priority}");

            var completion = GetCompletionStatus();
            Console.WriteLine($"Готовность: {completion.completed}/{completion.total} деталей");

            Console.WriteLine("\nДетали:");
            foreach (var item in items)
            {
                Console.WriteLine($"  {item.Detail.Name} x{item.Quantity} - {(item.IsCompleted ? "ГОТОВО" : "В РАБОТЕ")}");
            }

            Console.WriteLine($"\nОбщая стоимость: {CalculateTotalCost()} руб.");
            Console.WriteLine($"Расчетный срок готовности: {CalculateProductionDeadline():dd.MM.yyyy}");
        }
    }
}