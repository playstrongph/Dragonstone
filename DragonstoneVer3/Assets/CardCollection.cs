using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CardCollection : MonoBehaviour
{
    public bool Testing_Own_All = true;
    public int DefaultNumberOfBasicCards = 1; // how many cards of basic rarity should a character have by default;

    public static CardCollection Instance;
    private Dictionary<string, HeroAsset > AllCardsDictionary = new Dictionary<string, HeroAsset>();

    public Dictionary<HeroAsset, int> QuantityOfEachCard = new Dictionary<HeroAsset, int>();

    private HeroAsset[] allCardsArray;

    void Awake()
    {
        Instance = this;

        allCardsArray = Resources.LoadAll<HeroAsset>("Heroes");
        //Debug.Log(allCardsArray.Length);
        foreach (HeroAsset ca in allCardsArray)
        {
  
            if (!AllCardsDictionary.ContainsKey(ca.name))
            {
                AllCardsDictionary.Add(ca.name, ca);
            }
        }      

        LoadQuantityOfCardsFromPlayerPrefs();
    }

    private void LoadQuantityOfCardsFromPlayerPrefs()
    {
        // TODO: load only cards from the non-basic set. Basic set should always have quantities set to some standard number, not disenchantable 
/*
        foreach (HeroAsset ca in allCardsArray)
        {
            // quantity of basic cards should not be affected:
            if(ca.Rarity == RarityOptions.Basic)
                QuantityOfEachCard.Add(ca, DefaultNumberOfBasicCards);            
            else if (PlayerPrefs.HasKey("NumberOf" + ca.name))
                QuantityOfEachCard.Add(ca, PlayerPrefs.GetInt("NumberOf" + ca.name));
            else
                QuantityOfEachCard.Add(ca, 0);
        }
*/
        foreach (HeroAsset ca in allCardsArray)
        {
            // quantity of basic cards should not be affected:
/*            if(ca.Rarity == RarityOptions.Common)
                QuantityOfEachCard.Add(ca, DefaultNumberOfBasicCards);            
            else if (PlayerPrefs.HasKey("NumberOf" + ca.name))
                QuantityOfEachCard.Add(ca, PlayerPrefs.GetInt("NumberOf" + ca.name));
            else
            {
                if (Testing_Own_All)
*/                    QuantityOfEachCard.Add(ca, 1);

//                else
//                    QuantityOfEachCard.Add(ca, 0);
//            }
        }


    }

    private void SaveQuantityOfCardsIntoPlayerPrefs()
    {

/*
        foreach (HeroAsset ca in allCardsArray)
        {
            if (ca.Rarity == RarityOptions.Common)
                PlayerPrefs.SetInt("NumberOf" + ca.name, DefaultNumberOfBasicCards);
            else
                PlayerPrefs.SetInt("NumberOf" + ca.name, QuantityOfEachCard[ca]);
        }
*/

    }

    void OnApplicationQuit()
    {
        SaveQuantityOfCardsIntoPlayerPrefs();
    }

    public HeroAsset GetCardAssetByName(string name)
    {        
        if (AllCardsDictionary.ContainsKey(name))  // if there is a card with this name, return its HeroAsset
            return AllCardsDictionary[name];
        else        // if there is no card with name
            return null;
    }	

//    public List<HeroAsset> GetCardsOfCharacter(CharacterAsset asset)
//    {   
        /*
        // get cards that blong to a particular character or neutral if asset == null
        var cards = from card in allCardsArray
                                    where card.CharacterAsset == asset
                                    select card; 
        
        var returnList = cards.ToList<HeroAsset>();
        returnList.Sort();
        */
//        return GetCards(true, true, false, RarityOptions.Basic, asset);
//    }

//    public List<HeroAsset> GetCardsWithRarity(RarityOptions rarity)
//    {
        /*
        // get neutral cards
        var cards = from card in allCardsArray
                where card.Rarity == rarity
            select card; 

        var returnList = cards.ToList<HeroAsset>();
        returnList.Sort();

        return returnList;
        */
        
//        return GetCards(true, false, true, rarity);

//    }

    /// the most general method that will use multiple filters


//DS workaround to Sort problem

    private static int CompareListByName(HeroAsset i1, HeroAsset i2)
        {
            return i1.name.CompareTo(i2.name); 
        }

    public List<HeroAsset> GetAllCards()
    {

        List<HeroAsset> listca = new List<HeroAsset>();
        foreach (HeroAsset ca in allCardsArray)
        {
            listca.Add(ca);
        }
        return listca;

    }
/*
    public List<HeroAsset> GetRarityCards(RarityOptions rarity)
    {

        List<HeroAsset> listca = new List<HeroAsset>();
        foreach (HeroAsset ca in allCardsArray)
        {
            if (ca.Rarity == rarity)
                listca.Add(ca);
        }
        return listca;

    }
*/
}
