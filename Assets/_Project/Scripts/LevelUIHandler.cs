using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUIHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject UIPanel;
    void Start()
    {
        StartCoroutine(PanelHide());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator PanelHide()
    {
        yield return new WaitForSeconds(2);
        UIPanel.SetActive(false);
    }
}
