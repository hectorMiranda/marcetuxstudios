// async/await: fetch several URLs concurrently without blocking threads.
// Build against .NET 4.5.  csc AsyncDownloads.cs
//
// The point: WhenAll lets the I/O overlap; await frees the thread while waiting.
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

class AsyncDownloads
{
    static async Task<int> SizeOf(HttpClient http, string url)
    {
        // await releases this thread until the response comes back.
        string body = await http.GetStringAsync(url);
        Console.WriteLine("{0,-40} {1,8:N0} bytes", url, body.Length);
        return body.Length;
    }

    static async Task RunAsync(string[] urls)
    {
        using (var http = new HttpClient())
        {
            // Kick them all off, then await the whole set — the waits overlap.
            Task<int>[] tasks = Array.ConvertAll(urls, u => SizeOf(http, u));
            int[] sizes = await Task.WhenAll(tasks);
            Console.WriteLine("total: {0:N0} bytes across {1} urls", Sum(sizes), urls.Length);
        }
    }

    static int Sum(int[] xs) { int s = 0; foreach (var x in xs) s += x; return s; }

    static void Main(string[] args)
    {
        var urls = args.Length > 0 ? args : new[]
        {
            "http://example.com/", "http://www.iana.org/", "http://httpbin.org/get"
        };
        var sw = Stopwatch.StartNew();
        RunAsync(urls).GetAwaiter().GetResult();   // bridge to a console Main
        Console.WriteLine("elapsed: {0} ms", sw.ElapsedMilliseconds);
    }
}
