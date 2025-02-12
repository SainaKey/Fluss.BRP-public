using UnityEngine;
using System;
using GraphProcessor;
using UniRx;

namespace Fluss
{
    [Serializable]
    [NodeMenuItem("Math/Lerp")]
    public class LerpNode : FlussNode
    {
        public override string name => "Lerp";

        [Input(name = "A") , ShowAsDrawer]
        public float a;
        
        [Input(name = "B") , ShowAsDrawer]
        public float b;
        
        [Input(name = "T")]
        public Subject<float> tSubject;
        private IDisposable tDisposable;
        
        [Output(name = "Result")]
        public Subject<float> resultSubject = new Subject<float>();
        
        protected override void Process()
        {
            if (tSubject != null)
            {
                tDisposable = tSubject.Subscribe(t =>
                {
                    var result =  Mathf.Lerp(a, b, t);
                    resultSubject.OnNext(result);
                });
            }
        }

        public override void DisposeAllProcess()
        {
            tDisposable?.Dispose();
        }

        protected override void Destroy()
        {
            tDisposable?.Dispose();
        }
    }
}
