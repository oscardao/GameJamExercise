using System;
using UnityEngine;

namespace OsukaCreative.Utility.Variables {

    [CreateAssetMenu(menuName = "Variable/String Variable")]
    public class StringVariable : BaseVariable<string> { }

    [Serializable]
    public class StringReference : BaseReference<string, StringVariable> { };

}