namespace ChronosFall.Scripts.Core.Configs
{
    public static class CCamera
    {
        // 設定値を保持するプライベートな静的フィールド
        private static bool _sCameraRotateInvert = false;

        // 設定値を読み書きするための静的なプロパティ
        public static bool CameraRotateInvert
        {
            get => _sCameraRotateInvert;
            set => _sCameraRotateInvert = value;
        }

        // 設定を変更するロジックが必要であれば、メソッドは残しても良い
        public static void SetConfig(bool invert) 
        {
            _sCameraRotateInvert = invert;
        }
    }
}