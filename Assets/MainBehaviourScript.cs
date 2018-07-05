using System.Collections.Generic;
using UnityEngine;
using DeltaDNA; 




public class MainBehaviourScript : MonoBehaviour {

	public string configURL = "not set";

	// Use this for initialization
	void Start () {
		DDNA.Instance.StartSDK();
	}
	
    public Texture2D icon;

	void OnGUI (){
		GUI.skin.textField.wordWrap = true;
		GUI.skin.button.fontSize = 24;

		int xOffset = 0;
		int yOffset = 0;

		int buttonWidth = 300;
		int buttonHeight = 100;
		
		GUI.skin.box.fontSize = 36;

        GUI.Box(new Rect(50,50,400,400), "Menu");


		if (GUI.Button (new Rect (yOffset+=110, xOffset+=110, buttonWidth, buttonHeight), "Record event")) {
			print ("Record event");
			var gameEvent = new GameEvent("options")
				.AddParam("option", "sword")
				.AddParam("action", "sell");

			DDNA.Instance.RecordEvent(gameEvent);
                print ("You clicked me!");

			
		}
		//GUI.Button (new Rect (700,10,100,50), new GUIContent ("Click me", icon, "This is the tooltip"));

		if (GUI.Button (new Rect (yOffset, xOffset+=110, buttonWidth, buttonHeight),  new GUIContent ("Get Config", icon, configURL))) {
			print ("Do engagement");

			var engagement = new Engagement("startMission")
						.AddParam("missionID", "Disco Volante");

			DDNA.Instance.RequestEngagement(engagement, (response) =>
			{
				print("engagement");
				// Response is a Dictionary<string, object> of key-values returned from Engage.
				// It will be empty if no matching campaign was found or an error occurred.
				  if (response.ContainsKey("parameters")){
						Dictionary<string, object> parameters = response["parameters"] as Dictionary<string, object>; 
						// Check if the reply contained the configuration URL an
						if(parameters.ContainsKey("configURL")){
							// assume a string is returned as the configURL
							configURL = (string) parameters["configURL"];
							print (configURL);
						}else{
							print("No configURL in reply");
				  		}
				  }
			});
		}
		GUI.Label (new Rect (yOffset, xOffset+buttonHeight, buttonWidth, buttonHeight), GUI.tooltip);


	}

}
