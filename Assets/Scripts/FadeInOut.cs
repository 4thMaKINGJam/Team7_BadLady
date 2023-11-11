using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FadeInOut : MonoBehaviour
{
    [SerializeField]private bool active=false; 
    private Image image;
    private Color color;
    private float fadeInAlpha=0f;
    private float fadeOutAlpha = 1f;
    // Start is called before the first frame update
    void Start()
    {
        image=GetComponent<Image>();
        color = image.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (fadeInAlpha != 1f)
            {
                StartCoroutine(FadeIn());
            }
            if (fadeOutAlpha != 0f)
            {
                StartCoroutine(FadeOut());
            } 
            if(fadeInAlpha==1f && fadeOutAlpha == 0f)
            {
                //원상복귀
                /*active = false;
                fadeInAlpha = 0f;
                fadeOutAlpha = 1f;*/
                
                //패널이 클릭 막는 문제로 오브젝트 비활성화로 코드변경
                this.gameObject.SetActive(false);
            }
        }
    }

    IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(2f);//여기서 속도 조절 가능~
        fadeInAlpha += 0.05f;
        color =new Color(color.r,color.g, color.b,fadeInAlpha);
        image.color = color;
    }
    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(2f);//여기서 속도 조절 가능~
        fadeOutAlpha -= 0.05f;
        color = new Color(color.r, color.g, color.b, fadeOutAlpha);
        image.color = color;
        /*
         // GPT: Color는 구조체(Struct)이므로 참조가 아닌 값에 의한 할당이 발생합니다. 따라서 color가 image.color의 값을 복사한 것입니다. 이는 두 변수가 독립적으로 존재함을 의미합니다. 
         따라서 image.color에 color를 또 대입하는 과정 필요. 왜냐면 color는 image.color를 직접 가리키지 않아서
         */
    }
}
