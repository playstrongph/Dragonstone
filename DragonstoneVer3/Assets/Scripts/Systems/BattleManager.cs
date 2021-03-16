using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum GameState { INTRO, MAIN, END }
public delegate void OnStateChangeHandler();
public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;
    public GameState gameState;

    public CoroutineTree tree = new CoroutineTree();
    

    private void Awake() {
        if (Instance == null) {
            Instance = this;            
        }else{
            Destroy(gameObject);            
        }
    }

    ///<Summary>
    ///coroutine for initializing battle
    ///systems
    ///load players - no need for Player turns since ATB will dictate the order of heroes.
    ///load heroes - instantiate herocard prefabs under the Player GO
    ///load skills - instantiate as components of the heroes
    ///load turns - 
    ///load timers
    ///</Summary>
    void Start()
    {
        SetGameState(GameState.INTRO);

        //StartCoroutine(InitBattle());

       tree.Start();
       //tree.AddRoot(RootCoroutine());   
       tree.AddRoot(InitBattle(tree));


      
    }

    public void SetGameState(GameState state){
        this.gameState = state;
        //OnStateChange();
    }

    //  IEnumerator RootCoroutine()
    // {    

    //     tree.AddCurrent(InitBattle(tree));
    //     tree.CorQ.CoroutineCompleted();              
    //     yield return null;
    // }

    IEnumerator InitBattle(CoroutineTree tree)
    {
       
        //yield return (StartCoroutine(InitPlayers(tree)));
        tree.AddCurrent(InitPlayers(tree));
        
        //yield return (StartCoroutine(InitHeroes(tree)));
        tree.AddCurrent(InitHeroes(tree));

        //yield return (StartCoroutine(InitBasicAttack(tree)));
        tree.AddCurrent(InitBasicAttack(tree));
        
        //yield return (StartCoroutine(InitSkills()));
        tree.AddCurrent(InitSkills(tree));
        
        //yield return (StartCoroutine(EquipHeroes()));
        tree.AddCurrent(EquipHeroes(tree));
        
        //yield return (StartCoroutine(InitTurn()));
        tree.AddCurrent(InitTurn(tree));

        //yield return (StartCoroutine(InitTimers()));
        tree.AddCurrent(InitTimers(tree));

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator InitPlayers(CoroutineTree tree) //placeholder: can use this for intro/loading animations
    {
        //Debug.Log("InitPlayers");

        tree.CorQ.CoroutineCompleted();
        yield return null;
        
    }    

    IEnumerator InitHeroes(CoroutineTree tree)
    {
        //Debug.Log("InitHeroes");
        GlobalSettings.Instance.TopPlayer.InitializeHeroes();
        GlobalSettings.Instance.LowPlayer.InitializeHeroes();


        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator InitBasicAttack(CoroutineTree tree)
    {
        //Debug.Log("InitBasicAttack");
        for(int i=0; i<HeroManager.Instance.heroList.Count; i++)
        {
            HeroManager.Instance.heroList[i].GetComponent<HeroLogic>().InitBasicAttack();
        }

        tree.CorQ.CoroutineCompleted();

        yield return null;
    }    

    IEnumerator InitSkills(CoroutineTree tree)
    {
        //Debug.Log("InitSkills");
        for(int i=0; i<HeroManager.Instance.heroList.Count; i++)
        {
            HeroManager.Instance.heroList[i].GetComponent<HeroLogic>().InitSkills(HeroManager.Instance.heroList[i]);
        }

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }    

    IEnumerator EquipHeroes(CoroutineTree tree)
    {
        DelegateManager.Instance.StartOfGame();

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator InitTurn(CoroutineTree tree)
    {
        TurnManager.Instance.InitHeroTimers();

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }  

    IEnumerator InitTimers(CoroutineTree tree)
    {
        yield return null;
        
        SetGameState(GameState.MAIN);
        TurnManager.Instance.StartTick();
        tree.CorQ.CoroutineCompleted();
    }


}
