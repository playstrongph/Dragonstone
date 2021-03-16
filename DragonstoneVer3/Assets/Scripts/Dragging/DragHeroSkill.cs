using UnityEngine;
using System.Collections;

public class DragHeroSkill : DraggingActions {

    // reference to the sprite with a round "Target" graphic
    private SpriteRenderer sr;
    // LineRenderer that is attached to a child game object to draw the arrow
    private LineRenderer lr;
    // reference to WhereIsTheCardOrCreature to track this object`s state in the game
    //private WhereIsTheCardOrCreature whereIsThisCreature;
    // the pointy end of the arrow, should be called "Triangle" in the Hierarchy
    private Transform triangle;
    // SpriteRenderer of triangle. We need this to disable the pointy end if the target is too close.
    private SpriteRenderer triangleSR;
    // when we stop dragging, the gameObject that we were targeting will be stored in this variable.
    private GameObject Target;
    // Reference to creature manager, attached to the parent game object
    //private OneCreatureManager manager;

    void Awake()
    {
        // establish all the connections
        sr = GetComponent<SpriteRenderer>();
        lr = GetComponentInChildren<LineRenderer>();
        lr.sortingLayerName = "Above Everything";
        triangle = transform.Find("Triangle");
        triangleSR = triangle.GetComponent<SpriteRenderer>();

        //manager = GetComponentInParent<OneCreatureManager>();
        //whereIsThisCreature = GetComponentInParent<WhereIsTheCardOrCreature>();
    }

    public override bool CanDrag
    {
        get
        {   
            
            return GetComponentInParent<SkillComponent>().currCoolDown == 0;

            
           
        }
    }

    public override void OnStartDrag()
    {
        sr.enabled = true;
        lr.enabled = true;

        //TODO: determine valid targets for the skill
        TargetSystem.Instance.ShowValidTargets(GetComponentInParent<SkillComponent>());

    }

    public override void OnDraggingInUpdate()
    {

        Vector3 notNormalized = transform.position - transform.parent.position;
        Vector3 direction = notNormalized.normalized;
        float distanceToTarget = (direction*2.3f).magnitude;
        if (notNormalized.magnitude > distanceToTarget)
        {
            // draw a line between the creature and the target
            lr.SetPositions(new Vector3[]{ transform.parent.position, transform.position - direction*2.3f });
            lr.enabled = true;

            // position the end of the arrow between near the target.
            triangleSR.enabled = true;
            triangleSR.transform.position = transform.position - 1.5f*direction;

            // proper rotarion of arrow end
            float rot_z = Mathf.Atan2(notNormalized.y, notNormalized.x) * Mathf.Rad2Deg;
            triangleSR.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        }
        else
        {
            // if the target is not far enough from creature, do not show the arrow
            lr.enabled = false;
            triangleSR.enabled = false;
        }
            
    }

    public override void OnEndDrag()
    {
        
        Target = null;
        RaycastHit[] hits;
        // TODO: raycast here anyway, store the results in 
        hits = Physics.RaycastAll(origin: Camera.main.transform.position, 
            direction: (-Camera.main.transform.position + this.transform.position).normalized, 
            maxDistance: 30f) ;

        foreach (RaycastHit h in hits)
        {

            if (this.gameObject != h.transform.gameObject)
                Target = h.transform.parent.gameObject;              
        }


        transform.localPosition = Vector3.zero;
        sr.enabled = false;
        lr.enabled = false;
        triangleSR.enabled = false;


        //if (Target != null) new HeroAttackCommand(GetComponentInParent<HeroLogic>(),Target.GetComponent<HeroLogic>()).AddToQueue();
        
        if (Target != null && Target.GetComponent<HeroLogic>().isValidTarget)
         //TODO: determine valid targets for the skill
        //new TargetWithSkillCommand(this.transform.parent.gameObject, Target).AddToQueue();
        {
            //new TargetWithSkillCommand(GetComponentInParent<SkillComponent>(), Target).AddToQueue();
            //new HeroEndTurnCommand(GetComponentInParent<HeroLogic>()).AddToQueue();
            //TurnManager.Instance.ResetHeroTimer(GetComponentInParent<HeroLogic>());
            SkillSystem.Instance.UseSkill(GetComponentInParent<SkillComponent>(), Target);
            //TargetSystem.Instance.HideTargets();
        }
        else 
        {
            TargetSystem.Instance.HideTargets();
            TargetSystem.Instance.FindValidTargets(GetComponentInParent<SkillComponent>().heroLogic);
            Debug.Log ("Invalid target");
        }


    }

    // NOT USED IN THIS SCRIPT
    protected override bool DragSuccessful()
    {
        return true;
    }


    //DS
        public override void OnCancelDrag()
    {}
}
