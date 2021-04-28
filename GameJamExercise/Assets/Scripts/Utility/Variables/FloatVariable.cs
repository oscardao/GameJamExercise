using System;
using UnityEngine;

namespace OsukaCreative.Utility.Variables {

    [CreateAssetMenu(menuName = "Variable/Float Variable")]
    public class FloatVariable : BaseVariable<float> { }

    [Serializable]
    public class FloatReference : BaseReference<float, FloatVariable> { };
}