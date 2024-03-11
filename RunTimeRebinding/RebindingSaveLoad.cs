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
        // 저장된 Json파일을 획득
        string rebinds = PlayerPrefs.GetString("rebinds");

        // Json이 있는지 없는지 확인
        if (!string.IsNullOrEmpty(rebinds))
        {
            // 유니티에서 제공하는 Json에서 에셋으로 변경하는 메서드 호출
            asset.LoadBindingOverridesFromJson(rebinds);
        }

        // 키 바인딩 텍스트에 불러온 값을 적용
        foreach (RebindingUI rebindingUI in rebindingUIs)
        {
            rebindingUI.ShowBindingText();
        }
    }

    private void OnDisable()
    {
        // 유니티에서 제공하는 에셋에서 Json으로 변경하는 메서드 호출
        string rebinds = asset.SaveBindingOverridesAsJson();

        // 문자열을 저장
        PlayerPrefs.SetString("rebinds", rebinds);
    }

    public void ResetRebinding()
    {
        // 액션맵을 순회하면서 각 맵에있는 모든 InputAction의 오버라이드된 경로를 제거
        foreach (InputActionMap map in asset.actionMaps)
        {
            map.RemoveAllBindingOverrides();
        }

        // 초기화된 키 값을 갱신
        foreach (RebindingUI rebindingUI in rebindingUIs)
        {
            rebindingUI.ShowBindingText();
        }

        PlayerPrefs.DeleteKey("rebind");
    }

}
