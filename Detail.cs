using System;

namespace MillingFactory
{
    public class Detail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DrawingNumber { get; set; }
        public decimal Price { get; set; }
        public string Material { get; set; }
        public string Dimensions { get; set; }
        public decimal Tolerance { get; set; }

        public Detail(int id, string name, string drawingNumber, decimal price,
                     string material, string dimensions, decimal tolerance)
        {
            Id = id;
            Name = name;
            DrawingNumber = drawingNumber;

            if (price < 0)
                Price = 100;
            else
                Price = price;

            if (tolerance < 0.001m || tolerance > 0.1m)
                Tolerance = 0.01m;
            else
                Tolerance = tolerance;

            Material = material;
            Dimensions = dimensions;
        }

        public string GetComplexityLevel()
        {
            if (Tolerance > 0.05m)
                return "простая";
            else if (Tolerance >= 0.01m)
                return "средняя";
            else
                return "сложная";
        }

        public string GetRecommendedMachine()
        {
            string complexity = GetComplexityLevel();

            if (Material == "сталь" && complexity == "сложная")
                return "5-осевой фрезерный станок";
            else if (Material == "алюминий")
                return "3-осевой фрезерный станок";
            else if (complexity == "простая")
                return "Универсальный фрезерный станок";
            else
                return "3-осевой фрезерный станок";
        }

        public decimal CalculateWeight()
        {
            return 0;
        }

        public override string ToString()
        {
            return $"{Name} (чертеж {DrawingNumber}) - {Price} руб.";
        }
    }
}