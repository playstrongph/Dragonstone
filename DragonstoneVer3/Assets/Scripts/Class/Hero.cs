using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Hero
{
    public string name;
    public Sprite heroImage;
    public string description;
    //public List<HeroAttributes> heroAttributes;
    public int heroLevel;
    public Rarity rarity;
    public Faction faction;
    public CreatureType creatureType;

    public int health;
    public int attack;
    public int chance;
    public int speed;
    public int armor;
    public bool taunt = false;



}
