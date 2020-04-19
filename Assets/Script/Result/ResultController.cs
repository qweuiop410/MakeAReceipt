using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Reflection;
using System;

public class ResultController : MonoBehaviour
{
    public GameController gameController;

    public Dictionary<string, int> taxyDictionary = new Dictionary<string, int>();
    private int taxiPrice = 0;

    private List<string> resultUserList = new List<string>();
    private List<int> resultPriceList = new List<int>();

    private List<string> users = new List<string>(); //최종 유저 리스트
    private List<int> prices = new List<int>(); // 최종 청구액 리스트

    public Text resultText;
    private string result = "";
    // Start is called before the first frame update
    void Start()
    {
        makeAReceipt();
    }

    public void makeAReceipt() {
        List<string> taxyKeys = new List<string>(taxyDictionary.Keys);
        List<int> taxyValues = new List<int>(taxyDictionary.Values);
        for (int i = 0; i < taxyValues.Count; i++)
        {
            taxiPrice += taxyValues[i];
        }

        for (int i = 0; i < gameController.data.totalUser.Count; i++)
        {
            resultUserList.Add(gameController.data.totalUser[i]);
            resultPriceList.Add(0);
        }

        for (int i = 0; i < gameController.data.stageData.Count; i++)
        {
            for (int j = 0; j < gameController.data.stageData[i].stageMember.Count; j++)
            {
                for (int t = 0; t < resultUserList.Count; t++)
                {
                    if (gameController.data.stageData[i].stageMember[j] == resultUserList[t])
                    {
                        resultPriceList[t] += gameController.data.stageData[i].stagePrice / gameController.data.stageData[i].stageMember.Count;
                    }
                }
            }
        }

        for (int i = 0; i < resultPriceList.Count; i++)
        {
            if (resultPriceList[i] != 0)
            {
                users.Add(resultUserList[i]);
                prices.Add(resultPriceList[i]);
            }
        }

        // 택시비 추가
        for (int i = 0; i < users.Count; i++)
        {
            for (int j = 0; j < taxyKeys.Count; j++)
            {
                if (users[i] == taxyKeys[j])
                {
                    prices[i] += taxiPrice / taxyValues.Count;
                    prices[i] -= taxyValues[j];
                }
            }
        }

        //영수증 출력=============================== 한줄에 13자, ￦, '　'
        string hostName = gameController.data.hostName.PadRight(13, '　');

        result +=
            " 　　　　　정산　　　　　 \n" +
            "ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ\n" +
            returnRow(gameController.data.hostName, "　") + "\n" +
            returnRow(gameController.data.hostBank, "　") + "\n" +
            returnRow(gameController.data.hostAccount, "　") + "\n" +
            "ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ\n";

        // 청구액 추가
        for (int i = 0; i < users.Count; i++)
        {
            result += returnRow(users[i], (prices[i]  + (100  - prices[i] % 100)) + "￦") + "\n";
        }

        result += "ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ\n" +
            " 　　　　상세내역　　　　 \n" +
            "ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ\n";

        //상세 내역 추가
        for (int i = 0; i < gameController.data.stageData.Count; i++)
        {
            result += returnRow(gameController.data.stageData[i].stageName, gameController.data.stageData[i].stagePrice + "￦") + "\n";
            result += "\n";
            for (int j = 0; j < gameController.data.stageData[i].stageMember.Count; j++)
            {
                result += returnRow(gameController.data.stageData[i].stageMember[j], gameController.data.stageData[i].stagePrice / gameController.data.stageData[i].stageMember.Count + "￦") + "\n";
            }
            result += "ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ\n";
        }

        // 택시비 추가
        result += "택시비　　　　　　　　　  \n\n";
        for (int i = 0; i < taxyKeys.Count; i++)
        {
            result += returnRow(taxyKeys[i], taxyValues[i] + "￦") + "\n";
        }

        // 공지 추가
        result += "ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ\n" +
            "※택시비　추가는　마지막　\n" +
            "자리에있었던　사람끼리한다. 　\n" +
            "※100원　미만의　잔돈은　\n" +
            "올림하여　정산한다. 　　　\n";

        resultText.text = result;
    }

    private string returnRow(string key, string value) {
        string row = "";
        if (key.Length + value.Length > 13)
        {
            row = key.PadLeft(12 - value.Length) + "　" + value;
        }
        else
        {
            int distance = 13 - (key.Length + value.Length);
            string addBlank = "";
            for (int i = 0; i < distance; i++)
            {
                addBlank += "　";
            }
            row = key + addBlank + value;
        }
        return row;
    }

    public void retryApp() {
        SceneManager.LoadScene(0);
    }

    public void copyReceipt() {
        gameController.utills.startAlert("복사 완료!");
        UniClipboard.SetText(result);
        //Debug.Log("저장 완료! : " + UniClipboard.GetText());
    }
}
