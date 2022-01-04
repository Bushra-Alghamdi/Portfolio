using UnityEngine.Audio;
using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioSource _musicSource, _effectsSource;
    public AudioFile[] sounds;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }

        foreach (AudioFile s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }
  


    public void PlaySound(AudioClip clip)
    {
        _musicSource.PlayOneShot(clip);
        
    }

    public void PlaySFX(string sfxName)
    {
        AudioFile s = Array.Find(sounds, sound => sound.name == sfxName);
        s.source.Play();
    }

}
