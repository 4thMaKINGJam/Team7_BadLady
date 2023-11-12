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

    //★선택지 따라 state 업데이트 & 이벤트따라 챗박스 켜주는 역할. 
    //3개키는 애면 3개키고, 1개키는 애면 1개키고
    //초이스박스 텍스트 설정: GameManager에서
    public void onClick()
    {
     
        Debug.Log(this.gameObject.name + "이 메소드 호출");
        switch (gameManager.objectName)
        {
            case Define.ObjectName._2:
                if (this.gameObject.name == "Choice0")
                {
                    gameManager._2breakChooseNum++;
                    gameManager._2investigateChoose = false;
                    gameManager._2quit = false;
                }
                if (this.gameObject.name == "Choice1")
                {
                    gameManager._2investigateChoose = true;
                    gameManager._2quit = false;
                }
                if (this.gameObject.name == "Choice2")
                {
                    gameManager._2investigateChoose = false;
                    gameManager._2quit = true;
                }
                //선택지 켰던거 끄기
                turnOffChoices();
                break;

            case Define.ObjectName._4:
                if (this.gameObject.name == "Choice0")
                {
                    gameManager._4inviteWoman = true;
                    gameManager._4quit = false;
                }
                if (this.gameObject.name == "Choice1")
                {
                    gameManager._4inviteWoman = false;
                    gameManager._4quit = true;
                }
                //선택지 켰던거 끄기
                turnOffChoices();
                break;
        }

    }
   private void turnOffChoices()
    {
        GameObject.Find("Choice0").SetActive(false);
        GameObject.Find("Choice1").SetActive(false);
        GameObject.Find("Choice2").SetActive(false);
        gameManager.haveChoices = false;
    }
}
