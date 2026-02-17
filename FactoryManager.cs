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
            return null;
        }

        public Machine FindSuitableMachine(Detail detail)
        {
            return null;
        }

        public bool DistributeOrder(Order order)
        {
            return false;
        }

        public bool CompleteDetailProduction(int orderId, int detailId, int machineId)
        {
            return false;
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