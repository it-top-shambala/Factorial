using System;
using System.Windows.Input;

namespace Factorial.App;

public class LambdaCommand : ICommand
{
    private readonly Predicate<object?>? _canExecute;
    private readonly Action<object?> _execute;

    public LambdaCommand(Action<object?> execute, Predicate<object?>? canExecute = null)
    {
        _canExecute = canExecute ?? (_ => true);
        _execute = execute;
    }

    public bool CanExecute(object? parameter)
    {
        return _canExecute.Invoke(parameter);
    }

    public void Execute(object? parameter)
    {
        _execute.Invoke(parameter);
    }

    public event EventHandler? CanExecuteChanged;

    public void OnCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
