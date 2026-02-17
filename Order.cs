// TODO:
// 1. Реализовать хранение информации о заказе на детали
// 2. Реализовать расчет сроков изготовления
// 3. Реализовать отслеживание статуса заказа

using System;
using System.Collections.Generic;

namespace MillingFactory
{
    public class Order
    {
        public int Id { get; set; }               // Номер заказа
        public string CustomerName { get; set; }  // Заказчик
        public DateTime OrderDate { get; set; }   // Дата получения заказа
        public DateTime? Deadline { get; set; }   // Срок исполнения
        
        // TODO 1: Добавить свойство Status (статус: принят, в производстве, готов, отгружен)
        // TODO 1: Добавить свойство Priority (приоритет: обычный, срочный, сверхсрочный)
        
        private List<OrderItem> items = new List<OrderItem>();
        private decimal totalCost = 0;
        
        public class OrderItem
        {
            public Detail Detail { get; set; }    // Деталь
            public int Quantity { get; set; }     // Количество
            public bool IsCompleted { get; set; } // Изготовлено ли
        }
        
        public Order(int id, string customer, DateTime orderDate, DateTime? deadline = null)
        {
            Id = id;
            CustomerName = customer;
            OrderDate = orderDate;
            Deadline = deadline;
            
            // TODO 1: Установить статус "принят"
            // TODO 1: Установить приоритет "обычный"
        }
        
        // TODO 2: Добавить деталь в заказ
        public void AddDetail(Detail detail, int quantity)
        {
            // Создать новый OrderItem
            // Установить деталь, количество и IsCompleted = false
            // Добавить в список items
            // Пересчитать totalCost
        }
        
        // TODO 2: Рассчитать срок изготовления
        public DateTime CalculateProductionDeadline()
        {
            // Базовое время: 1 день на настройку + время на детали
            // Время на 1 деталь зависит от сложности:
            // - Простая: 0.5 дня
            // - Средняя: 1 день
            // - Сложная: 2 дня
            // Умножить на количество каждой детали
            // Для срочных заказов умножить на 0.7
            // Для сверхсрочных умножить на 0.5
            // Вернуть OrderDate + расчетное время
            return DateTime.Now.AddDays(5);
        }
        
        // TODO 3: Отметить деталь как изготовленную
        public bool MarkDetailComplete(int detailId)
        {
            // Найти OrderItem по ID детали
            // Если найден и еще не изготовлен:
            //   - Установить IsCompleted = true
            //   - Вернуть true
            // Иначе:
            //   - Вернуть false
            return false;
        }
        
        // TODO 3: Проверить готовность заказа
        public (int completed, int total, bool isFullyCompleted) GetCompletionStatus()
        {
            int completed = 0;
            int total = 0;
            
            // Посчитать общее количество деталей
            // Посчитать изготовленные детали
            // Определить завершен ли заказ полностью
            return (0, 0, false);
        }
        
        // TODO 2: Рассчитать стоимость заказа
        public decimal CalculateTotalCost()
        {
            decimal total = 0;
            
            // Пройти по всем items
            // Суммировать: detail.Price * quantity
            // Для срочных заказов +20%
            // Для сверхсрочных +40%
            return total;
        }
        
        public void ShowOrderInfo()
        {
            Console.WriteLine($"Заказ #{Id}");
            Console.WriteLine($"Заказчик: {CustomerName}");
            Console.WriteLine($"Дата заказа: {OrderDate:dd.MM.yyyy}");
            if (Deadline.HasValue)
                Console.WriteLine($"Срок исполнения: {Deadline:dd.MM.yyyy}");
            // TODO 1: Вывести статус и приоритет
            
            var completion = GetCompletionStatus();
            Console.WriteLine($"Готовность: {completion.completed}/{completion.total} деталей");
            
            Console.WriteLine("\nДетали:");
            foreach (var item in items)
            {
                Console.WriteLine($"  {item.Detail.Name} x{item.Quantity} - {item.IsCompleted ? "ГОТОВО" : "В РАБОТЕ"}");
            }
            
            Console.WriteLine($"\nОбщая стоимость: {CalculateTotalCost()} руб.");
            Console.WriteLine($"Расчетный срок готовности: {CalculateProductionDeadline():dd.MM.yyyy}");
        }
    }
}