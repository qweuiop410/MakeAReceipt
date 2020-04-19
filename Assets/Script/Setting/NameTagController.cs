using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class NameTagController : MonoBehaviour
{
    [Header("Setting")]
    public Animator ani;
    public GameController gameController;
    public int mHeight = 0;
    public int mWidth = 0;

    [Header("Data")]
    public Data data;
    public string nameTag = "";
    public GameObject scrollViewContent;

    [Header("UI")]
    public Text nameTxt;
    public ScrollRect nameTagViewResct;
    public InputField taxiPrice;

    private void Start()
    {
        nameTxt.text = nameTag;
    }

    private void Update()
    {
        if (transform.parent == null)
            Destroy(gameObject);
    }

    public void destroySettingVer_AniStart()
    {
        ani.Play("NameTag_EndMain");
    }

    public void destroySettingVer() {
        SettingController settingController = gameController.settingController;

        for (int i = 0; i < settingController.totalMember.Count; i++)
        {
            if (settingController.totalMember[i] == nameTag)
            {
                settingController.totalMember.RemoveAt(i);
                break;
            }
        }

        // Scroll View - Content 크기 지정
        nameTagViewResct.content.sizeDelta = new Vector2(0, (scrollViewContent.transform.childCount - 1) * mHeight);

        Destroy(gameObject);
    }

    public void destroyMainVer_AniStart() {
        ani.Play("NameTag_End");
    }

    public void destroyMainVer() {
        MainController mainController = gameController.mainController;

        for (int i = 0; i < mainController.selectMembers.Count; i++)
        {
            if (mainController.selectMembers[i] == nameTag)
            {
                mainController.selectMembers.RemoveAt(i);
                break;
            }
        }

        // Scroll View - Content 크기 지정
        nameTagViewResct.content.sizeDelta = new Vector2(0, (scrollViewContent.transform.childCount - 1) * mHeight);

        Destroy(gameObject);
    }

    public void onClickMainHor()
    {
        ani.Play("NameTAg_Hor_Select");
        MainController mainController = gameController.mainController;

        mainController.selectMembers.Add(nameTxt.text);
        mainController.nametag_Ver.GetComponent<NameTagController>().gameController = gameController;
        mainController.gameController.utills.setNameTag_Ver(nameTxt.text, data, mainController.nametag_Ver, mainController.scrollViewResct_Ver, mainController.scrollViewContent_Ver);
    }

    public int returnTaxiPrice() {
        if (taxiPrice.text == "")
            return 0;
        else
            return int.Parse(taxiPrice.text);
    }
}
