using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyPet : MonoBehaviour
{
    public AnimationClip Idle;
    public AnimationClip Dig;
    public AnimationClip backflip;
    public AnimationClip laugh;
    public AnimationClip run;
    public AnimationClip Walk;
    public int PetAnimTime = 2;
    float lastUpdate = 0;
    public GameObject Gear;

    void Start(){
        Sequence.Sequence Seq = new Sequence.Sequence(this, "TitleLoop", SequenceType.Lerp,
            Intervals.LerpRotation(Gear, 185.877f, 360f, 172.06f, 1),
            Intervals.LerpRotation(Gear, 185.877f, 1f, 172.06f, 1)
        );
        Seq.Loop();
        //GetComponent<Animation>().Play(run.name);
    }

    void Update()
    {
        
    }
}
