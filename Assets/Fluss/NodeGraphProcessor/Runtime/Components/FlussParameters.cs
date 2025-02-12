using System.Collections.Generic;
using GraphProcessor;
using UnityEngine;
using System;
using UniRx;

namespace Fluss
{
    [AddComponentMenu("Fluss/Parameters")]
    public class FlussParameters : MonoBehaviour
    {
        private FlussEngine _flussEngine;
        [SerializeReference]
        private List<ExposedParameter> parameters = new List<ExposedParameter>();
        
        public List<ExposedParameter> Parameters => parameters;
        
        private Subject<Unit> onValidateSubject = new Subject<Unit>();
        public IObservable<Unit> OnValidateObservable => onValidateSubject;

        public void GetEngine()
        {
            if(_flussEngine == null)
            {
                _flussEngine = GetComponent<FlussEngine>();
            }
        }
        
        public void UpdateParameters()
        {
            FlussGraph flussGraph = null;
            
            if (_flussEngine != null)
            {
                flussGraph = _flussEngine.FlussGraph;
            }
            
            if(flussGraph != null)
            {
                var tmp = new List<ExposedParameter>(parameters);
                parameters.Clear();
                parameters = new List<ExposedParameter>(flussGraph.exposedParameters);

                for (int i = 0; i < parameters.Count; i++)
                {
                    var parameter = parameters[i];
                    if (parameter == null || parameter.guid == null) continue;
                    
                    var tmpParameter = tmp.Find(x => x != null && x.guid == parameter.guid);
                    if (tmpParameter != null)
                    {
                        parameters[i].value = tmpParameter.value;
                    }
                }
            }
            else
            {
                parameters = new List<ExposedParameter>();
            }
        }
        
        private void OnValidate()
        {
            onValidateSubject.OnNext(Unit.Default);
        }
        
        
    }
}
