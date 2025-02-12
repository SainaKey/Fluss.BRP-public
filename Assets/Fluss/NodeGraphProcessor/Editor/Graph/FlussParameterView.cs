using System;
using System.Collections.Generic;
using GraphProcessor;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UniRx;
using Unity.VisualScripting;

namespace Fluss
{
    public class FlussParameterView : ExposedParameterView
    {
        static readonly string flussParameterStyleSheet = "FlussParameterView";
        
        public FlussParameterView()
        {
            var style = Resources.Load<StyleSheet>(flussParameterStyleSheet);
            if (style != null)
                styleSheets.Add(style);
        }

        protected override IEnumerable< Type > GetExposedParameterTypes()
        {
            // We only accept these types:
            /*
            yield return typeof(BoolParameter);
            yield return typeof(ColorParameter);
            yield return typeof(FloatParameter);
            yield return typeof(IntParameter);
            yield return typeof(ButtonParameter);
            */
            return ExposedParameterTypes.GetExposedParameterTypes();
        }
        
        
        protected override void UpdateParameterList()
        {
            //Debug.Log("updateParameterList");
            content.Clear();
            
            var usedNames = new HashSet<string>();

            foreach (var param in graphView.graph.exposedParameters)
            {
                //名前が重複している場合、一意な名前を生成する
                if (usedNames.Contains(param.name))
                {
                    int suffix = 0;
                    string baseName = param.name;
                    
                    while(usedNames.Contains($"{baseName} {suffix}"))
                    { 
                        suffix++;
                    }
                    
                    param.name = $"{baseName} {suffix}";
                }
                
                usedNames.Add(param.name);
                
                var row = new BlackboardRow(new ExposedParameterFieldView(graphView, param), new FlussExposedParameterPropertyView(graphView, param));
                row.expanded = param.settings.expanded;
                row.RegisterCallback<GeometryChangedEvent>(e => {
                    param.settings.expanded = row.expanded;
                });
                
                //名前変更
                //TDOO: もっといい方法があるかも
                row.Q<TextField>().RegisterCallback<FocusOutEvent>(e =>
                {
                    //Debug.Log("name changed");
                    UpdateParameterList();
                });

                content.Add(row);
            }
        }
    }
}
