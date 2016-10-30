using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectArrow : MonoBehaviour {
    public Button[] selectButton;       //選択ボタン
    protected EventSystem eventSystem;    //Button.Select()を使うのに必要
    public AudioSource soundBox;       //効果音ようのAudioSourceの空箱
    public AudioClip selectSE;          //選択ボタンの効果音
    public Vector3 offset;            //カーソルの位置調整
    protected GameObject currentSelected; //現在取得しているボタン
    protected GameObject lastSelected;    //最後に正常に取得したボタン, バックアップ用
    public bool isStartSelect;   //画像を表示し、選択を開始して良いか


    void Awake()
    {
        isStartSelect = false;
    }

    void Start () {
        SelectedStart();
    }

    public void SelectedStart()
    {
        eventSystem = FindObjectOfType<EventSystem>();
        eventSystem.enabled = false;
        GetComponent<Image>().enabled = false; //初期状態ではカーソルを見せない
        selectButton[0].Select();
        lastSelected = selectButton[0].gameObject;
        //Cursor.visible = false; //カーソル非表示
    }

    void Update () {
        SelectedUpdate();
    }

    public void SelectedUpdate()
    {
        if (isStartSelect)
        {
            //クリックしてnullになってしまったら
            if (eventSystem.currentSelectedGameObject == null)
            {
                currentSelected = lastSelected;
            }
            //現在選択しているボタンを取得
            else
            {
                currentSelected = eventSystem.currentSelectedGameObject;
            }
            currentSelected.GetComponent<Button>().Select();

            //カーソルの位置を動かす
            for (int i = 0; i < selectButton.Length; i++)
            {
                if (currentSelected == selectButton[i].gameObject)
                {
                    AjustPosition(selectButton[i].gameObject);
                }
            }
        }
        else
        {
            eventSystem.enabled = false;
        }
    }

    //カーソルの位置を動かす + lastボタンにバックアップを取る
    public void AjustPosition(GameObject newPos)
    {
        //カーソルの位置調整
        Vector3 pos = newPos.transform.position;
        transform.position = new Vector3(pos.x - offset.x, pos.y - offset.y, pos.z - offset.z);

        if(currentSelected != lastSelected)
        {
            soundBox.PlayOneShot(selectSE, 1f);
        }

        //バックアップ
        lastSelected = currentSelected;
    }

    public void StartSelect()
    {
        GameObject selectArrow = FindObjectOfType<SelectArrow>().gameObject;
        selectArrow.GetComponent<Image>().enabled = true;
        eventSystem.enabled = true;
        isStartSelect = true;
    }
}
