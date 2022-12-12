using FortniteReplayReader;
using FortniteReplayReader.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Unreal.Core.Models.Enums;

namespace ConsoleReader
{
    class Program
    {
        static void Main()
        {
            var serviceCollection = new ServiceCollection()
                .AddLogging(loggingBuilder => loggingBuilder
                    .AddConsole()
                    .SetMinimumLevel(LogLevel.Warning));
            var provider = serviceCollection.BuildServiceProvider();
            var logger = provider.GetService<ILogger<Program>>();

            Stopwatch sw = new();

            double totalTime = 0;
            int count = 0;

            List<double> times = new();

            var reader = new ReplayReader(null, new FortniteReplaySettings
            {
                PlayerLocationType = LocationTypes.None,
            });

            foreach (string replayFile in Directory.GetFiles(@"Replays\"))
            {
                Console.WriteLine(replayFile);

                for (int i = 0; i < 2; i++)
                {
                    ++count;

                    sw.Restart();
                    var replay = reader.ReadReplay(replayFile, ParseType.Full);

                    sw.Stop();

                    var b = replay.GameInformation.Players.OrderBy(x => x.Placement);

                    Console.WriteLine($"Elapsed: {sw.ElapsedMilliseconds}ms. Total Groups Read: {reader?.TotalGroupsRead}. Failed Bunches: {reader?.TotalFailedBunches}. Failed Replicator: {reader?.TotalFailedReplicatorReceives} Null Exports: {reader?.NullHandles} Property Errors: {reader?.PropertyError} Failed Property Reads: {reader?.FailedToRead}");

                    totalTime += sw.Elapsed.TotalMilliseconds;
                    times.Add(sw.Elapsed.TotalMilliseconds);
                }

                Console.WriteLine();
            }

            var fastest5 = times.OrderBy(x => x).Take(10);

            Console.WriteLine($"Total Time: {totalTime}ms. Average: {((double)totalTime / count):0.00}ms");
            Console.WriteLine($"Fastest 10 Time: {fastest5.Sum()}ms. Average: {(fastest5.Sum() / fastest5.Count()):0.00}ms");

            Console.WriteLine("---- done ----");
            Console.ReadLine();
        }
    }
}
