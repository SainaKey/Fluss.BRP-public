using UnityEngine;
using System;
using GraphProcessor;
using UniRx;
using System.Collections.Generic;
using System.Linq;

namespace Fluss
{
    [Serializable]
    [NodeMenuItem("CinemachineVirtualCamera/SetPriority")]
    public class SetPriority : FlussNode
    {
        public override string name => "SetPriority";

        [Input(name = "VirtualCamera")] public Cinemachine.CinemachineVirtualCamera virtualCamera;

        [Input(name = "Priority"),ShowAsDrawer] public int priority;

        
        [Input]
        public IEnumerable<IObservable<Unit>> triggerInputs;//Triggerの入力、ダミー
        private List<IDisposable> triggerInputDisposables = new List<IDisposable>();
        
        [CustomPortBehavior(nameof(triggerInputs))]
        IEnumerable< PortData > GetPortsForInputs(List< SerializableEdge > edges)
        {
            yield return new PortData{ displayName = "Triggers", displayType = typeof(IObservable<Unit>), acceptMultipleEdges = true};
        }
        
        [CustomPortInput(nameof(triggerInputs), typeof(float), allowCast = true)]
        public void GetInputs(List< SerializableEdge > edges)
        {
            triggerInputs = edges.Select(e => (IObservable<Unit>)e.passThroughBuffer);
        }

        protected override void Process()
        {
            foreach (var triggerInput in triggerInputs)
            {
                var triggerDisposable = triggerInput.Subscribe(_ =>
                {
                    virtualCamera.Priority = priority;
                });
                triggerInputDisposables.Add(triggerDisposable);
            }
        }
        
        public override void DisposeAllProcess()
        {
            triggerInputDisposables?.ForEach(d => d.Dispose());
        }
        
        protected override void Destroy()
        {
            triggerInputDisposables?.ForEach(d => d.Dispose());
        }
    }

}
