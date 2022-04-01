using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace ProducerConsumerModel
{
    class Program
    {
        static void Main(string[] args)
        {
            //声明输入队列
            ConcurrentQueue<string> inputQueue = new ConcurrentQueue<string>();

            //开启一个线程去读取文件
            Task.Run(() => {
                string line = "";
                using (StreamReader sr = new StreamReader("demo.txt"))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        inputQueue.Enqueue(line);
                        //newLineArrived?.Invoke(line);
                    }
                }
            });


            //声明输出队列
            ConcurrentQueue<string> outputQueue = new ConcurrentQueue<string>();


            //开启一个线程去从输入队列中取数据，处理完成后，放到输出队列里
            Task.Run(() =>
            {
                while (true)
                {
                    if(inputQueue.TryDequeue(out string newline))
                    {
                        var result= newline.Replace("Hello world", "Hello Thirra");
                        outputQueue.Enqueue(result);
                    }
                }
            });


            //开启一个线程从输出队列中取数据，取到后放到显示出来
            Task.Run(() =>
            {
                while (true)
                {
                    if (outputQueue.TryDequeue(out string newline))
                    {
                        Console.WriteLine(newline);
                    }
                }
            });
            Console.ReadLine();
        }
    }
}
