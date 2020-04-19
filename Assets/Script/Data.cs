using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    public List<GameObject> canvasList = new List<GameObject>();

    public string hostName = "";
    public string hostBank = "";
    public string hostAccount = "";

    public List<string> totalUser = new List<string>();
    public List<StageData> stageData = new List<StageData>();

    private void Awake()
    {
        hostName = PlayerPrefs.GetString("hostName");
        hostBank = PlayerPrefs.GetString("hostBank");
        hostAccount = PlayerPrefs.GetString("hostAccount");

        // set totalUser
        string getTotalUsers = PlayerPrefs.GetString("totalUser");
        string[] spUsers = getTotalUsers.Split('`');
        for (int i = 0; i < spUsers.Length - 1; i++) {
            totalUser.Add(spUsers[i]);
        }
    }
}

public class StageData {
    public int stageId = 0;
    public string stageName = "";
    public int stagePrice = 0;
    public int texiPrice = 0;
    public List<string> stageMember = new List<string>();
}