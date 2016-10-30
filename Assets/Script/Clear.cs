using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Clear : MonoBehaviour
{
    private FadeManager fadeManager;
    public Image rotateSun;
    public float rotateSpeed;
    private AudioSource soundBox;
    public AudioClip rollSE;
    private int isOnce = 1;
    private float resultScore = -1;
    public Text pushText;
    public Text scoreText;
    private float rollSpeed;

    void Start()
    {
        fadeManager = FindObjectOfType<FadeManager>();
        soundBox = GameObject.Find("SoundBox").GetComponent<AudioSource>();
        rollSpeed = StaticManager.GetResultSocre() / 5f;
    }

    void Update()
    {
        rotateSun.transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);

        if (isOnce == 1 && !soundBox.isPlaying && Input.anyKeyDown)
        {
            isOnce++;
            soundBox.PlayOneShot(rollSE, 1f);
            pushText.enabled = false;
        }

        if (isOnce == 2 && resultScore < StaticManager.GetResultSocre())
        {
            resultScore += Time.deltaTime * rollSpeed;
            scoreText.text = resultScore.ToString("N0");
            if (!soundBox.isPlaying || resultScore >= StaticManager.GetResultSocre())
            {
                scoreText.text = StaticManager.GetResultSocre().ToString();
                fadeManager.fadeMode = FadeManager.FadeMode.close;
                fadeManager.FadeStart("Title", waitForSeconds: 2f);
            }
        }
    }
}
