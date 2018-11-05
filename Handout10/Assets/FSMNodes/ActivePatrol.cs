using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions{

	public class ActivePatrol : ActionTask{

        public GameObject curve;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit(){
            
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute(){
            curve.SetActive(true);
           // EndAction(true);
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate(){
		}

        //Called when the task is disabled.
        protected override void OnStop(){
            curve.SetActive(false);
		}

		//Called when the task is paused.
		protected override void OnPause(){
			
		}
	}
}