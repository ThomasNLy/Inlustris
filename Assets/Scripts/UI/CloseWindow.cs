using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseWindow : MonoBehaviour {

    public void OpenClose(GameObject panel) {
        panel.SetActive(!panel.activeSelf);
    }
}
