using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TexiController : MonoBehaviour
{
    [Header("Setting")]
    public GameController gameController;

    [Header("Main")]
    public MainController mainController;

    [Header("UI")]
    public ScrollRect scrollViewResct;
    public GameObject scrollViewContent;
    public GameObject nametag;

    public List<string> taxiMember = new List<string>();
    public Dictionary<string, int> taxiResult = new Dictionary<string, int>();
    
    public void setTaxiMemberData() {
        scrollViewContent.transform.DetachChildren();
        for (int i = 0; i < taxiMember.Count; i++) {
            gameController.utills.setNameTag_Ver(taxiMember[i], gameController.data, nametag, scrollViewResct, scrollViewContent);
        }
    }

    public void onClickPre() {
        gameController.data.canvasList[1].SetActive(true);
        gameObject.SetActive(false);
    }

    public void submit() {
        for (int i = 0; i < scrollViewContent.transform.childCount; i++) {
            taxiResult.Add(scrollViewContent.transform.GetChild(i).GetComponent<NameTagController>().nameTag, scrollViewContent.transform.GetChild(i).GetComponent<NameTagController>().returnTaxiPrice());
        }
        
        //화면 전환
        gameController.data.canvasList[3].SetActive(true);
        gameController.data.canvasList[3].GetComponent<ResultController>().taxyDictionary = taxiResult;
        gameObject.SetActive(false);
    }
}
