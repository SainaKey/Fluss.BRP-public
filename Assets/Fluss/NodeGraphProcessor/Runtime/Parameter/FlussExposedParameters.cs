using UnityEngine;
using GraphProcessor;
using System;

namespace Fluss
{
    [Serializable]
    public class GameObjectParameter : ExposedParameter
    {
        [SerializeField] private GameObject val;
        
        public override object value { get => val; set => val = (GameObject)value; }
        
        public override Type GetValueType() => typeof(GameObject);
    }
    
    [Serializable]
    public class ToggleParameter : ExposedParameter
    {
        [SerializeField] private UnityEngine.UI.Toggle val;
        
        public override object value { get => val; set => val = (UnityEngine.UI.Toggle)value; }
        
        public override Type GetValueType() => typeof(UnityEngine.UI.Toggle);
    }
    
    [Serializable]
    public class SliderParameter : ExposedParameter
    {
        [SerializeField] private UnityEngine.UI.Slider val;
        
        public override object value { get => val; set => val = (UnityEngine.UI.Slider)value; }
        
        public override Type GetValueType() => typeof(UnityEngine.UI.Slider);
    }
    
    [Serializable]
    public class ButtonParameter : ExposedParameter
    {
        [SerializeField] private UnityEngine.UI.Button val;
        
        public override object value { get => val; set => val = (UnityEngine.UI.Button)value; }
        
        public override Type GetValueType() => typeof(UnityEngine.UI.Button);
    }
    
    [Serializable]
    public class DropdownParameter : ExposedParameter
    {
        [SerializeField] private UnityEngine.UI.Dropdown val;
        
        public override object value { get => val; set => val = (UnityEngine.UI.Dropdown)value; }
        
        public override Type GetValueType() => typeof(UnityEngine.UI.Dropdown);
    }
    
    [Serializable]
    public class CinemachineVirtualCameraParameter : ExposedParameter
    {
        [SerializeField] private Cinemachine.CinemachineVirtualCamera val;
        
        public override object value { get => val; set => val = (Cinemachine.CinemachineVirtualCamera)value; }
        
        public override Type GetValueType() => typeof(Cinemachine.CinemachineVirtualCamera);
    }

    
}
