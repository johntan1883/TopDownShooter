using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButtonSound : MonoBehaviour
{
    private AudioSource buttonSound;

    // Start is called before the first frame update
    void Start()
    {
        buttonSound = GetComponent<AudioSource>();
    }

    public void OnButtonClick()
    {

        if (buttonSound != null)
        {
            buttonSound.Play();
        }
    }
}
