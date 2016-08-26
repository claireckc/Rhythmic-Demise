using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventBrain : MonoBehaviour {

    public List<GameObject> synapses;
    public List<GameObject> synapses2;
    public MovingPoint first1;
    public MovingPoint first2;
    public MovingPoint middle1;
    public MovingPoint middle2;
    public GameObject fences1;
    public GameObject fences2;

    private bool done;
    private bool done2;

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

        for (int i = 0; i < synapses2.Count; i++)
        {
            if (synapses2[i] == null)
            {
                synapses2.Remove(synapses2[i]);
            }
        }

        if (synapses2.Count <= 0 & !done2)
        {
            first1.right = first2;
            Destroy(fences1.gameObject);

            done2 = true;
        }

        if (synapses.Count <= 0 && !done)
        {
            middle1.right = middle2;
            Destroy(fences2.gameObject);

            done = true;
        }

        
	}
}
