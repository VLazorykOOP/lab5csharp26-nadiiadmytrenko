using System;
using System.Collections.Generic;
using System.Linq;

namespace WarehouseAccounting
{
    // Абстрактний базовий клас
    public abstract class ProductBase
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        protected ProductBase(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        // Абстрактні методи для реалізації в похідних класах
        public abstract void DisplayInfo();
        public abstract bool IsExpired(DateTime currentDate);
    }

    // Клас Продукт
    public class IndividualProduct : ProductBase
    {
        public DateTime ProductionDate { get; set; }
        public int ShelfLifeDays { get; set; }

        public IndividualProduct(string name, decimal price, DateTime prodDate, int shelfLife)
            : base(name, price)
        {
            ProductionDate = prodDate;
            ShelfLifeDays = shelfLife;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"[Продукт] Назва: {Name}, Ціна: {Price} грн, Дата вир-ва: {ProductionDate.ToShortDateString()}, Термін придатності: {ShelfLifeDays} днів");
        }

        public override bool IsExpired(DateTime currentDate)
        {
            return currentDate > ProductionDate.AddDays(ShelfLifeDays);
        }
    }

    // Клас Партія
    public class Batch : ProductBase
    {
        public int Quantity { get; set; }
        public DateTime ProductionDate { get; set; }
        public int ShelfLifeDays { get; set; }

        public Batch(string name, decimal price, int quantity, DateTime prodDate, int shelfLife)
            : base(name, price)
        {
            Quantity = quantity;
            ProductionDate = prodDate;
            ShelfLifeDays = shelfLife;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"[Партія] Назва: {Name}, Ціна за од.: {Price} грн, К-сть: {Quantity}, Дата вир-ва: {ProductionDate.ToShortDateString()}, Термін: {ShelfLifeDays} днів");
        }

        public override bool IsExpired(DateTime currentDate)
        {
            return currentDate > ProductionDate.AddDays(ShelfLifeDays);
        }
    }

    // Клас Комплект
    public class Set : ProductBase
    {
        public List<string> ProductsList { get; set; }

        public Set(string name, decimal price, List<string> products)
            : base(name, price)
        {
            ProductsList = products;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"[Комплект] Назва: {Name}, Загальна ціна: {Price} грн, Склад: {string.Join(", ", ProductsList)}");
        }

        public override bool IsExpired(DateTime currentDate)
        {
            // Для комплекту логіка може бути різною, припустимо, комплект не має терміну придатності 
            // або він завжди придатний, якщо не вказано інше.
            return false;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            DateTime today = DateTime.Now;

            // Створення масиву (бази) товарів
            List<ProductBase> warehouse = new List<ProductBase>
            {
                new IndividualProduct("Молоко", 35.50m, new DateTime(2026, 3, 30), 7),
                new IndividualProduct("Хліб", 18.00m, new DateTime(2026, 4, 4), 3),
                new Batch("Йогурт", 25.00m, 50, new DateTime(2026, 3, 20), 14),
                new Batch("Печиво", 45.00m, 100, new DateTime(2026, 1, 10), 180),
                new Set("Подарунковий набір", 500.00m, new List<string> { "Шоколад", "Кава", "Чай" })
            };

            Console.WriteLine($"Поточна дата: {today.ToShortDateString()}\n");

            // Виведення повної інформації
            Console.WriteLine(" Всі товари в базі ");
            foreach (var item in warehouse)
            {
                item.DisplayInfo();
            }

            // Пошук прострочених товарів
            Console.WriteLine("\n Список прострочених товарів ");
            bool foundExpired = false;
            foreach (var item in warehouse)
            {
                if (item.IsExpired(today))
                {
                    item.DisplayInfo();
                    foundExpired = true;
                }
            }

            if (!foundExpired)
            {
                Console.WriteLine("Прострочених товарів не знайдено.");
            }

            Console.ReadKey();
        }
    }
}