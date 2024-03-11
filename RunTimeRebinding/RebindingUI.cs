using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using System.IO;

public class RebindingUI : MonoBehaviour
{
    // ����Ű�� ���������� �ϴ��� ����
    public InputActionReference currentAction = null;
    // ���ε��� Ű�� �ؽ�Ʈ�� ǥ��
    public TextMeshProUGUI bindingDisplayText = null;
    // Ű�� ǥ���� ���
    // https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/api/UnityEngine.InputSystem.InputBinding.DisplayStringOptions.html
    public InputBinding.DisplayStringOptions displayStringOptions;

    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;
    private string path = null;

    public void StartRebinding()
    {
        // Ű ���ε��� ���ؼ��� InputAction�� ��Ȱ��ȭ ���Ѿ���
        currentAction.action.Disable();

        // ���� �׼��� ���ε��� override(����)�Ǿ��ִ��� Ȯ��
        if (currentAction.action.bindings[0].hasOverrides)
        {
            // override�� ��θ� ����
            path = currentAction.action.bindings[0].overridePath;
        }
        else
        {
            // ���� ��θ� ����
            path = currentAction.action.bindings[0].path;
        }

        // https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/ActionBindings.html#runtime-rebinding
        // �÷��̾ Ű�� �����Ҷ� ���� ��ٷ���
        // ������ ���� ���� ���, ����, ���� �ݹ�, ���� �ݹ� ���� ȣ��
        rebindingOperation = currentAction.action.PerformInteractiveRebinding()
            // WithControlsExcluding�� �ش� Ű�� ������ �����ϵ��� ����
            .WithControlsExcluding("<Mouse>/rightButton")
            // WithCancelingThrough�� �ش� Ű�� ������ Ű ���ε��� ����ϵ��� ����
            .WithCancelingThrough("<Mouse>/leftButton")
            // Ű ���ε� ��ҽ� �ݹ� �޼���
            .OnCancel(operation => CancelRebinding())
            // Ű ���ε� �Ϸ�� �ݹ� �޼���
            .OnComplete(operation => CompleteRebinding())
            // ī ���ε� ����
            // Start�޼��带 �������� ������ Ű ���ε��� �������� ����
            // ���� �߰����� ������ ���� ȣ���������
            .Start();

        // ���� ȣ�Ⱑ��
        //rebindingOperation.Start();
    }

    public void CancelRebinding()
    {
        Debug.Log("CancelBinding");
        // operation �ν��Ͻ� ����
        rebindingOperation.Dispose();
        // ��Ȱ��ȭ�� �׼ǵ� �ٽ� Ȱ��ȭ
        currentAction.action.Enable();
    }

    public void CompleteRebinding()
    {
        Debug.Log("CompleteBinding");
        // ����� �Ϸ�Ǹ� Dispose�� �ν��Ͻ��� ����
        // ���Ÿ� ���ϸ� �޸� ����
        // ���� ���� ������ ������ �ʾ����� �Ű������� �޾ƿ� ����
        rebindingOperation.Dispose();
        // ��Ȱ��ȭ�ߴ� InputAction�� Ȱ��ȭ
        currentAction.action.Enable();

        // �ߺ� �Ҵ� �˻�
        if (CheckDuplicationBindings(currentAction.action))
        {
            string path = currentAction.action.bindings[0].hasOverrides ?
                currentAction.action.bindings[0].overridePath : currentAction.action.bindings[0].path;
            Debug.Log($"{path} is Duplication");
            if (path != null)
            {
                // overridePath�� �Ű������� �ѱ� path�� ����
                currentAction.action.ApplyBindingOverride(path);
            }
            return;
        }

        // ���ε��� Ű�� ����
        ShowBindingText();
    }

    public void ShowBindingText()
    {
        string displayString = string.Empty;
        string deviceLayoutName = string.Empty;
        string controlPath = string.Empty;

        // ù��° �Ű������� Reference�� �Ҵ�� Ű�� �ε����� �Ѱ��ش�. ������ �� Reference���� 1���� �Ҵ��߱� ������ 0�ۿ� ����
        // �ι�°, ����° �Ű������� ����� ����, ��θ� out���� ���� ȹ���Ѵ�.
        // EX) Ű������ KŰ�� ������ deviceLayoutName���� Keyboard�尡 ����ǰ�, controlPath���� K�� �����
        // ������ �Ű������� ��� ����� ������ ������ ����
        displayString = currentAction.action.GetBindingDisplayString(0, out deviceLayoutName, out controlPath, displayStringOptions);

        bindingDisplayText.text = displayString;
    }

    // �ߺ� Ű ���� üũ �޼��� �߰�
    public bool CheckDuplicationBindings(InputAction action)
    {
        // �Ű������� �Ѿ�� �׼��� ���ε� ���� ȹ��
        InputBinding newBinding = action.bindings[0];
        Debug.Log("newBinding.effectivePath : " + newBinding.effectivePath);

        // �׼��� ���ԵǾ��ִ� �׼Ǹ��� �׼ǵ��� ���ε� ���� ��ȸ
        foreach (InputBinding binding in action.actionMap.bindings)
        {
            // �ڱ� �ڽ��̸� ����
            if (newBinding.action == binding.action)
            {
                continue;
            }
            // �ڱ� �ڽŰ� ������ ��ΰ� �ִٸ� ������ Ű�� �ִ°����� Ȯ��
            // EX) newBinding.effectivePath : Keyboard/p[Keyboard&Mouse]�� ������ ������� ��
            if (newBinding.effectivePath == binding.effectivePath)
            {
                return true;
            }
        }

        return false;
    }

}
