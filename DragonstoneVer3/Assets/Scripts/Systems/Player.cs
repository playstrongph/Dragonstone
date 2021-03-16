using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : MonoBehaviour
{

    public Deck deck;
    // Start is called before the first frame update

    public void InitializeHeroes()
    {
        for (int i=0; i<deck.cards.Count; i++)
        {
            GameObject hero = GameObject.Instantiate (GlobalSettings.Instance.HeroPrefab, transform);
            hero.tag = tag;
            foreach (Transform t in hero.GetComponentsInChildren<Transform>())
                t.tag = tag;

            hero.name = deck.cards[i].name;

            hero.GetComponent<HeroLogic>().heroAsset = deck.cards[i];
            hero.GetComponent<HeroLogic>().ReadCreatureFromAsset();
            
            hero.GetComponent<CardManager>().heroAsset = deck.cards[i];
            hero.GetComponent<CardManager>().ReadCreatureFromAsset();
            
            HeroManager.Instance.heroList.Add(hero);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
