using System;
using System.Threading.Tasks.Dataflow;
using System.IO;
namespace FinalSampleCode{
    /// <summary>
    /// 文件写入类
    /// </summary>
    public class FileWriteBlock
    {
        private string _path;
        public FileWriteBlock(string path)
        {
            _path = path;
            _Write = new ActionBlock<string>(p => {
                File.AppendAllText(_path, p+'\n');
            });
        }
        private ActionBlock<string> _Write;
        public void Enqueue(string input)
        {
            _Write.Post(input);
        }
    }
}