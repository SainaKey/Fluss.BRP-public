using System;
using GraphProcessor;
using UniRx;
using UnityEngine;

namespace Fluss
{
    [Serializable]
    [NodeMenuItem("GameObject/AddRotation")]
    public class AddRotation : FlussNode
    {
        public override string name => "AddRotation";
        
        [Input(name = "Object")] public GameObject gameObject;
        
        [Input(name = "x"),ShowAsDrawer] public float x;
        [Input(name = "y"),ShowAsDrawer] public float y;
        [Input(name = "z"),ShowAsDrawer] public float z;
        
        [Input(name = "Trigger")] public Subject<Unit> triggerSubject;
        
        private IDisposable triggerDisposable;
        
        protected override void Process()
        {
            if (triggerSubject != null)
            {
                triggerDisposable = triggerSubject.Subscribe(_ =>
                {
                    gameObject.transform.Rotate(new Vector3(x, y, z));
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

