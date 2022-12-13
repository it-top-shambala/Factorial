namespace Factorial.Lib;

public static class Calc
{
    private static long Factorial(int number, CancellationToken cancellationToken, IProgress<int> progress = null)
    {
        long factorial = 1;
        for (var i = 1; i <= number; i++)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                cancellationToken.ThrowIfCancellationRequested();
            }

            factorial *= i;
            Thread.Sleep(1000);
            progress?.Report(i);
        }

        return factorial;
    }

    public static async Task<long> FactorialAsync(int number, CancellationToken cancellationToken,
        IProgress<int> progress = null)
    {
        return await Task.Run(
            () => Factorial(number, cancellationToken, progress),
            cancellationToken
        );
    }
}
