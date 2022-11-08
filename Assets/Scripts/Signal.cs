using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signal : MonoBehaviour
{
    public MeshRenderer mr;
    public AudioSource soundEffect;

    public void SetColorGreen()
    {
        mr.material.color = new Color(0, 1, 0);
    }

    public void SetSignalRed()
    {
        mr.material.color = new Color(1, 0, 0);
    }

    public void PlayBellSound()
    {
        soundEffect.Play();
    }
}
