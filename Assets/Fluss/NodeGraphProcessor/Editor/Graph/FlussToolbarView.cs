using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using GraphProcessor;
using Status = UnityEngine.UIElements.DropdownMenuAction.Status;

namespace Fluss
{
	public class FlussToolbarView : ToolbarView
	{
		public FlussToolbarView(BaseGraphView graphView) : base(graphView) {}
		
		FlussGraph graph => graphView.graph as FlussGraph;
		
		new FlussGraphView	graphView => base.graphView as FlussGraphView;
		
		class Styles
		{
			public const string saveAllText = "Save";
			public const string parameterViewsText = "Parameters";
			public const string showInProjectViewsText = "Show In Project";
			public static GUIContent focusText = new GUIContent("Fit View");
			static GUIStyle _improveButtonStyle = null;
			public static GUIStyle improveButtonStyle => _improveButtonStyle == null ? _improveButtonStyle = new GUIStyle(GUI.skin.button) { alignment = TextAnchor.MiddleLeft } : _improveButtonStyle;
		}
		
		protected override void AddButtons()
		{
			// Left buttons
			//AddButton(Styles.saveAllText, SaveAll);
			AddToggle(Styles.parameterViewsText, graph.isParameterViewOpen, ToggleParameterView, left: true);
			
			AddSeparator(5);

			AddButton(Styles.focusText, () => graphView.FrameAll());

			AddSeparator(5);
			
			AddButton(Styles.showInProjectViewsText, ShowInProject);

			// Right buttons

			AddFlexibleSpace(left: false);

			//AddButton(Styles.settingsIcon, ShowSettingsWindow, left: false);

			//AddDropDownButton(Styles.improveMixture, ShowImproveMixtureWindow, left: false);
		}
		
		
		void ShowInProject()
		{
			EditorGUIUtility.PingObject(graph);
			ProjectWindowUtil.ShowCreatedAsset(graph);
		}
		
		void ToggleParameterView(bool state)
		{
			graphView.ToggleView<FlussParameterView>();
			graph.isParameterViewOpen = state;
		}
	}
}
