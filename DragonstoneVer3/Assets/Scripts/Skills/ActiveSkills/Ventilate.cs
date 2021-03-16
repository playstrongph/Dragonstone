using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ventilate : SkillComponent
{
    
   
    GameObject Attacker, Target;

    CoroutineTree tree = new CoroutineTree();

    ///<Summary>
    /// Connected to Subscribe to Events
    /// One time Call during start of the game
    /// Keep this as methods for now
    ///</Summary>
    public override void RegisterEventEffect()
    {
       UsePassiveSkill();                                     
    }
   
    public override void UsePassiveSkill()
    {
        var skillEffect = this.gameObject.AddComponent<SkillCDImmunity>();
        skillEffect.referenceHero = heroLogic.gameObject;
        skillEffect.referenceSkill = this;     
    }    

    ///<Summary>
    /// This is the root starter
    ///</Summary>    
    public override void UseSkill(GameObject Attacker, GameObject Target)
    {   
         //UseSkillLogic();         
         this.Attacker = Attacker;
         this.Target = Target;
         
         tree.AddRoot(UseSkillLogic());
         tree.Start();

    }

    
    public IEnumerator UseSkillLogic()
    {
        tree.AddCurrent(UseSkillCoroutine(Attacker, Target));    

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator UseSkillCoroutine(GameObject Attacker, GameObject Target)
    {                  
       //Active Skills Mandatory
       tree.AddCurrent(UseSkillPreviewCoroutine(Attacker, Target));         
       tree.AddCurrent(UseSkillAnimationCoroutine(Attacker, Target));

       tree.AddCurrent(RefreshAllSkillCooldownsToReadyCoroutine(Target));  
       
       //MANDATORY 
       tree.AddCurrent(ResetSkillCooldownAfterUse(Attacker, Target,tree));  
       tree.AddCurrent(TurnManager.Instance.EndTurn(Attacker, tree));      
       tree.CorQ.CoroutineCompleted();
       yield return null;
    }

    

    IEnumerator UseSkillPreviewCoroutine(GameObject Attacker, GameObject Target)
    {
        base.UseSkillPreview(Attacker, Target); //pure visual
       tree.CorQ.CoroutineCompleted();
       yield return null;
    }    

     IEnumerator UseSkillAnimationCoroutine(GameObject Attacker, GameObject Target)
    {
        base.UseSkillAnimation(Target, Target, this); //pure visual

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator RefreshAllSkillCooldownsToReadyCoroutine(GameObject Target)
    {
        tree.AddCurrent(RefreshAllSkillCooldownsToReady(Target,tree));

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

     

    

    





}
