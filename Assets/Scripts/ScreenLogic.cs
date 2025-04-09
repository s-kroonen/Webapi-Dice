using UnityEngine;

public class ScreenLogic : MonoBehaviour
{
    public GameObject AccountObject;
    public GameObject MainObject;
    public GameObject EnvObject;
    public void accountActive()
    {
        AccountObject.SetActive(true);
        MainObject.SetActive(false);
        EnvObject.SetActive(false);
    }
    public void gameActive()
    {
        AccountObject.SetActive(false);
        MainObject.SetActive(true);
        EnvObject.SetActive(false);
    }
    public void envActive()
    {
        AccountObject.SetActive(false);
        MainObject.SetActive(false);
        EnvObject.SetActive(true);
    }

}
