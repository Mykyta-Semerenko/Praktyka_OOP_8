
using System;
using System.Threading;

class Program
{
    static double[] numbers = new double[10];

    static double sum = 0;
    static double product = 1;

    static void Main()
    {
        Thread T1 = new Thread(() => FillArray(0));
        Thread T0 = new Thread(() => FillArray(1));

        T1.Name = "T1";
        T0.Name = "T0";

        T1.Start();
        T0.Start();

        T1.Join();
        T0.Join();

        Console.WriteLine("Заповнений масив:");

        for (int i = 0; i < numbers.Length; i++)
        {
            Console.WriteLine($"[{i}] = {numbers[i]:F2}");
        }

        Thread T0_Calc = new Thread(FindSum);
        Thread T1_Calc = new Thread(FindProduct);

        T0_Calc.Name = "T0";
        T1_Calc.Name = "T1";

        T0_Calc.Start();
        T1_Calc.Start();

        T0_Calc.Join();
        T1_Calc.Join();

        Console.WriteLine("\nРезультати обчислень:");

        Console.WriteLine($"T0: Сума чисел більших за 15 = {sum:F2}");
        Console.WriteLine($"T1: Добуток чисел менших за 10 = {product:F2}");

        Console.WriteLine("\nРоботу потоків завершено.");
    }

    static void FillArray(int startIndex)
    {
        for (int i = startIndex; i < numbers.Length; i += 2)
        {
            numbers[i] = 0.01 + Random.Shared.NextDouble() * 49.98;
        }
    }

    static void FindSum()
    {
        sum = 0;

        for (int i = 0; i < numbers.Length; i++)
        {
            if (numbers[i] > 15)
            {
                sum += numbers[i];
            }
        }
    }

    static void FindProduct()
    {
        product = 1;

        for (int i = 0; i < numbers.Length; i++)
        {
            if (numbers[i] < 10)
            {
                product *= numbers[i];
            }
        }
    }
}
