using UnityEngine;
using System.Collections;

public class PlayerAnim : MonoBehaviour {
    private Animator anim;
    public Sprite baseSprite;   //基本となる立ち絵
	

	void Start () {
        anim = GetComponent<Animator>();
	}
	
	
	void Update () {
        if (!anim.enabled)
        {
            //アニメーションがストップしているときは基本スプライトを表示する
            var renderer = GetComponent<SpriteRenderer>();
            if(renderer.sprite != baseSprite)
            {
                renderer.sprite = baseSprite;
            }
        }
	}

    public void EndAnim()
    {
        anim.SetBool("Frying", false);
        anim.SetTrigger("Wait");
        anim.enabled = false;
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
