using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using GraphProcessor;
using UnityEngine.Assertions;

namespace Fluss
{
    public class FlussGraphWindow : BaseGraphWindow
    {
        protected override void InitializeWindow(BaseGraph graph)
        {
            Assert.IsNotNull(graph);
        
            // ウィンドウのタイトルを適当に設定
            var fileName = Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(graph));
            titleContent = new GUIContent(ObjectNames.NicifyVariableName(fileName));

            // グラフを編集するためのビューであるGraphViewを設定
            if (graphView == null)
            {
                graphView = new FlussGraphView(this);
                graphView.Add(new FlussToolbarView(graphView));
            }
            
            rootView.Add(graphView);
        }
        
        // ダブルクリックでウィンドウが開かれるように
        [OnOpenAsset(0)]
        public static bool OnBaseGraphOpened(int instanceID, int line)
        {
            var asset = EditorUtility.InstanceIDToObject(instanceID) as FlussGraph;

            if (asset == null) return false;
        
            var window = EditorWindow.GetWindow<FlussGraphWindow>();
            window.InitializeGraph(asset);
            return true;
        }
    }

}
