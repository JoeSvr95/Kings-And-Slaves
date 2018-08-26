using UnityEngine.Audio;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {

    public Sound[] sounds;
    public static AudioManager instance;


	// Use this for initialization
	void Awake () {

        if(instance == null){
            instance = this;
        }    
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
		
	}

    private void Start()
    {
        Play("backgroundStartMenu");
    }
    
    void Play (String name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound" + name + " not found!");
            return;
        }

        s.source.Play();
		
	}

    void Stop (String name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }

        s.source.Stop();
	}

    public void StopSceneMusic(int sceneIndex){
        sounds[sceneIndex].source.Stop();
    }

    public void PlaySceneMusic(int sceneIndex){
        switch (sceneIndex){
            case 0:
                Play("backgroundStartMenu");
                break;
            case 1:
                Play("backgroundLevel");
                break;
            default:
                Play("backgroundStartMenu");
                break;
        }
    }

    public void PlaySwordSound(){
        Play("swordSound");
    }

    public void PlayDoorSound(){
        Play("doorUnlockSound");
    }

    public void PlayDoorLocked(){
        Play("doorLockSound");
    }

    public void PlayKeyPickUp(){
        Play("keyPickUpSound");
    }

    public void PlayBrakeVase(){
        Play("vaseBreakSound");
    }

    public void PlayBowShotSound(){
        Play("bowShootSound");
    }

    public void PlayHealthSound(){
        Play("healthSound");
    }

    public void PlayPotionSound(){
        Play("potionSound");
    }

    public void PlayOuchEnemySound(){
        Play("ouchEnemySound");
    }

    public void PlayOuchPlayerSound(){
        Play("ouchPlayerSound");
    }

    public void PlayGameOverSound(){
        Stop("backgroundLevel");
        Play("gameOverSound");
    }

    public void StopGameOverSound(){
        Stop("gameOverSound");
    }

}
