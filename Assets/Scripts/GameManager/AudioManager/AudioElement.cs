using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioElement : MonoBehaviour
{
    private AudioSource audioSource;

    private void OnEnable()
    {
        if(audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    public void Play(AudioClip m_clip)
    {
        audioSource.loop = false;
        audioSource.PlayOneShot(m_clip);
        float audioLen = m_clip.length;
        StartCoroutine(DisableAfterPlay(audioLen));
    }

    IEnumerator DisableAfterPlay(float length)
    {
        yield return new WaitForSeconds(length);
        ObjectPoolManager.Instance.Destroy("AudioElement", gameObject);
    }
}
