using System.Diagnostics;

public enum BenchmarkUnit {Ms, Ticks}

public static class Benchmark
{
    public static void Bench(long times, Action action, out long avarageTimeMs, BenchmarkUnit benchmarkUnit)
    {
        avarageTimeMs = 0;

        long minMs = 0;
        long maxMs = 0;

        Stopwatch stopwatch = new Stopwatch();

        for (int i = 0; i < times; i++)
        {
            stopwatch.Reset();
            
            stopwatch.Start();
            action?.Invoke();
            stopwatch.Stop();
                
            long elapsed = GetElapsedByBenchmarUnit(stopwatch, benchmarkUnit);

            if ((maxMs == 0) && (minMs == 0))
            {
                maxMs = elapsed;
                minMs = elapsed;
                continue;
            }
                
            minMs = Math.Min(minMs, elapsed);
            maxMs = Math.Max(maxMs, elapsed);   
        }
        
        avarageTimeMs = (maxMs + minMs) / 2;
    }

    private static long GetElapsedByBenchmarUnit(Stopwatch stopwatch, BenchmarkUnit benchmarkUnit){
        switch (benchmarkUnit){
            case BenchmarkUnit.Ticks : return stopwatch.ElapsedTicks;
            default: 
              return stopwatch.ElapsedMilliseconds;
        }
    }

    public static void BenchMs(long times, Action action, out long avarageTimeMs){
        Bench(times, action, out avarageTimeMs, BenchmarkUnit.Ms);
    }
    
    public static void BenchOnceMs(Action action, out long avarageTimeMs){
        BenchMs(1, action, out avarageTimeMs);
    }
}