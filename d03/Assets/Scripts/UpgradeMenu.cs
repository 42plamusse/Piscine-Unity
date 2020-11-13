using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    public GameObject radiusMenu;
    // Start is called before the first frame update

    public static UpgradeMenu upgradeMenu;

    GameObject menuInstance;
    bool isPopped = false;
    //Singleton basique  : Voir unity design patterns sur google.
    void Awake()
    {
        if (upgradeMenu == null)
            upgradeMenu = this;
    }

    public void PopMenu(Transform targetTransform)
    {
        if (isPopped)
            CloseMenu();
        isPopped = true;
        menuInstance= Instantiate(radiusMenu, transform);
        RectTransform menuRectTransform = menuInstance.GetComponent<RectTransform>();
        menuRectTransform.position = targetTransform.position;
    }

    void CloseMenu()
    {
        if (menuInstance != null)
            Destroy(menuInstance);
    }
}
