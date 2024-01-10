using System;

class PeriodicBoundaryProblem
{
    static void Main()
    {
        int N = 100; // количество узлов сетки
        double h = 2 * Math.PI / N; // шаг сетки
        double[] a = new double[N + 1];
        double[] b = new double[N + 1];
        double[] c = new double[N + 1];
        double[] d = new double[N + 1];
        double[] y = new double[N + 1];

        // Заполнение массивов коэффициентов
        for (int i = 1; i <= N - 1; i++)
        {
            a[i] = 1.0 / (h * h) + 1.0 / h;
            b[i] = -2.0 / (h * h) - 2.0;
            c[i] = 1.0 / (h * h) - 1.0 / h;
            d[i] = Math.Sin(i * h);
        }

        // Учет периодических граничных условий
        a[1] = 1.0 / (h * h) + 1.0 / h;
        c[N - 1] = 1.0 / (h * h) - 1.0 / h;

        // Прямая прогонка
        for (int i = 2; i <= N - 1; i++)
        {
            double m = a[i] / b[i - 1];
            b[i] -= m * c[i - 1];
            d[i] -= m * d[i - 1];
        }

        // Обратная прогонка
        y[N - 1] = d[N - 1] / b[N - 1];
        for (int i = N - 2; i >= 1; i--)
        {
            y[i] = (d[i] - c[i] * y[i + 1]) / b[i];
        }

        // Вывод результатов
        Console.WriteLine("x\t\ty(x)");
        for (int i = 0; i <= N; i++)
        {
            double x = i * h;
            Console.WriteLine($"{x:F3}\t\t{y[i]:F6}");
        }
    }
}

