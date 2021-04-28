using System;
using UnityEngine;

namespace OsukaCreative.Utility.Variables {

    [CreateAssetMenu(menuName = "Variable/Int Variable")]
    public class IntVariable : BaseVariable<int> { }

    [Serializable]
    public class IntReference : BaseReference<int, IntVariable> { };

}