using System;
using GraphProcessor;
using UniRx;
using UnityEngine;

namespace Fluss
{
    [Serializable]
    [NodeMenuItem("Debug/Debug Log")]
    public class DebugLogNode : FlussNode
    {
        public override string name => "Debug Log";
        
        [Input(name = "Object")]
        public object inputObject;
        
        [Input(name = "Trigger")] 
        public IObservable<Unit> triggerInput;
        private IDisposable triggerInputDisposable;
        protected override void Process()
        {
            triggerInputDisposable = triggerInput.Subscribe(_ => DebugLog());
        }
        
        private void DebugLog()
        {
            Debug.Log(inputObject + "がトリガーされました");
        }
        
        public override void DisposeAllProcess()
        {
            triggerInputDisposable?.Dispose();
        }
        
        protected override void Destroy()
        {
            triggerInputDisposable?.Dispose();
        }
    }
}
