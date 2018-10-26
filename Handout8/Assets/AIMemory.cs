using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

// TODO 1: Create a simple class to contain one entry in the blackboard
// should at least contain the gameobject, position, timestamp and a bool
// to know if it is in the past memory

public class BlackBoardConteiner
{
    public GameObject emiter;
    public Vector3 position;
    public float timeStamp;
    public bool inMemory;
    public GameObject reciever;

    public BlackBoardConteiner(GameObject emiter, Vector3 position, float timeStamp, bool inMemory, GameObject reciever)
    {
        this.emiter = emiter;
        this.position = position;
        this.timeStamp = timeStamp;
        this.inMemory = inMemory;
        this.reciever = reciever;
    }

}

public class AIMemory : MonoBehaviour {

	public GameObject Cube;
	public Text Output;

    // TODO 2: Declare and allocate a dictionary with a string as a key and
    // your previous class as value
    public Dictionary<string, BlackBoardConteiner> my_data;

    // TODO 3: Capture perception events and add an entry if the player is detected
    // if the player stop from being seen, the entry should be "in the past memory"

    // Use this for initialization
    void Start () {
        my_data = new Dictionary<string, BlackBoardConteiner>();
    }

    // Update is called once per frame

    public void PerceptionEvent(PerceptionEvent perception)
    {
        int sameEntries = 0;
        if (perception.type == global::PerceptionEvent.types.NEW)
        {
            foreach (KeyValuePair<string, BlackBoardConteiner> entry in my_data)
            {
                if (entry.Value.emiter.ToString().Equals(perception.go.ToString()))
                {
                    entry.Value.position = perception.go.transform.position;
                    entry.Value.timeStamp = Time.time;
                    entry.Value.inMemory = true;
                    entry.Value.reciever = transform.gameObject;
                    sameEntries++;
                    break;
                }
            }
            if (sameEntries == 0)
            {
                BlackBoardConteiner newConteiner = new BlackBoardConteiner(perception.go, perception.go.transform.position, Time.time, true, transform.gameObject);
                my_data.Add(perception.go.ToString(), newConteiner);
            }
        }
        else if (perception.type == global::PerceptionEvent.types.LOST)
        {
            foreach (KeyValuePair<string, BlackBoardConteiner> entry in my_data)
            {
                if (entry.Key.Equals(perception.go.ToString()))
                {
                    entry.Value.position = perception.go.transform.position;
                    entry.Value.inMemory = false;
                    break;
                }
            }
        }
    }

    void Update () 
	{

        // TODO 4: Add text output to the bottom-left panel with the information
        // of the elements in the Knowledge base

        foreach (KeyValuePair<string, BlackBoardConteiner> entry in my_data)
        {
            string new_Text = "\n Emiter: " + entry.Value.emiter.ToString() + "Position: " + entry.Value.position.ToString() + "TimeStamp: " + entry.Value.timeStamp.ToString() + "inMemory: " + entry.Value.inMemory.ToString() + "Reciever:" + entry.Value.reciever.ToString();
            Output.text = new_Text;
            Cube.transform.position = entry.Value.position;
        }

        //Output.text = "test";
    }

}
