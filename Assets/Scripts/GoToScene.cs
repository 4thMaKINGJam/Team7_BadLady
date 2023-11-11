using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
/// <summary>
/// 다른 클래스에서 씬전환 기능 쓰고싶으면 객체 만들어서 메소드 접근하기
/// </summary>
public class GoToScene : MonoBehaviour
{

    public void GoToStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }
    public void GoToPlayerRoomScene()
    {
        SceneManager.LoadScene("PlayerRoomScene");
    }
    public void GoToLivingRoomScene()
    {
        SceneManager.LoadScene("LivingRoomScene");
    }
    public void GoToGardenScene()
    {
        SceneManager.LoadScene("GardenScene");
    }
    public void GoToDescriptScene()
    {
        SceneManager.LoadScene("DescriptScene");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
