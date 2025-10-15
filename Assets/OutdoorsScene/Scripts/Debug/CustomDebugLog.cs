using System;
using JetBrains.Annotations;
using UnityEngine;

namespace CustomDebug
{
    public class CustomDebugLog
    {
        public void DebugLog(MarkType selectMarkType, ColorType selectColorType, String msg, GameObject gameObject = null)
        {
            string debugOutputLog = GetColorType(selectColorType) + msg + "</color>";
            
            switch (selectMarkType)
            {
                case MarkType.Log:
                    Debug.Log(debugOutputLog, gameObject);
                    break;
                case MarkType.Assert:
                    Debug.Log(debugOutputLog, gameObject);
                    break;
                case MarkType.Warning:
                    Debug.LogWarning(debugOutputLog, gameObject);
                    break;
                case MarkType.Error:
                    Debug.LogError(debugOutputLog, gameObject);
                    break;
                default:
                    Debug.LogError("[ CustomDebugLog.cs ] ログ出力が正常に動作しませんでした。");
                    break;
            }
        }
        
        // カラーリスト
        public enum ColorType
        {
            [UsedImplicitly] Red,
            [UsedImplicitly] Orange,
            [UsedImplicitly] Yellow,
            [UsedImplicitly] Green,
            [UsedImplicitly] Cyan,
            [UsedImplicitly] Blue,
            [UsedImplicitly] Magenta
        }
        
        // マークリスト
        public enum MarkType
        {
            Log,
            Assert,
            Warning,
            Error
        }
        
        private string GetColorType(ColorType colorType)
        {
            string[] colorTables = { "<color=red>", "<color=orange>", "<color=yellow>", "<color=green>", "<color=cyan>", "<color=blue>", "<color=magenta>" };
            return colorTables[(int)colorType];
        }
    }
}