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
    [HideInInspector] public bool _choose21= false;

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

        // ********************************************
        // ***** Map1 (PlayerRoomScene) 내용 ******
        // ********************************************
        if (SceneManager.GetActiveScene().name.Equals("PlayerRoomScene"))
        {
            if (objectName == Define.ObjectName._3)
            { 
                //계속대입 문제 해결하기
                chatBoxText.text = "‘화려한 틀이 인상적인 거울 속에서 적발의 날카롭게 생겼지만 아름다운 여성이 보인다.' '살짝 손을 들어 얼굴을 건드려 보지만 여전히 이 모습이 나라는 게 믿기지 않아.’";
            }
        }

        // ********************************************
        // ***** Map2 (LivingRoomScene) 내용 ******
        // ********************************************
        else if (SceneManager.GetActiveScene().name.Equals("LivingRoomScene"))
        {
            if(objectName == Define.ObjectName._6)
            {
                int cnt = 0;
                // chatBoxText.text = "길고 큰 유럽식 창문이다. 밖으로 아름다운 정원의 풍경이 보인다.";
                void cntText1()
                {
                    chatBoxText.text = "지금 내 몸의 초상화다. 이렇게 큰 단독 초상화라니, 진짜 나도 아니면서 괜히 내가 머쓱해지는 기분이다.";
                    cnt++;
                    cntText2();
                }
                void cntText2()
                {
                    chatBoxText.text = "그나저나 저 시선을 사로잡는 초록색 목걸이.. 내 방의 보석함에는 없었는데?";
                    cnt++;
                }
            }
            else if(objectName == Define.ObjectName._7)
            {
                int cnt = 0;
                if (_choose18 == false)
                {
                    chatBoxText.text = "지금 내 몸의 초상화다. 이렇게 큰 단독 초상화라니, 진짜 나도 아니면서 괜히 내가 머쓱해지는 기분이야.";
                }
                else
                {
                    switch ( cnt)
                    {
                        case 0:
                            chatBoxText.text = "지금 내 몸의 초상화다. 이렇게 큰 단독 초상화라니, 진짜 나도 아니면서 괜히 내가 머쓱해지는 기분이다.";
                            cnt++;
                            break;
                        case 1:
                            chatBoxText.text = "그나저나 저 시선을 사로잡는 초록색 목걸이.. 내 방의 보석함에는 없었는데?";
                            cnt++;
                            break;
                    }
                }
                }
            else if(objectName == Define.ObjectName._8) // 선택지 추가 필요
            {
                chatBoxText.text = "저택 밖 정원으로 나가는 문이다.";
            }
            else if(objectName == Define.ObjectName._9)
            {
                chatBoxText.text = "화려한 장식이 돋보이는 소파와 티 테이블이다. 이곳에서 그 로판 소설 속 티타임이 이뤄지는 건가 봐.";
            }
            else if(objectName == Define.ObjectName._10) // need to add Dialogue Choices
            {
                chatBoxText.text = "매우 비싸 보이는 러그다. 청소하고 관리하려면 매우 힘들겠지.";
            }
            else if(objectName == Define.ObjectName._11)
            {
                if(_choose21==false || (_choose18 == false && _choose7 == false))
                {
                    chatBoxText.text = "로사벨라: 와, 내가 평생 일해도 못 살 것 같은 꽃병인데.";
                }
                else if(_choose21== true && (_choose18==true && _choose7== true)) // need to add Dialogue Choice
                {
                    chatBoxText.text = "에밀리라는 하녀가 누군진 모르겠으나, 혹시나 하는 마음에 꽂혀있는 꽃을 꺼내고 꽃병 안을 보았다.";
                }
            }
            else if(objectName == Define.ObjectName._12)// need to add Dialogue Choices
            {
                chatBoxText.text = "협탁 위에 편지 봉투 3개가 올려져 있다.";
            }
            //else if(objectName == Define.ObjectName._25)  // need to add Dialogue Choices
            //{
            //    chatBoxText.text = "내 방으로 돌아간다";
            //}
        }

        // *****************************
        // ******* Event1 Scene *******
        else if (SceneManager.GetActiveScene().name.Equals("Event1Scene"))
        {
            if(objectName == Define.ObjectName._66)
            {
                chatBoxText.text = "지금은 이게 중요한 게 아니야.";
            }
            else if(objectName == Define.ObjectName._77)
            {
                chatBoxText.text = "이 어색한 분위기에 당장이라도 이 창문 밖으로 뛰쳐나가고 싶다.";
            }
            else if(objectName == Define.ObjectName._88)
            {
                chatBoxText.text = "저택 밖 정원으로 나가는 문이다. 하지만 손님을 두고 나갈 수는 없지.";
            }
            else if(objectName == Define.ObjectName._99 || objectName == Define.ObjectName._1111 || objectName == Define.ObjectName._1212 || objectName == Define.ObjectName._1919 )
            {
                chatBoxText.text = "지금은 이게 중요한 게 아니야.";
            }
            //else if(objectName == Define.ObjectName._1010)    // need to add Dialogue Choices
            //{
            //    선택지 제공
            //}
        }

        // *****************************
        // ******* Event2 Scene *******
     else if (SceneManager.GetActiveScene().name.Equals("Event2Scene"))
        {
            //else if(objectName == Define.ObjectName._111)  // need Modification
            //{
            //    if(bringScissors == true)
            //    {
            //        chatBoxText.text = "마침 정원용 가위를 챙겨두긴 했는데 이럴 때 쓰일 줄이야.";
            //    }
            //}
            if(objectName == Define.ObjectName._222)
            {
                chatBoxText.text = "지금 여유롭게 누워있을 때가 아니야.";
            }
            else if(objectName == Define.ObjectName._333 || objectName == Define.ObjectName._444 || objectName == Define.ObjectName._1818){
                chatBoxText.text = "지금은 이걸 볼 때가 아니야.";
            }
            else if(objectName == Define.ObjectName._555)
            {
                chatBoxText.text = "응접실로 다시 나가는 건 자살행위나 다름없어.";
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
