using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System;

public enum SequenceState{ // Sequence states
    running,
    paused,
    stopped,
    notStarted,
    finished
}

public enum SequenceType{
    Lerp,
    Landing,
    General
}

namespace Sequence{ 

    // Manages the sequences
    public static class SequenceManager{

        public static Dictionary<string, Sequence> Sequences = new Dictionary<string, Sequence> {}; // Dictionary of sequences, so we can keep track of them

        public static void AddSequence(string name, Sequence seq){ // Adds a sequence
            RemoveSequence(name);
            Sequences.Add(name, seq);
        }

        public static void RemoveSequence(string name, bool throwEx = false){ // Removes  a sequence (and stops it)
            if(!Sequences.ContainsKey(name) && throwEx == true) throw new NullReferenceException(name  +" Is not a running sequence");
            if(!Sequences.ContainsKey(name) && throwEx != true) return;
            Sequences[name].Stop();
            Sequences.Remove(name);
        }

        public static bool HasSequenceNamed(string name){ //(checks if we have a sequence named x)
            return Sequences.ContainsKey(name);
        }

        public static Sequence GetSequence(string name){ //(Gets a sequence named x)
            return Sequences[name];
        }

        public static void DumpSequences(){
            Debug.Log("-------------------------------------");
            foreach(KeyValuePair<string, Sequence> SQ in Sequences){
                Debug.Log("Sequence: " + SQ.Value.Name + " Playing routine: " + SQ.Value.CurrentRoutine);
            }
            Debug.Log("-------------------------------------");
        }

    }
    // The actual sequence
    public class Sequence
    {
        // Stores the Coroutines to run
        public List<IEnumerator> Coroutines = new List<IEnumerator> {};

        //Stores the running state of this sequence
        public SequenceState running = SequenceState.notStarted;

        // Stores the sequences own coroutine
        public IEnumerator Coroute = null;

        public IEnumerator CurrentRoutine = null;

        public MonoBehaviour Parent;

        public string Name;

        public SequenceType Type = SequenceType.Lerp;

        public bool Looping = false;

        // Add the coroutines to the list
        public Sequence(MonoBehaviour prnt, string name, SequenceType type, params IEnumerator[] coroutines){
            Parent = prnt;
            Name = name;
            Type = type;
            SequenceManager.AddSequence(Name, this);
            foreach(IEnumerator co in coroutines){
                Coroutines.Add(co);
            }
        }


        // Used to add New coroutines
        public void Append(params IEnumerator[] coroutines){
            foreach(IEnumerator co in coroutines){
                Coroutines.Add(co);
            }
        }

        // Starts the sequence from a mono behavior - ran like
        // Sequence.Sequence _seq = New Sequence();
        // Sequence.Start(this);
        public void Start(){
            running = SequenceState.running;
            if(Coroute != null) return;
            Coroute = __start();
            Parent.StartCoroutine(Coroute);
        }

        public void Loop(){
            running = SequenceState.running;
            if(Coroute != null) return;
            Looping = true;
            Coroute = __loop();
            Parent.StartCoroutine(Coroute);
        }

        // Pauses a running sequence
        public void Pause(){
            running = SequenceState.paused;
        }

        // Stops the sequence and cleans it up
        public void Stop(){
            running = SequenceState.stopped;
            Looping = false;
            if(CurrentRoutine != null && Parent != null){
                Parent.StopCoroutine(CurrentRoutine);
                CurrentRoutine = null;
            }
            Coroute = null;
            Coroutines.Clear();
        }

        // Used to get the running status of a sequence
        public SequenceState GetState(){
            return running;
        }

        // This actually runs the sequences
        private IEnumerator __start(){
            foreach(IEnumerator CRT in Coroutines.ToArray()){
                if(running == SequenceState.stopped) break;
                if(Parent == null){
                    Stop();
                    break;
                }
                if(running == SequenceState.paused){
                    yield return new WaitForSeconds(.1f);
                    continue;
                }
                CurrentRoutine = CRT;
                yield return Parent.StartCoroutine(CRT);
            }
            Coroutines.Clear();
            running = SequenceState.finished;
            SequenceManager.RemoveSequence(Name);
        }

        private IEnumerator __loop(){
            while(Looping){
                foreach(IEnumerator CRT in Coroutines.ToArray()){
                    if(running == SequenceState.stopped) break;
                    if(Parent == null){
                        Stop();
                        break;
                    }
                    if(running == SequenceState.paused){
                        yield return new WaitForSeconds(.1f);
                        continue;
                    }
                    CurrentRoutine = CRT;
                    yield return Parent.StartCoroutine(CRT);
                }
            }
            Coroutines.Clear();
            running = SequenceState.finished;
            SequenceManager.RemoveSequence(Name);
        }

    }


    public class Parallel: Sequence
    {

        public Parallel(MonoBehaviour prnt, string name, SequenceType type, params IEnumerator[] coroutines) : base(prnt, name, type, coroutines){
        }

        public void Start(){
            running = SequenceState.running;
            if(Coroute != null) return;
            Coroute = __start();
            Parent.StartCoroutine(Coroute);
        }

        // This actually runs the sequences
        private IEnumerator __start(){
            foreach(IEnumerator CRT in Coroutines){
                if(running == SequenceState.stopped) break;
                if(Parent == null){
                    Stop();
                    break;
                }
                if(running == SequenceState.paused){
                    yield return new WaitForSeconds(.1f);
                    continue;
                }
                Parent.StartCoroutine(CRT);
                Debug.Log("Playing corutine: " + CRT);
            }
            yield return null;//new WaitForSeconds(.1f);
            Coroutines.Clear();
            running = SequenceState.finished;
            SequenceManager.RemoveSequence(Name);
        }

    }

}