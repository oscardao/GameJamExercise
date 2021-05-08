using OsukaCreative.Utility.Variables;
using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Variable/Level Data Variable")]
public class LevelDataVariable : BaseVariable<LevelData> { }

[Serializable]
public class LevelDataReference : BaseReference<LevelData, LevelDataVariable> { };
