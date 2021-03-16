using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSettings : MonoBehaviour
{

    [Header("Players")]
    public Player TopPlayer;
    public Player LowPlayer;
    public int HeroesCount;
    public int timerFull;

    public GameObject HeroPrefab;
    public GameObject SkillPanelPrefab;
    public GameObject BuffCardPrefab;

    public GameObject SkillPanelTransform;
    public GameObject TopSkillPanelTransform;
    public GameObject HeroPanelPrefab;
    public GameObject TopHeroPanelTransform;
    public GameObject LowHeroPanelTransform; 
    public GameObject DamageEffectPrefab; 

     public GameObject FloatingTextPrefab; 

    public GameObject skillPreviewLocation;

    public List<GameObject> specialEffects;

    public int commandID = 0;


    public List<BuffAsset> buffAssetsList;
    public static GlobalSettings Instance;

      public Queue<IEnumerator> coroutineQueue = new Queue<IEnumerator>();
 
    private void Awake() {
        if (Instance == null) {
            Instance = this;            
        }else{
            Destroy(gameObject);            
        }
    }

    void Start()
    {
         StartCoroutine(CoroutineCoordinator());
    }

    IEnumerator CoroutineCoordinator()
    {
        while (true)
        {
            while (coroutineQueue.Count >0)
                yield return StartCoroutine(coroutineQueue.Dequeue());
            yield return null;
        }
    }

     public IEnumerator Wait(float seconds)
        {
            yield return new WaitForSeconds(seconds);
        }

}
