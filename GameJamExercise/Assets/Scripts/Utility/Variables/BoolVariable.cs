using System;
using UnityEngine;

namespace OsukaCreative.Utility.Variables {

    [CreateAssetMenu(menuName = "Variable/Bool Variable")]
    public class BoolVariable : BaseVariable<bool> { }

    [Serializable]
    public class BoolReference : BaseReference<bool, BoolVariable> { };
}