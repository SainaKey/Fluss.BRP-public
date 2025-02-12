using System;
using GraphProcessor;
using UniRx;
using UnityEngine.UI;

namespace Fluss
{
    [Serializable]
    [NodeMenuItem("UI/Dropdown To Int")]
    public class DropdownToIntNode : FlussNode
    {
        public override string name => "Dropdown To Int";

        [Input(name = "Dropdown")] public Dropdown dropdown;

        [Output(name = "Int", allowMultiple = true)]
        public Subject<int> intOutput = new Subject<int>();

        private IDisposable dropdownDisposable;

        protected override void Process()
        {
            dropdownDisposable = dropdown.OnValueChangedAsObservable()
                .Subscribe(value => { intOutput.OnNext(value); });
        }

        public override void DisposeAllProcess()
        {
            dropdownDisposable?.Dispose();
        }

        protected override void Destroy()
        {
            dropdownDisposable?.Dispose();
        }
    }
}
