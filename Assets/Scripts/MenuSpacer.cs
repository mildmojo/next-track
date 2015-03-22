using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class MenuSpacer : MonoBehaviour {

  public List<GameObject> Menus;

  void Awake() {
    for (var i = 0; i < Menus.Count(); i++) {
      var menu = Menus[i];
      menu.transform.position = new Vector3(Screen.width * i * 1.5f,
        menu.transform.position.y, menu.transform.position.z);
      // Debug.Log(menu.transform.position);
    }
  }

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
}
