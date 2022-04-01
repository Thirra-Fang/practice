using System;
using System.Threading.Tasks.Dataflow;
using System.IO;
using System.Threading.Tasks;

namespace TPLDataFlow
{
    class Program
    {
        static void Main(string[] args)
        {
            //定义一个 文本处理流程
            TransformBlock<string, string> transformBlock = new TransformBlock<string, string>((input) =>
            {
                return input.Replace("Hello world", "Hello Thirra");
            });
            //定义一个 命令行输出流程
            ActionBlock<string> consoleBlock = new ActionBlock<string>((input) =>
            {
                Console.WriteLine(input);
            });
            //定义一个 文件输出流程
            ActionBlock<string> fileBlock = new ActionBlock<string>((input) =>
            {
                using (StreamWriter sw = new StreamWriter("output.txt",true))
                { 
                    sw.WriteLine(input); 
                }
            });
            BroadcastBlock<string> broadcastBlock = new BroadcastBlock<string>((p)=>{return p;});

            transformBlock.LinkTo(broadcastBlock);
            broadcastBlock.LinkTo(consoleBlock);
            broadcastBlock.LinkTo(fileBlock);
            Task.Run(() =>
            {
                string line = "";
                using (StreamReader sr = new StreamReader("demo.txt"))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        transformBlock.Post(line);
                    }
                }
            });
            Console.ReadLine();
        }
    }
}
