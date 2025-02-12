using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using GraphProcessor;
using UniRx;
using Unity.Jobs;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Fluss
{
    [AddComponentMenu("Fluss/Engine")]
    [RequireComponent(typeof(FlussParameters))]
    public class FlussEngine : MonoBehaviour
    {
        [SerializeField] private FlussGraph flussGraph;
        public FlussGraph FlussGraph => flussGraph;
        
        [Header("Runtime")]
        private FlussParameters flussParameters;
        private List<BaseNode> _processList;

        private void Start()
        {
            flussParameters = GetComponent<FlussParameters>();
            
            
            flussGraph.OnValidateObservable.DelayFrame(1).Subscribe(_ =>
            {
                Compile();
            }).AddTo(this);
            
            flussParameters.OnValidateObservable.DelayFrame(1).Subscribe(_ =>
            {
                Compile();
            }).AddTo(this);
            
            
            Compile();
        }
        
        private void Compile()
        {
            Debug.Log("Compile");
            UpdateComputeOrder();
            SetParameters();
            Run();
        }
        
        private void SetParameters()
        {
            //グラフのパラメーターに参照を渡す
            var paramters = flussParameters.Parameters;
            foreach (var paramter in paramters)
            {
                flussGraph.SetParameterValue(paramter.name, paramter.value);
            }
        }
        
        private void UpdateComputeOrder()
        {
            _processList = flussGraph.nodes.OrderBy(n => n.computeOrder).ToList();
        }

        private void Run()
        {
            var count = _processList.Count;
            
            // すべてのノードを順番に処理する
            for (var i = 0; i < count; i++)
            {
                
                if (_processList[i] is FlussNode flussNode)
                {
                    flussNode.DisposeAllProcess();
                }
                
                _processList[i].OnProcess();
            }

            JobHandle.ScheduleBatchedJobs();
        }

    }
}
