// TODO:
// 1. Реализовать хранение деталей, заказов и станков
// 2. Реализовать распределение заказов по станкам
// 3. Реализовать учет производства и загрузки оборудования

using System.Collections.Generic;

namespace MillingFactory
{
    public class FactoryManager
    {
        private List<Detail> details = new List<Detail>();
        private List<Order> orders = new List<Order>();
        private List<Machine> machines = new List<Machine>();
        
        private int nextOrderId = 1000;
        private decimal monthlyRevenue = 0;
        
        // TODO 1: Добавить деталь
        public void AddDetail(Detail detail)
        {
            // Добавить деталь в список details
        }
        
        // TODO 1: Добавить станок
        public void AddMachine(Machine machine)
        {
            // Добавить станок в список machines
        }
        
        // TODO 2: Создать новый заказ
        public Order CreateOrder(string customerName, DateTime? deadline = null)
        {
            // Создать новый Order с nextOrderId
            // Увеличить nextOrderId
            // Добавить заказ в список orders
            // Вернуть созданный заказ
            return null;
        }
        
        // TODO 2: Найти подходящий станок для детали
        public Machine FindSuitableMachine(Detail detail)
        {
            // Пройти по всем исправным станкам (IsOperational == true)
            // Проверить через machine.CanProduceDetail(detail)
            // Выбрать станок с наименьшей загрузкой (наименьшее количество задач)
            // Вернуть найденный станок или null если нет подходящих
            return null;
        }
        
        // TODO 2: Распределить заказ по станкам
        public bool DistributeOrder(Order order)
        {
            // Для каждой детали в заказе:
            //   - Найти подходящий станок через FindSuitableMachine()
            //   - Если станок найден:
            //       Добавить задачу на станок через machine.AddTask()
            //   - Если не найден:
            //       Вернуть false (нельзя выполнить заказ)
            // Вернуть true если все детали распределены
            return false;
        }
        
        // TODO 3: Зафиксировать завершение детали
        public bool CompleteDetailProduction(int orderId, int detailId, int machineId)
        {
            // Найти заказ по orderId
            // Найти станок по machineId
            // Если оба найдены:
            //   - Отметить деталь как изготовленную через order.MarkDetailComplete()
            //   - Завершить задачу на станке
            //   - Если заказ полностью готов:
            //       Увеличить monthlyRevenue на стоимость заказа
            //   - Вернуть true
            // Иначе:
            //   - Вернуть false
            return false;
        }
        
        // TODO 3: Получить статистику производства
        public (int ordersCount, int completedOrders, decimal revenue, int busyMachines) GetProductionStats()
        {
            // Посчитать:
            // - Общее количество заказов
            // - Количество завершенных заказов
            // - Месячную выручку (monthlyRevenue)
            // - Количество занятых станков (с задачами в очереди)
            return (0, 0, 0, 0);
        }
        
        // TODO 3: Проверить станки требующие ТО
        public List<Machine> GetMachinesNeedingMaintenance()
        {
            List<Machine> needingMaintenance = new List<Machine>();
            
            // Пройти по всем станкам
            // Если machine.NeedsMaintenance() == true
            // Добавить в список
            return needingMaintenance;
        }
        
        // Готовые методы:
        public List<Detail> GetAllDetails()
        {
            return details;
        }
        
        public List<Order> GetActiveOrders()
        {
            List<Order> active = new List<Order>();
            foreach (var order in orders)
            {
                var completion = order.GetCompletionStatus();
                if (!completion.isFullyCompleted)
                    active.Add(order);
            }
            return active;
        }
        
        public List<Machine> GetAllMachines()
        {
            return machines;
        }
        
        public Detail GetDetailById(int id)
        {
            foreach (var detail in details)
            {
                if (detail.Id == id)
                    return detail;
            }
            return null;
        }
    }
}