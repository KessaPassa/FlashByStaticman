using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {
    private FadeManager fadeManager;
    public GameObject player;
    private Vector2 startPos;

	void Start () {
        fadeManager = FindObjectOfType<FadeManager>();
        //fadeManager.FadeStart(null);
        startPos = player.transform.position;
    }
	
	
	void Update () {
        //上下にフワフワ揺らす
        float range = 0.5f;
        var pingpong = Mathf.PingPong(Time.time * 0.4f, range / 2f) - range;
        player.transform.position = new Vector2(startPos.x, startPos.y + pingpong);
    }

    public void OnStaartButton()
    {
        if (fadeManager.isFadeFinished)
        {
            fadeManager.fadeMode = FadeManager.FadeMode.close;
            fadeManager.FadeStart(1);
        }
    }

    public void OnTutorialButton()
    {
        if (fadeManager.isFadeFinished)
        {
            fadeManager.fadeMode = FadeManager.FadeMode.close;
            fadeManager.FadeStart("Tutorial");
        }
    }
}
