using System.Collections;
using System.Collections.Generic;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using UnityEngine;

public class CutsceneManager : TemporalSingleton<CutsceneManager>
{
    public List<TimelineAsset> cutscenes;
    public PlayableDirector director;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayCutscene(int cutsceneNum, TimelineAsset playable)
    {
        if(cutsceneNum == 0)
        {
            director.playableAsset = playable;
        }
        else
        {
            director.playableAsset = cutscenes[cutsceneNum];
        }
        director.Play();

    }
}
