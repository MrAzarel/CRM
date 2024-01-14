using System;

class CyclicReductionMethod
{
    static void Main()
    {
        int N = 5; // количество узлов сетки
        double h = 2 * Math.PI / N; // шаг сетки
        double[] a = new double[N + 1];
        double[] b = new double[N + 1];
        double[] c = new double[N + 1];
        double[] d = new double[N + 1];

        double[] y = new double[N + 1];

        double[] alpha = new double[N + 1];
        double[] betta = new double[N + 1];
        double[] gamma = new double[N + 1];

        double[] u = new double[N + 1];
        double[] v = new double[N + 1];

        bool isCoefficientsCorrect = true;

        // Заполнение массивов коэффициентов
        for (int i = 1; i <= N; i++)
        {
            a[i] = 1 + h;
            b[i] = 1 - h;
            c[i] = 2 - 2 * h * h;
            d[i] = -h * h * Math.Sin(i * h);
        }

        if (isCoefficientsCorrect)
        {
            // Учет периодических граничных условий
            a[1] = 1 + h;
            b[N - 1] = 1 - h;

            // Прямая прогонка
            alpha[2] = b[1] / c[1];
            betta[2] = d[1] / c[1];
            gamma[2] = a[1] / c[1];

            for (int i = 3; i <= N; i++)
            {
                alpha[i] = b[i - 1] / (c[i - 1] - a[i - 1] * alpha[i - 1]);
            }

            // Проверка
            for (int i = 1; i < N; i++)
            {
                if (Math.Abs(c[i]) - Math.Abs(a[i]) * Math.Abs(alpha[i]) > 0)
                {
                    Console.WriteLine("Коэффициенты системы удовлетворяют условиям.");
                }
                else
                {
                    Console.WriteLine("Коэффициенты системы неверны.");
                    isCoefficientsCorrect = false;
                    break;
                }
            }

            for (int i = 3; i <= N; i++)
            {
                betta[i] = (d[i - 1] + a[i - 1] * betta[i - 1]) / (c[i - 1] - a[i - 1] * alpha[i - 1]);
                gamma[i] = (a[i - 1] * gamma[i - 1]) / (c[i - 1] - a[i - 1] * alpha[i - 1]);
            }

            // Обратная прогонка
            u[N - 1] = betta[N];
            v[N - 1] = alpha[N] + gamma[N];
            for (int i = N - 2; i >= 1; i--)
            {
                u[i] = alpha[i + 1] * u[i + 1] + betta[i + 1];
                v[i] = alpha[i + 1] * v[i + 1] + gamma[i + 1];
            }

            // Вывод результатов
            double x;
            Console.WriteLine("x\t\ty(x)");
            y[0] = (betta[2] + alpha[2] * u[1]) / (1 - gamma[2] - alpha[2] * v[1]);
            Console.WriteLine($"0\t\t{y[0]:F6}");
            for (int i = 1; i <= N - 1; i++)
            {
                x = i * h;
                y[i] = u[i] + y[0] * v[i];
                Console.WriteLine($"{x:F6}\t{y[i]:F6}");
            }
        }
    }
}

