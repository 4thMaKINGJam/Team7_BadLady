using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public enum SoundType
    {
        bigger
    }

    public SoundType type;
    public AudioSource audio;
    
    // Start is called before the first frame update
    void Start()
    {
        audio=this.gameObject.GetComponent<AudioSource>();
        switch (type)
        {
            case SoundType.bigger:
                audio.volume = 0f;
                StartCoroutine(bigger());
                break;
        }      
    }
    IEnumerator bigger()
    {
        while (true)
        {
            if (audio.volume >= 1f)
            {
                break;
            }
            this.audio.volume += 0.05f;
            yield return new WaitForSeconds(0.5f);
        }
    }
    
}
