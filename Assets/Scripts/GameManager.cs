using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Define;
using UnityEngine.SceneManagement;
using System.Threading;
/// <summary>
/// 상태관할
/// </summary>
public class GameManager : MonoBehaviour
{
 
    public TextMeshProUGUI chatBoxText;
    public GameObject chatBox;
    //ObjectController에서 접근
    [HideInInspector] public Define.ObjectName objectName;
    [HideInInspector] public bool _choose7 = false;
    [HideInInspector] public bool _choose18 = false;

    //대화문 구현에서 버튼선택지 구현->따라서 선택지 버튼따라 달라지는 출력문,씬전환은 함께 구현
    //대화문 안에서 특정 행동 선택시 true로 넣어주기. 둘다 false일 때 출력할 대화문은 대화문스크립트에서 작성되어야 함
    //대화문 스크립트 안에서 이 변수가 true일 시 출력할 대화문을 작성해줘야 함
    [HideInInspector] public bool foundNeckless = false;
    [HideInInspector] public bool foundClue = false;
    [HideInInspector] public bool bringScissors = false;
    /*
     [HideInInspector] public bool _choose7 = false;
    [HideInInspector] public bool _choose18 = false;
    18번, 7번 불값 대화문에서 if문으로 비교해서 둘다 true면 그 대화문 출력, 18번과 7번 모두 false면 그 대화문 출력
     */


    //"침대에 누워 휴식을 취한다" 선택 시 대화문스크립트에서 ++해주어야 함
    //값에 따라 출력되는 대화문이 달라져야 함
    [HideInInspector] public int _2chooseNum = 0;
    //"편지 봉투 하나를 열어본다" 선택 시 대화문스크립트에서 ++해주어야 함
    //값에 따라 출력되는 대화문이 달라져야 함
    [HideInInspector] public int _12chooseNum = 0;


    private void Update()
    {
        //챗박스가 활성화되어있고 화면 클릭도 일어났다면 챗박스 끄기
        if (chatBox.active)
        {
            Invoke("CheckClickChatBox", 0.2f);
        }
        //얘를 밑으로 쫙 추가
        if (SceneManager.GetActiveScene().Equals("PlayerRoomScene"))
        {
            if (objectName == Define.ObjectName._3)
            { 
                //계속대입 문제 해결하기
                chatBoxText.text = "‘화려한 틀이 인상적인 거울 속에서 적발의 날카롭게 생겼지만 아름다운 여성이 보인다.' '살짝 손을 들어 얼굴을 건드려 보지만 여전히 이 모습이 나라는 게 믿기지 않아.’";
            }
        }
    }

    private void CheckClickChatBox()
    {
        if (Input.GetMouseButtonDown(0))
        {
            chatBox.SetActive(false);
        }
    }
}
