using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Utills : MonoBehaviour
{
    public AlertController alertController;

    public void startAlert(string message) {
        alertController.startAlert(message);
    }

    public void endAlert() {
        alertController.endAlert();
    }

    public void setPrefabData(string hostName,string hostBank,string hostAccount, List<string> totalUserList) {
        string totalUsers = "";
        for (int i = 0; i < totalUserList.Count; i++)
        {
            totalUsers += totalUserList[i] + "`";
        }

        PlayerPrefs.SetString("hostName", hostName);
        PlayerPrefs.SetString("hostBank", hostBank);
        PlayerPrefs.SetString("hostAccount", hostAccount);
        PlayerPrefs.SetString("totalUser", totalUsers);
    }

    public void setNameTag_Ver(string name, Data data, GameObject nameTag, ScrollRect scrollViewResct, GameObject scrollViewContent )
    {
        // 이름표 생성
        GameObject tag = Instantiate(nameTag);
        tag.transform.parent = scrollViewContent.transform;
        tag.transform.localScale = new Vector3(1, 1, 1);
        tag.transform.GetComponent<NameTagController>().data = data;
        tag.transform.GetComponent<NameTagController>().nameTag = name;

        scrollViewResct.content.sizeDelta = new Vector2(0, scrollViewContent.transform.childCount * nameTag.GetComponent<NameTagController>().mHeight);

        tag.transform.GetComponent<NameTagController>().nameTagViewResct = scrollViewResct;
        tag.transform.GetComponent<NameTagController>().scrollViewContent = scrollViewContent;
    }

    public void setNameTag_Hor(string name, Data data, GameObject nameTag, ScrollRect scrollViewResct, GameObject scrollViewContent)
    {
        // 이름표 생성
        GameObject tag = Instantiate(nameTag);
        tag.transform.parent = scrollViewContent.transform;
        tag.transform.localScale = new Vector3(1, 1, 1);
        tag.transform.GetComponent<NameTagController>().data = data;
        tag.transform.GetComponent<NameTagController>().nameTag = name;
        tag.transform.GetComponent<NameTagController>().nameTagViewResct = scrollViewResct;
        tag.transform.GetComponent<NameTagController>().scrollViewContent = scrollViewContent;
        
        scrollViewResct.content.sizeDelta = new Vector2(scrollViewContent.transform.childCount * nameTag.GetComponent<NameTagController>().mWidth, 0);
    }
}
