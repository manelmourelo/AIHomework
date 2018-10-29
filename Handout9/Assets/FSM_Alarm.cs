﻿using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class FSM_Alarm : MonoBehaviour {

    private bool player_detected = false;
    private bool in_alarm = false;
    private Vector3 patrol_pos;

    public GameObject alarm;
    public BansheeGz.BGSpline.Curve.BGCurve path;

    NavMeshAgent navAgent;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == alarm)
            in_alarm = true;
    }

    // Update is called once per frame
    void PerceptionEvent(PerceptionEvent ev)
    {
        if (ev.type == global::PerceptionEvent.types.NEW)
        {
            player_detected = true;
        }
    }

    // TODO 1: Create a coroutine that executes 20 times per second
    // and goes forever. Make sure to trigger it from Start()

    IEnumerator WaitForPlayer()
    {
        while (player_detected == false){
            Debug.Log("entering Patrol");
            yield return new WaitForSeconds(0.05f);
        }
        StartCoroutine("RunToAlarm");
    }

    // Use this for initialization
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        StartCoroutine("WaitForPlayer");
    }


    // TODO 2: If player is spotted, jump to another coroutine that should
    // execute 20 times per second waiting for the player to reach the alarm
    IEnumerator RunToAlarm()
    {
        StopCoroutine("WaitForPlayer");
        patrol_pos = transform.position;
        path.gameObject.SetActive(false);
        while (in_alarm == false)
        {
            navAgent.destination = alarm.transform.position;
            Debug.Log("going to alarm");
            yield return new WaitForSeconds(0.05f);
        }
        StartCoroutine("ReturnToPos");
    }


    // TODO 3: Create the last coroutine to have the tank waiting to reach
    // the point where he left the path, and trigger again the patrol
    IEnumerator ReturnToPos()
    {
        StopCoroutine("RunToAlarm");
        in_alarm = false;
        player_detected = false;
        while (Vector3.Distance(transform.position, patrol_pos) >= 0.001f)
        {
            navAgent.destination = patrol_pos;
            yield return new WaitForSeconds(0.05f);
        }
        navAgent.ResetPath();
        //navAgent.SetPath(path);
        path.gameObject.SetActive(true);
        StopCoroutine("ReturnToPos");
    }


}