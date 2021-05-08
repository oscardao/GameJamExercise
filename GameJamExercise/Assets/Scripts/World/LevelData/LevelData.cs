using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "World/Level Data")]
public class LevelData : ScriptableObject {

    [Header("Level Properties")]
    [SerializeField, Min(0)]
    private int appearsAtLevel;
    public int AppearsAtLevel {
        get { return this.appearsAtLevel; }
    }
    [SerializeField]
    private Texture2D[] maps;
    public Texture2D[] Maps {
        get { return this.maps; }
    }

    [Header("Enemies")]
    [SerializeField]
    private int numberOfEnemies;
    public int NumberOfEnemies {
        get { return this.numberOfEnemies; }
    }
    [SerializeField]
    private GameObject[] enemies;
    public GameObject[] Enemies {
        get { return this.enemies; }
    }

    [Header("Items")]
    [SerializeField]
    private int numberOfItems;
    public int NumberOfItems {
        get { return this.numberOfItems; }
    }

}
