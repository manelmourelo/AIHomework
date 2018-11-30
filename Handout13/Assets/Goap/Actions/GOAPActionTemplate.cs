using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public class GOAPActionTemplate : Action {

    //Constructor
    public GOAPActionTemplate() { }

    //Called by the planner when the action is being considered. This function is used to recieve the iGoap and current state variables.
    public override void OnActionSetup(IGoap igoap, List<Condition> state)
    {
    //    base.OnActionSetup(igoap, state);
    }

    //Called by the GoapUpdater, this function is called when this action is in the current pland and started.
    public override void OnActionStart()
    {
    //    base.OnActionStart();
    }

    //Called by the GoapUpdater, this function will be called until the action is either finished, or aborted.
    public override void OnActionPerform()
    {
    //    base.OnActionPerform();
    }

    //Called by the GoapUpdater, this function will be called when the action is finished.
    public override void OnActionFinished()
    {
    //    base.OnActionFinished();
    }

    //Called by the GoapUpdater, this function will be called when this action is aborted.
    public override void OnActionAborted()
    {
    //    base.OnActionAborted();
    }

}
