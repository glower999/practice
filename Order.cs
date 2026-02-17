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
            OrderItem item = new OrderItem();
            item.Detail = detail;
            item.Quantity = quantity;
            item.IsCompleted = false;
            items.Add(item);

            totalCost = CalculateTotalCost();
        }

        public DateTime CalculateProductionDeadline()
        {
            double totalDays = 1;

            foreach (var item in items)
            {
                double daysPerDetail;
                string complexity = item.Detail.GetComplexityLevel();

                switch (complexity)
                {
                    case "простая": daysPerDetail = 0.5; break;
                    case "средняя": daysPerDetail = 1.0; break;
                    case "сложная": daysPerDetail = 2.0; break;
                    default: daysPerDetail = 1.0; break;
                }

                totalDays += daysPerDetail * item.Quantity;
            }

            if (Priority == "срочный")
                totalDays *= 0.7;
            else if (Priority == "сверхсрочный")
                totalDays *= 0.5;

            return OrderDate.AddDays(totalDays);
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
            decimal total = 0;

            foreach (var item in items)
            {
                total += item.Detail.Price * item.Quantity;
            }

            if (Priority == "срочный")
                total *= 1.20m;
            else if (Priority == "сверхсрочный")
                total *= 1.40m;

            return total;
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