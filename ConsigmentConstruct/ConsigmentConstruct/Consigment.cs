using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsigmentConstruct
{
    public struct Consigment
    {
        public int Quantity;
        public double Price;
        
        public readonly double Cost;

        public Consigment(int quantity, double price)
        {
            if (quantity <= 0 || price <=0)
            {
                throw new Exception("Вводимые значения должны быть положительными");
            }

            Quantity = quantity;
            Price = Math.Round(price,2);
            Cost = Price*Quantity;

        }
        public static Consigment Read()
        {
            int quantity;
            double price;
            

            while (true)
            {
                Console.WriteLine("Введите кол-во товара:");
                quantity = int.Parse(Console.ReadLine());

                Console.WriteLine("Введите цену за 1 товар:");
                price = Math.Round(double.Parse(Console.ReadLine()),2);



                if (quantity <= 0 || price <= 0)
                {
                    throw new Exception("Вводимые значения должны быть положительными");
                }
                else
                {
                    break;
                }
            }

            return new Consigment(quantity, price);
        }

        public override string ToString()
        {
            return $"{Quantity} шт. по {Price} руб.";
        }

        public override bool Equals(object obj)
        {
            if (obj is Consigment)
            {
                Consigment p = (Consigment)obj;

                return Cost * Price == p.Cost * p.Price;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return Price.GetHashCode() * Quantity.GetHashCode();
        }
        public void Display()
        {
            Console.WriteLine($"Структура Consigment, {Quantity} штук по {Price} рублей");
        }

        public static Consigment operator +(Consigment c1, Consigment c2)
        {
            if (c1.Price != c2.Price)
            {
                throw new Exception("Цена за 1 товар должна совпадать");
            }
            else
            {
                int newQuantity = c1.Quantity + c2.Quantity;
                return new Consigment(newQuantity, c1.Price);
            }
        }

        public static Consigment operator -(Consigment c1, Consigment c2)
        {
            if (c1.Price != c2.Price)
            {
                throw new Exception("Цена за 1 товар должна совпадать");
            }
            else if (c1.Quantity<c2.Quantity)
            {
                throw new Exception("Кол-во первого товара не должно быть меньше кол-ва второго товара");
            }
            else
            {
                int newQuantity = c1.Quantity - c2.Quantity;
                return new Consigment(newQuantity, c1.Price);
            }

        }

    }
}
