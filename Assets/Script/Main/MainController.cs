using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using System.Threading;

public class MainController : MonoBehaviour
{
    [Header("Setting")]
    public GameController gameController;
    public List<string> selectMembers = new List<string>();
    private int stageNum = 0;

    [Header("UI")]
    public Text stageText;
    public Text preButtonTxt;
    public Text nextButtonTxt;

    public InputField nameInputField;
    public InputField priceInputField;

    public ScrollRect scrollViewResct_Ver;
    public GameObject scrollViewContent_Ver;
    public GameObject nametag_Ver;

    public ScrollRect scrollViewResct_Hor;
    public GameObject scrollViewContent_Hor;
    public GameObject nametag_Hor;
    
    void Start()
    {
        stageText.text = (stageNum + 1) + "차";
        nextButtonTxt.text = (stageNum + 2) + "차";
        if (stageNum == 0)
            preButtonTxt.text = "설정";
        else
            preButtonTxt.text = (stageNum - 1) + "차";

        setHorNameTag();
    }
    
    public void setHorNameTag() {
        nametag_Hor.GetComponent<NameTagController>().gameController = gameController;

        // 가로 이름표 초기화
        scrollViewContent_Hor.transform.DetachChildren();

        // 가로 이름표 생성
        for (int i = 0; i < gameController.data.totalUser.Count; i++)
        {
            gameController.utills.setNameTag_Hor(gameController.data.totalUser[i], gameController.data, nametag_Hor, scrollViewResct_Hor, scrollViewContent_Hor);
        }
    }

    public void moveToTaxi() {
        if (nameInputField.text == "")
        {
            gameController.utills.startAlert("가게 이름 입력했는지 확인!");
            Debug.Log("상호 입력했는지 확인!");
        }
        else if (priceInputField.text == "")
        {
            gameController.utills.startAlert("금액 입력했는지 확인!");
            Debug.Log("금액 입력했는지 확인!");
        }
        else if (selectMembers.Count == 0)
        {
            gameController.utills.startAlert("구성원 입력했는지 확인!");
            Debug.Log("구성원 선택했는지 확인!");
        }
        else
        {
            StageData inputData = new StageData();
            inputData.stageId = stageNum;
            inputData.stageName = nameInputField.text;
            inputData.stagePrice = int.Parse(priceInputField.text);

            gameController.data.canvasList[2].GetComponent<TexiController>().taxiMember.Clear();
            for (int i = 0; i < selectMembers.Count; i++)
            {
                inputData.stageMember.Add(selectMembers[i]);
                gameController.data.canvasList[2].GetComponent<TexiController>().taxiMember.Add(selectMembers[i]);
            }

            gameController.data.stageData.Add(inputData);
            
            gameController.data.canvasList[2].SetActive(true);
            gameController.data.canvasList[2].GetComponent<TexiController>().setTaxiMemberData();
            gameObject.SetActive(false);
        }
    }

    public void nextStage() {
        if (stageNum == gameController.data.stageData.Count)
        {
            //새로운 스테이지이면
            if (nameInputField.text == "")
            {
                gameController.utills.startAlert("가게 이름 입력했는지 확인!");
                Debug.Log("상호 입력했는지 확인!");
            }
            else if (priceInputField.text == "")
            {
                gameController.utills.startAlert("금액 입력했는지 확인!");
                Debug.Log("금액 입력했는지 확인!");
            }
            else if (selectMembers.Count == 0)
            {
                gameController.utills.startAlert("구성원 입력했는지 확인!");
                Debug.Log("구성원 선택했는지 확인!");
            }
            else {
                StageData inputData = new StageData();
                inputData.stageId = stageNum;
                inputData.stageName = nameInputField.text;
                inputData.stagePrice = int.Parse(priceInputField.text);

                for (int i = 0; i < selectMembers.Count; i++)
                {
                    inputData.stageMember.Add(selectMembers[i]);
                }

                gameController.data.stageData.Add(inputData);

                // UI 갱신
                nameInputField.text = "";
                priceInputField.text = "";
                stageNum++;
            }
        }
        else {
            // 이미 저장한 스테이지이면
            if (nameInputField.text == "")
            {
                gameController.utills.startAlert("가게 이름 입력했는지 확인!");
                Debug.Log("상호 입력했는지 확인!");
            }
            else if (priceInputField.text == "")
            {
                gameController.utills.startAlert("금액 입력했는지 확인!");
                Debug.Log("금액 입력했는지 확인!");
            }
            else if (selectMembers.Count == 0)
            {
                gameController.utills.startAlert("구성원 입력했는지 확인!");
                Debug.Log("구성원 선택했는지 확인!");
            }
            else
            {
                scrollViewContent_Ver.transform.DetachChildren();

                gameController.data.stageData[stageNum].stageName = nameInputField.text;
                gameController.data.stageData[stageNum].stagePrice = int.Parse(priceInputField.text);

                gameController.data.stageData[stageNum].stageMember.Clear();
                for (int i = 0; i < selectMembers.Count; i++)
                {
                    gameController.data.stageData[stageNum].stageMember.Add(selectMembers[i]);
                }

                // UI 갱신
                stageNum++;
                if (stageNum != gameController.data.stageData.Count)
                {
                    scrollViewContent_Ver.transform.DetachChildren();

                    nameInputField.text = gameController.data.stageData[stageNum].stageName;
                    priceInputField.text = gameController.data.stageData[stageNum].stagePrice + "";

                    selectMembers.Clear();
                    for (int i = 0; i < gameController.data.stageData[stageNum].stageMember.Count; i++)
                    {
                        selectMembers.Add(gameController.data.stageData[stageNum].stageMember[i]);
                        gameController.utills.setNameTag_Ver(gameController.data.stageData[stageNum].stageMember[i], gameController.data, nametag_Ver, scrollViewResct_Ver, scrollViewContent_Ver);
                    }
                }
                else {
                    nameInputField.text = "";
                    priceInputField.text = "";

                    selectMembers.Clear();
                    for (int i = 0; i < gameController.data.stageData[stageNum - 1].stageMember.Count; i++)
                    {
                        selectMembers.Add(gameController.data.stageData[stageNum - 1].stageMember[i]);
                        gameController.utills.setNameTag_Ver(gameController.data.stageData[stageNum - 1].stageMember[i], gameController.data, nametag_Ver, scrollViewResct_Ver, scrollViewContent_Ver);
                    }
                }
            }
        }
        
        stageText.text = (stageNum + 1) + "차";
        nextButtonTxt.text = (stageNum + 2) + "차";
        if (stageNum == 0)
            preButtonTxt.text = "설정";
        else
            preButtonTxt.text = stageNum + "차";
    }

