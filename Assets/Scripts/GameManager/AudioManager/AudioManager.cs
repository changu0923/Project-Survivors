using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Singleton
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("AudioManager");
                    instance = obj.AddComponent<AudioManager>();
                }
            }
            return instance;
        }
    }
    #endregion

    #region Properties
    public AudioClip UiSelect { get => uiSelect; set => uiSelect = value; }
    public AudioClip Bgm { get => bgm; set => bgm = value; }
    public AudioClip Levelup { get => levelup; set => levelup = value; }
    public AudioClip Win { get => win; set => win = value; }
    public AudioClip Lose { get => lose; set => lose = value; }
    public AudioClip Hit0 { get => hit0; set => hit0 = value; }
    public AudioClip Hit1 { get => hit1; set => hit1 = value; }
    public AudioClip Dead { get => dead; set => dead = value; }
    public AudioClip Melee0 { get => melee0; set => melee0 = value; }
    public AudioClip Melee1 { get => melee1; set => melee1 = value; }
    public AudioClip Shoot { get => shoot; set => shoot = value; }
    public AudioClip Coin { get => coin; set => coin = value; }
    #endregion

    private AudioSource audioSource;

    [Header("AudioClip Storage")]
    [SerializeField] AudioSource t_BGM;
    [SerializeField] Transform t_Effect;
    [SerializeField] GameObject audioSourcePrefab;

    [Header("AudioClip List")]
    [Header("UI")]
    [SerializeField] AudioClip uiSelect;
    [Header("GameScene")]
    [SerializeField] AudioClip bgm;
    [SerializeField] AudioClip levelup;
    [SerializeField] AudioClip win;
    [SerializeField] AudioClip lose;
    [Header("Effect")]
    [SerializeField] AudioClip hit0;
    [SerializeField] AudioClip hit1;
    [SerializeField] AudioClip dead;
    [SerializeField] AudioClip melee0;
    [SerializeField] AudioClip melee1;
    [SerializeField] AudioClip shoot;
    [SerializeField] AudioClip coin;

    private bool flagBGM;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(this);
    }

    public void PlayBGM()
    {
        flagBGM = !flagBGM;
        if (flagBGM == true)
        {
            t_BGM.clip = bgm;
            t_BGM.Play();
            t_BGM.loop = true;
        }
        else
        {
            t_BGM.Stop();
        }
    }
    
    public void PlayUIButton()
    {
        audioSource.PlayOneShot(uiSelect);
    }
    
    public void Play(AudioClip m_clip)
    {
        AudioElement newSource = ObjectPoolManager.Instance.Instantiate("AudioElement", audioSourcePrefab).GetComponent<AudioElement>();
        newSource.transform.SetParent(t_Effect);
        newSource.Play(m_clip);
    }
}
