using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroQueue : MonoBehaviour
{
    public Queue<Command> heroQueue = new Queue<Command>();
	public List<string> heroCommandQueue;

    public bool playingQueue = false;
    
    //public HeroLogic heroLogic;

    public virtual void AddToQueue(Command command)
    {
        //HeroQueue hq = Hero.GetComponent<HeroQueue>();    

        heroQueue.Enqueue(command);
        if (QueueManager.Instance!=null)
        QueueManager.Instance.commandQueue.Add(this.ToString());
        //Debug.Log ("Enqueue: " + this.ToString());
        if (!playingQueue)
            PlayFirstCommandFromQueue();
    }

    public void PlayFirstCommandFromQueue()
    {
        playingQueue = true;
        heroQueue.Dequeue().StartCommandExecution();
       
    }

    // public void StartCommandExecution(Command command)
    // {
    //      command.StartCommandExecution();
    // }

    public void CommandExecutionComplete()
    {

        Debug.Log("HeroQueue Command Execution Complete");

        if (heroQueue.Count > 0)
            PlayFirstCommandFromQueue();
        else
            playingQueue = false;

    }
	void Awake()
	{

	}

	// void Start () {
    //     heroLogic = GetComponent<HeroLogic>();	
		
	// }
	
	// Update is called once per frame
	void Update () {
		
	}
}
