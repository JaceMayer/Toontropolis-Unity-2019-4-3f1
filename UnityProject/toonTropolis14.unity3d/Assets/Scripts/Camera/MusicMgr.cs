using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMgr : MonoBehaviour
{
    AudioSource NormalBGM;
    AudioSource CogdoFellMusic;

    bool cogdo = false;

    Dictionary<string, AudioSource> AudioDict = new Dictionary<string, AudioSource> {};

    // Start is called before the first frame update
    void Start()
    {
        AudioSource[] Audio = GetComponents<AudioSource>();
        for (int i = 0; i < Audio.Length; i++)
        {
            Debug.Log(Audio[i].clip.name);
            AudioDict.Add(Audio[i].clip.name, Audio[i]);
        }
    }

    public void ChangeToCogdo(){
        cogdo = true;
        AudioDict["INX075_15"].Stop();
        AudioDict["INX075_11"].Stop();
        AudioDict["Villian"].Play();
    }

    public void ChangeToNight(){
        AudioDict["INX075_15"].Stop();
        if(cogdo == true){
            AudioDict["Villian"].Play();
            return;
        }
        AudioDict["Villian"].Stop();
        AudioDict["INX075_11"].Play();
    }

    public void ChangeToDay(){
        AudioDict["INX075_11"].Stop();
        if(cogdo == true){
            AudioDict["Villian"].Play();
            return;
        }
        AudioDict["INX075_15"].Play();
    }


    public void Play(string Clip){
        if(AudioDict[Clip].isPlaying) return;
        AudioDict[Clip].Play();
    }

    public void Stop(string Clip){
        AudioDict[Clip].Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
