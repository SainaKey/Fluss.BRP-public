using UnityEngine.UIElements;
using GraphProcessor;
using Unity.VisualScripting;
using UnityEngine;

namespace Fluss
{
    public class FlussExposedParameterPropertyView : VisualElement
    {
        protected FlussGraphView flussGraphView;
        
        public ExposedParameter parameter { get; private set; }
        
        public Toggle     hideInInspector { get; private set; }
        
        public FlussExposedParameterPropertyView(BaseGraphView graphView, ExposedParameter param)
        {
            flussGraphView = graphView as FlussGraphView;
            parameter      = param;
            
            var valueField = graphView.exposedParameterFactory.GetParameterValueField(param, (newValue) => {
                graphView.RegisterCompleteObjectUndo("Updated Parameter Value");
                param.value = newValue;
                graphView.graph.NotifyExposedParameterValueChanged(param);
                //flussGraphView.ProcessGraph();
            });

            var field = graphView.exposedParameterFactory.GetParameterSettingsField(param, (newValue) => {
                param.settings = newValue as ExposedParameter.Settings;
            });

            Add(valueField);

            Add(field);
        }
    }
}
