using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Factorial.Lib;

namespace Factorial.App;

public class MainWindowModel : WindowModelBase
{
    private int _number;

    public int Number
    {
        get => _number;
        set
        {
            SetField(ref _number, value);
            CommandCalcFactorial.OnCanExecuteChanged();
        }
    }

    private long _factorial;

    public long Factorial
    {
        get => _factorial;
        set => SetField(ref _factorial, value);
    }

    private int _progress;

    public int Progress
    {
        get => _progress;
        set => SetField(ref _progress, value);
    }

    public LambdaCommand CommandCalcFactorial { get; set; }
    public LambdaCommand CommandCancel { get; set; }

    private CancellationTokenSource _cts;

    public MainWindowModel()
    {
        _number = 0;
        _factorial = 0;

        CommandCalcFactorial = new LambdaCommand(async _ => await FactorialCalcAsync());
        CommandCancel = new LambdaCommand(_ => _cts.Cancel());
    }

    private async Task FactorialCalcAsync()
    {
        var progress = new Progress<int>();
        progress.ProgressChanged += (_, progress) =>
        {
            Progress = progress;
        };

        _cts = new CancellationTokenSource();
        _cts.Token.Register(() => MessageBox.Show("Operation canceled"));

        Factorial = await Calc.FactorialAsync(Number, _cts.Token, progress);
    }
}
