using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSystem : MonoBehaviour
{

    public static TargetSystem Instance;

    private void Awake() {
        if (Instance == null) {
            Instance = this;            
        }else{
            Destroy(gameObject);            
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //for BASIC Attacks
    public void FindValidTargets (HeroLogic attacker)
    {
       // Debug.Log ("Find Valid Targets");

       //This is causing problems in displaying targets correctly
       //HideTargets(); 

        List<GameObject> validTargetsList = new List<GameObject>();
        List<GameObject> tempTargetsList = HeroManager.Instance.AllLivingEnemiesList(attacker.gameObject);

        //from opponents, search for taunt. then add to valid targets list
        foreach(GameObject go in tempTargetsList)
        {
            if (go.GetComponent<Taunt>() != null && go.GetComponent<Taunt>().enabled == true)
            {
                validTargetsList.Add(go);
            }
        }
        //if no taunt was found, copy all opponents list to valid targets list
        if (validTargetsList.Count == 0)
        {
            validTargetsList = tempTargetsList;
        }

        VisualSystem.Instance.ShowTargets(validTargetsList); // highlight and tag valid targets

    }

    //for SKILLS

    public void ShowValidTargets(SkillComponent skill)
    {
        HideTargets();
        List<GameObject> validTargetsList = FindValidTargets(skill);

        VisualSystem.Instance.ShowTargets(validTargetsList);

    }

    public void HideTargets()
    {
        /*
        foreach(GameObject go in HeroManager.Instance.heroList)
        {
             VisualSystem.Instance.HideTargetGlow(go);
        }
        */

        VisualSystem.Instance.HideTargetsGlow(HeroManager.Instance.LivingHeroesList());

    }

    public List<GameObject> FindValidTargets(SkillComponent skill)
    {
        List<GameObject> validTargetsList = new List<GameObject>();

        List<GameObject> tempTargetsList = new List<GameObject>();

        switch (skill.skillTarget)
        {
            case SkillTarget.ENEMY:
                validTargetsList = new List<GameObject>();
                tempTargetsList = HeroManager.Instance.AllLivingEnemiesList(skill.heroLogic.gameObject);               

                //from opponents, search for taunt. then add to valid targets list
                foreach(GameObject go in tempTargetsList)
                {
                    if (go.GetComponent<Taunt>() != null && go.GetComponent<Taunt>().enabled == true)
                    {
                        validTargetsList.Add(go);
                    }
                }
                //if no taunt was found, copy all opponents list to valid targets list
                if (validTargetsList.Count == 0)
                {
                    validTargetsList = tempTargetsList;
                }

                break;

            case SkillTarget.ANY_ENEMY:
                validTargetsList = HeroManager.Instance.AllLivingEnemiesList(skill.heroLogic.gameObject);            
                break;

            case SkillTarget.OTHER_ALLY:
                validTargetsList = HeroManager.Instance.OtherLivingAlliesList(skill.heroLogic.gameObject);               
                break;

            case SkillTarget.ANY_ALLY:
                validTargetsList = HeroManager.Instance.AllLivingAlliesList(skill.heroLogic.gameObject);
                break;

            case SkillTarget.ANY:
                validTargetsList = HeroManager.Instance.LivingHeroesList();               
                break;          

        }
        return validTargetsList;

    }

    public GameObject TargetRandomEnemy(GameObject randomTarget)
    {
        var enemiesList = HeroManager.Instance.AllLivingEnemiesList(randomTarget); 

        int index = Random.Range(0, enemiesList.Count);

        var randomEnemy = enemiesList[index];

        return randomEnemy;        
        
    }

}
