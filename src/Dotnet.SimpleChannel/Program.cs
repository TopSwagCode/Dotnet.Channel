using System;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Dotnet.SimpleChannel
{
    class Program
    {

        static async Task Main(string[] args)
        {
            await Example0();
            await Example1();
            await Example2();
            await Example3();
            await Example4();
        }

        private static async Task Example0()
        {
            var myChannel = Channel.CreateUnbounded<int>();

            for (int i = 0; i < 10; i++)
            {
                await myChannel.Writer.WriteAsync(i);
            }

            while (true)
            {
                var item = await myChannel.Reader.ReadAsync();
                Console.WriteLine(item);
            }
        }

        private static async Task Example1()
        {
            var myChannel = Channel.CreateUnbounded<int>();

            _ = Task.Factory.StartNew(async () =>
            {
                for (int i = 0; i < 10; i++)
                {
                    await myChannel.Writer.WriteAsync(i);
                    await Task.Delay(1000);
                }
            });

            while (true)
            {
                var item = await myChannel.Reader.ReadAsync();
                Console.WriteLine(item);
            }
        }

        private static async Task Example2()
        {
            var myChannel = Channel.CreateBounded<int>(5);

            _ = Task.Factory.StartNew(async () =>
            {
                for (int i = 0; i < 10; i++)
                {
                    await myChannel.Writer.WriteAsync(i);
                    Console.WriteLine("Writing:"+i);

                }
            });

            while (true)
            {
                var item = await myChannel.Reader.ReadAsync();
                await Task.Delay(1000);
                Console.WriteLine(item);
            }
        }

        private static async Task Example3()
        {
            var myChannel = Channel.CreateUnbounded<int>();

            _ = Task.Factory.StartNew(async () =>
            {
                for (int i = 0; i < 10; i++)
                {
                    await myChannel.Writer.WriteAsync(i);
                    Console.WriteLine("Writing:" + i);
                }

                myChannel.Writer.Complete();
            });

            try
            {
                while (true)
                {
                    var item = await myChannel.Reader.ReadAsync();
                    Console.WriteLine("Reading:"+item);
                    await Task.Delay(1000);
                }
            }
            catch (ChannelClosedException e)
            {
                Console.WriteLine("Channel was closed!");
            }
        }

        private static async Task Example4()
        {
            var myChannel = Channel.CreateUnbounded<int>();

            _ = Task.Factory.StartNew(async () =>
            {
                for (int i = 0; i < 10; i++)
                {
                    await myChannel.Writer.WriteAsync(i);
                    Console.WriteLine("Thread1-Writing:" + i);
                }
            });

            _ = Task.Factory.StartNew(async () =>
            {
                for (int i = 0; i > -10; i--)
                {
                    await myChannel.Writer.WriteAsync(i);
                    Console.WriteLine("Thread2-Writing:" + i);
                }
            });

            try
            {
                while (true)
                {
                    var item = await myChannel.Reader.ReadAsync();
                    Console.WriteLine("Reading:" + item);
                    await Task.Delay(1000);
                }
            }
            catch (ChannelClosedException e)
            {
                Console.WriteLine("Channel was closed!");
            }
        }
    }
}
