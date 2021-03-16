using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HeroManager : MonoBehaviour
{
    public static HeroManager Instance;

    public List<GameObject> heroList;
    public List<GameObject> deadHeroesList;
    // Start is called before the first frame update
    private void Awake() {
        if (Instance == null) {
            Instance = this;            
        }else{
            Destroy(gameObject);            
        }
    }

    public List<GameObject> AllAlliesList(GameObject hero)
    {
        List<GameObject> alliedHeroes = new List<GameObject>();
        foreach (GameObject go in AllHeroesList())
        {

            if (go.tag == hero.tag)
            {
                alliedHeroes.Add (go);
            }        
        }
        return alliedHeroes;
    }

    public List<GameObject> AllEnemiesList(GameObject hero)
    {
        List<GameObject> enemyHeroes = new List<GameObject>();
        foreach (GameObject go in AllHeroesList())
        {

            if (go.tag != hero.tag)
            {
                enemyHeroes.Add (go);
            }        
        }
        return enemyHeroes;
    }

    public List<GameObject> OtherLivingAlliesList(GameObject hero)
    {
        List<GameObject> alliedHeroes = new List<GameObject>();
        foreach (GameObject go in LivingHeroesList())
        {

            if (go != hero && go.tag == hero.tag)
            {
                alliedHeroes.Add (go);
            }        
        }
        return alliedHeroes;
    }


    public List<GameObject> AllLivingAlliesList(GameObject hero)
    {
        List<GameObject> alliedHeroes = new List<GameObject>();
        foreach (GameObject go in LivingHeroesList())
        {

            if (go.tag == hero.tag)
            {
                alliedHeroes.Add (go);
            }        
        }
        return alliedHeroes;
    }

    
    public List<GameObject> AllLivingEnemiesList(GameObject hero)
    {
        List<GameObject> enemyHeroes = new List<GameObject>();
        foreach (GameObject go in LivingHeroesList())
        {

            if (go.tag != hero.tag)
            {
                enemyHeroes.Add (go);
            }        
        }
        return enemyHeroes;
    }

    public List<GameObject> AllHeroesList()
    {
        return heroList;
    }

    public List<GameObject> DeadHeroesList()
    {
        return deadHeroesList;
    }

    public List<GameObject> DeadEnemiesList(GameObject hero)
    {
        List<GameObject> enemyHeroes = new List<GameObject>();
        foreach (GameObject go in DeadHeroesList())
        {

            if (go.tag != hero.tag)
            {
                enemyHeroes.Add (go);
            }        
        }
        return enemyHeroes;
    }

    public List<GameObject> DeadAlliesList(GameObject hero)
    {
        List<GameObject> alliedHeroes = new List<GameObject>();
        foreach (GameObject go in DeadHeroesList())
        {

            if (go != hero && go.tag == hero.tag)
            {
                alliedHeroes.Add (go);
            }        
        }
        return alliedHeroes;
    }

    public void AddHeroToDeadHeroesList(GameObject hero)
    {
        deadHeroesList.Add(hero);
    }

    public void RemoveHeroFromDeadHeroesList(GameObject hero)
    {
        deadHeroesList.Remove(hero);
    }

    public List<GameObject> LivingHeroesList()
    {  
        List<GameObject> livingHeroes = new List<GameObject>();
        foreach (GameObject go in AllHeroesList())
        {

            if (go.GetComponent<HeroLogic>().Health > 0)
            {
                livingHeroes.Add (go);
            }        
        }
        return livingHeroes;
    }

    public GameObject RandomHeroFromList(List<GameObject> heroList)
    {
        return LivingHeroesList()[Random.Range(0,LivingHeroesList().Count)];
    }

    public GameObject WeakestHero()
    {
        List<GameObject> sortList = SortHeroesByHealth(LivingHeroesList());
        return sortList[0];
    }

    public GameObject WeakestOtherAlly(GameObject hero)
    {
        List<GameObject> sortList = SortHeroesByHealth(OtherLivingAlliesList(hero));
        return sortList[0];
    }

    public GameObject WeakestEnemy(GameObject hero)
    {
        List<GameObject> sortList = SortHeroesByHealth(AllLivingEnemiesList(hero));
        return sortList[0];
    }

    public GameObject StrongestHero()
    {
        List<GameObject> sortList = SortHeroesByHealth(LivingHeroesList());
        return sortList[sortList.Count-1];
    }

    public GameObject StrongestOtherAlly(GameObject hero)
    {
        List<GameObject> sortList = SortHeroesByHealth(OtherLivingAlliesList(hero));
        return sortList[sortList.Count-1];
    }

    public GameObject StrongestEnemy(GameObject hero)
    {
        List<GameObject> sortList = SortHeroesByHealth(AllLivingEnemiesList(hero));
        return sortList[sortList.Count-1];
    }


    public List<GameObject> SortHeroesByHealth(List<GameObject> heroList)
    {
        var returnList = heroList;
        returnList.Sort(CompareListByHealth);
        return returnList;
    }
    private static int CompareListByHealth(GameObject i1, GameObject i2)
    {
        return i1.GetComponent<HeroLogic>().health.CompareTo(i2.GetComponent<HeroLogic>().health); 
    }



    public GameObject SlowestHero()
    {
        List<GameObject> sortList = SortHeroesBySpeed(LivingHeroesList());
        return sortList[0];
    }

    public GameObject SlowestOtherAlly(GameObject hero)
    {
        List<GameObject> sortList = SortHeroesBySpeed(OtherLivingAlliesList(hero));
        return sortList[0];
    }

    public GameObject SlowestEnemy(GameObject hero)
    {
        List<GameObject> sortList = SortHeroesBySpeed(AllLivingEnemiesList(hero));
        return sortList[0];
    }

    public GameObject FastestHero()
    {
        List<GameObject> sortList = SortHeroesBySpeed(LivingHeroesList());
        return sortList[sortList.Count-1];
    }

    public GameObject FastestOtherAlly(GameObject hero)
    {
        List<GameObject> sortList = SortHeroesBySpeed(OtherLivingAlliesList(hero));
        return sortList[sortList.Count-1];
    }

    public GameObject FastestEnemy(GameObject hero)
    {
        List<GameObject> sortList = SortHeroesBySpeed(AllLivingEnemiesList(hero));
        return sortList[sortList.Count-1];
    }

    public List<GameObject> SortHeroesBySpeed(List<GameObject> heroList)
    {
        var returnList = heroList;
        returnList.Sort(CompareListBySpeed);
        return returnList;
    }
    private static int CompareListBySpeed(GameObject i1, GameObject i2)
    {
        return i1.GetComponent<HeroLogic>().Speed.CompareTo(i2.GetComponent<HeroLogic>().Speed); 
    }

    public bool HasBuff (BuffName buffName, GameObject hero)
    {
        foreach (BuffComponent bc in hero.GetComponents<BuffComponent>())
        {
            if (bc.buffAsset.buffBasicInfo.buffName == buffName)
                {
                    return true;
                }
        }
        return false;
    }

    public bool HasAnyBuff (GameObject hero)
    {
        foreach (BuffComponent bc in hero.GetComponents<BuffComponent>())
        {
            if (bc.buffAsset.buffBasicInfo.buffType == BuffType.BUFF)
                {
                    return true;
                }
        }
        return false;
    }

    public bool HasAnyDebuff (GameObject hero)
    {
        foreach (BuffComponent bc in hero.GetComponents<BuffComponent>())
        {
            if (bc.buffAsset.buffBasicInfo.buffType == BuffType.DEBUFF)
                {
                    return true;
                }
        }
        return false;
    }

    public List<GameObject> AllHeroesWithBuff (BuffName buffName)
    {
        List<GameObject> heroList = new List<GameObject>();
        foreach (GameObject go in LivingHeroesList())
        {
            foreach (BuffComponent bc in go.GetComponents<BuffComponent>())
            {
                if (HasBuff(buffName,go))
                {
                    heroList.Add(go);
                    break;
                }
            }
        }
        return heroList;
    }

    public List<GameObject> OtherAlliesWithBuff (BuffName buffName, GameObject hero)
    {
        List<GameObject> heroList = new List<GameObject>();
        foreach (GameObject go in OtherLivingAlliesList(hero))
        {
            foreach (BuffComponent bc in go.GetComponents<BuffComponent>())
            {
                if (HasBuff(buffName,go))
                {
                    heroList.Add(go);
                    break;
                }
            }
        }
        return heroList;
    }

    public List<GameObject> EnemiesWithBuff (BuffName buffName, GameObject hero)
    {
        List<GameObject> heroList = new List<GameObject>();
        foreach (GameObject go in AllLivingEnemiesList(hero))
        {
            foreach (BuffComponent bc in go.GetComponents<BuffComponent>())
            {
                if (HasBuff(buffName,go))
                {
                    heroList.Add(go);
                    break;
                }
            }
        }
        return heroList;
    }



    public List<GameObject> AllHeroesWithAnyBuff ()
    {
        List<GameObject> heroList = new List<GameObject>();
        foreach (GameObject go in LivingHeroesList())
        {
            if (HasAnyBuff(go))
            {
                heroList.Add(go);
            }
        }
        return heroList;
    }

    public List<GameObject> OtherAlliesWithAnyBuff (GameObject hero)
    {
        List<GameObject> heroList = new List<GameObject>();
        foreach (GameObject go in OtherLivingAlliesList(hero))
        {
            if (HasAnyBuff(go))
            {
                heroList.Add(go);
            }
        }
        return heroList;
    }

    public List<GameObject> EnemiesWithAnyBuff (GameObject hero)
    {
        List<GameObject> heroList = new List<GameObject>();
        foreach (GameObject go in AllLivingEnemiesList(hero))
        {
            if (HasAnyBuff(go))
            {
                heroList.Add(go);
            }
        }
        return heroList;
    }  

    public List<GameObject> AllHeroesWithAnyDebuff ()
    {
        List<GameObject> heroList = new List<GameObject>();
        foreach (GameObject go in LivingHeroesList())
        {
            if (HasAnyDebuff(go))
            {
                heroList.Add(go);
            }
        }
        return heroList;
    }

    public List<GameObject> OtherAlliesWithAnyDebuff (GameObject hero)
    {
        List<GameObject> heroList = new List<GameObject>();
        foreach (GameObject go in OtherLivingAlliesList(hero))
        {
            if (HasAnyDebuff(go))
            {
                heroList.Add(go);
            }
        }
        return heroList;
    }

    public List<GameObject> EnemiesWithAnyDebuff (GameObject hero)
    {
        List<GameObject> heroList = new List<GameObject>();
        foreach (GameObject go in AllLivingEnemiesList(hero))
        {
            if (HasAnyDebuff(go))
            {
                heroList.Add(go);
            }
        }
        return heroList;
    }  

    public List<BuffComponent> GetAnyBuffs (GameObject hero)
    {
        return hero.GetComponents<BuffComponent>().ToList();
    }
    public List<BuffComponent> GetOnlyBuffs (GameObject hero)
    {
        List<BuffComponent> buffsList = new List<BuffComponent>();
        foreach(BuffComponent bc in hero.GetComponents<BuffComponent>())
        {
            if (bc.buffAsset.buffBasicInfo.buffType == BuffType.BUFF)
            {
                buffsList.Add(bc);
            }
        }
        return buffsList;
    }
    public List<BuffComponent> GetOnlyDebuffs (GameObject hero)
    {
        List<BuffComponent> debuffsList = new List<BuffComponent>();
        foreach(BuffComponent bc in hero.GetComponents<BuffComponent>())
        {
            if (bc.buffAsset.buffBasicInfo.buffType == BuffType.DEBUFF)
            {
                debuffsList.Add(bc);
            }
        }
        return debuffsList;
    }
    public List<BuffComponent> GetBuff (BuffName buffName, GameObject hero)
    {
        List<BuffComponent> buffsList = new List<BuffComponent>();
        foreach(BuffComponent bc in hero.GetComponents<BuffComponent>())
        {
            if (bc.buffAsset.buffBasicInfo.buffName == buffName)
            {
                buffsList.Add(bc);
            }
        }
        return buffsList;
    }


}
