using System;
using GraphProcessor;
using UniRx;
using UnityEngine.UI;

namespace Fluss
{
    [Serializable]
    [NodeMenuItem("UI/Slider To Float")]
    public class SliderToFloatNode : FlussNode
    {
        public override string name => "Slider To Float";

        [Input(name = "Slider")] public Slider slider;

        [Output(name = "Float", allowMultiple = true)]
        public Subject<float> floatOutput = new Subject<float>();

        private IDisposable sliderDisposable;

        protected override void Process()
        {
            sliderDisposable = slider.OnValueChangedAsObservable()
                .Subscribe(value => { floatOutput.OnNext(value); });
        }

        public override void DisposeAllProcess()
        {
            sliderDisposable?.Dispose();
        }

        protected override void Destroy()
        {
            sliderDisposable?.Dispose();
        }
    }
}
