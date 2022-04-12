using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
/// <summary>
/// 命令行输出类
/// </summary>
namespace FinalSampleCode{
    public class ConsoleWriteBlock
    {
        private ActionBlock<string> _InputBlock;
        public ConsoleWriteBlock()
        {
            _InputBlock = new ActionBlock<string>(p => {
                Console.Write("ouput:");
                Console.WriteLine(p);
            });
        }
        public void Enqueue(string input)
        {
            _InputBlock.Post(input);
        }
    }
}