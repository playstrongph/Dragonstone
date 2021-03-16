using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class CoroutineQueue
{
    
       private Queue<IEnumerator> _queue;

        /// <summary>
        /// Whether the queue is empty, that is there is nothing queued and nothing is currently being executed.
        /// </summary>
        public bool Empty { get; private set; }

        


        bool playingQueue = false;

        /// <summary>
        /// Ctor for the queue.
        /// </summary>
        public CoroutineQueue()
        {
            _queue = new Queue<IEnumerator>();
            Empty = true;         
            

        }

        public void AddToCorQ(IEnumerator coroutine)
        {
            _queue.Enqueue(coroutine);
        
            if (!playingQueue)
                PlayFirstCommandFromQueue();

        }

         public void PlayFirstCommandFromQueue()
        {
        
            playingQueue = true;             
            GlobalSettings.Instance.StartCoroutine(_queue.Dequeue());       
       
        }

        public bool CoroutineCompleted()
        {
            if(_queue.Count > 0)
            {
                PlayFirstCommandFromQueue();
            }else
            {
                playingQueue = false;
            }

            return true;
        }


    

}
