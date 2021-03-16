using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreventFatalDamageSkillEffect : SkillEffect
{
    
    //PASSIVE SKILL
    public SkillComponent skill;
   
    public IEnumerator UseSkillEffect(GameObject Target, CoroutineTree tree)
    {
        tree.AddCurrent(FloatingTextCoroutine(Target,tree))  ;

        //tree.AddCurrent(SetHealthToOne(Target, tree));

        tree.AddCurrent(SkillSystem.Instance.DestroySkillEffect(this, tree));

        tree.CorQ.CoroutineCompleted();
        yield return null;     
        
    }

    public IEnumerator SetHealthToOne(GameObject Target, CoroutineTree tree)
    {
       
        
        Target.GetComponent<HeroLogic>().Health = 1;    

       

        tree.CorQ.CoroutineCompleted();
        yield return null;     
    }

    public IEnumerator FloatingTextCoroutine(GameObject Target, CoroutineTree tree)
    {

        if(skill != null)
       {
          VisualSystem.Instance.CreateFloatingText(skill.skillAsset.skillBasicInfo.skillName.ToString(), Target, Color.yellow);
          VisualSystem.Instance.Delay(1f);
          VisualSystem.Instance.CreateFloatingText("Prevent Fatal Damage", Target, Color.white);
          VisualSystem.Instance.Delay(0.5f);
       }else       
          VisualSystem.Instance.CreateFloatingText("Prevent Fatal Damage", Target, Color.yellow);

        



        tree.CorQ.CoroutineCompleted();
        yield return null;   
    }

    
}
