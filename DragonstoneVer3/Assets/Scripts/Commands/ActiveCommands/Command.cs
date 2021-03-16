using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class Command
{
    public static Queue<Command> CommandQueue = new Queue<Command>();
    public static bool playingQueue = false;

    public float shortAnimationDuration = 0.5f;

    public float mediumAnimationDuration = 1f;

    public float longAnimationDuration = 2f;

    public static Command thisCommand;

    // public delegate Command ParallelCommandsDelegate(GameObject Attacker, GameObject Target);  //for cleanup
    // public static ParallelCommandsDelegate m_ParallelCommands;  //for cleanup

    public delegate void CommandLogicDelegate();

    public virtual void AddToQueue()
    {
        
        

        CommandQueue.Enqueue(this);

        thisCommand = this;  //For Quemanager cleanup
        if (QueueManager.Instance!=null)
        QueueManager.Instance.commandQueue.Add(this.ToString());
        //Debug.Log ("Enqueue: " + this.ToString());


        if (!playingQueue)
            PlayFirstCommandFromQueue();
    }

    public virtual void AddToHeroQueue(GameObject Hero) //for cleanup
    {
        HeroQueue hq = Hero.GetComponent<HeroQueue>();
        hq.AddToQueue(this);

    }

    public virtual void StartCommandExecution()
    {
        // list of everything that we have to do with this command (draw a card, play a card, play spell effect, etc...)
        // there are 2 options of timing : 
        // 1) use tween sequences and call CommandExecutionComplete in OnComplete()
        // 2) use coroutines (IEnumerator) and WaitFor... to introduce delays, call CommandExecutionComplete() in the end of coroutine
    }

    public virtual void CommandExecComplete()
    {
        
        CommandExecutionComplete();
    }

    public virtual void Requeue()
    {

    }

    public static void CommandExecutionComplete()
    {

        
       
        //Debug.Log("ComExecComplete : " +commandname.ToString() +"Command ID:" +commandID.ToString());

        if (CommandQueue.Count > 0)
        {
            PlayFirstCommandFromQueue();
             
        }
            
            
        else
            playingQueue = false;
        //DS
        //comment out
        //    TurnManager.Instance.whoseTurn.HighlightPlayableCards();
    }


    public static void PlayFirstCommandFromQueue()
    {
        
            //Debug.Log("Playing : " +commandname.ToString() +"Command ID:" +commandID.ToString());
       
            playingQueue = true;             

            CommandQueue.Dequeue().StartCommandExecution();
       
          

        
        //if (QueueManager.Instance!=null)
    }

    

}