    public void preStage() {
        if (stageNum == 0) {
            // 1차 스테이지이면
            gameController.data.canvasList[0].SetActive(true);
            gameObject.SetActive(false);
        }
        else {
            if (stageNum == gameController.data.stageData.Count)
            {
                //새로운 스테이지이면
                if (nameInputField.text == "")
                {
                    gameController.utills.startAlert("가게 이름 입력했는지 확인!");
                    Debug.Log("상호 입력했는지 확인!");
                }
                else if (priceInputField.text == "")
                {
                    gameController.utills.startAlert("금액 입력했는지 확인!");
                    Debug.Log("금액 입력했는지 확인!");
                }
                else if (selectMembers.Count == 0)
                {
                    gameController.utills.startAlert("구성원 입력했는지 확인!");
                    Debug.Log("구성원 선택했는지 확인!");
                }
                else
                {
                    stageNum--;
                    StageData inputData = new StageData();
                    inputData.stageId = stageNum;
                    inputData.stageName = nameInputField.text;
                    inputData.stagePrice = int.Parse(priceInputField.text);

                    for (int i = 0; i < selectMembers.Count; i++)
                    {
                        inputData.stageMember.Add(selectMembers[i]);
                    }

                    gameController.data.stageData.Add(inputData);


                    // UI 갱신
                    scrollViewContent_Ver.transform.DetachChildren();

                    nameInputField.text = gameController.data.stageData[stageNum].stageName;
                    priceInputField.text = gameController.data.stageData[stageNum].stagePrice + "";

                    selectMembers.Clear();
                    for (int i = 0; i < gameController.data.stageData[stageNum].stageMember.Count; i++)
                    {
                        selectMembers.Add(gameController.data.stageData[stageNum].stageMember[i]);
                        gameController.utills.setNameTag_Ver(gameController.data.stageData[stageNum].stageMember[i], gameController.data, nametag_Ver, scrollViewResct_Ver, scrollViewContent_Ver);
                    }
                }
            }
            else {
                stageNum--;
                scrollViewContent_Ver.transform.DetachChildren();

                nameInputField.text = gameController.data.stageData[stageNum].stageName;
                priceInputField.text = gameController.data.stageData[stageNum].stagePrice + "";

                selectMembers.Clear();
                for (int i = 0; i < gameController.data.stageData[stageNum].stageMember.Count; i++)
                {
                    selectMembers.Add(gameController.data.stageData[stageNum].stageMember[i]);
                    gameController.utills.setNameTag_Ver(gameController.data.stageData[stageNum].stageMember[i], gameController.data, nametag_Ver, scrollViewResct_Ver, scrollViewContent_Ver);
                }
            }
            stageText.text = (stageNum + 1) + "차";
            nextButtonTxt.text = (stageNum + 2) + "차";
            if (stageNum == 0)
                preButtonTxt.text = "설정";
            else
                preButtonTxt.text = stageNum + "차";
        }
    }


}

