using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
public class ChoiceManager : MonoBehaviour
{
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager=GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    //선택지 따라 state 업데이트
    public void onClick()
    {
        Debug.Log(this.gameObject.name + "이 메소드 호출");
        if (gameManager.objectName == Define.ObjectName._2)
        {
            if(this.gameObject.name== "Choice0")
            {
                gameManager._2breakChooseNum++;
                this.gameObject.SetActive(false);
            }
        }
    }
   
}
