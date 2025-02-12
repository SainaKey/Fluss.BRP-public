using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;
using GraphProcessor;

namespace Fluss
{
    [CustomEditor(typeof(FlussParameters))]
    public class ParametersEditor : Editor
    {
        private SerializedProperty parametersProperty;
        private List<Type> exposedParameterTypes;

        private void OnEnable()
        {
            parametersProperty = serializedObject.FindProperty("parameters");
            FlussParameters flussParametersScript = (FlussParameters)target;
            flussParametersScript.GetEngine();
            flussParametersScript.UpdateParameters();
            exposedParameterTypes = new List<Type>(ExposedParameterTypes.GetExposedParameterTypes());
        }

        public override void OnInspectorGUI()
        {
            FlussParameters flussParametersScript = (FlussParameters)target;
            flussParametersScript.GetEngine();
            flussParametersScript.UpdateParameters();
            serializedObject.Update();
            exposedParameterTypes = new List<Type>(ExposedParameterTypes.GetExposedParameterTypes());

            EditorGUILayout.LabelField("Exposed Parameters", EditorStyles.boldLabel);

            if (parametersProperty != null && parametersProperty.isArray)
            {
                for (int i = 0; i < parametersProperty.arraySize; i++)
                {
                    SerializedProperty element = parametersProperty.GetArrayElementAtIndex(i);

                    // 'settings.isHidden' を取得
                    SerializedProperty settingsProp = element.FindPropertyRelative("settings");
                    bool isHidden = false;
                    if (settingsProp != null)
                    {
                        SerializedProperty isHiddenProp = settingsProp.FindPropertyRelative("isHidden");
                        if (isHiddenProp != null)
                        {
                            isHidden = isHiddenProp.boolValue;
                        }
                    }

                    if (isHidden)
                    {
                        // 'isHidden' が true の場合、このパラメータを表示しない
                        continue;
                    }

                    EditorGUILayout.BeginVertical("box");

                    DrawExposedParameter(element);

                    EditorGUILayout.EndVertical();
                }
            }

            // シリアライズオブジェクトの変更を反映
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawExposedParameter(SerializedProperty parameterProp)
        {
            if (parameterProp.managedReferenceValue != null)
            {
                // 'name' フィールドを読み取り専用で描画
                SerializedProperty nameProp = parameterProp.FindPropertyRelative("name");
                if (nameProp != null)
                {
                    EditorGUI.BeginDisabledGroup(true); // フィールドを無効化（編集不可）
                    EditorGUILayout.PropertyField(nameProp, new GUIContent("Name"));
                    EditorGUI.EndDisabledGroup();
                }

                // 'val' フィールドの描画と変更検知
                DrawValField(parameterProp);
            }
            else
            {
                // パラメータが null の場合、型選択 UI を表示
                EditorGUILayout.LabelField("Set Type:");
                foreach (var type in exposedParameterTypes)
                {
                    if (GUILayout.Button(type.Name))
                    {
                        parameterProp.managedReferenceValue = Activator.CreateInstance(type);
                    }
                }
            }
        }

        private void DrawValField(SerializedProperty parameterProp)
        {
            // 'val' フィールドを取得
            SerializedProperty valProp = parameterProp.FindPropertyRelative("val");
            if (valProp != null)
            {
                // 変更検知開始
                EditorGUI.BeginChangeCheck();

                EditorGUILayout.PropertyField(valProp, new GUIContent("Value"), true);

                // 変更があった場合
                if (EditorGUI.EndChangeCheck())
                {
                    // 変更を反映
                    serializedObject.ApplyModifiedProperties();

                    // ExposedParameter インスタンスを取得
                    ExposedParameter param = parameterProp.managedReferenceValue as ExposedParameter;
                    if (param != null)
                    {
                        FlussParameters flussParametersScript = (FlussParameters)target;
                        flussParametersScript.GetEngine();
                        //Debug.Log($"Parameter '{param.name}' value changed to: {param.value}");
                    }
                    else
                    {
                        //Debug.Log("Parameter value changed.");
                    }
                }
            }
            else
            {
                EditorGUILayout.HelpBox("No 'val' field found.", MessageType.Warning);
            }
        }
    }
}
