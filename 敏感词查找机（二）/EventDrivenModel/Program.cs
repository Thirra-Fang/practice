using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace EventDrivenModel
{
    class Program
    {
        static void Main(string[] args)
        {
            ConcurrentQueue<string> inputQueue = new ConcurrentQueue<string>();

            //读取结束时执行
            Action ReadFinished = null;

            ReadFinished+=() =>
            {
                Console.WriteLine("读取完成后，我需要进行第一个处理");
            };


            ReadFinished += () =>
            {
                Console.WriteLine("读取完成后，我需要进行第二个处理");
            };

            ReadFinished += () =>
            {
                Console.WriteLine("读取完成后，我需要进行第三个处理");
            };
            //读取每一行时执行
            Action<string> ReadLine = null;
            ReadLine+=(input) =>
            {
                inputQueue.Enqueue(input);
            };


            //开启一个线程去读取文件
            Task.Run(() => {  
                    string line = ""; 
                    using (StreamReader sr = new StreamReader("demo.txt"))
                    {
                        while ((line = sr.ReadLine()) != null)
                        {
                            Console.WriteLine(line);
                            ReadLine?.Invoke(line);
                        }
                        ReadFinished?.Invoke();
                    } 
            });


            //声明输出队列
            ConcurrentQueue<string> outputQueue1 = new ConcurrentQueue<string>();
            ConcurrentQueue<string> outputQueue2 = new ConcurrentQueue<string>();

            Action<string> ProcessedLine = null;
            ProcessedLine += (str) =>
            { 
                //添加到输出队列1
                outputQueue1.Enqueue(str);
                //添加到输出队列2
                outputQueue2.Enqueue(str);
            };



            //开启一个线程去从输入队列中取数据，处理完成后，放到输出队列里
            Task.Run(() =>
            {
            while (true)
            {
                if(inputQueue.TryDequeue(out string newline))
                {
                    var result= newline.Replace("Hello world", "Hello Thirra");
                    ProcessedLine?.Invoke(result);
                }
            }
            });


            //开启一个线程从输出队列中取数据，取到后放到显示出来
            Task.Run(() =>
            {
                while (true)
                {
                    if (outputQueue1.TryDequeue(out string newline))
                    {
                        Console.WriteLine(newline);
                    }
                }
            });

            Console.ReadLine();
        }
    }
}
