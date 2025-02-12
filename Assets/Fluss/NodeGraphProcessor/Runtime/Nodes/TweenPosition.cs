using System;
using DG.Tweening;
using GraphProcessor;
using UniRx;
using UnityEngine;

namespace Fluss
{
    [Serializable]
    [NodeMenuItem("GameObject/TweenPosition")]
    public class TweenPosition : FlussNode
    {
        public override string name => "TweenPosition";

        [Input(name = "Object")] public GameObject gameObject;

        [Input(name = "x")] public Subject<float> x;
        private IDisposable xDisposable;

        [Input(name = "y")] public Subject<float> y;
        private IDisposable yDisposable;

        [Input(name = "z")] public Subject<float> z;
        private IDisposable zDisposable;
        
        public Ease ease = Ease.Linear;
        public float duration = 1f;
        
        private Vector3 targetPosition;

        private Tweener tweener = null;
        
        protected override void Process()
        {
            if (x != null)
            {
                xDisposable = x.Subscribe(value =>
                {
                    targetPosition.x = value;
                    Tween();
                });
            }
            
            if (y != null)
            {
                yDisposable = y.Subscribe(value =>
                {
                    targetPosition.y = value;
                    Tween();
                });
            }
            
            if (z != null)
            {
                zDisposable = z.Subscribe(value =>
                {
                    targetPosition.z = value;
                    Tween();
                });
            }
        }

        private void Tween()
        {
            if (tweener != null)
            {
                tweener.Kill();
            }
                
            
            tweener = gameObject.transform.DOLocalMove(targetPosition, duration).SetEase(ease);
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


