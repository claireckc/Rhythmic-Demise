using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventBrain : MonoBehaviour {

    public List<GameObject> synapses;
    public MovingPoint top1;
    public MovingPoint top2;
    public MovingPoint middle1;
    public MovingPoint middle2;
    public MovingPoint bottom1;
    public MovingPoint bottom2;

    private bool done;

	void Start () {
	
	}
	
	void Update () {
        for (int i = 0; i < synapses.Count; i++)
        {
            if (synapses[i] == null)
            {
                synapses.Remove(synapses[i]);
            }
        }

        if (synapses.Count <= 0 && !done)
        {
            top1.right = top2;
            middle1.right = middle2;
            bottom1.right = bottom2;

            done = true;
        }
	}
}
