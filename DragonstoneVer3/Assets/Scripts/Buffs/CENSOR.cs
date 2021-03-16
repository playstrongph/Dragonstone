using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CENSOR : BuffComponent
{
        
        
    int reduceCounter = 1;
    
    public override void RegisterEventEffect()
    {    
       
        heroEvents.e_AfterHeroEndTurn += BuffEventEffect;
    }

    public override void UnRegisterEventEffect()
    {     
       
       heroEvents.e_AfterHeroEndTurn -= BuffEventEffect;
    }

    public void BuffEventEffect(GameObject Target, CoroutineTree tree)
    {
        tree.AddCurrent(CauseBuffEffectEvent(Target, tree));
    }

     public override IEnumerator CauseBuffEffectEvent(GameObject Target, CoroutineTree tree)
    {              
        tree.AddCurrent(ReduceBuffCounters(reduceCounter,tree));     

       tree.CorQ.CoroutineCompleted();
       yield return null;

    }
    

    public override IEnumerator CauseBuffEffect(CoroutineTree tree)
    {                
       
       VisualSystem.Instance.DebuffEffectAnimation(this.gameObject, this);          
        foreach(var heroSkill in heroLogic.skills)
        {
            if(heroSkill.skillType == SkillType.PASSIVE || heroSkill.skillType == SkillType.PASSIVE_CD || heroSkill.skillType == SkillType.PASSIVE_ATTACK)
            {
                Debug.Log("Skill: " +heroSkill.skillAsset.ToString());
                Debug.Log("Skilltype: " +heroSkill.skillType.ToString());
                
                
                heroSkill.GetComponent<SkillCardManager>().XskillGraphic.gameObject.SetActive(true);                           
                heroSkill.GetComponentInChildren<BoxCollider>().enabled = false;  //FOR CLEANUP

                heroSkill.UnRegisterEventEffect();

            }
        }

        tree.CorQ.CoroutineCompleted();
        yield return null;
        
    
    }

    public override IEnumerator UndoBuffEffect(CoroutineTree tree)
    {        
       
        foreach(var heroSkill in heroLogic.skills)
        {
            if(heroSkill.skillType == SkillType.PASSIVE || heroSkill.skillType == SkillType.PASSIVE_CD || heroSkill.skillType == SkillType.PASSIVE_ATTACK)
            {
                Debug.Log("Skill: " +heroSkill.skillAsset.ToString());
                Debug.Log("Skilltype: " +heroSkill.skillType.ToString());                
                
                heroSkill.GetComponent<SkillCardManager>().XskillGraphic.gameObject.SetActive(false);                           
                heroSkill.GetComponentInChildren<BoxCollider>().enabled = true; //FOR CLEANUP                
                heroSkill.RegisterEventEffect();
            }
        }

        tree.CorQ.CoroutineCompleted();
        yield return null;      
    }


    public void BuffEffectAnimation (GameObject Target) //Used by external references, do NOT delete
    {          
        VisualSystem.Instance.CreateFloatingText(buffAsset.buffBasicInfo.description, Target, Color.magenta);        
    }

    

     
}
