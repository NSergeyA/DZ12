using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace DZ12
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Упражнение 1");
            Thread thread1 = new Thread(NumbersFromOneToTen);
            Thread thread2 = new Thread(NumbersFromOneToTen);
            Thread thread3 = new Thread(NumbersFromOneToTen);

            thread1.Start();
            thread2.Start();
            thread3.Start();

            thread1.Join();
            thread2.Join();
            thread3.Join();
            Console.WriteLine("Для продолжения нажмите любую кнопку...");
            Console.ReadKey();

            Console.WriteLine("Упражнение 2");
            Console.Write("Введите неотрицательное целое число: ");
            if (!int.TryParse(Console.ReadLine(), out int number))
            {
                Console.WriteLine("Неправильное значение");
                return;
            }

            Console.WriteLine("Квадрат числа: " + CalculateSquare(number));
            Console.WriteLine("Ожидание завершения вычисления факториала...");
            Thread.Sleep(8000);

            await CalculateFactorialAsync(number).ContinueWith(task =>
            {
                long factorial = task.Result;
                Console.WriteLine("Факториал числа: " + factorial);
            });
            Console.WriteLine("Для продолжения нажмите любую кнопку...");
            Console.ReadKey();


            Console.WriteLine("Упражнение 3");
            Refl refl = new Refl();

            string[] methodNames = GetMethodNames(refl);
            foreach (var methodName in methodNames)
            {
                Console.WriteLine(methodName);
            }
        }
        static void NumbersFromOneToTen()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(i);
            }
        }
        public static async Task<long> CalculateFactorialAsync(int n)
        {
            return await Task.Run(() =>
            {
                Thread.Sleep(8000);
                long factorial = 1;
                for (int i = 1; i <= n; i++)
                {
                    factorial *= i;
                }
                return factorial;
            });
        }
        public static long CalculateSquare(int n)
        {
            return n * n;
        }
        public static string[] GetMethodNames(object obj)
        {
            Type type = obj.GetType();
            MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance);
            string[] methodNames = new string[methods.Length];

            for (int i = 0; i < methods.Length; i++)
            {
                methodNames[i] = methods[i].Name;
            }

            return methodNames;
        }
    }
}
