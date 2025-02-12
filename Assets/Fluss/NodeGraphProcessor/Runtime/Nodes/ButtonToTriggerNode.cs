using System;
using GraphProcessor;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Fluss
{
    [Serializable]
    [NodeMenuItem("UI/Button To Trigger")]
    public class ButtonToTriggerNode : FlussNode
    {
        public override string name => "Button To Trigger";
        
        [Input(name = "button")]
        public Button button;
        
        [Output(name = "Trigger" , allowMultiple = true)]
        public Subject<Unit> triggerOutput = new Subject<Unit>();
        
        private IDisposable buttonDisposable;
        
        protected override void Process()
        {
            Debug.Log("aiu"+button);
            buttonDisposable = button.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    triggerOutput.OnNext(Unit.Default);
                });
        }
        
        public override void DisposeAllProcess()
        {
            buttonDisposable?.Dispose();
        }
        
        protected override void Destroy()
        {
            buttonDisposable?.Dispose();
        }
    }
}
