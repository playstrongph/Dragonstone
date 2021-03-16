using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffAction
{
    ADDNEW,
    UPDATE,

    CRIPPLE,

    IMMUNITY,

    IMMUNITYPASSIVE,
    ANTIBUFF

}

public class BuffSystem : MonoBehaviour
{
    public static BuffSystem Instance;

    public List<BuffAsset> buffAssetsList;

    public List<BuffAsset> debuffAssetsList;

    public List<GameObject> buffAnimations;

    public List<GameObject> debuffAnimations;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator AddBuff(GameObject attacker, GameObject target, BuffName buffName, int buffCounter, CoroutineTree tree)
    {
        /// <TODO> these events should only add IEnumerators to the tree
        attacker.GetComponent<HeroEvents>().BeforeHeroGrantsBuff(attacker, target, buffName, buffCounter);
        target.GetComponent<HeroEvents>().BeforeHeroReceivesBuff(attacker, target, buffName, buffCounter);

        tree.AddCurrent(BuffHandler(attacker, target, buffName, buffCounter, tree)); 
        
        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    public IEnumerator AddRandomBuff(GameObject attacker, GameObject target, int buffCounter, CoroutineTree tree)
    {   

        var buffList = BuffSystem.Instance.buffAssetsList;
        var randomBuffAsset = buffAssetsList[UnityEngine.Random.Range(0, buffList.Count)];               
        var randomBuffName = randomBuffAsset.buffBasicInfo.buffName;
        
        attacker.GetComponent<HeroEvents>().BeforeHeroGrantsBuff(attacker, target, randomBuffName, buffCounter);
        target.GetComponent<HeroEvents>().BeforeHeroReceivesBuff(attacker, target, randomBuffName, buffCounter);

        tree.AddCurrent(BuffHandler(attacker, target, randomBuffName, buffCounter, tree)); 
        
        tree.CorQ.CoroutineCompleted();
        yield return null;
    }
 

    public IEnumerator AddDebuff(GameObject attacker, GameObject target, BuffName buffName, int buffCounter, CoroutineTree tree)
    {
         /// <TODO> these events should only add IEnumerators to the tree
        attacker.GetComponent<HeroEvents>().BeforeHeroInflictsDebuff(attacker, target, buffName, buffCounter);
        target.GetComponent<HeroEvents>().BeforeHeroReceivesDebuff(attacker, target, buffName, buffCounter);

        //logic for counter manipulation
        if (attacker.GetComponents<AddDebuffCounters>() != null)
        {
            foreach (var skillEffect in attacker.GetComponents<AddDebuffCounters>())
            {
                buffCounter = skillEffect.UseSkillEffect(attacker, buffCounter);
            }
        }

        tree.AddCurrent(DebuffHandler(attacker, target, buffName, buffCounter, tree));

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    public IEnumerator AddRandomDebuff(GameObject attacker, GameObject target, int buffCounter, CoroutineTree tree)
    {   

        var debuffList = BuffSystem.Instance.debuffAssetsList;
        var randomDebuffAsset = debuffAssetsList[UnityEngine.Random.Range(0, debuffList.Count)];               
        var randomDebuffName =  randomDebuffAsset.buffBasicInfo.buffName;
        
        attacker.GetComponent<HeroEvents>().BeforeHeroInflictsDebuff(attacker, target, randomDebuffName, buffCounter);
        target.GetComponent<HeroEvents>().BeforeHeroReceivesDebuff(attacker, target, randomDebuffName, buffCounter);

        tree.AddCurrent(DebuffHandler(attacker, target, randomDebuffName, buffCounter, tree)); 
        
        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    

    
    

    public IEnumerator AddDebuffIgnoreImmunity(GameObject attacker, GameObject target, BuffName buffName, int buffCounter, CoroutineTree tree)
    {
        //Pre Debuff Events
        attacker.GetComponent<HeroEvents>().BeforeHeroInflictsDebuff(attacker, target, buffName, buffCounter);
        target.GetComponent<HeroEvents>().BeforeHeroReceivesDebuff(attacker, target, buffName, buffCounter);

        //logic for counter manipulation
        if (attacker.GetComponents<AddDebuffCounters>() != null)
        {
            foreach (var skillEffect in attacker.GetComponents<AddDebuffCounters>())
            {
                buffCounter = skillEffect.UseSkillEffect(attacker, buffCounter);
            }
        }

        tree.AddCurrent(DebuffHandlerIgnoreImmunity(attacker, target, buffName, buffCounter, tree));

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator BuffHandler (GameObject attacker, GameObject target, BuffName buffName, int buffCounter, CoroutineTree tree)
    {

        BuffAction buffAction = GetBuffAction(target, buffName);

        switch (buffAction)
        {
            case BuffAction.ANTIBUFF:
                tree.AddCurrent(BlockBuff(attacker, target, BuffName.ANTI_BUFF, buffCounter, tree));
                break;

            case BuffAction.UPDATE:
                tree.AddCurrent(UpdateBuff(attacker, target, buffName, buffCounter, tree));
                break;

            case BuffAction.ADDNEW:
                tree.AddCurrent(AddNewBuff(attacker, target, buffName, buffCounter, tree));
                break;

            default:
                break;

        }

        tree.CorQ.CoroutineCompleted();
        yield return null;


    }

    IEnumerator DebuffHandler(GameObject attacker, GameObject target, BuffName buffName, int buffCounter, CoroutineTree tree)
    {


        BuffAction debuffAction = GetDebuffAction(attacker, target, buffName);

        switch (debuffAction)
        {
            case BuffAction.CRIPPLE:
                tree.AddCurrent(BlockDebuffAttacker(attacker, target, BuffName.CRIPPLE, tree));
                break;

            case BuffAction.IMMUNITY:
                tree.AddCurrent(BlockDebuff(attacker, target, BuffName.IMMUNITY, buffCounter, tree));
                break;

            case BuffAction.IMMUNITYPASSIVE:
                tree.AddCurrent(BlockDebuffPassive(attacker, target, BuffName.NULL, buffCounter, tree));
                break;

            case BuffAction.UPDATE:
                tree.AddCurrent(UpdateDebuff(attacker, target, buffName, buffCounter, tree));
                break;

            case BuffAction.ADDNEW:
                tree.AddCurrent(AddNewDebuff(attacker, target, buffName, buffCounter, tree));
                break;

            default:
                break;

        }

        tree.CorQ.CoroutineCompleted();
        yield return null;

    }

     IEnumerator DebuffHandlerIgnoreImmunity(GameObject attacker, GameObject target, BuffName buffName, int buffCounter, CoroutineTree tree)
    {


        BuffAction debuffAction = GetDebuffActionIgnoreImmunity(attacker, target, buffName);

        switch (debuffAction)
        {
            case BuffAction.CRIPPLE:
                tree.AddCurrent(BlockDebuffAttacker(attacker, target, BuffName.CRIPPLE, tree));
                break;
            
            case BuffAction.UPDATE:
                tree.AddCurrent(UpdateDebuff(attacker, target, buffName, buffCounter, tree));
                break;

            case BuffAction.ADDNEW:
                tree.AddCurrent(AddNewDebuff(attacker, target, buffName, buffCounter, tree));
                break;

            default:
                break;

        }

        tree.CorQ.CoroutineCompleted();
        yield return null;

    }

    IEnumerator AddNewBuff(GameObject attacker, GameObject target, BuffName buffName, int buffCounter, CoroutineTree tree)
    {

        tree.AddCurrent(CreateBuff(attacker, target, buffName, buffCounter, tree));
        //BuffEvents       
        attacker.GetComponent<HeroEvents>().AfterHeroGrantsBuff(attacker, target, buffName, buffCounter);
        target.GetComponent<HeroEvents>().AfterHeroReceivesBuff(attacker, target, buffName, buffCounter);

        tree.CorQ.CoroutineCompleted();
        yield return null;
       

    }

    IEnumerator AddNewDebuff(GameObject attacker, GameObject target, BuffName buffName, int buffCounter, CoroutineTree tree)
    {

        tree.AddCurrent(CreateDebuff(attacker, target, buffName, buffCounter, tree));

        //BuffEvents
        attacker.GetComponent<HeroEvents>().AfterHeroInflictsDebuff(attacker, target, buffName, buffCounter);        
        target.GetComponent<HeroEvents>().AfterHeroReceivesDebuff(attacker, target, buffName, buffCounter);

        tree.CorQ.CoroutineCompleted();
        yield return null;

    }

    IEnumerator CreateBuff(GameObject attacker, GameObject target, BuffName buffName, int buffCounter, CoroutineTree tree)
    {
        //create new buff       

        BuffAsset ba = buffAssetsList.Find(x => x.buffBasicInfo.buffName == buffName);        

        BuffComponent bc = ba.Use(target, buffCounter); //this is where Add BuffComponent is

        VisualSystem.Instance.CreateFloatingText(ba.buffBasicInfo.description, target, Color.green);        
        VisualSystem.Instance.AddBuffSymbol(bc, buffCounter, target);

        tree.AddCurrent(bc.CauseBuffEffect(tree));       

        tree.CorQ.CoroutineCompleted();
        yield return null;

    }

    IEnumerator CreateDebuff(GameObject attacker, GameObject target, BuffName buffName, int buffCounter, CoroutineTree tree)
    {
        

        BuffAsset ba = debuffAssetsList.Find(x => x.buffBasicInfo.buffName == buffName);
        BuffComponent bc = ba.Use(target, buffCounter);

        VisualSystem.Instance.CreateFloatingText(ba.buffBasicInfo.description, target, Color.red);
        VisualSystem.Instance.AddBuffSymbol(bc, buffCounter, target);  //this is the buffsymobl above the hero              

        tree.AddCurrent(bc.CauseBuffEffect(tree));

        tree.CorQ.CoroutineCompleted();
        yield return null;

    }

    IEnumerator UpdateBuff(GameObject attacker, GameObject target, BuffName buffName, int buffCounter, CoroutineTree tree)
    {

        BuffAsset ba = buffAssetsList.Find(x => x.buffBasicInfo.buffName == buffName);
        VisualSystem.Instance.CreateFloatingText(ba.buffBasicInfo.description, target, Color.green);

        tree.AddCurrent(UpdateBuffStatus(attacker, target, buffName, buffCounter, tree));

        // Buff Events          
        attacker.GetComponent<HeroEvents>().AfterHeroGrantsBuff(attacker, target, buffName, buffCounter);
        target.GetComponent<HeroEvents>().AfterHeroReceivesBuff(attacker, target, buffName, buffCounter);

        tree.CorQ.CoroutineCompleted();
        yield return null;

    }

    IEnumerator UpdateDebuff(GameObject attacker, GameObject target, BuffName buffName, int buffCounter, CoroutineTree tree)
    {

        BuffAsset ba = debuffAssetsList.Find(x => x.buffBasicInfo.buffName == buffName);

        VisualSystem.Instance.CreateFloatingText(ba.buffBasicInfo.description, target, Color.red);

        tree.AddCurrent(UpdateBuffStatus(attacker, target, buffName, buffCounter, tree));

        //Debuff Events            
        attacker.GetComponent<HeroEvents>().AfterHeroInflictsDebuff(attacker, target, buffName, buffCounter);
        target.GetComponent<HeroEvents>().AfterHeroReceivesDebuff(attacker, target, buffName, buffCounter);

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator UpdateBuffStatus(GameObject attacker, GameObject target, BuffName buffName, int buffCounter, CoroutineTree tree)
    {

        BuffComponent existingBuff = FindBuffComponent(target, buffName);
        existingBuff.newCounters = buffCounter;

        tree.AddCurrent(existingBuff.IncreaseBuffCounters(buffCounter, existingBuff, tree));
       
        tree.AddCurrent(existingBuff.CauseBuffEffect(tree));

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator BlockBuff(GameObject attacker, GameObject target, BuffName buffName, int buffCounter, CoroutineTree tree)
    {
        BuffComponent bc = FindBuffComponent(target, buffName);

        tree.AddCurrent(bc.CauseBuffEffectEvent(attacker, target, tree));

        ///<TODO> To be updated to JondzMethod
        attacker.GetComponent<HeroEvents>().AfterHeroGrantsBuff(attacker, target, buffName, buffCounter);

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator BlockDebuff(GameObject attacker, GameObject target, BuffName buffName, int buffCounter, CoroutineTree tree)
    {
        BuffComponent bc = FindBuffComponent(target, buffName);
        
       tree.AddCurrent(bc.CauseBuffEffectEvent(attacker, target, tree));

        //event
        attacker.GetComponent<HeroEvents>().AfterHeroInflictsDebuff(attacker, target, buffName, buffCounter);

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator BlockDebuffPassive(GameObject attacker, GameObject target, BuffName buffName, int buffCounter, CoroutineTree tree)
    {

        target.GetComponent<DebuffImmunity>().UseSkillEffect(attacker, target);

        //event
        attacker.GetComponent<HeroEvents>().AfterHeroInflictsDebuff(attacker, target, buffName, buffCounter);

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator BlockDebuffAttacker(GameObject attacker, GameObject target, BuffName buffName, CoroutineTree tree)
    {
        BuffComponent bc = FindBuffComponent(attacker, buffName);

        //THIS IS CRIPPLE'S BUFF EFFECT EVENT 
       tree.AddCurrent(bc.CauseBuffEffectEvent(attacker, target, tree));

        VisualSystem.Instance.CreateFloatingText("CRIPPLED", attacker, Color.magenta);
        VisualSystem.Instance.GenericSkillEffectAnimation(target, GenericSkillName.CRIPPLE);
        
        tree.CorQ.CoroutineCompleted();
        yield return null;

    }


    BuffAction GetBuffAction(GameObject target, BuffName buffName)
    {

        //can use switch here but all buff values need to be enumerated

        if (FindBuffComponent(target, BuffName.ANTI_BUFF))
            return BuffAction.ANTIBUFF;

        else if (FindBuffComponent(target, buffName))
            return BuffAction.UPDATE;

        else
            return BuffAction.ADDNEW;
    }

    BuffAction GetDebuffAction(GameObject attacker, GameObject target, BuffName buffName)
    {
        if (FindBuffComponent(attacker, BuffName.CRIPPLE))
            return BuffAction.CRIPPLE;

        else if (FindBuffComponent(target, BuffName.IMMUNITY))
            return BuffAction.IMMUNITY;

        else if (target.GetComponent<DebuffImmunity>() != null)
            return BuffAction.IMMUNITYPASSIVE;

        else if (FindBuffComponent(target, buffName))
            return BuffAction.UPDATE;

        else
            return BuffAction.ADDNEW;
    }

    BuffAction GetDebuffActionIgnoreImmunity(GameObject attacker, GameObject target, BuffName buffName)
    {
        if (FindBuffComponent(attacker, BuffName.CRIPPLE))
            return BuffAction.CRIPPLE;       

        else if (FindBuffComponent(target, buffName))
            return BuffAction.UPDATE;

        else
            return BuffAction.ADDNEW;
    }




    public IEnumerator RemoveRandomBuffOfType(GameObject target, BuffType buffType, CoroutineTree tree)
    {
        List<BuffComponent> buffs = GetAllBuffsOfType(target.GetComponents<BuffComponent>(), buffType);
        if (buffs.Count > 0)
        {
            int i = Random.Range(0, buffs.Count - 1);
            BuffComponent bc = buffs[i];

         

         

            tree.AddCurrent(DestroyBuff(bc, tree));
        }
        else{
            //Debug.Log("No " + buffType + " to remove");
        }
            

         tree.CorQ.CoroutineCompleted();
         yield return null;
    }

    public IEnumerator RemoveSpecificBuff(GameObject target, BuffName buffName, CoroutineTree tree)
    {
        BuffComponent buff;
        if (FindBuffComponent(target, buffName) != null)
        {
            buff = FindBuffComponent(target, buffName);
            tree.AddCurrent(DestroyBuff(buff, tree));

        }
        else
            Debug.Log("No specific buff: " + buffName.ToString() + " to remove");

         tree.CorQ.CoroutineCompleted();
         yield return null;

    }

    

    public IEnumerator RemoveAllBuffsOfType(GameObject target, BuffType buffType, CoroutineTree tree)
    {
        List<BuffComponent> buffs = GetAllBuffsOfType(target.GetComponents<BuffComponent>(), buffType);

        if(buffs != null)
        tree.AddCurrent(DestroyAllBuffs(buffs, tree));

         tree.CorQ.CoroutineCompleted();
         yield return null;
    }

    

    //for special methods like HeroDies where Resurrect should not be reomved
    public IEnumerator RemoveAllBuffsOfTypeWithException(GameObject target, BuffType buffType, BuffName buffName, CoroutineTree tree)
    {
        List<BuffComponent> buffs = GetAllBuffsOfType(target.GetComponents<BuffComponent>(), buffType);

        List<BuffComponent> buffsToDestroy = new List<BuffComponent>();

        foreach (BuffComponent bc in buffs)
        {
            if (bc.buffAsset.buffBasicInfo.buffName != buffName)
                buffsToDestroy.Add(bc);
        }

        tree.AddCurrent(DestroyAllBuffs(buffsToDestroy, tree));

         tree.CorQ.CoroutineCompleted();
         yield return null;


    }

    public IEnumerator DestroyAllBuffs(List<BuffComponent> buffs, CoroutineTree tree)
    {
        for (int i = 0; i < buffs.Count; i++)
        {
            for (int j = buffs.Count - 1; j >= 0; j--)
            {
                tree.AddCurrent(DestroyBuff(buffs[j],tree));
            }
        }

         tree.CorQ.CoroutineCompleted();
         yield return null;
    }

    public IEnumerator DestroyBuff(BuffComponent bc, CoroutineTree tree)
    {
        
       tree.AddCurrent(UndoBuffEffectFirst(bc, tree));

       tree.AddCurrent(ThenDestroyBuffEffectAfter(bc, tree));      

        tree.CorQ.CoroutineCompleted();
        yield return null;
          
    }

    IEnumerator UndoBuffEffectFirst(BuffComponent bc, CoroutineTree tree)
    {        
        tree.AddCurrent(bc.UndoBuffEffect(tree));
   
        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator ThenDestroyBuffEffectAfter(BuffComponent bc, CoroutineTree tree)
    {

        //Destroy Visual Components.
        VisualSystem.Instance.DestroyBuffSymbol(bc);

        tree.AddCurrent(DestroyBuffComponent(bc, tree));

        tree.CorQ.CoroutineCompleted();
        yield return null;

    }

    IEnumerator DestroyBuffComponent(BuffComponent bc, CoroutineTree tree)
    {
        
        if(bc != null)
        GameObject.Destroy(bc);  

         tree.CorQ.CoroutineCompleted();
         yield return null;

    }



    public BuffComponent FindBuffComponent(GameObject target, BuffName buffName)
    {
        BuffComponent[] allBuffs = target.GetComponents<BuffComponent>();

        foreach (BuffComponent buff in allBuffs)
        {
            if (buff.buffAsset.buffBasicInfo.buffName == buffName)
            {
                BuffComponent existingBuff = buff;
                return existingBuff;
            }
            else
            {

            }
        }

        //Debug.Log("No specific buff: " + buffName.ToString() + " to find");
        return null;
    }


    public BuffType FindBuffType(GameObject target, BuffName buffName)
    {

        BuffComponent bc = FindBuffComponent(target, buffName);
        BuffType buffType = bc.buffAsset.buffBasicInfo.buffType;
        return buffType;


    }

    public List<BuffComponent> GetAllBuffsOfType(BuffComponent[] buffs, BuffType buffType)
    {
        List<BuffComponent> BuffsOfType = new List<BuffComponent>();

        if(buffs != null)
        {
            foreach (BuffComponent buff in buffs)
            {
                if (buff.buffAsset.buffBasicInfo.buffType == buffType)
                {
                    BuffsOfType.Add(buff);
                }
            }

            return BuffsOfType;

        }
        else
            return null;

        
    }

    ///<TODO> Transfer to Buffs and Skills Effect Class and remake
    public IEnumerator IncreaseRandomBuffCounters(GameObject Target, int counters, CoroutineTree tree)
    {

        BuffComponent[] buffs = Target.GetComponents<BuffComponent>();  //all buffs and debuffs

        var buffList = GetAllBuffsOfType(buffs, BuffType.BUFF);  //buffs only

        int buffsCount = buffList.Count;

        if (buffsCount > 0)
        {
            int randomBuffIndex = UnityEngine.Random.Range(0, buffsCount);

            buffList[randomBuffIndex].newCounters = counters;

            tree.AddCurrent(buffList[randomBuffIndex].IncreaseBuffCounters(counters, buffList[randomBuffIndex],tree));

            tree.AddCurrent(buffList[randomBuffIndex].CauseBuffEffect(tree));

        }
        else
        {
            Debug.Log("No Buffs");
        }

         tree.CorQ.CoroutineCompleted();
         yield return null;

    }

    ///<TODO> Transfer to Buffs and Skills Effect Class and remake
    public IEnumerator IncreaseSpecificBuffCounters(GameObject Target, BuffName buffName, int counters, CoroutineTree tree)
    {

        BuffComponent[] buffs = Target.GetComponents<BuffComponent>();

        var buffList = GetAllBuffsOfType(buffs, BuffType.BUFF);

        int buffsCount = buffList.Count;

        if (buffsCount > 0)
        {
            foreach (BuffComponent buff in buffList)
            {
                if (buff.buffAsset.buffBasicInfo.buffName == buffName)
                {
                    buff.newCounters = counters;

                    tree.AddCurrent(buff.IncreaseBuffCounters(counters, buff, tree));

                    tree.AddCurrent(buff.CauseBuffEffect(tree));
                }

            }
        }
        else
        {
            Debug.Log("No " + buffName.ToString() + " buff");
        }

         tree.CorQ.CoroutineCompleted();
         yield return null;

    }


    ///<TODO> Transfer to Buffs and Skills Effect Class and remake
    public IEnumerator IncreaseAllBuffCounters(GameObject Target, int counters, CoroutineTree tree)
    {

        BuffComponent[] buffs = Target.GetComponents<BuffComponent>();

        var buffList = GetAllBuffsOfType(buffs, BuffType.BUFF);  //buffs only

        int buffsCount = buffList.Count;

        if (buffsCount > 0)
        {
            foreach (BuffComponent buff in buffList)
            {
                buff.newCounters = counters;

                tree.AddCurrent(buff.IncreaseBuffCounters(counters, buff, tree));

                tree.AddCurrent(buff.CauseBuffEffect(tree));
            }

        }
        else
        {
            Debug.Log("No Buffs");
        }

         tree.CorQ.CoroutineCompleted();
         yield return null;

    }

    ///<TODO> Transfer to Buffs and Skills Effect Class and remake
    public IEnumerator IncreaseRandomDebuffCounters(GameObject Target, int counters, CoroutineTree tree)
    {

        BuffComponent[] buffs = Target.GetComponents<BuffComponent>();  //all buffs and debuffs

        var buffList = GetAllBuffsOfType(buffs, BuffType.DEBUFF);  //buffs only

        int buffsCount = buffList.Count;

        if (buffsCount > 0)
        {
            int randomBuffIndex = UnityEngine.Random.Range(0, buffsCount);

            buffList[randomBuffIndex].newCounters = counters;

            tree.AddCurrent(buffList[randomBuffIndex].IncreaseBuffCounters(counters, buffList[randomBuffIndex],tree));

            tree.AddCurrent(buffList[randomBuffIndex].CauseBuffEffect(tree));

        }
        else
        {
            Debug.Log("No Debuffs");
        }

         tree.CorQ.CoroutineCompleted();
         yield return null;

    }

    ///<TODO> Transfer to Buffs and Skills Effect Class and remake
    public IEnumerator IncreaseSpecificDebuffCounters(GameObject Target, BuffName buffName, int counters, CoroutineTree tree)
    {

        BuffComponent[] buffs = Target.GetComponents<BuffComponent>();

        var buffList = GetAllBuffsOfType(buffs, BuffType.DEBUFF);

        int buffsCount = buffList.Count;

        if (buffsCount > 0)
        {
            foreach (BuffComponent buff in buffList)
            {
                if (buff.buffAsset.buffBasicInfo.buffName == buffName)
                {
                    buff.newCounters = counters;

                    tree.AddCurrent(buff.IncreaseBuffCounters(counters, buff,tree));

                    //buff.CauseBuffEffect();

                    tree.AddCurrent(buff.CauseBuffEffect(tree));
                }

            }
        }
        else
        {
            Debug.Log("No " + buffName.ToString() + " buff");
        }

         tree.CorQ.CoroutineCompleted();
         yield return null;
    }

    ///<TODO> Transfer to Buffs and Skills Effect Class and remake
    public IEnumerator IncreaseAllDebuffCounters(GameObject Target, int counters, CoroutineTree tree)
    {

        BuffComponent[] buffs = Target.GetComponents<BuffComponent>();

        var buffList = GetAllBuffsOfType(buffs, BuffType.DEBUFF);  //buffs only

        int buffsCount = buffList.Count;

        if (buffsCount > 0)
        {
            foreach (BuffComponent buff in buffList)
            {
                buff.newCounters = counters;

                tree.AddCurrent(buff.IncreaseBuffCounters(counters, buff, tree));

                //buff.CauseBuffEffect();

                tree.AddCurrent(buff.CauseBuffEffect(tree));
            }

        }
        else
        {
            Debug.Log("No Debuffs");
        }

         tree.CorQ.CoroutineCompleted();
         yield return null;


    }

    ///<TODO> Transfer to Buffs and Skills Effect Class and remake
    public IEnumerator ReduceRandomBuffCounters(GameObject Target, int counters, CoroutineTree tree)
    {

        BuffComponent[] buffs = Target.GetComponents<BuffComponent>();  //all buffs and debuffs

        var buffList = GetAllBuffsOfType(buffs, BuffType.BUFF);  //buffs only

        int buffsCount = buffList.Count;

        if (buffsCount > 0)
        {
            int randomBuffIndex = UnityEngine.Random.Range(0, buffsCount);

            buffList[randomBuffIndex].newCounters = counters;

            tree.AddCurrent(buffList[randomBuffIndex].ReduceBuffCounters(counters,tree));

            tree.AddCurrent(buffList[randomBuffIndex].CauseBuffEffect(tree));

        }
        else
        {
            Debug.Log("No Buffs");
        }

         tree.CorQ.CoroutineCompleted();
         yield return null;



    }

    ///<TODO> Transfer to Buffs and Skills Effect Class and remake
    public IEnumerator ReduceSpecificBuffCounters(GameObject Target, BuffName buffName, int counters, CoroutineTree tree)
    {

        BuffComponent[] buffs = Target.GetComponents<BuffComponent>();

        var buffList = GetAllBuffsOfType(buffs, BuffType.BUFF);

        int buffsCount = buffList.Count;

        if (buffsCount > 0)
        {
            foreach (BuffComponent buff in buffList)
            {
                if (buff.buffAsset.buffBasicInfo.buffName == buffName)
                {
                    buff.newCounters = counters;

                    tree.AddCurrent(buff.ReduceBuffCounters(counters,tree));

                    //buff.CauseBuffEffect();

                    tree.AddCurrent(buff.CauseBuffEffect(tree));
                }

            }
        }
        else
        {
            Debug.Log("No " + buffName.ToString() + " buff");
        }

         tree.CorQ.CoroutineCompleted();
         yield return null;


    }

    ///<TODO> Transfer to Buffs and Skills Effect Class and remake
    public IEnumerator ReduceAllBuffCounters(GameObject Target, int counters, CoroutineTree tree)
    {

        BuffComponent[] buffs = Target.GetComponents<BuffComponent>();

        var buffList = GetAllBuffsOfType(buffs, BuffType.BUFF);  //buffs only

        int buffsCount = buffList.Count;

        if (buffsCount > 0)
        {
            foreach (BuffComponent buff in buffList)
            {
                buff.newCounters = counters;

                tree.AddCurrent(buff.ReduceBuffCounters(counters,tree));

                //buff.CauseBuffEffect();

                tree.AddCurrent(buff.CauseBuffEffect(tree));
            }

        }
        else
        {
            Debug.Log("No Buffs");
        }

         tree.CorQ.CoroutineCompleted();
         yield return null;


    }

    ///<TODO> Transfer to Buffs and Skills Effect Class and remake
    public IEnumerator ReduceRandomDebuffCounters(GameObject Target, int counters, CoroutineTree tree)
    {

        BuffComponent[] buffs = Target.GetComponents<BuffComponent>();  //all buffs and debuffs

        var buffList = GetAllBuffsOfType(buffs, BuffType.DEBUFF);  //buffs only

        int buffsCount = buffList.Count;

        if (buffsCount > 0)
        {
            int randomBuffIndex = UnityEngine.Random.Range(0, buffsCount);

            buffList[randomBuffIndex].newCounters = counters;
            
            tree.AddCurrent(buffList[randomBuffIndex].ReduceBuffCounters(counters,tree));

            tree.AddCurrent(buffList[randomBuffIndex].CauseBuffEffect(tree));

        }
        else
        {
            Debug.Log("No Debuffs");
        }

         tree.CorQ.CoroutineCompleted();
         yield return null;

    }

    ///<TODO> Transfer to Buffs and Skills Effect Class and remake
    public IEnumerator ReduceSpecificDebuffCounters(GameObject Target, BuffName buffName, int counters, CoroutineTree tree)
    {

        BuffComponent[] buffs = Target.GetComponents<BuffComponent>();

        var buffList = GetAllBuffsOfType(buffs, BuffType.DEBUFF);

        int buffsCount = buffList.Count;

        if (buffsCount > 0)
        {
            foreach (BuffComponent buff in buffList)
            {
                if (buff.buffAsset.buffBasicInfo.buffName == buffName)
                {
                    buff.newCounters = counters;

                    tree.AddCurrent(buff.ReduceBuffCounters(counters,tree));

                    //buff.CauseBuffEffect();

                    tree.AddCurrent(buff.CauseBuffEffect(tree));
                }

            }
        }
        else
        {
            Debug.Log("No " + buffName.ToString() + " buff");
        }

        tree.CorQ.CoroutineCompleted();
        yield return null;


    }

    ///<TODO> Transfer to Buffs and Skills Effect Class and remake
    public IEnumerator ReduceAllDebuffCounters(GameObject Target, int counters, CoroutineTree tree)
    {

        BuffComponent[] buffs = Target.GetComponents<BuffComponent>();

        Debug.Log("Steal Random Buff, all buffs count " + buffs.Length);

        var buffList = GetAllBuffsOfType(buffs, BuffType.DEBUFF);  //debuffs only

        int buffsCount = buffList.Count;

        if (buffsCount > 0)
        {
            foreach (BuffComponent buff in buffList)
            {
                buff.newCounters = counters;

                tree.AddCurrent(buff.ReduceBuffCounters(counters,tree));

                //buff.CauseBuffEffect();

                tree.AddCurrent(buff.CauseBuffEffect(tree));
            }

        }
        else
        {
            Debug.Log("No Debuffs");
        }

         tree.CorQ.CoroutineCompleted();
         yield return null;


    }

    ///<TODO> Transfer to Buffs and Skills Effect Class and remake
    public IEnumerator StealRandomBuff(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {

        BuffName stealBuff;
        int stealBuffCounteers;

        BuffComponent[] buffs = Target.GetComponents<BuffComponent>();

        var buffList = GetAllBuffsOfType(buffs, BuffType.BUFF);  //buffs only

        int buffsCount = buffList.Count;

        Debug.Log("Steal Random Buff, buffsCount " + buffsCount);

        if (buffsCount > 0)
        {

            Debug.Log("BuffCount > 1");

            int i = UnityEngine.Random.Range(0, buffsCount);
            stealBuff = buffList[i].buffAsset.buffBasicInfo.buffName;
            stealBuffCounteers = buffList[i].counter;
            
            tree.AddCurrent(DestroyBuff(buffList[i], tree));

            tree.AddCurrent(AddBuff(Attacker, Attacker, stealBuff, stealBuffCounteers, tree));
        }
        else
        {
            Debug.Log("No Buffs");
        }

         tree.CorQ.CoroutineCompleted();
         yield return null;

    }

    ///<TODO> Transfer to Buffs and Skills Effect Class and remake
    public IEnumerator StealAllBuffs(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {

        BuffName stealBuff;
        int stealBuffCounteers;

        BuffComponent[] buffs = Target.GetComponents<BuffComponent>();

        var buffList = GetAllBuffsOfType(buffs, BuffType.BUFF);  //buffs only

        int buffsCount = buffList.Count;

        if (buffsCount > 0)
        {

            foreach (var buff in buffList)
            {

                stealBuff = buff.buffAsset.buffBasicInfo.buffName;
                stealBuffCounteers = buff.counter;
                tree.AddCurrent(DestroyBuff(buff, tree));

                tree.AddCurrent(AddBuff(Attacker, Attacker, stealBuff, stealBuffCounteers, tree));
            }


        }
        else
        {
            Debug.Log("No Buffs");
        }

         tree.CorQ.CoroutineCompleted();
         yield return null;

    }

    ///<TODO> Transfer to Buffs and Skills Effect Class and remake
    public IEnumerator TransferRandomDebuff(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {

        BuffName stealBuff;
        int stealBuffCounteers;

        BuffComponent[] buffs = Attacker.GetComponents<BuffComponent>();

        var buffList = GetAllBuffsOfType(buffs, BuffType.DEBUFF);  //buffs only

        int buffsCount = buffList.Count;

        if (buffsCount > 0)
        {

            int i = UnityEngine.Random.Range(0, buffsCount);
            stealBuff = buffList[i].buffAsset.buffBasicInfo.buffName;
            stealBuffCounteers = buffList[i].counter;

            tree.AddCurrent(DestroyBuff(buffList[i],tree));

            tree.AddCurrent(AddDebuff(Attacker, Attacker, stealBuff, stealBuffCounteers, tree));
        }
        else
        {
            Debug.Log("No Debuffs");
        }

         tree.CorQ.CoroutineCompleted();
         yield return null;

    }

   ///<TODO> Transfer to Buffs and Skills Effect Class and remake
    public IEnumerator TransferAllDebuffs(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {

        BuffName stealBuff;
        int stealBuffCounteers;

        BuffComponent[] buffs = Attacker.GetComponents<BuffComponent>();

        var buffList = GetAllBuffsOfType(buffs, BuffType.DEBUFF);  //buffs only

        int buffsCount = buffList.Count;

        if (buffsCount > 0)
        {

            foreach (var buff in buffList)
            {

                stealBuff = buff.buffAsset.buffBasicInfo.buffName;
                stealBuffCounteers = buff.counter;
                tree.AddCurrent(DestroyBuff(buff,tree));

               tree.AddCurrent(AddDebuff(Attacker, Attacker, stealBuff, stealBuffCounteers, tree));
            }


        }
        else
        {
            Debug.Log("No Buffs");
        }

         tree.CorQ.CoroutineCompleted();
         yield return null;

    }




}
