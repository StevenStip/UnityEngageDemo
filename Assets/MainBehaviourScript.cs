using System.Collections.Generic;
using UnityEngine;
using DeltaDNA;



public class MainBehaviourScript : MonoBehaviour {

	string configURL = "not set";

	// Use this for initialization
	void Start () {
		DDNA.Instance.StartSDK();
	}
	

	void OnGUI (){
		GUI.skin.textField.wordWrap = true;
		GUI.skin.button.fontSize = 32;

		int xOffset = 0;
		int yOffset = 0;

		int buttonWidth = 350;
		int buttonHeight = 100;

		if (GUI.Button (new Rect (yOffset+=110, xOffset+=110, buttonWidth, buttonHeight), "Record event")) {
			Debug.Log ("Record event");
			var gameEvent = new GameEvent("options")
				.AddParam("option", "sword")
				.AddParam("action", "sell");

			DDNA.Instance.RecordEvent(gameEvent);

			
		}

		if (GUI.Button (new Rect (yOffset, xOffset+=110, buttonWidth, buttonHeight), "Decision point campaign")) {
			Debug.Log ("Do engagement");

			var engagement = new Engagement("startMission")
						.AddParam("missionID", "Disco Volante");

			DDNA.Instance.RequestEngagement(engagement, (response) =>
			{

				Debug.Log("engagement");
				



				// Response is a Dictionary<string, object> of key-values returned from Engage.
				// It will be empty if no matching campaign was found or an error occurred.
				  if (response.ContainsKey("parameters")){

						Dictionary<string, object> parameters = response["parameters"] as Dictionary<string, object>; 

				  		// assume a string is returned as the configURL
						Debug.Log((string) parameters["configURL"]);
						Debug.Log (configURL);
				  }else
				  {
					  Debug.Log("No configURL in reply");
				  }

				
			});


		}

		if (GUI.Button (new Rect (yOffset, xOffset+=110, buttonWidth, buttonHeight), "Button 3")) {
			Debug.Log ("Button 3");

		}

	}

	// Update is called once per frame
	void Update () {
		
	}
}
