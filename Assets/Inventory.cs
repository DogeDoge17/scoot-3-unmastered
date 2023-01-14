using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    public int selectedItem;
    [Space]
    public List<GameObject> slots;
    public List<GameObject> icons;
    public List<GameObject> panels;

    [Space]
    [SerializeField]
    private Color selectedColor;
    [SerializeField]
    private Color defaultColor;

    [Space]
    public List<string> objectNames;

    public List<GameObject> objects;

    public bool open;
    private float hideTimer;

    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {

    }
    
    void Update()
    {
        hideTimer -= 1 * Time.deltaTime;

        if (SimpleInput.GetKeyDown(KeyCode.Alpha1)) SwitchHotBar(0);
        else if (SimpleInput.GetKeyDown(KeyCode.Alpha2)) SwitchHotBar(1);
        else if (SimpleInput.GetKeyDown(KeyCode.Alpha3)) SwitchHotBar(2);
        else if (SimpleInput.GetKeyDown(KeyCode.Alpha4)) SwitchHotBar(3);

        if (hideTimer <= 0)
        {
            animator.SetBool("Open", false);

            open = false;
        }

        
    }

    public void SwitchHotBar(int spot)
    {
        if (!open)
        {
            animator.SetBool("Open", true);
            hideTimer = 2;
        }

        selectedItem = spot;

        for (int i = 0; i < panels.Count; i++)
        {
            panels[i].GetComponent<Image>().color = defaultColor;
        }

        panels[spot].GetComponent<Image>().color = selectedColor;

    } 

}
