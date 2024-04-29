using System.Text.Json;
using OtusSerializator.Entities;
using OtusSerializator.Utils;

public class Program
{
    public static long TimesRepeat = 10000000;
    public static string QuitKey = "Q";

    public static void Main(string[] args)
    {
        var program = new Program();
        program.Run();  
    }

    public void Run()
    {
        PrintHeaderAndVariants();
        string inputVal = Ask();
        do
        {
            if (IsQuit(inputVal))
            {
                break;
            }

            int action = int.TryParse(inputVal, out action) ? action : -1;

            switch (action)
            {
                case 1:
                    BenchForFClass(TimesRepeat);
                    break;
                case 2:
                    BenchForFClassWithSystemTextJson(TimesRepeat);
                    break;
            }

            Console.WriteLine("=================================================");
            inputVal = Ask();
        } while (true);
    }
    
    private void PrintHeaderAndVariants()
    {
        Console.WriteLine("Benchmark:");
        Console.WriteLine($"{QuitKey}: Quit");
        Console.WriteLine("1: Bench for FClass with custom serializer");
        Console.WriteLine("2: Bench for FClass with default System.Text.Json");
        Console.WriteLine("=================================================");
    }

    private string Ask()
    {
        Console.Write("Input:");
        return Console.ReadLine();
    }

    private bool IsQuit(string value)
    {
        return string.Equals(value, QuitKey, StringComparison.InvariantCultureIgnoreCase);
    }

    public void BenchForFClass(long times)
    {
        string csvData = string.Empty;
        F deserializedObject = null;

        Benchmark.BenchMs(times, () => csvData = CommaSeparatedValueSerializator.Serialize(F.CreateTestObject()), out long avarageTimeMs);

        Benchmark.BenchMs(times, () => deserializedObject = CommaSeparatedValueSerializator.Deserialize<F>(csvData), out long avarageTimeMsDeserialize);

        Benchmark.BenchOnceMs(() => Console.WriteLine($"({times}): Serialized data: {csvData}; Avarage ~ {avarageTimeMs} ms;"), out long avarageTimeMsConsole);
        Console.WriteLine($"({times}): Deserialize obj=> {deserializedObject}; Avarage ~ {avarageTimeMsDeserialize} ms");

        Console.WriteLine($"Time to print to console: ~ {avarageTimeMsConsole} ms");
    }

    public void BenchForFClassWithSystemTextJson(long times)
    {
        string csvData = string.Empty;

        Benchmark.BenchMs(times, () => csvData = JsonSerializer.Serialize(F.CreateTestObject()), out long avarageTimeMs);
        Console.WriteLine($"({times}): Serialized data: {csvData}; Avarage ~ {avarageTimeMs} ms;");
    }
}






