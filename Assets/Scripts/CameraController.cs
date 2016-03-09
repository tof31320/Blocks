using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        Cell center = GameController.instance.grille.GetCellInCenter();
        Vector3 centerPosition = center.transform.position;

        transform.position = Vector3.Lerp(transform.position, new Vector3(centerPosition.x, transform.position.y, centerPosition.z), 0.5f);
	}
}
