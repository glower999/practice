using System;
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

        public void AddDetail(Detail detail)
        {
            details.Add(detail);
        }

        public void AddMachine(Machine machine)
        {
            machines.Add(machine);
        }

        public Order CreateOrder(string customerName, DateTime? deadline = null)
        {
            Order order = new Order(nextOrderId, customerName, DateTime.Now, deadline);
            nextOrderId++;
            orders.Add(order);
            return order;
        }

        public Machine FindSuitableMachine(Detail detail)
        {
            Machine bestMachine = null;
            int minTasks = int.MaxValue;

            foreach (var machine in machines)
            {
                if (machine.IsOperational && machine.CanProduceDetail(detail))
                {
                    int taskCount = machine.GetTaskCount();
                    if (taskCount < minTasks)
                    {
                        minTasks = taskCount;
                        bestMachine = machine;
                    }
                }
            }

            return bestMachine;
        }

        public bool DistributeOrder(Order order)
        {
            var items = order.GetItems();

            foreach (var item in items)
            {
                Machine machine = FindSuitableMachine(item.Detail);
                if (machine == null)
                {
                    return false;
                }
                machine.AddTask(order, item.Detail, item.Quantity);
            }

            order.Status = "в производстве";
            return true;
        }

        public bool CompleteDetailProduction(int orderId, int detailId, int machineId)
        {
            Order targetOrder = null;
            foreach (var order in orders)
            {
                if (order.Id == orderId)
                {
                    targetOrder = order;
                    break;
                }
            }

            Machine targetMachine = null;
            foreach (var machine in machines)
            {
                if (machine.Id == machineId)
                {
                    targetMachine = machine;
                    break;
                }
            }

            if (targetOrder == null || targetMachine == null)
                return false;

            bool detailMarked = targetOrder.MarkDetailComplete(detailId);
            if (!detailMarked)
                return false;

            var machineTasks = targetMachine.GetTasks();
            for (int i = 0; i < machineTasks.Count; i++)
            {
                if (machineTasks[i].Order.Id == orderId && machineTasks[i].Detail.Id == detailId)
                {
                    targetMachine.CompleteTask(i);
                    break;
                }
            }

            var completion = targetOrder.GetCompletionStatus();
            if (completion.isFullyCompleted)
            {
                targetOrder.Status = "готов";
                monthlyRevenue += targetOrder.CalculateTotalCost();
            }

            return true;
        }

        public (int ordersCount, int completedOrders, decimal revenue, int busyMachines) GetProductionStats()
        {
            return (0, 0, 0, 0);
        }

        public List<Machine> GetMachinesNeedingMaintenance()
        {
            List<Machine> needingMaintenance = new List<Machine>();
            return needingMaintenance;
        }

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

        public List<Order> GetAllOrders()
        {
            return orders;
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

        public Machine GetMachineById(int id)
        {
            foreach (var machine in machines)
            {
                if (machine.Id == id)
                    return machine;
            }
            return null;
        }
    }
}