using UnityEngine;
using System;
using GraphProcessor;
using UniRx;
namespace Fluss
{
    [Serializable]
    [NodeMenuItem("Tween/Delay Time")]
    public class DelayTime : FlussNode
    {
        public override string name => "Delay Time";
        
        [Input(name = "Time") , ShowAsDrawer]
        public float time;
        
        [Input(name = "Trigger")]
        public Subject<Unit> triggerSubject;
        
        [Output(name = "Trigger")]
        public Subject<Unit> triggerOutput = new Subject<Unit>();
        private IDisposable triggerDisposable;
        
        protected override void Process()
        {
            if (triggerSubject != null)
            {
                triggerDisposable = triggerSubject.Delay(TimeSpan.FromSeconds(time))
                    .Subscribe(_ =>
                    {
                        triggerOutput.OnNext(Unit.Default);
                    });
            }
        }
        
        public override void DisposeAllProcess()
        {
            triggerDisposable?.Dispose();
        }
        
        protected override void Destroy()
        {
            triggerDisposable?.Dispose();
        }
    }

}
