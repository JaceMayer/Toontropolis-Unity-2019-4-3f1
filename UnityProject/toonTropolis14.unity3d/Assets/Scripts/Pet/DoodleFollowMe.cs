using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoodleFollowMe : MonoBehaviour
{
    public GameObject Player;
    public int Distance = 5;
    public AnimationClip Idle;
    public AnimationClip Laugh;
    public AnimationClip Run;
    public AnimationClip Walk;

    Sequence.Sequence Seq = new Sequence.Sequence(null, "doodleMove", SequenceType.Lerp);

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(Player.transform.position, this.gameObject.transform.position);
        if(dist > Distance && Seq.GetState() != SequenceState.running){
            float T = dist * 0.6f;
            //if(!this.GetComponent<Animation>().IsPlaying(Run.name)) this.GetComponent<Animation>().Play(Run.name);
            Seq = new Sequence.Sequence(this, "doodleMove", SequenceType.Lerp,
                Intervals.Func(() => {this.GetComponent<Animation>().Play(Run.name);}),
                Intervals.LerpPos(this.gameObject, Player.transform.position.x, Player.transform.position.y, Player.transform.position.z, T),
                Intervals.Func(() => {this.GetComponent<Animation>().Play(Idle.name);})
            );
            Seq.Start();
            //Vector3 wayPointPos = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z);
            //transform.position = Vector3.MoveTowards(this.gameObject.transform.position, wayPointPos, T);

        }
        else{
            //if(!this.GetComponent<Animation>().IsPlaying(Idle.name)) this.GetComponent<Animation>().Play(Idle.name);
        }


    }
}
