using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AccountScreenLogic : MonoBehaviour, IPointerClickHandler
{

    [Header("Settings")]
    public string registerInfo; 


    public string loginInfo;


    [Header("Dependencies")]
    public AccountState currentState;
    public DiceApp diceApp;
    public enum AccountState
    {
        Register,
        Login
    }
    [Header("Accountscreen assets")]
    public TMP_Text infoText;
    public GameObject AccountLoginRegion;
    public TMP_InputField[] LoginFields;
    public Button ActionButton;

    void Start()
    {

        LoginFields = GetAccountInputFieldsFromGameObject(AccountLoginRegion);
        setRegisterState();
        //StartCoroutine(WaitForKeepAliveAndUpdateUI());
        updateUI();
    }

    void OnEnable()
    {
        Debug.Log("enabled account screen");
        //StartCoroutine(WaitForKeepAliveAndUpdateUI());
        updateUI();
    }
    private void OnDisable()
    {
        Debug.Log("dissabeld account screen");
    }


    #region statesetters
    public void setRegisterState()
    {
        currentState = AccountState.Register;
        AccountLoginRegion.SetActive(true);
        infoText.text = registerInfo;
        ActionButton.GetComponentInChildren<TMP_Text>().text = "Register";
        Debug.Log($"Set State: {currentState}");
    }

    public void setLoginState()
    {
        currentState = AccountState.Login;
        AccountLoginRegion.SetActive(true);
        infoText.text = loginInfo;
        ActionButton.GetComponentInChildren<TMP_Text>().text = "Login";
        Debug.Log($"Set State: {currentState}");
    }


    #endregion
    public void updateUI()
    {
        Debug.Log("Updating accountscreen ui");





        Debug.Log($"Current State: {currentState}");
    }


    #region buttons
    public void actionButtonClick()
    {
        if (currentState == AccountState.Login)
        {
            diceApp.Login();
        }
        if (currentState == AccountState.Register)
        {
            diceApp.Register();
        }
        updateUI();
        updateUI();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(infoText, Input.mousePosition, null);
        if (linkIndex != -1)
        {
            TMP_LinkInfo linkInfo = infoText.textInfo.linkInfo[linkIndex];
            string linkID = linkInfo.GetLinkID();

            Debug.Log("Clicked link: " + linkID);
            switch (linkID)
            {
                case "login":
                    setLoginState();
                    break;
                case "register":
                    setRegisterState();
                    break;
            }
        }
    }


    #endregion

    #region helpermethods
    private TMP_InputField[] GetAccountInputFieldsFromGameObject(GameObject mainGameObject)
    {
        Transform fieldsGameObject = mainGameObject.transform.Find("Fields");
        if (fieldsGameObject != null)
        {
            TMP_InputField nameField = fieldsGameObject.transform.Find("UsernameInput")?.GetComponent<TMP_InputField>();
            TMP_InputField surnameField = fieldsGameObject.transform.Find("PasswordInput")?.GetComponent<TMP_InputField>();

            if (nameField == null || surnameField == null)
            {
                Debug.LogError("Name field or surname field is missing.");
            }


            return new TMP_InputField[] { nameField, surnameField };
        }
        else
        {
            Debug.LogWarning("Fields not found in mainGameObject");
            return null;
        }
    }
    private Button[] GetButtonsFromGameObject(GameObject mainGameObject)
    {
        if (mainGameObject == null)
        {
            Debug.LogError("Main GameObject is null.");
            return new Button[0];
        }

        Button[] buttons = mainGameObject.GetComponentsInChildren<Button>();

        if (buttons.Length == 0)
        {
            Debug.LogWarning("No buttons found in mainGameObject.");
        }

        return buttons;
    }
    #endregion

}
