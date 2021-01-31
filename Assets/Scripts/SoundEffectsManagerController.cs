using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsManagerController : MonoBehaviour
{

    public static SoundEffectsManagerController _instance;

    [SerializeField] private AudioSource sound;

    [SerializeField] AudioClip attackSound, collectMeep, hurtPlayer, menuOptionSound, selectMenuOptionSound;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void PlayMenuOptionSound()
    {
       sound.clip = menuOptionSound; //Setear el AudioClip
       sound.Play(); //Reproducir sonido
    }

    public void PlaySelectMenuOptionSound()
    {
        sound.clip = selectMenuOptionSound;
        sound.Play();
    }

    public void PlayAttackSound()
    {
        sound.clip = attackSound; 
        sound.Play(); 
    }

    public void PlayCollectMeepSound()
    {
        sound.clip = collectMeep;
        sound.Play();
    }

    public void PlayHurtPlayerSound()
    {
        sound.clip = hurtPlayer;
        sound.Play();
    }


}
