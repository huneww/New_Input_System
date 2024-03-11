using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }
    
    public void OnSkill1(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("1번 스킬" + context.action.bindings[0].overridePath + " 입력");
            Debug.Log(context.action.bindings[0].overridePath);
            Debug.Log(context.action.bindings[0].name);
            Debug.Log(context.action.bindings[0].ToString());
            //Debug.Log(context.ReadValue<T>()) ;
        }
    }

    public void OnSkill2(InputAction.CallbackContext context)
    {
        if (context.started)
            Debug.Log("2번 스킬");
    }

    public void OnSkill3(InputAction.CallbackContext context)
    {
        if (context.started)
            Debug.Log("3번 스킬");
    }

    public void OnSkill4(InputAction.CallbackContext context)
    {
        if (context.started)
            Debug.Log("4번 스킬");
    }

    public void OnSpell1(InputAction.CallbackContext context)
    {
        if (context.started)
            Debug.Log("1번 스펠");
    }

    public void OnSpell2(InputAction.CallbackContext context)
    {
        if (context.started)
            Debug.Log("2번 스펠");
    }

    public void OnItem1(InputAction.CallbackContext context)
    {
        if (context.started)
            Debug.Log("1번 아이템");
    }

    public void OnItem2(InputAction.CallbackContext context)
    {
        if (context.started)
            Debug.Log("2번 아이템");
    }

    public void OnItem3(InputAction.CallbackContext context)
    {
        if (context.started)
            Debug.Log("3번 아이템");
    }

    public void OnItem4(InputAction.CallbackContext context)
    {
        if (context.started)
            Debug.Log("4번 아이템");
    }

    public void OnItem5(InputAction.CallbackContext context)
    {
        if (context.started)
            Debug.Log("5번 아이템");
    }

    public void OnItem6(InputAction.CallbackContext context)
    {
        if (context.started)
            Debug.Log("6번 아이템");
    }

    public void OnAccessories(InputAction.CallbackContext context)
    {
        if (context.started)
            Debug.Log("악세서리");
    }
}
