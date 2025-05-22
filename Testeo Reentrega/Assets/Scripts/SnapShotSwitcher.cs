using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SnapShotSwitcher : MonoBehaviour
{
    [SerializeField] private AudioMixerSnapshot m_OutsideSnapshot;
    [SerializeField] private AudioMixerSnapshot m_InsideSnapshot;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {

        switch(other.tag)
        {
            case "Outside":
                m_OutsideSnapshot.TransitionTo(0.5f);
                break;
            case "Inside":
                m_InsideSnapshot.TransitionTo(0.5f);
                break;
        }
    }
}
