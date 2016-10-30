using UnityEngine;
using System.Collections;

public class EndAnimManager : MonoBehaviour {
    public string triggerName;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void EndAnim()
    {
        GetComponent<Animator>().SetTrigger(triggerName);
    }
}
