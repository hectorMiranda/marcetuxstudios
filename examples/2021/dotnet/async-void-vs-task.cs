// async void swallows exceptions — the caller has no way to observe them.
// async Task propagates them so the caller can catch or let them surface correctly.
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        // BAD: exception escapes to thread pool, not caught here
        try
        {
            BadFire();        // returns void — nothing to await
            await Task.Delay(50);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Caught: {ex.Message}");  // never runs
        }

        // GOOD: exception propagates through the Task
        try
        {
            await GoodFire();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Caught: {ex.Message}");  // runs as expected
        }
    }

    static async void BadFire()
    {
        await Task.Delay(10);
        throw new InvalidOperationException("from async void — nobody catches this");
    }

    static async Task GoodFire()
    {
        await Task.Delay(10);
        throw new InvalidOperationException("from async Task — caller can catch this");
    }
}
