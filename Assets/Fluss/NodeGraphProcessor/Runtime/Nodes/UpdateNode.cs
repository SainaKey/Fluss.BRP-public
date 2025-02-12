using System;
using GraphProcessor;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Fluss
{
    [Serializable]
    [NodeMenuItem("Update")]
    public class UpdateNode : FlussNode
    {
        [Output(name = "Out" , allowMultiple = true)]
        public Subject<Unit> triggerOutput = new Subject<Unit>();

        private IDisposable updateDisposable;
        
        protected override void Process()
        {
            updateDisposable = Observable.EveryUpdate()
                .Subscribe(_ =>
                {
                    triggerOutput.OnNext(Unit.Default);
                });
        }
        
        public override void DisposeAllProcess()
        {
            updateDisposable?.Dispose();
        }
        
        protected override void Destroy()
        {
            updateDisposable?.Dispose();
        }
    }
}
