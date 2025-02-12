using System;
using System.Collections.Generic;
using System.Linq;
using GraphProcessor;
using UnityEditor;

namespace Fluss
{
    public class FlussGraphView : BaseGraphView
    {
        
        public new FlussGraph graph => base.graph as FlussGraph;
        public FlussGraphView(EditorWindow window) : base(window) { }
    }

}
