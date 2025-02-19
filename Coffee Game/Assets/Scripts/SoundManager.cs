using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource soundObject;

    void Awake()
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
    }

    public static SoundManager Instance { get; private set; }

    public void playSoundClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        //spawn object
        AudioSource audiosource = Instantiate(soundObject, spawnTransform.position, Quaternion.identity);
        //assign clip
        audiosource.clip = audioClip;
        //assign volume
        audiosource.volume = volume;
        //play sound
        audiosource.Play();
        //get length of clip
        float clipLength = audiosource.clip.length;
        //destroy clip when done
        Destroy(audiosource.gameObject, clipLength);
    }


}
