using System;
using GraphProcessor;
using UniRx;

namespace Fluss
{
    [Serializable]
    [NodeMenuItem("Convert/TriggerToBool")]
    public class TriggerToBool : FlussNode
    {
        public override string name => "Trigger To Bool";

        [Input(name = "Trigger")] public IObservable<Unit> triggerInput;
        private IDisposable triggerInputDisposable;
        [Output(name = "Bool")] public Subject<bool> boolOutput = new Subject<bool>();

        private bool currentBool = false;
        protected override void Process()
        {
            triggerInputDisposable = triggerInput.Subscribe(_ =>
            {
                currentBool = !currentBool;
                boolOutput.OnNext(currentBool);
            });
        }

        public override void DisposeAllProcess()
        {
            triggerInputDisposable?.Dispose();
        }

        protected override void Destroy()
        {
            triggerInputDisposable?.Dispose();
        }
    }
}
