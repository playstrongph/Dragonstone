using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : SkillComponent
{ 
    

    GameObject Attacker, Target;

    CoroutineTree tree = new CoroutineTree();   


    

    public override void UseSkill(GameObject Attacker, GameObject Target)
    {
       this.Attacker = Attacker;
       this.Target = Target;
       
       

       //tree is initialized in SkillComponent 
       tree.Start();
       tree.AddRoot(UseSkillLogic(tree));
       

    }


    


    public IEnumerator UseSkillLogic(CoroutineTree tree)
    {

      if(Attacker.GetComponent<CENSOR>() != null)
      {
        //StartCoroutine(BasicAttackCoroutine(Attacker, Target));
        tree.AddCurrent(BasicAttackCoroutine(tree));
        heroLogic.gameObject.GetComponent<CENSOR>().BuffEffectAnimation(Attacker);
      }
      else
      //StartCoroutine(UseSkillCoroutine(Attacker, Target));
      tree.AddCurrent(UseSkillCoroutine(tree));

      tree.CorQ.CoroutineCompleted();
      yield return null;
    } 



    IEnumerator UseSkillCoroutine(CoroutineTree tree)
    {
     
      //yield return StartCoroutine(UsePreAttackPassiveSkillCoroutine(Attacker, Target));         
      tree.AddCurrent(UsePreAttackPassiveSkillCoroutine(tree));

      //yield return StartCoroutine(AttackHeroCoroutine(Attacker, Target));            
      tree.AddCurrent(AttackHeroCoroutine(tree));
      
      //yield return StartCoroutine(UsePostAttackPassiveSkillCoroutine(Attacker, Target));      
      tree.AddCurrent(UsePostAttackPassiveSkillCoroutine(tree));

      //yield return StartCoroutine(EndTurnCoroutine(Attacker));
      tree.AddCurrent(EndTurnCoroutine(tree));

      tree.CorQ.CoroutineCompleted();
      yield return null; 
      
    }

    IEnumerator BasicAttackCoroutine(CoroutineTree tree)
    {      
      //yield return StartCoroutine(AttackHeroCoroutine(Attacker, Target));
      tree.AddCurrent(AttackHeroCoroutine(tree));      

      //yield return StartCoroutine(EndTurnCoroutine(Attacker));      
      tree.AddCurrent(EndTurnCoroutine(tree));

      tree.CorQ.CoroutineCompleted();
      yield return null; 
    }

    IEnumerator UsePreAttackPassiveSkillCoroutine(CoroutineTree tree)
    {

      tree.AddCurrent(GetPreAttackPassiveSkill(Attacker, Target,tree));
      tree.CorQ.CoroutineCompleted();
      yield return null;
    }

    IEnumerator AttackHeroCoroutine(CoroutineTree tree)
    {

      //AttackHero(Attacker, Target);
      tree.AddCurrent(AttackHero(Attacker, Target, tree));


      tree.CorQ.CoroutineCompleted();
      yield return null;
    }

    IEnumerator UsePostAttackPassiveSkillCoroutine(CoroutineTree tree)
    {     
      tree.AddCurrent(GetPostAttackPassiveSkill(Attacker, Target,tree));

      tree.CorQ.CoroutineCompleted();
      yield return null;
    }


    IEnumerator EndTurnCoroutine(CoroutineTree tree)
    {      
      tree.AddCurrent(TurnManager.Instance.EndTurn(Attacker, tree));

      tree.CorQ.CoroutineCompleted();
    
      yield return null;
    }



    ///<Summary>
    /// Below are methods used by Active Skills to do basic attacks
    ///</Summary>
    public IEnumerator SkillUseSkillLogic(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {

      this.Attacker = Attacker;
      this.Target = Target;

      if(Attacker.GetComponent<CENSOR>() != null)
      {
        //StartCoroutine(BasicAttackCoroutine(Attacker, Target));
        tree.AddCurrent(BasicAttackCoroutine(tree));
        heroLogic.gameObject.GetComponent<CENSOR>().BuffEffectAnimation(Attacker);
      }
      else
      //StartCoroutine(UseSkillCoroutine(Attacker, Target));
      tree.AddCurrent(SkillUseSkillCoroutine(tree));

      tree.CorQ.CoroutineCompleted();
      yield return null;
    } 

    IEnumerator SkillUseSkillCoroutine(CoroutineTree tree)
    {
     

      tree.AddCurrent(UsePreAttackPassiveSkillCoroutine(tree));


      tree.AddCurrent(AttackHeroCoroutine(tree));
      

      tree.AddCurrent(UsePostAttackPassiveSkillCoroutine(tree));

      tree.CorQ.CoroutineCompleted();
      yield return null; 
      
    }

    

    

}
