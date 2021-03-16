using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ParallelSkillEffectsAttackerToTargets : Command
{
    
    GameObject Attacker;
    GameObject Target;

    public ParallelSkillEffectsAttackerToTargets(GameObject Attacker, GameObject Target)
    {
        this.Attacker = Attacker;
        this.Target = Target;
    }

    //Sample of HeroCommand Wrapped inside system command

    public override void StartCommandExecution()
    {
        foreach(var enemyHero in HeroManager.Instance.AllLivingEnemiesList(Attacker))
        {
            new SkillAttackEffectHeroQCommand(Attacker, enemyHero).AddToHeroQueue(enemyHero);
        }  

       Sequence s = DOTween.Sequence();

        //animation Delay    
        s.AppendInterval(1f).OnComplete(()=>{
            Command.CommandExecutionComplete();
        });

    }


}
