using UnityEngine;
using System.Collections;

public class PlayerAnim : MonoBehaviour {
    private Animator anim;
    public Sprite baseSprite;
	

	void Start () {
        anim = GetComponent<Animator>();
	}
	
	
	void Update () {
        if (!anim.enabled)
        {
            var renderer = GetComponent<SpriteRenderer>();
            if(renderer.sprite != baseSprite)
            {
                renderer.sprite = baseSprite;
            }
        }
	}

    public void AttackAnim(string trigger)
    {
        anim.enabled = true;
        anim.SetTrigger(trigger);
    }

    public void AnimEnabed()
    {
        anim.enabled = false;
    }
}
