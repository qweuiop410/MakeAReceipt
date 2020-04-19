using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SettingController : MonoBehaviour
{
    [Header("Game Controller")]
    public GameController gameController;
    public List<string> totalMember = new List<string>();

    [Header("Host")]
    public InputField hostNameInputField;
    public InputField hostBankInputField;
    public InputField hostAccountInputField;

    [Header("Name Tag")]
    public InputField inputField;
    public ScrollRect scrollViewResct;
    public GameObject scrollViewContent;
    public GameObject nametag;

    // Start is called before the first frame update
    void Start()
    {
        hostNameInputField.text = gameController.data.hostName;
        hostBankInputField.text = gameController.data.hostBank;
        hostAccountInputField.text = gameController.data.hostAccount;
        totalMember = gameController.data.totalUser;

        nametag.GetComponent<NameTagController>().gameController = gameController;
        for (int i = 0; i < gameController.data.totalUser.Count; i++) {
            // 이름표 생성
            gameController.utills.setNameTag_Ver(gameController.data.totalUser[i], gameController.data, nametag, scrollViewResct, scrollViewContent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 입력창에서 엔터치면 추가
        if (Input.GetKeyDown(KeyCode.Return) && inputField.isFocused) {
            //입력창이 공백이 아니면
            if (inputField.text != "")
            {
                // 이름 리스트에 저장
                totalMember.Add(inputField.text.Substring(0, inputField.text.Length - 1));

                // 이름표 생성
                gameController.utills.setNameTag_Ver(inputField.text, gameController.data, nametag, scrollViewResct, scrollViewContent);
                inputField.text = "";
            }
        }
    }

    public void addNameTag() {
        //입력창이 공백이 아니면
        if (inputField.text != "") {
            // 이름 리스트에 저장
            totalMember.Add(inputField.text);

            // 이름표 생성
            gameController.utills.setNameTag_Ver(inputField.text, gameController.data, nametag, scrollViewResct, scrollViewContent);
            inputField.text = "";
        }
    }

    public void submit() {
        // 입력창들이 빈값이 아니면
        if (hostNameInputField.text == "")
        {
            gameController.utills.startAlert("작성자 이름 입력했는지 확인!");
            Debug.Log("상호 입력했는지 확인!");
        }
        else if (hostBankInputField.text == "")
        {
            gameController.utills.startAlert("은행 입력했는지 확인!");
            Debug.Log("금액 입력했는지 확인!");
        }
        else if (hostAccountInputField.text == "")
        {
            gameController.utills.startAlert("계좌번호 입력했는지 확인!");
            Debug.Log("구성원 선택했는지 확인!");
        }
        else if (totalMember.Count == 0)
        {
            gameController.utills.startAlert("구성원 입력했는지 확인!");
            Debug.Log("구성원 선택했는지 확인!");
        }
        else
        {

            gameController.data.hostName = hostNameInputField.text;
            gameController.data.hostBank = hostBankInputField.text;
            gameController.data.hostAccount = hostAccountInputField.text;
            gameController.data.totalUser = totalMember;

            gameController.utills.setPrefabData(gameController.data.hostName, gameController.data.hostBank, gameController.data.hostAccount, gameController.data.totalUser);
            gameObject.SetActive(false);
            gameController.data.canvasList[1].SetActive(true);
            gameController.data.canvasList[1].GetComponent<MainController>().setHorNameTag();
        }
    }
}
