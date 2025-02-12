using System;
using GraphProcessor;
using UnityEngine;
using System.Linq;
using UniRx;

namespace Fluss
{
    // CreateメニューからScriptableObjectのアセットを作れるように
    [CreateAssetMenu(menuName = "Fluss Graph")]
    public class FlussGraph : BaseGraph
    {
        public bool isParameterViewOpen;
        private Subject<Unit> onValidateSubject = new Subject<Unit>();
        public IObservable<Unit> OnValidateObservable => onValidateSubject;
        
        private void OnValidate()
        {
            Debug.Log("OnValidate");
            onValidateSubject.OnNext(Unit.Default);
        }
        

    }

}
