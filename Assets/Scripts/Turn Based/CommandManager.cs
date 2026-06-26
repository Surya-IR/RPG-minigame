using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CommandManager : MonoBehaviour
{
    public static CommandManager Ins;

    [SerializeField] Camera cam;
    [SerializeField] Command cmd;

    #region COMMAND_ENUM
    public enum Command
    {
        none,
        attack,
        skill,
        item,
        escape
    }
    #endregion

    #region MAIN_MENU_BUTTONS
    [SerializeField] Button attackButton;
    [SerializeField] Button skillButton;
    [SerializeField] Button itemButton;
    [SerializeField] Button escapeButton;
    #endregion

    #region MENU_PARENTS
    public Transform commandMenuList;
    public Transform itemMenuList;
    #endregion

    #region BUTTON_PREFABS
    [SerializeField] GameObject itemButtonPrefab;
    [SerializeField] GameObject cancelButtonPrefab;
    #endregion

    private void Awake()
    {
        if (Ins == null)
        {
            Ins = this;
        }
    }

    void Start()
    {
        cam = Camera.main;
        ButtonsPlacement(commandMenuList);
        ItemButtonsPlacement();
        itemMenuList.gameObject.SetActive(false);

    }

    public void ClickToSelectTarget()
    {

        if (Input.GetMouseButtonDown((int)MouseButton.Left))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            bool isClickingObject = Physics.Raycast(ray, out hit);

            GameObject target = hit.collider?.gameObject;

            var targetParty = target?.GetComponent<PartyScript>();
            var targetEnemy = target?.GetComponent<EnemyScript>();
            if (isClickingObject)
            {
                switch (cmd)
                {
                    case Command.attack:
                        if (target.GetComponent<EnemyScript>() != null)
                            {
                                TurnBasedManager.Ins.ActivePartyAttackEnemy(targetEnemy.characterName);
                            }
                        break;
                    case Command.skill:
                        //TBA
                        break;
                    case Command.item:
                        switch (ItemManager.Ins.selectedItem.type)
                        {
                            case Items.ItemType.heal:
                                if (targetParty != null)
                                {
                                    targetParty.GetHeal(ItemManager.Ins.selectedItem.effectNumber);
                                    itemMenuList.gameObject.SetActive(false);

                                    TurnBasedManager.Ins.StartEnemyTurn();
                                }
                                break;
                            case Items.ItemType.damage:

                                if (targetEnemy != null)
                                {
                                    targetEnemy.GetDamage(ItemManager.Ins.selectedItem.effectNumber);
                                }
                                break;
                            default:
                                //TBA_DEBUFF
                                break;
                        }
                        break;
                    default:
                        //TBA_ESCAPE
                        break;
                }
            }
        }

    }

    public void SetAction(string command)
    {
        switch (command)
        {
            case "Attack":
                cmd = Command.attack;
                break;
            case "Skill":
                cmd = Command.skill;
                break;
            case "Item":
                cmd = Command.item;
                ShowItems();
                EnableButton(true, "items");
                break;
            case "Skip":
                Debug.Log("Skipping Turn");
               // TurnBasedManager.Ins.endPlayerTurn();
                break;
            default:
                cmd = Command.escape;
                break;
        }
    }

    public void ShowItems()
    {
        itemMenuList.gameObject.SetActive(true);
        ButtonsPlacement(itemMenuList);
        commandMenuList.gameObject.SetActive(false);
    }

    public void ItemButtonsPlacement()
    {
        int siblingIndex = 0;
        var FetchedItemList = ItemManager.Ins.GetItemsList();

        foreach (Items item in FetchedItemList)
        {
            var newButton = Instantiate(itemButtonPrefab);
            newButton.transform.GetChild(0).GetComponent<TMP_Text>().text = item.itemName;

            Vector3 buttonPos = itemMenuList.position;
            newButton.transform.position = buttonPos;
            newButton.transform.SetParent(itemMenuList);
            newButton.transform.SetSiblingIndex(siblingIndex);

            newButton.GetComponent<Button>().onClick.AddListener(()=>ItemManager.Ins.SelectItem(item.itemName));
            siblingIndex++;
        }

        Vector3 cancelPos = itemMenuList.position;
        var cancelButton = Instantiate(cancelButtonPrefab);
        cancelButton.transform.position = cancelPos;
        cancelButton.transform.SetParent(itemMenuList);
        cancelButton.transform.SetAsLastSibling();
        cancelButton.GetComponent<Button>().onClick.AddListener(() => BackToMain(itemMenuList.gameObject));
    }


    public void BackToMain(GameObject currentMenu)
    {
        commandMenuList.gameObject.SetActive(true);
        ButtonsPlacement(commandMenuList);
        currentMenu.SetActive(false);
    }

    public void ButtonsPlacement(Transform parent)
    {
        float commandMenuHeight = parent.GetComponent<RectTransform>().sizeDelta.y;
        int commandButtonCount = parent.transform.childCount;
        float currentButtonPos = 0f;

        List<Transform> commandChilds = new List<Transform>();

        for (int i = 0; i < commandButtonCount; i++)
        {
            commandChilds.Add(parent.transform.GetChild(i));
        }
        commandChilds.Reverse();

        float buttonPlacementDistance = (commandMenuHeight / commandChilds.Count) - 20f;

        foreach (Transform button in commandChilds)
        {
            Vector3 buttonPos = button.gameObject.GetComponent<RectTransform>().position;
            buttonPos.y = currentButtonPos + buttonPlacementDistance;
            currentButtonPos += buttonPlacementDistance;
            button.GetComponent<RectTransform>().position = buttonPos;
        }
    }

    public void EnableButton(bool e, string list)
    {
        switch (list)
        {
            case "items":
                var itemButtons = itemMenuList.childCount;
                for (int i = 0; i < itemButtons; i++)
                {
                    GameObject button = itemMenuList.GetChild(i).gameObject;
                    if (button.GetComponent<Button>() != null)
                    {
                        button.GetComponent<Button>().enabled = e;
                    }
                }
                break;
            default:
                itemButtons = commandMenuList.childCount;

                for (int i = 0; i > itemButtons; i++)
                {
                    GameObject button = commandMenuList.GetChild(i).gameObject;
                    if (button.GetComponent<Button>() != null)
                    {
                        button.GetComponent<Button>().enabled = e;
                    }
                }
                break;
        }
        
    }

    public IEnumerator CommandStartPlayerTurn()
    {
        yield return new WaitForSeconds(1);
        commandMenuList.gameObject.SetActive(true);

        ButtonsPlacement(commandMenuList);

        attackButton.enabled = true;
        skillButton.enabled = true;
        itemButton.enabled = true;
        escapeButton.enabled = true;
    }

    public IEnumerator CommandEndPlayerTurn()
    {
        attackButton.enabled = false;
        skillButton.enabled = false;
        itemButton.enabled = false;
        escapeButton.enabled = false;
        
        commandMenuList.gameObject.SetActive(false);

        yield return new WaitForSeconds(1);
    }

    void Update()
    {
        ClickToSelectTarget();
    }
}
