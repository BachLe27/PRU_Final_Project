using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Loot : ScriptableObject
{
    public Sprite lootSprite;
    public string lootName;
    public int dropChange;
    public int value;
    public Loot(string lootName, int dropChance)
    {
        this.lootName = lootName;
        this.dropChange = dropChance;
    }
}
