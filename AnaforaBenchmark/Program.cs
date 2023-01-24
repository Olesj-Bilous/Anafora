using AnaforaBenchmark.Dynamics;
using BenchmarkDotNet.Running;

namespace AnaforaBenchmark
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<GetAll>();
        }
    }
}