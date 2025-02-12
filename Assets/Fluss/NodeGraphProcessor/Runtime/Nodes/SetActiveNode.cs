using System;
using GraphProcessor;
using UniRx;
using UnityEngine;

namespace Fluss
{
    [Serializable]
    [NodeMenuItem("GameObject/SetActive")]
    public class SetActiveNode : FlussNode
    {
        public override string name => "SetActive";

        [Input(name = "Object")] public GameObject gameObject;
        
        [Input(name = "Active"),ShowAsDrawer] public bool isActive;
        
        [Input(name = "Trigger")] public Subject<Unit> triggerSubject;
        private IDisposable triggerDisposable;

        protected override void Process()
        {
            if (triggerSubject != null)
            {
                triggerDisposable = triggerSubject.Subscribe(_ =>
                {
                    gameObject.SetActive(isActive);
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
