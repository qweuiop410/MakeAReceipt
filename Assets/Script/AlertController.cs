using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AlertController : MonoBehaviour
{
    public Animator ani;
    public Text alertText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startAlert(string message) {
        alertText.text = message;
        ani.Play("Alert_start");
    }

    public void endAlert() {
        ani.Play("Alert_end");
    }
}
