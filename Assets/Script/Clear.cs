using UnityEngine;
using System.Collections;

public class Clear : MonoBehaviour {
    private FadeManager fadeManager;

    void Start()
    {
        fadeManager = FindObjectOfType<FadeManager>();
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            fadeManager.fadeMode = FadeManager.FadeMode.close;
            fadeManager.FadeStart("Title");
        }
    }
}
