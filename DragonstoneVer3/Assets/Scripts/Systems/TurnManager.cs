using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HeroTimer
{
    public HeroLogic hero;
    public float timerValue;
    public float timerValuePercentage;
}

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;

    public int timerFull;
    public int speedConstant = 5;
    public bool tick = false; //only for Inspector
    public bool freezeTick = false;
    public List<HeroTimer> heroTimerList = new List<HeroTimer>();

    public List<HeroTimer> activeHeroesQueue = new List<HeroTimer>();
    public delegate void OnTurnManagerTick ();
    public event OnTurnManagerTick turnManagerTick;

    CoroutineTree tree = new CoroutineTree();

    



    private void Awake() {
        if (Instance == null) {
            Instance = this;            
        }else{
            Destroy(gameObject);            
        }
    }   
    void Start()
    {
        timerFull = GlobalSettings.Instance.timerFull;
        turnManagerTick += UpdateHeroTimers;       
        
    }    

    ///<Summary>
    ///Starting Point for TurnManager.
    ///</Summary>
    public void StartTick()
    {
        //StartCoroutine (RunTick());
        tree.Start();
        tree.AddRoot(RunTick(tree));
        
    }

     IEnumerator RunTick(CoroutineTree tree)
	{
        freezeTick = false;
        //loop while no Hero is active yet
        while(!freezeTick)
        {
            //tick only used for inspector display that the clock is working
            tick = true;
			//yield return new WaitForSeconds(0.5f);
            tick = false;
            //yield return new WaitForSeconds(0.5f);
            yield return null;
            //subscribe hero ATB to this event
            turnManagerTick();

            
        }
        
        //StartCoroutine(AllowHeroActions());
        tree.AddCurrent(AllowHeroActions(tree));
        

        tree.CorQ.CoroutineCompleted();
        yield return null;
	}

    IEnumerator AllowHeroActions(CoroutineTree tree)
    {
        //yield return StartCoroutine(SortActiveHeroesByATB());        
        tree.AddCurrent(SortActiveHeroesByATB(tree));


        //yield return StartCoroutine(SetActiveHero());
        tree.AddCurrent(SetActiveHero(tree));

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

     IEnumerator SortActiveHeroesByATB(CoroutineTree tree)
    {
        var returnList = activeHeroesQueue;
        returnList.Sort(CompareListByATB);

        yield return returnList;
        tree.CorQ.CoroutineCompleted();
    }

    IEnumerator SetActiveHero(CoroutineTree tree)    
    {

        int i = activeHeroesQueue.Count - 1;
        var activeHero = activeHeroesQueue[i].hero;

        //StartCoroutine(activeHero.SetHeroActiveCoroutine());
        tree.AddCurrent(activeHero.SetHeroActiveCoroutine(tree));

        if(activeHero.GetComponent<STUN>() != null)
        {
            tree.AddCurrent(activeHero.GetComponent<STUN>().CauseBuffEffectEvent(activeHero.gameObject, tree));             
        }

        else if(activeHero.GetComponent<SLEEP>() != null)
        {
            tree.AddCurrent(activeHero.GetComponent<SLEEP>().CauseBuffEffectEvent(activeHero.gameObject,tree));
        }

        tree.CorQ.CoroutineCompleted();
        yield return null;
        
    }



    public IEnumerator EndTurn(GameObject hero, CoroutineTree tree)
    {
        var heroLogic = hero.GetComponent<HeroLogic>();

        if(hero.GetComponent<ExtraTurn>() != null)        
        {
            VisualSystem.Instance.CreateFloatingText("Extra Turn", hero, Color.yellow);                               
            Destroy(hero.GetComponent<ExtraTurn>());            
            heroLogic.extraTurn = true; 
        }
        else
        {            
            heroLogic.extraTurn = false;        

            //StartCoroutine(NextActiveHero(tree));
            tree.AddCurrent(NextActiveHero(tree))                                    ;
        }       

       

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }


    ///<Summary>
    //This is called by End Turn - which is independently called by UseSKill and BasicAttack
    ///This is independent of hero start turn
    ///</Summary>
    public IEnumerator NextActiveHero(CoroutineTree tree)
    {       
        if (!isThereAWinner())
        {
            if (activeHeroesQueue.Count > 0)  //multiple heroes are active
            {
                //StartCoroutine(NextActiveHeroCoroutine(tree));                
                tree.AddCurrent(NextActiveHeroCoroutine(tree));
            }
        }
        else DeclareWinner();
        
        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    

    IEnumerator NextActiveHeroCoroutine(CoroutineTree tree)
    {
        //yield return StartCoroutine(SetHeroInactiveCoroutine());              
        tree.AddCurrent(SetHeroInactiveCoroutine(tree));
        
        //yield return StartCoroutine(SetHeroActiveCoroutine());        
        tree.AddCurrent(SetHeroActiveCoroutine(tree));


        //yield return StartCoroutine(NextActiveHeroComExecComplete());

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator SetHeroInactiveCoroutine(CoroutineTree tree)
    {
                int i = activeHeroesQueue.Count - 1;  
                var currentActiveHero = activeHeroesQueue[i].hero;

                currentActiveHero.SetHeroInactive(tree);


                activeHeroesQueue.RemoveAt(i);

                tree.CorQ.CoroutineCompleted();
                yield return null;
    }

    IEnumerator SetHeroActiveCoroutine(CoroutineTree tree)
    {   
        if (activeHeroesQueue.Count > 0)
           {
             //StartCoroutine(SetActiveHero(tree));
             tree.AddCurrent(SetActiveHero(tree));
           }
           else
           {
             StartTick();
           }
        
        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    // IEnumerator NextActiveHeroComExecComplete()
    // {
    //      Command.CommandExecutionComplete();
    //      yield return null;    
    // }

    bool isThereAWinner()
    {
        bool topPlayerHasHeroes = false;
        bool lowPlayerHasHeroes = false;
        foreach(GameObject go in HeroManager.Instance.LivingHeroesList())
        {
            if (go.tag == "TOP_PLAYER")
            {
                topPlayerHasHeroes = true;
            }
            else if ((go.tag == "LOW_PLAYER"))
            {
                lowPlayerHasHeroes = true;
            }
        }

        if (topPlayerHasHeroes && lowPlayerHasHeroes)
            return false;
        else
            return true;
    }

    void DeclareWinner()
    {
        foreach(GameObject go in HeroManager.Instance.LivingHeroesList())
        {
            if (go.tag == "TOP_PLAYER")
            {
                Debug.Log("There is a winner! TOP_PLAYER");
                break;
            }
            else if ((go.tag == "LOW_PLAYER"))
            {
                Debug.Log("There is a winner! LOW_PLAYER");
                break;
            }
        }
       
    }


    void UpdateHeroTimers()
    {
        foreach(HeroTimer ht in heroTimerList)
        {
            ht.timerValue += ht.hero.Speed*Time.deltaTime*speedConstant;

            if (ht.timerValue >= timerFull)
            {
                freezeTick = true;
                activeHeroesQueue.Add(ht);
            }

            ht.timerValuePercentage = Mathf.FloorToInt(ht.timerValue*100/GlobalSettings.Instance.timerFull);
            

        }
        UpdateHeroTimer(heroTimerList);
    }

    public void UpdateHeroTimer(List<HeroTimer> ht)
    {
        VisualSystem.Instance.UpdateHeroTimer(ht);
    }


   

    private static int CompareListByATB(HeroTimer i1, HeroTimer i2)
    {
        return i1.timerValue.CompareTo(i2.timerValue); 
    }

    

    public void ResetHeroTimer(HeroLogic hero)
    {
        foreach (HeroTimer ht in heroTimerList)
        {
            if (ht.hero == hero)
            {
                ht.timerValue = 0;
                ht.timerValuePercentage = Mathf.FloorToInt(ht.timerValue*100/GlobalSettings.Instance.timerFull);
                
            }
        }
        UpdateHeroTimer(heroTimerList);        
    }

    public void DeactivateHeroTimer(GameObject hero)
    {
        foreach (HeroTimer ht in activeHeroesQueue)
        {
            if (ht.hero.gameObject == hero)
            {
                          
                activeHeroesQueue.Remove(ht);
                break;
            }
        }
        foreach (HeroTimer ht in heroTimerList)
        {
            if (ht.hero.gameObject == hero)
            {
                heroTimerList.Remove(ht);
                break;
            }
        }

    }

    public void ReactivateHeroTimer(GameObject hero)
    {

      //HeroManager.Instance.heroList.Add(hero);
    
            HeroTimer ht = new HeroTimer();
            ht.hero = hero.GetComponent<HeroLogic>();
            ht.timerValue = 0;
            heroTimerList.Add(ht);
      

    }

    


    ///<Summary>
    /// These are the methods that doesn't need to be sequential coroutines
    ///</Summary>

    public void InitHeroTimers()
    {
        foreach (GameObject go in HeroManager.Instance.heroList)
        {
            HeroTimer ht = new HeroTimer();
            ht.hero = go.GetComponent<HeroLogic>();
            ht.timerValue = 0;
            heroTimerList.Add(ht);
            
            ht.hero.heroTimer = ht;  //create reference of heroTimer to hero 
            
        }
    }

    ///<Summary>
    /// Used by the End Turn button in BattleScene
    ///</Summary>
    public void EndTurnButton()
    {
        tree.Start();
        tree.AddRoot(NextActiveHeroCoroutine(tree));
    }

   



}
