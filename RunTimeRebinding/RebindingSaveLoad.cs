using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RebindingSaveLoad : MonoBehaviour
{
    public InputActionAsset asset;
    public List<RebindingUI> rebindingUIs = new List<RebindingUI>();

    private void OnEnable()
    {
        // ����� Json������ ȹ��
        string rebinds = PlayerPrefs.GetString("rebinds");

        // Json�� �ִ��� ������ Ȯ��
        if (!string.IsNullOrEmpty(rebinds))
        {
            // ����Ƽ���� �����ϴ� Json���� �������� �����ϴ� �޼��� ȣ��
            asset.LoadBindingOverridesFromJson(rebinds);
        }

        // Ű ���ε� �ؽ�Ʈ�� �ҷ��� ���� ����
        foreach (RebindingUI rebindingUI in rebindingUIs)
        {
            rebindingUI.ShowBindingText();
        }
    }

    private void OnDisable()
    {
        // ����Ƽ���� �����ϴ� ���¿��� Json���� �����ϴ� �޼��� ȣ��
        string rebinds = asset.SaveBindingOverridesAsJson();

        // ���ڿ��� ����
        PlayerPrefs.SetString("rebinds", rebinds);
    }

    public void ResetRebinding()
    {
        // �׼Ǹ��� ��ȸ�ϸ鼭 �� �ʿ��ִ� ��� InputAction�� �������̵�� ��θ� ����
        foreach (InputActionMap map in asset.actionMaps)
        {
            map.RemoveAllBindingOverrides();
        }

        // �ʱ�ȭ�� Ű ���� ����
        foreach (RebindingUI rebindingUI in rebindingUIs)
        {
            rebindingUI.ShowBindingText();
        }

        PlayerPrefs.DeleteKey("rebind");
    }

}
