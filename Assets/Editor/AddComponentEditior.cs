using UnityEditor;
using UnityEngine;

namespace Editor
{
    /// <summary>
    /// 選択中のGameObjectにスクリプトを一括アタッチするエディタ拡張
    /// </summary>
    public class AddComponentEditor : EditorWindow
    {
        private MonoScript targetScript; // アタッチするスクリプト
        private bool removeExisting = false; // 既存のコンポーネントを削除するか

        [MenuItem("ChronosFall/Tools/Add Component to Selected")]
        public static void ShowWindow()
        {
            GetWindow<AddComponentEditor>("Add Component");
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("選択中のGameObjectにスクリプトをアタッチ", EditorStyles.boldLabel);
            EditorGUILayout.Space(10);

            // スクリプト選択
            EditorGUILayout.LabelField("アタッチするスクリプト:", EditorStyles.label);
            targetScript = (MonoScript)EditorGUILayout.ObjectField(targetScript, typeof(MonoScript), false);

            EditorGUILayout.Space(5);

            // オプション
            removeExisting = EditorGUILayout.Toggle("既存のコンポーネントを削除", removeExisting);

            EditorGUILayout.Space(10);

            // 選択中のオブジェクト表示
            GameObject[] selectedObjects = Selection.gameObjects;
            EditorGUILayout.LabelField($"選択中: {selectedObjects.Length}個のGameObject", EditorStyles.helpBox);

            if (selectedObjects.Length > 0)
            {
                EditorGUILayout.BeginVertical("box");
                foreach (var obj in selectedObjects)
                {
                    EditorGUILayout.LabelField($"  - {obj.name}");
                }
                EditorGUILayout.EndVertical();
            }

            EditorGUILayout.Space(10);

            // 実行ボタン
            GUI.enabled = targetScript != null && selectedObjects.Length > 0;
            
            if (GUILayout.Button("スクリプトをアタッチ", GUILayout.Height(30)))
            {
                AddComponentToSelected();
            }

            GUI.enabled = true;

            EditorGUILayout.Space(5);

            // ヘルプ
            EditorGUILayout.HelpBox(
                "使い方:\n" +
                "1. Hierarchyで対象のGameObjectを選択\n" +
                "2. アタッチしたいスクリプトをドラッグ&ドロップ\n" +
                "3. 「スクリプトをアタッチ」ボタンをクリック",
                MessageType.Info
            );
        }

        private void AddComponentToSelected()
        {
            if (targetScript == null)
            {
                EditorUtility.DisplayDialog("エラー", "スクリプトが選択されていません", "OK");
                return;
            }

            // スクリプトから型を取得
            System.Type componentType = targetScript.GetClass();

            if (componentType == null)
            {
                EditorUtility.DisplayDialog("エラー", "スクリプトの型を取得できませんでした", "OK");
                return;
            }

            if (!typeof(Component).IsAssignableFrom(componentType))
            {
                EditorUtility.DisplayDialog("エラー", "このスクリプトはMonoBehaviourを継承していません", "OK");
                return;
            }

            GameObject[] selectedObjects = Selection.gameObjects;
            int successCount = 0;
            int skipCount = 0;

            // Undo記録開始
            Undo.RecordObjects(selectedObjects, "Add Component to Selected");

            foreach (GameObject obj in selectedObjects)
            {
                // 既存のコンポーネントチェック
                Component existingComponent = obj.GetComponent(componentType);

                if (existingComponent != null)
                {
                    if (removeExisting)
                    {
                        Undo.DestroyObjectImmediate(existingComponent);
                        obj.AddComponent(componentType);
                        successCount++;
                    }
                    else
                    {
                        Debug.LogWarning($"{obj.name} には既に {componentType.Name} がアタッチされています");
                        skipCount++;
                        continue;
                    }
                }
                else
                {
                    obj.AddComponent(componentType);
                    successCount++;
                }
            }

            // 結果表示
            string message = $"完了しました！\n\n追加: {successCount}個\nスキップ: {skipCount}個";
            EditorUtility.DisplayDialog("完了", message, "OK");

            Debug.Log($"[AddComponent] {componentType.Name} を {successCount}個のGameObjectに追加しました");
        }
    }
}