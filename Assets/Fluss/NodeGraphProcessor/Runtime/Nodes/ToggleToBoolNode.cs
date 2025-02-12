using System;
using GraphProcessor;
using UniRx;
using UnityEngine.UI;

namespace Fluss
{
    [Serializable]
    [NodeMenuItem("UI/Toggle To Bool")]
    public class ToggleToBoolNode : FlussNode
    {
        public override string name => "Toggle To Bool";

        [Input(name = "Toggle")] public Toggle toggle;

        [Output(name = "Bool", allowMultiple = true)]
        public Subject<bool> boolOutput = new Subject<bool>();

        private IDisposable toggleDisposable;

        protected override void Process()
        {
            toggleDisposable = toggle.OnValueChangedAsObservable()
                .Subscribe(value => { boolOutput.OnNext(value); });
        }

        public override void DisposeAllProcess()
        {
            toggleDisposable?.Dispose();
        }

        protected override void Destroy()
        {
            toggleDisposable?.Dispose();
        }
    }
}
