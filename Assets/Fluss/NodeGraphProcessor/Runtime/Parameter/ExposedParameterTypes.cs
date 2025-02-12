using System;
using System.Collections.Generic;
using GraphProcessor;

namespace Fluss
{
    public static class ExposedParameterTypes
    {
        public static IEnumerable<Type> GetExposedParameterTypes()
        {
            // We only accept these types:
            yield return typeof(BoolParameter);
            yield return typeof(ColorParameter);
            yield return typeof(FloatParameter);
            yield return typeof(IntParameter);
            yield return typeof(GameObjectParameter);
            yield return typeof(ToggleParameter);
            yield return typeof(SliderParameter);
            yield return typeof(ButtonParameter);
            yield return typeof(DropdownParameter);
            yield return typeof(CinemachineVirtualCameraParameter);
        }
    }
}
