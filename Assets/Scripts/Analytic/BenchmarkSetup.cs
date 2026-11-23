using UnityEngine;

public class BenchmarkSetup : MonoBehaviour
{
    void Awake()
    {
        // 1. ปิด VSync
        QualitySettings.vSyncCount = 0;

        // 2. ปลดเพดานเฟรมเรต (Unlimit FPS)
        Application.targetFrameRate = -1;

        // 5. ล็อกความละเอียดหน้าจอ 1920x1080
        // (ปรับ FullScreenMode.FullScreenWindow หรือ Windowed ได้ตามต้องการ)
        Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);
    }
}