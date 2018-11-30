using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public class GOAP_Agent : MonoBehaviour, IGoap {

    public List<Condition> state { get; set;}
    public List<Goal> availableGoals { get; set; }
    public List<Action> availableActions { get; set; }

    // Use this for initialization
    private void Awake()
    {
        state = new List<Condition>();
        availableGoals = new List<Goal>();
        availableActions = new List<Action>();

        //availableGoals.Add(ScriptableObject.CreateInstance<GOAP_Goal_AlertBase>());
        //availableActions.Add(ScriptableObject.CreateInstance<GOAP_Action_Goto>());

        foreach (Goal g in availableGoals)
            g.OnGoalInitialize(this);

    }

}
