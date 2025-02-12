using System;
using GraphProcessor;
using UniRx;
using UnityEngine;

namespace Fluss
{
    [Serializable]
    [NodeMenuItem("UI/KeybordInput")]
    public class KeybordInput : FlussNode
    {
        public override string name => "KeybordInput";
        
        public KeyCode key = KeyCode.Space;
        
        [Output(name = "Trigger" , allowMultiple = true)]
        public Subject<Unit> triggerOutput = new Subject<Unit>();
        
        private IDisposable keybordDisposable;
        
        protected override void Process()
        {
            keybordDisposable = Observable.EveryUpdate()
                .Where(_ => Input.GetKeyDown(key))
                .Subscribe(_ =>
                {
                    triggerOutput.OnNext(Unit.Default);
                });
        }
        
        public override void DisposeAllProcess()
        {
            keybordDisposable?.Dispose();
        }
        
        protected override void Destroy()
        {
            keybordDisposable?.Dispose();
        }
    }

}

