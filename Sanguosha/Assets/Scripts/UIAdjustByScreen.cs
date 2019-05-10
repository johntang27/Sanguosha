using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ScreenAspectRatio
{
    NotFound,
    TwoByThree,
    ThreeByFour,
    ThreeByFive,
    FiveByEight,
    NineBySixteen,
    iPhoneX,
    Samsung,
    KindleFire7,
    Pixel_OneByTwo,
}

[Serializable]
public class UIChangeParam
{
    public ScreenAspectRatio aspectRatio;
    public Vector2 changeInPosition;
    public Vector3 newScale;

    public UIChangeParam(int ratio)
    {
        aspectRatio = (ScreenAspectRatio)ratio;
        newScale = Vector3.one;
    }
}

public class UIAdjustByScreen : MonoBehaviour {

    public ScreenAspectRatio aspectRatio;

    public UIChangeParam Aspect_2By3 = new UIChangeParam(1);
    public UIChangeParam Aspect_3By4 = new UIChangeParam(2);
    public UIChangeParam Aspect_3By5 = new UIChangeParam(3);
    public UIChangeParam Aspect_5By8 = new UIChangeParam(4);
    public UIChangeParam Aspect_9By16 = new UIChangeParam(5);
    public UIChangeParam Aspect_iPhoneX = new UIChangeParam(6);
    public UIChangeParam Aspect_Samsung = new UIChangeParam(7);
    public UIChangeParam Aspect_Kindle7 = new UIChangeParam(8);
    public UIChangeParam Aspect_Pixel_1By2 = new UIChangeParam(9);

    public bool disableStart = false;
    public bool isLandscape = false;

    RectTransform rectTrans;

    // Use this for initialization
    void Start () {

        if (disableStart) return;

        SetAspectRatio();

        switch(aspectRatio)
        {
            case ScreenAspectRatio.TwoByThree: UpdateUI(Aspect_2By3);
                break;
            case ScreenAspectRatio.ThreeByFour: UpdateUI(Aspect_3By4);
                break;
            case ScreenAspectRatio.ThreeByFive: UpdateUI(Aspect_3By5);
                break;
            case ScreenAspectRatio.FiveByEight: UpdateUI(Aspect_5By8);
                break;
            case ScreenAspectRatio.NineBySixteen: UpdateUI(Aspect_9By16);
                break;
            case ScreenAspectRatio.iPhoneX: UpdateUI(Aspect_iPhoneX);
                break;
            case ScreenAspectRatio.Samsung: UpdateUI(Aspect_Samsung);
                break;
            case ScreenAspectRatio.KindleFire7: UpdateUI(Aspect_Kindle7);
                break;
            case ScreenAspectRatio.Pixel_OneByTwo: UpdateUI(Aspect_Pixel_1By2);
                break;
        }
    }

    void UpdateUI(UIChangeParam param)
    {
        if (rectTrans == null) return;

        rectTrans.anchoredPosition = new Vector2(rectTrans.anchoredPosition.x + param.changeInPosition.x, rectTrans.anchoredPosition.y + param.changeInPosition.y);
        rectTrans.localScale = param.newScale;
    }

    public void SetAspectRatio()
    {
        float ar = (float)Math.Round(((float)Screen.width / (float)Screen.height), 3);
        if (isLandscape) ar = (float)Math.Round(((float)Screen.height / (float)Screen.width), 3);
        //Debug.Log("aspect ratio :" + ar);

        if (ar == 0.667f)
        {
            aspectRatio = ScreenAspectRatio.TwoByThree;
        }
        else if (ar == 0.75f)
        {
            aspectRatio = ScreenAspectRatio.ThreeByFour;
        }
        else if (ar == 0.6f)
        {
            aspectRatio = ScreenAspectRatio.ThreeByFive;
        }
        else if (ar == 0.625f)
        {
            aspectRatio = ScreenAspectRatio.FiveByEight;
        }
        else if (ar == 0.562f)
        {
            aspectRatio = ScreenAspectRatio.NineBySixteen;
        }
        else if (ar == 0.462f)
        {
            aspectRatio = ScreenAspectRatio.iPhoneX;
        }
        else if (ar == 0.486f)
        {
            aspectRatio = ScreenAspectRatio.Samsung;
        }
        else if (ar == 0.586f)
        {
            aspectRatio = ScreenAspectRatio.KindleFire7;
        }
        else if (ar == 0.5f)
        {
            aspectRatio = ScreenAspectRatio.Pixel_OneByTwo;
        }
        else
        {
            aspectRatio = ScreenAspectRatio.NotFound;
        }
        
        rectTrans = this.GetComponent<RectTransform>();
    }
}
