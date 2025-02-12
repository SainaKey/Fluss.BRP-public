using UnityEngine;
using System;
using GraphProcessor;
using UniRx;
using Random = UnityEngine.Random;

namespace Fluss
{
    [Serializable]
    [NodeMenuItem("Math/RandomVector3")]
    public class RandomVector3 : FlussNode
    {
        public override string name => "RandomVector3";
        
        [Input(name = "Min") , ShowAsDrawer]
        public Vector3 min;
        
        [Input(name = "Max") , ShowAsDrawer]
        public Vector3 max;
        
        [Input(name = "Trigger")]
        public Subject<Unit> triggerSubject;
        private IDisposable triggerDisposable;
        
        [Output(name = "x")]
        public Subject<float> x = new Subject<float>();
        
        [Output(name = "y")]
        public Subject<float> y = new Subject<float>();
        
        [Output(name = "z")]
        public Subject<float> z = new Subject<float>();
        
        protected override void Process()
        {
            if (triggerSubject != null)
            {
                triggerDisposable = triggerSubject.Subscribe(_ =>
                {
                    var result = new Vector3(
                        Random.Range(min.x, max.x),
                        Random.Range(min.y, max.y),
                        Random.Range(min.z, max.z)
                    );
                    x.OnNext(result.x);
                    y.OnNext(result.y);
                    z.OnNext(result.z);
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
