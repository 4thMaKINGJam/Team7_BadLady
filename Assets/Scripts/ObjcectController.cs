using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Define;

/// <summary>
/// 1. GameObject.Find("GameManager") 으로 오브젝트 찾기 2. .GetComponent<GameManager>()하기. 3. .objectName 접근해서 무슨 오브젝트 클릭한 상태인지 확인하기
/// </summary>
public class ObjcectController : MonoBehaviour
{
    [SerializeField] private ObjectName chooseObjectName;

    private GameManager gameManager;
    public GameObject chatBox;//직접연결
    //선택지있는오브젝트일때만 넣기
    [SerializeField] private GameObject[] choices;
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Debug.Log("chooseObjectName: " + chooseObjectName.ToString() + ", objectName: " + gameManager.objectName);        
    }
    /// <summary>
    ///  Collider2D를 가진 2D 오브젝트에 대해 동작할 수 있다
    ///  화면 전체 말고, >해당 오브젝트<가 
    ///  //클릭됐을 시 호출되는 메소드
    /// </summary>
    private void OnMouseDown()
    {
        //오브젝트 상태 업뎃: 선택지가 켜져있을때는 업뎃하면 x
        if (!choices[0].active)
        {
            gameManager.objectName = chooseObjectName;
        }
        //챗박스 뜨게: 기본
        chatBox.SetActive(true);
        //*디버그용
        Debug.Log("chooseObjectName: " + chooseObjectName.ToString() + ", objectName: " + gameManager.objectName);

        switch (gameManager.objectName)
        {
            //23번: 챙기면 사라지는 가위
            case Define.ObjectName._23:
                this.gameObject.SetActive(false);
                break;
            case Define.ObjectName._18:
                gameManager._choose18 = true;
                break;
            case Define.ObjectName._7:
                gameManager._choose7 = true;
                break;
            case Define.ObjectName._2://★3개 선택지면 3개키고 2개면 2개키되, ★0~부터 키기. 
                choices[0].SetActive(true);
                choices[1].SetActive(true);
                choices[2].SetActive(true);
                break;
        }



    }
}

