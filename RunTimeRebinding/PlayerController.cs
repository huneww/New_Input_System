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
            Debug.Log("1�� ��ų" + context.action.bindings[0].overridePath + " �Է�");
            Debug.Log(context.action.bindings[0].overridePath);
            Debug.Log(context.action.bindings[0].name);
            Debug.Log(context.action.bindings[0].ToString());
            //Debug.Log(context.ReadValue<T>()) ;
        }
    }

    public void OnSkill2(InputAction.CallbackContext context)
    {
        if (context.started)
            Debug.Log("2�� ��ų");
    }

    public void OnSkill3(InputAction.CallbackContext context)
    {
        if (context.started)
            Debug.Log("3�� ��ų");
    }

    public void OnSkill4(InputAction.CallbackContext context)
    {
        if (context.started)
            Debug.Log("4�� ��ų");
    }

    public void OnSpell1(InputAction.CallbackContext context)
    {
        if (context.started)
            Debug.Log("1�� ����");
    }

    public void OnSpell2(InputAction.CallbackContext context)
    {
        if (context.started)
            Debug.Log("2�� ����");
    }

    public void OnItem1(InputAction.CallbackContext context)
    {
        if (context.started)
            Debug.Log("1�� ������");
    }

    public void OnItem2(InputAction.CallbackContext context)
    {
        if (context.started)
            Debug.Log("2�� ������");
    }

    public void OnItem3(InputAction.CallbackContext context)
    {
        if (context.started)
            Debug.Log("3�� ������");
    }

    public void OnItem4(InputAction.CallbackContext context)
    {
        if (context.started)
            Debug.Log("4�� ������");
    }

    public void OnItem5(InputAction.CallbackContext context)
    {
        if (context.started)
            Debug.Log("5�� ������");
    }

    public void OnItem6(InputAction.CallbackContext context)
    {
        if (context.started)
            Debug.Log("6�� ������");
    }

    public void OnAccessories(InputAction.CallbackContext context)
    {
        if (context.started)
            Debug.Log("�Ǽ�����");
    }
}
