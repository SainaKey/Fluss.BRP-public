using System;
using GraphProcessor;
using UniRx;
using UnityEngine;

namespace Fluss
{
    [Serializable]
    [NodeMenuItem("GameObject/SetPosition")]
    public class SetPosition : FlussNode
    {
        public override string name => "SetPosition";

        [Input(name = "Object")] public GameObject gameObject;

        [Input(name = "x")] public Subject<float> x;
        private IDisposable xDisposable;

        [Input(name = "y")] public Subject<float> y;
        private IDisposable yDisposable;

        [Input(name = "z")] public Subject<float> z;
        private IDisposable zDisposable;

        protected override void Process()
        {
            if (x != null)
            {
                xDisposable = x.Subscribe(value =>
                {
                    var pos = gameObject.transform.localPosition;
                    pos.x = value;
                    gameObject.transform.localPosition = pos;
                });
            }

            if (y != null)
            {
                yDisposable = y.Subscribe(value =>
                {
                    var pos = gameObject.transform.localPosition;
                    pos.y = value;
                    gameObject.transform.localPosition = pos;
                });
            }

            if (z != null)
            {
                zDisposable = z.Subscribe(value =>
                {
                    var pos = gameObject.transform.localPosition;
                    pos.z = value;
                    gameObject.transform.localPosition = pos;
                });
            }
        }
        
        public override void DisposeAllProcess()
        {
            xDisposable?.Dispose();
            yDisposable?.Dispose();
            zDisposable?.Dispose();
        }
        
        protected override void Destroy()
        {
            xDisposable?.Dispose();
            yDisposable?.Dispose();
            zDisposable?.Dispose();
        }
    }
}
