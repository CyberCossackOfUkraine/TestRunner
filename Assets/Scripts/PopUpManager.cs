using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class PopUpManager : MonoBehaviour
{
    [SerializeField] private Image popUpWindow;
    [SerializeField] private Text popUpText;

    [HideInInspector] public static PopUpManager instance;

    private void Awake()
    {
        if (instance != this && instance != null)
        {
            Destroy(this);
        } else
        {
            instance = this;
        }

        popUpWindow.DOFade(0f, 0f);
        popUpText.DOFade(0f, 0f);
    }

    public void ShowMessage(string message)
    {
        CancelInvoke("HideMessage");
        popUpWindow.DOFade(1f, 1f);
        popUpText.DOFade(1f, 1f);
        popUpText.text = message;
        Invoke("HideMessage", 5);
    }

    public void HideMessage()
    {
        popUpWindow.DOFade(0f, 1f);
        popUpText.DOFade(0f, 1f);
    }
}
