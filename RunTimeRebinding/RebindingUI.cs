using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using System.IO;

public class RebindingUI : MonoBehaviour
{
    // 원본키가 무슨역할을 하는지 저장
    public InputActionReference currentAction = null;
    // 바인딩된 키를 텍스트로 표시
    public TextMeshProUGUI bindingDisplayText = null;
    // 키를 표시할 방법
    // https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/api/UnityEngine.InputSystem.InputBinding.DisplayStringOptions.html
    public InputBinding.DisplayStringOptions displayStringOptions;

    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;
    private string path = null;

    public void StartRebinding()
    {
        // 키 바인딩을 위해서는 InputAction을 비활성화 시켜야함
        currentAction.action.Disable();

        // 현재 액션의 바인딩이 override(변경)되어있는지 확인
        if (currentAction.action.bindings[0].hasOverrides)
        {
            // override된 경로를 저장
            path = currentAction.action.bindings[0].overridePath;
        }
        else
        {
            // 기존 경로를 저장
            path = currentAction.action.bindings[0].path;
        }

        // https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/ActionBindings.html#runtime-rebinding
        // 플레이어가 키를 설정할때 까지 기다려줌
        // 설정한 값에 따라 취소, 무시, 실패 콜백, 성공 콜백 등을 호출
        rebindingOperation = currentAction.action.PerformInteractiveRebinding()
            // WithControlsExcluding은 해당 키를 누르면 무시하도록 설정
            .WithControlsExcluding("<Mouse>/rightButton")
            // WithCancelingThrough은 해당 키를 누르면 키 바인딩을 취소하도록 설정
            .WithCancelingThrough("<Mouse>/leftButton")
            // 키 바인딩 취소시 콜백 메서드
            .OnCancel(operation => CancelRebinding())
            // 키 바인딩 완료시 콜백 메서드
            .OnComplete(operation => CompleteRebinding())
            // 카 바인딩 시작
            // Start메서드를 포함하지 않으면 키 바인딩을 시작하지 않음
            // 따로 추가하지 않으면 따로 호출해줘야함
            .Start();

        // 따로 호출가능
        //rebindingOperation.Start();
    }

    public void CancelRebinding()
    {
        Debug.Log("CancelBinding");
        // operation 인스턴스 제거
        rebindingOperation.Dispose();
        // 비활성화된 액션도 다시 활성화
        currentAction.action.Enable();
    }

    public void CompleteRebinding()
    {
        Debug.Log("CompleteBinding");
        // 사용이 완료되면 Dispose로 인스턴스를 제거
        // 제거를 안하면 메모리 누수
        // 따로 전역 변수로 만들지 않았으면 매개변수로 받아와 제거
        rebindingOperation.Dispose();
        // 비활성화했더 InputAction을 활성화
        currentAction.action.Enable();

        // 중복 할당 검사
        if (CheckDuplicationBindings(currentAction.action))
        {
            string path = currentAction.action.bindings[0].hasOverrides ?
                currentAction.action.bindings[0].overridePath : currentAction.action.bindings[0].path;
            Debug.Log($"{path} is Duplication");
            if (path != null)
            {
                // overridePath를 매개변수로 넘긴 path로 변경
                currentAction.action.ApplyBindingOverride(path);
            }
            return;
        }

        // 바인딩된 키를 갱신
        ShowBindingText();
    }

    public void ShowBindingText()
    {
        string displayString = string.Empty;
        string deviceLayoutName = string.Empty;
        string controlPath = string.Empty;

        // 첫번째 매개변수로 Reference에 할당된 키의 인덱스를 넘겨준다. 지금은 각 Reference마다 1개를 할당했기 때문에 0밖에 없음
        // 두번째, 세번째 매개변수로 기기의 정보, 경로를 out으로 값을 획득한다.
        // EX) 키보드의 K키를 누르면 deviceLayoutName에는 Keyboard드가 저장되고, controlPath에는 K가 저장됨
        // 마지막 매개변수로 출력 방식을 설정한 값으로 지정
        displayString = currentAction.action.GetBindingDisplayString(0, out deviceLayoutName, out controlPath, displayStringOptions);

        bindingDisplayText.text = displayString;
    }

    // 중복 키 설정 체크 메서드 추가
    public bool CheckDuplicationBindings(InputAction action)
    {
        // 매개변수로 넘어온 액션의 바인딩 값을 획득
        InputBinding newBinding = action.bindings[0];
        Debug.Log("newBinding.effectivePath : " + newBinding.effectivePath);

        // 액션이 포함되어있는 액션맵의 액션들의 바인딩 값을 순회
        foreach (InputBinding binding in action.actionMap.bindings)
        {
            // 자기 자신이면 무시
            if (newBinding.action == binding.action)
            {
                continue;
            }
            // 자기 자신과 동일한 경로가 있다면 동일한 키가 있는것으로 확인
            // EX) newBinding.effectivePath : Keyboard/p[Keyboard&Mouse]와 동일한 경로인지 비교
            if (newBinding.effectivePath == binding.effectivePath)
            {
                return true;
            }
        }

        return false;
    }

}
