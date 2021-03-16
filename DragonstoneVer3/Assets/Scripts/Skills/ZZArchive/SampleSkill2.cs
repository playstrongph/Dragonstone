using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleSkill2 : SkillComponent
{

    CoroutineTree tree = new CoroutineTree();
    public override void UseSkill(GameObject Attacker, GameObject Target)
    {
        tree.Start();
        tree.AddRoot(UseSkillCoroutine(Attacker, Target));

    }

    IEnumerator UseSkillCoroutine(GameObject Attacker, GameObject Target)
    {

        VisualSystem.Instance.CreateFloatingText("Hi Raynor!", Attacker, Color.yellow);

        //tree.AddCurrent(Heal(Attacker, Target, 7, tree));

        //tree.AddCurrent(RefreshAllSkillCooldownsToReady(Target,tree));

        //tree.AddCurrent(RemoveRandomBuff(Attacker, Target, tree));

        //tree.AddCurrent(AddBuff(Attacker, Target, BuffName.ATTACK_UP, 2, tree));

        tree.AddCurrent(AddBuff(Attacker, Target, BuffName.CRITICAL_STRIKE, 1, tree));

        //tree.AddCurrent(AddDebuff(Attacker, Target, BuffName.CENSOR, 2, tree));


        //tree.AddCurrent(RemoveRandomDebuff(Attacker, Attacker, tree));

        //tree.AddCurrent(AttackHero(Attacker, Target, tree));

        tree.AddCurrent(ResetSkillCooldownAfterUse(Attacker, Target, tree));
        tree.AddCurrent(TurnManager.Instance.EndTurn(Attacker, tree));

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }
}
