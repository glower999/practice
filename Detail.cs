// TODO:
// 1. Добавить поля для технических характеристик детали
// 2. Реализовать проверку корректности данных (размеры, материал)
// 3. Реализовать метод расчета веса детали

namespace MillingFactory
{
    public class Detail
    {
        public int Id { get; set; }               // Код детали
        public string Name { get; set; }          // Название
        public string DrawingNumber { get; set; } // Номер чертежа
        public decimal Price { get; set; }        // Цена за штуку
        
        // TODO 1: Добавить свойство Material (материал: сталь, алюминий, латунь, титан)
        // TODO 1: Добавить свойство Dimensions (габариты в мм: "100x50x20")
        // TODO 1: Добавить свойство Tolerance (допуск в мм, например 0.01)
        
        public Detail(int id, string name, string drawingNumber, decimal price, 
                     string material, string dimensions, decimal tolerance)
        {
            Id = id;
            Name = name;
            DrawingNumber = drawingNumber;
            Price = price;
            
            // TODO 2: Проверить что цена не отрицательная
            // Если цена < 0, установить минимальную цену 100
            
            // TODO 2: Проверить что допуск в разумных пределах (0.001 - 0.1 мм)
            // Если не так - установить 0.01
            
            // TODO 1: Сохранить материал, габариты и допуск
        }
        
        // TODO 3: Рассчитать примерный вес
        public decimal CalculateWeight()
        {
            // Рассчитать вес на основе материала и габаритов
            // Плотности материалов (г/см³):
            // - Сталь: 7.85
            // - Алюминий: 2.7
            // - Латунь: 8.5
            // - Титан: 4.5
            // Разобрать Dimensions на отдельные размеры
            // Рассчитать объем и умножить на плотность
            // Вернуть вес в граммах
            return 0;
        }
        
        // TODO 1: Проверить сложность изготовления
        public string GetComplexityLevel()
        {
            // Определить сложность по допуску:
            // - > 0.05 мм: "простая"
            // - 0.01-0.05 мм: "средняя"
            // - < 0.01 мм: "сложная"
            return "";
        }
        
        // TODO 1: Получить рекомендуемый станок
        public string GetRecommendedMachine()
        {
            // В зависимости от материала и сложности:
            // - Сталь + сложная: 5-осевой фрезерный станок
            // - Алюминий: 3-осевой фрезерный станок
            // - Простая деталь: универсальный фрезерный станок
            return "Универсальный фрезерный станок";
        }
        
        public override string ToString()
        {
            return $"{Name} (чертеж {DrawingNumber}) - {Price} руб.";
        }
    }
}