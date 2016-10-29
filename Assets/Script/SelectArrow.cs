using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectArrow : MonoBehaviour {
    public Button[] selectButton;       //選択ボタン
    private EventSystem eventSystem;    //Button.Select()を使うのに必要
    public Vector3 distance;            //カーソルの位置調整
    private GameObject currentSelected; //現在取得しているボタン
    private GameObject lastSelected;    //最後に正常に取得したボタン, バックアップ用
    static public bool isStartSelect;   //画像を表示し、選択を開始して良いか


    void Awake()
    {
        isStartSelect = false;
    }

    void Start () {
        eventSystem = FindObjectOfType<EventSystem>();
        GetComponent<Image>().enabled = false; //初期状態ではカーソルを見せない
        selectButton[0].Select();
        lastSelected = selectButton[0].gameObject;
        //Cursor.visible = false; //カーソル非表示
    }

	void Update () {
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
    }

    //カーソルの位置を動かす + lastボタンにバックアップを取る
    void AjustPosition(GameObject newPos)
    {
        //カーソルの位置調整
        Vector3 pos = newPos.transform.position;
        transform.position = new Vector3(pos.x - distance.x, pos.y - distance.y, pos.z - distance.z);

        //バックアップ
        lastSelected = currentSelected;
    }

    static public void StartSelect()
    {
        GameObject selectArrow = FindObjectOfType<SelectArrow>().gameObject;
        selectArrow.GetComponent<Image>().enabled = true;
        isStartSelect = true;
    }
}
