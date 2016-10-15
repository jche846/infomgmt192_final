using UnityEngine;
using Pathfinding.Serialization.JsonFx; //make sure you include this using

public class Sketch : MonoBehaviour {
    public GameObject myPrefab;
	private string _WebsiteURL = "http://infosys320lab2.azurewebsites.net/tables/TrelloData?zumo-api-version=2.0.0";

    void Start () {
        //Reguest.GET can be called passing in your ODATA url as a string in the form:
        //http://{Your Site Name}.azurewebsites.net/tables/{Your Table Name}?zumo-api-version=2.0.0
        //The response produce is a JSON string
        string jsonResponse = Request.GET(_WebsiteURL);

        //Just in case something went wrong with the request we check the reponse and exit if there is no response.
        if (string.IsNullOrEmpty(jsonResponse))
        {
            return;
        }
			
		//We can now deserialize into an array of objects - in this case the class we created. The deserializer is smart enough to instantiate all the classes and populate the variables based on column name.
		//jche846
		Trello[] cards = JsonReader.Deserialize<Trello[]>(jsonResponse);

		int totalCubes = cards.Length;
		int totalDistance = 5;
		int i = 0;

        //We can now loop through the array of objects and access each object individually
		//jche846
		foreach (Trello item in cards)
        {
			float perc = i / (float)totalCubes;
			i++;
			float x = perc * totalDistance;
			float z = 0.0f;


			if (item.Date.Substring(0,9)=="October 7"){
				float y = 7.0f;

				GameObject newCube = (GameObject)Instantiate(myPrefab, new Vector3(x, y, z), Quaternion.identity);
				newCube.GetComponent<CubeScript>().SetSize((1.0f - perc) * 2);
				newCube.GetComponent<CubeScript>().rotateSpeed= perc;
				newCube.GetComponentInChildren<TextMesh>().text = item.Operation;
				newCube.GetComponentInChildren<TextMesh> ().color = new Color (0,0,1,1);


			}//jche846
			else {
				float y = 4.0f;

				GameObject newCube = (GameObject)Instantiate(myPrefab, new Vector3(x, y, z), Quaternion.identity);
				newCube.GetComponent<CubeScript>().SetSize((1.0f - perc) * 2);
				newCube.GetComponent<CubeScript>().rotateSpeed= perc;
				newCube.GetComponentInChildren<TextMesh>().text = item.Operation;
				newCube.GetComponentInChildren<TextMesh> ().color = new Color (1,0,0,1);
			}


        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
