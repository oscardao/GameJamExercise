using System;
using UnityEngine;

namespace OsukaCreative.Utility.Variables {

    [CreateAssetMenu(menuName = "Variable/GameObject Variable")]
    public class GameObjectVariable : BaseVariable<GameObject> { }

    [Serializable]
    public class GameObjectReference : BaseReference<GameObject, GameObjectVariable> { };

}