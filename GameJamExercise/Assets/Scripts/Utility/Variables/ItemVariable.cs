using OsukaCreative.Utility.Variables;
using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Variable/Item Variable")]
public class ItemVariable : BaseVariable<Item> { }

[Serializable]
public class ItemReference : BaseReference<Item, ItemVariable> { };

