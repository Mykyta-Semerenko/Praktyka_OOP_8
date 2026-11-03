using System;
using System.Threading;

class Program
{
    static double[] numbers = new double[10];

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

        Console.WriteLine("\nРоботу потоків завершено.");
    }
    
    static void FillArray(int startIndex)
    {
        for (int i = startIndex; i < numbers.Length; i += 2)
        {
            numbers[i] = 0.01 + Random.Shared.NextDouble() * 49.98;
        }
    }
}