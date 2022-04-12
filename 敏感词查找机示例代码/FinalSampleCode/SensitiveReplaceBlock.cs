using System;
using System.Threading.Tasks.Dataflow;
namespace FinalSampleCode{
    /// 敏感词替换类
    /// </summary>
    public class SensitiveReplaceBlock
    {
        private string _oldValue;
        private string _newValue;
        public SensitiveReplaceBlock(string oldValue,string newValue)
        {
            _oldValue = oldValue;
            _newValue = newValue;
            _Process = new ActionBlock<string>(p => {
                var result= p.Replace(_oldValue, _newValue);
                DataArrived?.Invoke(result);
            });
        }
        public void Enqueue(string result)
        {
            _Process.Post(result);
            //DataArrived?.Invoke(result);
        }
        private ActionBlock<string> _Process;
        public Action<string> DataArrived;
    }
}