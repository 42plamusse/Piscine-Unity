using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    public GameObject radiusMenu;
    // Start is called before the first frame update

    public static UpgradeMenu upgradeMenu;

    GameObject menuInstance;
    GameObject selectedTower;
    bool isPopped = false;
    //Singleton basique  : Voir unity design patterns sur google.
    void Awake()
    {
        if (upgradeMenu == null)
            upgradeMenu = this;
    }

    public void PopMenu(GameObject tower)
    {
        if (isPopped)
            CloseMenu();
        selectedTower = tower;
        isPopped = true;
        menuInstance = Instantiate(radiusMenu, transform);
        UpgradeRadius radiusScript = menuInstance.GetComponent<UpgradeRadius>();
        towerScript towerScript = tower.GetComponent<towerScript>();
        towerScript upgradeTowerScript = towerScript.
            upgrade.GetComponent<towerScript>();
        radiusScript.energyCost.text = upgradeTowerScript.energy.ToString();
        radiusScript.energyValue.text = (towerScript.energy / 2).ToString();
        RectTransform menuRectTransform =
            menuInstance.GetComponent<RectTransform>();
        menuRectTransform.position = tower.transform.position;
    }


    public void CloseMenu()
    {
        if (upgradeMenu.menuInstance != null)
            Destroy(upgradeMenu.menuInstance);
        isPopped = false;
        selectedTower = null;
    }

    public void UpgradeTower()
    {
        GameObject tower = upgradeMenu.selectedTower;
        towerScript towerScript = tower.GetComponent<towerScript>();
        if (towerScript.CanUpgrade())
        {
            towerScript.UpgradeSelf();
            CloseMenu();
        }
    }
    public void DowngradeTower()
    {
        GameObject tower = upgradeMenu.selectedTower;
        towerScript towerScript = tower.GetComponent<towerScript>();
        towerScript.DowngradeSelf();
        CloseMenu();
    }
}
