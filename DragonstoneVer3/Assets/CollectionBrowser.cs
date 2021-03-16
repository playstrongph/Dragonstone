using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionBrowser : MonoBehaviour
{
    public Transform[] Slots;
    public GameObject CreatureMenuPrefab;
    private List<GameObject> CreatedCards = new List<GameObject>();

    private int _pageIndex = 0;
    public int PageIndex 
    {
        get{ return _pageIndex;}
        set
        {
            _pageIndex = value;
            UpdatePage();
        }
    }

    void Start()
    {
        DeckBuildingScreen.Instance.ShowScreenForCollectionBrowsing();
    }

    public void ShowCollectionForBrowsing()
    {
        ShowCards(0);
       // DeckBuildingScreen.Instance.TabsScript.NeutralTabWhenCollectionBrowsing.Select(instant: true);   
       // DeckBuildingScreen.Instance.TabsScript.SelectTab(DeckBuildingScreen.Instance.TabsScript.NeutralTabWhenCollectionBrowsing, instant: true);
    }

    public void ClearCreatedCards()
    {
        while(CreatedCards.Count>0)
        {
            GameObject g = CreatedCards[0];
            CreatedCards.RemoveAt(0);
            Destroy(g);
        }
    }
/*
    public void UpdateQuantitiesOnPage()
    {
        foreach (GameObject card in CreatedCards)
        {
            AddCardToDeck addCardComponent = card.GetComponent<AddCardToDeck>();
            addCardComponent.UpdateQuantity();
        }
    }
*/

    // a method to display cards based on all the selected parameters
    public void UpdatePage()
    {
        ShowCards(_pageIndex);
    }

    private void ShowCards(int pageIndex = 0)
    {

        _pageIndex = pageIndex;

        
        List<HeroAsset> CardsOnThisPage = PageSelection(pageIndex);

        // clear created cards list 
        ClearCreatedCards();

        if (CardsOnThisPage.Count == 0)
            return;
        
        // Debug.Log(CardsOnThisPage.Count);

        for (int i = 0; i < CardsOnThisPage.Count; i++)
        {
            GameObject newMenuCard;
            newMenuCard = Instantiate(CreatureMenuPrefab, Slots[i].position, Quaternion.identity) as GameObject;
            newMenuCard.transform.SetParent(this.transform);
            newMenuCard.transform.localScale = new Vector3(50,50,0);

            CreatedCards.Add(newMenuCard);

            CardManager manager = newMenuCard.GetComponent<CardManager>();
            manager.heroAsset = CardsOnThisPage[i];
            manager.ReadCreatureFromAsset();

            AddCardToDeck addCardComponent = newMenuCard.GetComponent<AddCardToDeck>();
            addCardComponent.SetCardAsset(CardsOnThisPage[i]);
            addCardComponent.UpdateQuantity();
        }
    }

    public void Next()
    {
        if (PageSelection(_pageIndex+1).Count == 0)
            return;
        
        ShowCards( _pageIndex+1);
    }

    public void Previous()
    {
        if (_pageIndex == 0)
            return;

        ShowCards(_pageIndex-1);
    }

    // Returns a list with assets of cards that we have to show on page with pageIndex that. Selects cards that satisfy all the other parameters (rarity, manaCost, etc...)
    private List<HeroAsset> PageSelection(int pageIndex = 0)
    {
        List<HeroAsset> returnList = new List<HeroAsset>();

        // obtain cards from collection that satisfy all the selected criteria
        List<HeroAsset> cardsToChooseFrom = CardCollection.Instance.GetAllCards();

        // if there are enough cards so that we can show some cards on page with pageIndex
        // otherwise an empty list will be returned
        if (cardsToChooseFrom.Count > pageIndex * Slots.Length)
        {
            // the following for loop has 2 conditions for counter i:
            // 1) i < cardsToChooseFrom.Count - pageIndex * Slots.Length checks that we did not run out on cards on the last page 
            // (for example, there are 10 slots on the page, but we only have to show 5 cards) 
            // 2) i < Slots.Length checks that we have reached the limit of cards to display on one page (filled the whole page)
            for (int i = 0; (i < cardsToChooseFrom.Count - pageIndex * Slots.Length && i < Slots.Length); i++)
            {
                returnList.Add(cardsToChooseFrom[pageIndex * Slots.Length + i]);
            }
        }

        return returnList;
    }

}