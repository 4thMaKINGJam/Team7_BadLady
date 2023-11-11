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

    private FadeInOut fadeInOut;
    public TextMeshProUGUI chatBoxText;
    public GameObject chatBox;
    public GameObject[] choices;
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
    [HideInInspector] public int _2breakChooseNum = 0;
    [HideInInspector] public bool _2investigateChoose= false;
    [HideInInspector] public bool _2quit = false;

    //"편지 봉투 하나를 열어본다" 선택 시 대화문스크립트에서 ++해주어야 함
    //값에 따라 출력되는 대화문이 달라져야 함
    [HideInInspector] public int _12chooseNum = 0;
    private void Start()
    {
        fadeInOut = GameObject.Find("FadeInOutPanel").GetComponent<FadeInOut>();
        
    }

    private void Update()
    {
        //챗박스가 활성화되어있고
        //선택란은 비활성화되어있고
        //화면 클릭도 일어났다면 챗박스 끄기
        if (chatBox.active && (!choices[0].active))//★최소 선택란은 1개인데 그 1개는 [0]번 인덱스에 위치. 따라서 [0]만 체크
        {
            Invoke("CheckClickChatBox", 0.2f);
        }
  

        //얘를 밑으로 쫙 추가
        if (SceneManager.GetActiveScene().name.Equals("PlayerRoomScene"))
        {
            if (objectName == Define.ObjectName._3)
            { 
                //*계속대입 문제 해결하기
                chatBoxText.text = "‘화려한 틀이 인상적인 거울 속에서 적발의 날카롭게 생겼지만 아름다운 여성이 보인다.' '살짝 손을 들어 얼굴을 건드려 보지만 여전히 이 모습이 나라는 게 믿기지 않아.’";
            }
            if (objectName == Define.ObjectName._2)
            {
                //★예시
                choices[0].GetComponent<RectTransform>().transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "침대에 누워 휴식을 취한다.";
                choices[1].GetComponent<RectTransform>().transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "침대를 조사한다.";
                choices[2].GetComponent<RectTransform>().transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "그만둔다.";

                chatBoxText.text = "‘어렸을 때 한 번쯤 꿈꿔왔던 공주풍 침대다. 한눈에 봐도 매우 푹신하고 편안해 보인다.’ ";
                //1번선택지 분기점
                if (_2breakChooseNum==1|| _2breakChooseNum == 2 || _2breakChooseNum == 4 || (_2breakChooseNum > 5 && !_2investigateChoose))
                {
                    //1. 페이드인아웃을 켠다
                    startFadeInOut();
                    ifChoiceSetChatBoxText("'포근한 침대에 눕자 깜박 잠에 들었다. 일어나 보니 벌써 다음날이 되었다.'");
                }
                if (_2breakChooseNum == 3)
                {
                    //1. 페이드인아웃을 켠다
                    startFadeInOut();
                    ifChoiceSetChatBoxText("'또 다음날이 되었다.' / '나 이렇게 계속 지내도 되는 걸까? 문득 두려워졌다.'");
                }
                if (_2breakChooseNum == 5)
                {
                    //페이드인아웃을 켠다
                    startFadeInOut();
                    //*긴 대사-> 코루틴대체
                    ifChoiceSetChatBoxText("2번선택지 분기점: 코루틴 대체해야함. 구현 완료하시면 넣기");
                    //2번 엔딩 사진 뜨는 씬으로 이동-> 클릭 시 다시 PlayerRoom씬 로드
                    Debug.Log("아직 엔딩사진 안 나와서 나중 구현");

                }
                //2번 선택지 분기점
                if (_2investigateChoose)
                {
                    startFadeInOut();
                    ifChoiceSetChatBoxText("2번선택지 분기점: 코루틴 대체해야함. 구현 완료하시면 넣기");
                    
                }
                //3번 선택지 분기점
                if(_2quit)
                {
                    ifChoiceSetChatBoxText("그만두자.");
                }
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

    private void startFadeInOut()
    {
        fadeInOut.activate=true;
    }


    //★선택지 뜨고난 후 챗박스 텍스트 세팅    
    private void ifChoiceSetChatBoxText(string text)
    {
        
        //2. 대화창만남긴다.: choice클래스에서 클릭감지후 active false

        //3. 대화창 내용 변화: 선택란이 꺼졌을 시에.
        if (!choices[0].active)//★최소 선택란은 1개인데 그 1개는 [0]번 인덱스에 위치. 따라서 [0]만 체크
        {
            chatBoxText.text = text;   
        }
        
        
        
    }
}
