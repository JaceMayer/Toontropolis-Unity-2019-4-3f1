using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogDominium : MonoBehaviour
{
    public GameObject DoorL;
    public GameObject DoorR;
    public GameObject Locator; 
    public GameObject ToonHall;
    public GameObject Library;
    public GameObject Bank;
    public GameObject Camera;
    public GameObject[] suits;

    float XOpenL = 1.45f;
    float XOpenR = -1.45f;
    float XClosed = 0f;

    int numSuits = 10;
    float TimeBetweenSuit = 1.5f;

    bool HasFallen = false;

    bool DoorIsOpen(){
        return DoorL.transform.position.x == XOpenL && DoorR.transform.position.x == XOpenR;
    }

    void DoDoorOpen(){
        new Sequence.Parallel(this, "Cogdo-door-anim", SequenceType.Lerp,
            Intervals.LerpLocalPos(DoorL, XOpenL, 0, 0, 3),
            Intervals.LerpLocalPos(DoorR, XOpenR, 0, 0, 3)
        ).Start();
    }

    void DoDoorClose(){
        new Sequence.Parallel(this, "Cogdo-door-animClose", SequenceType.Lerp,
            Intervals.LerpPos(DoorL, XClosed, 0, 0, 1),
            Intervals.LerpPos(DoorR, XClosed, 0, 0, 1)
        ).Start();
    }

    void AffectSurroundings(){
        Bank.GetComponent<RandomAnim>().ToggleFear();
        Library.GetComponent<RandomAnim>().ToggleFear();
        Camera.GetComponent<MusicMgr>().ChangeToCogdo();

    }

    IEnumerator StartSuits(){
        Debug.Log(suits);
        for(int i = 0; i < numSuits; i++){
            int Cog = UnityEngine.Random.Range(0, 2);
            GameObject TheCog = (GameObject)Instantiate(suits[Cog]);
            TheCog.SetActive(true);
            yield return new WaitForSeconds(TimeBetweenSuit);
        }
    }

    void DoFall(){
        if(HasFallen) return;
        HasFallen = true;
        ToonHall.GetComponent<RandomAnim>().ToggleFall();
        new Sequence.Sequence(this, "Cogdo-fall", SequenceType.Lerp, 
            Intervals.Wait(2f),
            Intervals.Func(AffectSurroundings),
            Intervals.LerpPos(this.gameObject, 51.44428f, Locator.transform.position.y, 51.12434f, 2),
            Intervals.Wait(3),
            Intervals.Func(DoDoorOpen),
            Intervals.Wait(1),
            StartSuits()
        ).Start();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) DoFall();
        
    }
}
