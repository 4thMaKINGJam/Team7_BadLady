using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

/// <summary>
/// 1. GameObject.Find("GameManager") 으로 오브젝트 찾기 2. .GetComponent<GameManager>()하기. 3. .objectName 접근해서 무슨 오브젝트 클릭한 상태인지 확인하기
/// </summary>
public class ObjcectController : MonoBehaviour
{    
    [SerializeField] private ObjectName chooseObjectName;
    private GameManager gameManager;
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
        gameManager.objectName = chooseObjectName;        
        //디버그용
        Debug.Log("chooseObjectName: "+chooseObjectName.ToString()+", objectName: "+gameManager.objectName);
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
        }
        
        
        
    }
}

