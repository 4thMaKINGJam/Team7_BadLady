using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// 상태관할
/// </summary>
public class GameManager : MonoBehaviour
{

    public TextMeshProUGUI chatBoxText;
    public GameObject chatBox;
    public GameObject SpaceBar;
    //ObjectController에서 접근
    [HideInInspector] public Define.ObjectName objectName;
    [HideInInspector] public bool _choose7 = false;
    [HideInInspector] public bool _choose18 = false;
    [HideInInspector] public bool _choose21 = false;

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

    // 한 아이템당 대사가 여러 개일 때 사용할 변수
    int i = 0;

    // LivingRoomScene에서 필요한 변수
    int clickEnvelope = 1;

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
            if (objectName == Define.ObjectName._6)
            {
                chatBoxText.text = "길고 큰 유럽식 창문이다. 밖으로 아름다운 정원의 풍경이 보인다.";
            }
            else if (objectName == Define.ObjectName._7)
            {
                if (_choose18 == false)
                {
                    chatBoxText.text = "지금 내 몸의 초상화다. 이렇게 큰 단독 초상화라니, 진짜 나도 아니면서 괜히 내가 머쓱해지는 기분이야.";
                }
                else
                {
                    string[] lineArray = { "지금 내 몸의 초상화다. 이렇게 큰 단독 초상화라니, 진짜 나도 아니면서 괜히 내가 머쓱해지는 기분이다.", "그나저나 저 시선을 사로잡는 초록색 목걸이.. 내 방의 보석함에는 없었는데?" };

                    if (i == 0)
                    {   // 첫번째 대사 출력
                        SpaceBar.SetActive(true);
                        chatBoxText.text = lineArray[i];
                        i++;
                    }
                    else
                    {   // 두번째 대사부터
                        // Check for space key press한 후 대사 출력
                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            if (i < lineArray.Length)
                            {
                                chatBoxText.text = lineArray[i];
                                i++;
                            }
                            else
                            {
                                i = 0;  // 대사 개수 초기화
                                chatBox.SetActive(false);   // 대화창 닫기
                                SpaceBar.SetActive(false);
                            }
                        }
                    }

                }
            }
            else if (objectName == Define.ObjectName._8) // 선택지 추가 필요
            {
                chatBoxText.text = "저택 밖 정원으로 나가는 문이다.";
            }
            else if (objectName == Define.ObjectName._9)
            {
                chatBoxText.text = "화려한 장식이 돋보이는 소파와 티 테이블이다. 이곳에서 그 로판 소설 속 티타임이 이뤄지는 건가 봐.";
            }
            else if (objectName == Define.ObjectName._10) // need to add Dialogue Choices
            {
                string[] lineArray = { "매우 비싸 보이는 러그다. 청소하고 관리하려면 매우 힘들겠지.", "혹시나 하는 마음에 러그를 구석구석 조사했다. 러그 밑에서 찢긴 책의 한 페이지를 발견했다.", "' ...세계와 세계를 잇고 시간과 공간을 뛰어넘는 힘을 가질지어니, 이 주문은 바로 모빌리코푸스 엑스파시오 이다. 이 두 가지 조건을 반드시 기억하라. 조건을 충족시키지 않으면 무슨 일이 일어날지 모른다.' 라고 적혀있다." };

                if (i == 0)
                {   // 첫번째 대사 출력
                    SpaceBar.SetActive(true);
                    chatBoxText.text = lineArray[i];
                    i++;
                }
                else
                {   // 두번째 대사부터
                    // Check for space key press한 후 대사 출력
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        if (i < lineArray.Length)
                        {
                            chatBoxText.text = lineArray[i];
                            i++;
                        }
                        else
                        {
                            i = 0;  // 대사 개수 초기화
                            chatBox.SetActive(false);   // 대화창 닫기
                            SpaceBar.SetActive(false);
                        }
                    }
                }

            }
            else if (objectName == Define.ObjectName._11)
            {
                if (_choose21 == false || (_choose18 == false && _choose7 == false))
                {
                    chatBoxText.text = "로사벨라: 와, 내가 평생 일해도 못 살 것 같은 꽃병인데.";
                }
                else if (_choose21 == true && (_choose18 == true && _choose7 == true)) // need to add Dialogue Choice
                {
                    string[] lineArray = { "에밀리라는 하녀가 누군진 모르겠으나, 혹시나 하는 마음에 꽂혀있는 꽃을 꺼내고 꽃병 안을 보았다.", "꽃병 속에서 그림 속 초록색 목걸이를 찾았다. 잘 챙겨둬야겠다." };

                    if (i == 0)
                    {   // 첫번째 대사 출력
                        SpaceBar.SetActive(true);
                        chatBoxText.text = lineArray[i];
                        i++;
                    }
                    else
                    {   // 두번째 대사부터
                        // Check for space key press한 후 대사 출력
                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            if (i < lineArray.Length)
                            {
                                chatBoxText.text = lineArray[i];
                                i++;
                            }
                            else
                            {
                                i = 0;  // 대사 개수 초기화
                                chatBox.SetActive(false);   // 대화창 닫기
                                SpaceBar.SetActive(false);
                            }
                        }

                    }
                }

            }
            else if (objectName == Define.ObjectName._12)// need to add Dialogue Choices
            {
                switch (clickEnvelope)
                {
                    case 1:
                        string[] lineArray1 = { "협탁 위에 편지 봉투 3개가 올려져 있다. 그중 하나를 열어보았다.", "로사벨라 영애 잘 지내셨나요? 저번에 그 요망한 아네이스 영애에게 당했던 일 너무 마음에 두지 마세요. 우리는 언제나 당신의 편이랍니다. ...", "편지에는 시시콜콜한 잡담으로 가득하다. 소설 속에서 로사벨라가 아네이스에게 망신 주려다가 역으로 당하는 에피소드가 기억난다. 아무래도 그 에피소드를 말하는 것 같은데?" };

                        if (i == 0)
                        {   // 첫번째 대사 출력
                            SpaceBar.SetActive(true);
                            chatBoxText.text = lineArray1[i];
                            i++;
                        }
                        else
                        {   // 두번째 대사부터
                            // Check for space key press한 후 대사 출력
                            if (Input.GetKeyDown(KeyCode.Space))
                            {
                                if (i < lineArray1.Length)
                                {
                                    chatBoxText.text = lineArray1[i];
                                    i++;
                                }
                                else
                                {
                                    i = 0;  // 대사 개수 초기화
                                    chatBox.SetActive(false);   // 대화창 닫기
                                    SpaceBar.SetActive(false);
                                }
                            }
                        }
                        clickEnvelope++;
                        break;
                    case 2:
                        string[] lineArray2 = { "두 번째 편지봉투를 열어보았다.", "로사벨라 영애, 그 이야기 들으셨나요? 글쎄, 아네이스 영애가 어제 열린 연회에서 음식을 먹고 쓰러진 거 있죠? 알고 보니 아네이스 영애가 견과류 알레르기가 있다네요. ... ", "그 뒤로는 쓸모없는 이야기만 있다." };

                        if (i == 0)
                        {   // 첫번째 대사 출력
                            SpaceBar.SetActive(true);
                            chatBoxText.text = lineArray2[i];
                            i++;
                        }
                        else
                        {   // 두번째 대사부터
                            // Check for space key press한 후 대사 출력
                            if (Input.GetKeyDown(KeyCode.Space))
                            {
                                if (i < lineArray2.Length)
                                {
                                    chatBoxText.text = lineArray2[i];
                                    i++;
                                }
                                else
                                {
                                    i = 0;  // 대사 개수 초기화
                                    chatBox.SetActive(false);   // 대화창 닫기
                                    SpaceBar.SetActive(false);
                                }
                            }
                        }
                        clickEnvelope++;
                        break;
                    case 3:
                        string[] lineArray3 = { "세 번째 편지봉투를 열어보았다.", "봉투를 여니 정갈하고 유려한 글씨체로 적힌 편지지가 나왔다.", "'친애하는 로사벨라 영애에게, 저번에 그 일은 정말 죄송하게 생각하고 있어요. 고의가 아니었지만 찾아봬서 한번 이야기할 수 있을까요?  ...초대 기다리고 있을게요, 아네이스 드림' ", "여자 주인공이 나에게 쓴 편지다. 대충 읽어봐도 여자 주인공이 얼마나 사려 깊고 착한 캐릭터인지 느껴져." };

                        if (i == 0)
                        {   // 첫번째 대사 출력
                            SpaceBar.SetActive(true);
                            chatBoxText.text = lineArray3[i];
                            i++;
                        }
                        else
                        {   // 두번째 대사부터
                            // Check for space key press한 후 대사 출력
                            if (Input.GetKeyDown(KeyCode.Space))
                            {
                                if (i < lineArray3.Length)
                                {
                                    chatBoxText.text = lineArray3[i];
                                    i++;
                                }
                                else
                                {
                                    i = 0;  // 대사 개수 초기화
                                    chatBox.SetActive(false);   // 대화창 닫기
                                    SpaceBar.SetActive(false);
                                }
                            }
                        }
                        clickEnvelope++;
                        break;

                }
                //else if(objectName == Define.ObjectName._25)  // need to add Dialogue Choices
                //{
                //    chatBoxText.text = "내 방으로 돌아간다";
                //}
            }
        }
        // *****************************
        // ******* Event1 Scene *******
        else if (SceneManager.GetActiveScene().name.Equals("Event1Scene"))
        {
            if (objectName == Define.ObjectName._66)
            {
                chatBoxText.text = "지금은 이게 중요한 게 아니야.";
            }
            else if (objectName == Define.ObjectName._77)
            {
                chatBoxText.text = "이 어색한 분위기에 당장이라도 이 창문 밖으로 뛰쳐나가고 싶다.";
            }
            else if (objectName == Define.ObjectName._88)
            {
                chatBoxText.text = "저택 밖 정원으로 나가는 문이다. 하지만 손님을 두고 나갈 수는 없지.";
            }
            else if (objectName == Define.ObjectName._99 || objectName == Define.ObjectName._1111 || objectName == Define.ObjectName._1212 || objectName == Define.ObjectName._1919)
            {
                chatBoxText.text = "지금은 이게 중요한 게 아니야.";
            }
            //else if (objectName == Define.ObjectName._1010 && clickEnvelope == 2)    // need to add Dialogue Choices
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
            if (objectName == Define.ObjectName._222)
            {
                chatBoxText.text = "지금 여유롭게 누워있을 때가 아니야.";
            }
            else if (objectName == Define.ObjectName._333 || objectName == Define.ObjectName._444 || objectName == Define.ObjectName._1818)
            {
                chatBoxText.text = "지금은 이걸 볼 때가 아니야.";
            }
            else if (objectName == Define.ObjectName._555)
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
                SpaceBar.SetActive(false);
            }
        }
}
