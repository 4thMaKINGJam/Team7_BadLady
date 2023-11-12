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
    //오디오관련
    public AudioSource audioSource;
    public AudioClip audioPlay;
    public AudioClip audiowater;
    public AudioClip audiobed;
    public AudioClip audioPaper;
    public AudioClip audioPen;
    public AudioClip audioShoes;
    public AudioClip audioDesk;
    public AudioClip audioAccessory;
    ///////////////////////////////////////////////////

    [HideInInspector] public bool haveChoices = false;

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

            switch (objectName)
            {
                case Define.ObjectName._1:
                    chatBoxText.text = " ‘우아한 창틀과 고급스럽고 두꺼운 소재의 커튼이다. 창밖으로는 비현실적으로 아름다운 정원이 보인다.' \r\n/ '밑을 내려다보니 상당히 높이 있는 게 떨어지면 큰일 날지도..’";
                    break;
                case Define.ObjectName._3:
                    //*계속대입 문제 해결하기
                    chatBoxText.text = "‘화려한 틀이 인상적인 거울 속에서 적발의 날카롭게 생겼지만 아름다운 여성이 보인다.' '살짝 손을 들어 얼굴을 건드려 보지만 여전히 이 모습이 나라는 게 믿기지 않아.’";
                    break;
                case Define.ObjectName._2:
                    //★예시
                    choices[0].GetComponent<RectTransform>().transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "침대에 누워 휴식을 취한다.";
                    choices[1].GetComponent<RectTransform>().transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "침대를 조사한다.";
                    choices[2].GetComponent<RectTransform>().transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "그만둔다.";

                    chatBoxText.text = "‘어렸을 때 한 번쯤 꿈꿔왔던 공주풍 침대다. 한눈에 봐도 매우 푹신하고 편안해 보인다.’ ";
                    //1번선택지 분기점
                    if (_2breakChooseNum == 1 || _2breakChooseNum == 2 || _2breakChooseNum == 4 || (_2breakChooseNum > 5 && !_2investigateChoose))
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
                    if (_2quit)
                    {
                        ifChoiceSetChatBoxText("그만두자.");
                    }
                    break;
            }
  
     
        }
        
        //가든씬
        if (SceneManager.GetActiveScene().name.Equals("GardenScene"))
        {

            if (objectName == Define.ObjectName._20)
            {
                chatBoxText.text = "정원 한쪽에 어마어마하게 큰 호수가 보인다. 깊어 보이기도 하는데 빠지면 큰일 나겠는데?";

            }
            if (objectName == Define.ObjectName._21)
            {
                if (_choose18 == true && _choose7 == true)
                {
                    int cnt = 0;
                    switch (cnt)
                    {
                        case 0:
                            chatBoxText.text = "아, 아가씨가 평소 하고 다니시는 그 에메랄드빛 목걸이요? 분명 보석함에 있을 텐데..";
                            cnt++;
                            break;
                        case 1:
                            chatBoxText.text = "아가씨 방을 청소하는 에밀리가 아까 꽃병을 닦던데, 에밀리에게 물어보는 거 어떤가요?";
                            cnt++;
                            break;
                    }
                }
                else
                {
                    chatBoxText.text = "아가씨, 무슨 일이세요? 필요한 게 있으면 언제든지 말씀하세요!";
                }

            }
            if (objectName == Define.ObjectName._22)
            {
                string sen1 = "베르사유 궁전 뺨치는 화려한 저택이다.";
                string sen2 = "이 저택이 내 집이라니 대한민국에서는 작은 내 집 마련도 힘든데 그냥 이 몸으로 사는 것도 나쁘지 않을지도?";
                StartCoroutine(NextComment(sen1, sen2));

            }

            if (objectName == Define.ObjectName._23)
            {
                //계속대입 문제 해결하기
                chatBoxText.text = "정원용 가위다. 분명 어디 쓸 곳이 있을 것 같은데, 슬쩍 몰래 챙겨둬야겠다.";

            }

            if (objectName == Define.ObjectName._24)
            {
                //계속대입 문제 해결하기
                chatBoxText.text = "빨간 장미가 풍성하게 피어있는 덤불이다. 로사벨라의 취향일 것 같다.";
            }


            if (objectName == Define.ObjectName._26)
            {
                //계속대입 문제 해결하기
                chatBoxText.text = "가만히 보고만 있어도 기분이 좋아지는 고풍스러운 스타일의 분수다";
            }
        }
        if (SceneManager.GetActiveScene().name.Equals("Event_GardenScene"))
        {
            if (objectName == Define.ObjectName._2222)
            {
                chatBoxText.text = "다행히 아직 건물 안에 있는 사람들은 내가 밖으로 나온 걸 모르는 듯하다.";
            }

            if (objectName == Define.ObjectName._2424)
            {
                chatBoxText.text = "이 덤불에 내 몸을 숨길 수 있을까? 아니 애초에 숨고 나서는 어떡하게..";
            }
            if (objectName == Define.ObjectName._2626)
            {
                chatBoxText.text = "지금은 이게 중요한 게 아니야";
            }
            if (objectName == Define.ObjectName._2020)
            {
                if (foundNeckless == true
                 && foundClue == true)
                {

                    string sen1 = "이야기의 흐름을 방해하면 안 된다... 그 말은 내가 원작대로 죽어야, 아니 적어도 죽은 것처럼 보여야 한다는 것이겠지.";
                    string sen2 = "나는 신발을 벗어 호수 앞에 가지런히 두고 차가운 호수로 거침없이 들어갔다.";
                    string sen3 = "마침 타이밍 좋게 저 멀리서 나를 찾는 소리가 들린다";
                    string sen4 = "호수의 중심부까지 들어가면서 난 서둘러 품 안에서 오묘한 에메랄드빛을 내는 목걸이와 주문이 적힌 페이지를 꺼냈다.";
                    string sen5 = "로사벨라: 제발 나 집으로 돌아가게 해줘! 모빌리코푸스 엑스파시오!";
                    string sen6 = "주문을 다 외침과 동시에 더 이상 내 발이 호수 바에 닿지 않으면서 나는 물속으로 가라앉았다. 그 순간 손에 쥐고 있던 목걸이에서 기이한 초록빛이 나를 덮쳤다";
                    StartCoroutine(Ending(sen1, sen2, sen3, sen4, sen5, sen6));
                }

                else
                {
                    string sen1 = "나는 무작정 호수까지 뛰어갔지만, 이제는 더 이상 도망칠 곳도 없다";
                    string sen2 = "뒤에서 에드윈 황태자와 기사들의 섬뜩한 발걸음이 들린다";
                    string sen3 = "로사벨라: 잠시만요, 제발 제 말 좀 믿어주세요! 아네이스 영애가 쓰러진 건 저도 모르는 일이에요!";
                    string sen4 = "내가 한거 아니라고, 애초에 난 로사벨라도 아니라고 미친놈아!";
                    string sen5 = "하지만 나의 애절한 외침에도 나에게 다가오는 에드윈 황태자는 마음이 바뀔 생각이 전혀 없어 보인다.";
                    StartCoroutine(Ending2(sen1, sen2, sen3, sen4, sen5));
                }
            }
        }

    }
    IEnumerator NextComment(string sen1, string sen2)
    {
        chatBoxText.text = sen1;
        yield return new WaitForSeconds(1.0f);
        chatBoxText.text = sen2;
    }
    IEnumerator Ending(string sen1, string sen2, string sen3, string sen4, string sen5, string sen6)
    {
        chatBoxText.text = sen1;
        yield return new WaitForSeconds(1.0f);
        chatBoxText.text = sen2;
        yield return new WaitForSeconds(1.0f);
        Playsound("water");
        chatBoxText.text = sen3;
        yield return new WaitForSeconds(1.0f);
        chatBoxText.text = sen4;
        yield return new WaitForSeconds(1.0f);
        chatBoxText.text = sen5;
        yield return new WaitForSeconds(1.0f);
        chatBoxText.text = sen6;

    }
    IEnumerator Ending2(string sen1, string sen2, string sen3, string sen4, string sen5)
    {
        chatBoxText.text = sen1;
        yield return new WaitForSeconds(1.0f);
        chatBoxText.text = sen2;
        yield return new WaitForSeconds(1.0f);
        Playsound("water");
        chatBoxText.text = sen3;
        yield return new WaitForSeconds(1.0f);
        chatBoxText.text = sen4;
        yield return new WaitForSeconds(1.0f);
        chatBoxText.text = sen5;


    }
    void Playsound(string action)
    {
        switch (action)
        {

            case "water":
                audioSource.clip = audiowater;
                break;

            case "bed":
                audioSource.clip = audiobed;
                break;

            case "paper":
                audioSource.clip = audioPaper;
                break;
            case "pen":
                audioSource.clip = audioPen;
                break;
            case "shoe":
                audioSource.clip = audioShoes;
                break;
            case "desk":
                audioSource.clip = audioDesk;
                break;
            case "acessory":
                audioSource.clip = audioAccessory;
                break;


        }

        audioSource.Play();
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
