using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public static class ScriptableObjectResetter
{
    static ScriptableObjectResetter()
    {
        // 플레이 모드 상태가 변경될 때 호출되는 콜백 함수 등록
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        // 에디터가 플레이 모드에서 나갈 때 초기화 작업을 수행
        if (state == PlayModeStateChange.EnteredEditMode)
        {
            ResetScriptableObjects();
        }
    }

    private static void ResetScriptableObjects()
    {
        // 초기화하고자 하는 ScriptableObject를 불러와 초기화 작업 수행
        PlayerSO[] objects = Resources.FindObjectsOfTypeAll<PlayerSO>();
        foreach (var obj in objects)
        {
            obj.ResetData(); // ResetData()는 ScriptableObject에서 초기화 작업을 수행하는 함수
            EditorUtility.SetDirty(obj); // 변경사항이 있다고 에디터에 알림
        }

        // 필요한 경우, 변경된 오브젝트들을 저장
        AssetDatabase.SaveAssets();
    }
}
