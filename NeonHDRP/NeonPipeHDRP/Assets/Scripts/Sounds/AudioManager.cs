using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MySingleton<AudioManager>
{

    public SoundParam[] soundsFx;

    
    private bool fightLaunched = false; 


    protected override void Awake()
    {
        base.Awake();
    }

    public void Start()
    {
        Play("AmbienceLabo", this.gameObject);
    }

    public void FightLaunch()
    {
        if(fightLaunched == false)
        {
            fightLaunched = true;
            Play("MusicFight", this.gameObject);
        }
    }

    public void FightStop()
    {
        SoundParam sp = Array.Find(soundsFx, sound => sound.name.Equals(name));
        sp.audioSource.Stop();
    }

    public void Play(string name, GameObject sourceObject)
    {
        try{
            SoundParam sp = Array.Find(soundsFx, sound => sound.name.Equals(name));
        

            //Si vide, on l'initialise (il va avoir la position de l'objet child)
            if(sp.audioSource == null)
            {

                LoadSource(sp, sourceObject);

            }

            if( name.Equals("FootSteps"))
            {
                float rand = UnityEngine.Random.Range(0.95f,1.5f);
                sp.audioSource.pitch = rand;
            }
            if(sp.loop == true){
                sp.audioSource.Play();
            }
            {
                sp.audioSource.PlayOneShot(sp.audioClip);
            }
        }
        catch(NullReferenceException){
            Debug.Log("Audio clip" + name + " non existant");
        }
    } 

    void LoadSource(SoundParam sp, GameObject sourceObject)
    {
        sp.audioSource = sourceObject.AddComponent<AudioSource>();

        sp.audioSource.clip = sp.audioClip;
        sp.audioSource.volume = sp.volume;

        sp.audioSource.pitch = sp.pitch;
        sp.audioSource.loop = sp.loop;

        sp.audioSource.spatialBlend = sp.spatialBlend;
    }
}
