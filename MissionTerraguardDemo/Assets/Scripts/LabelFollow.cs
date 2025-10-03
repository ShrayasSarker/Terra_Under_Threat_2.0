using UnityEngine;

public class LabelFollow : MonoBehaviour
{
    public Transform target;     // কোন মার্কারকে ফলো করবে
    public Vector3 offset = new Vector3(0, 0.2f, 0);

    void LateUpdate()
    {
        if (target == null || Camera.main == null) return;

        // টার্গেটের সাথে মুভ করো
        transform.position = target.position + offset;

        // ক্যামেরার দিকে মুখ ঘোরাও (billboard)
        Vector3 toCam = transform.position - Camera.main.transform.position;
        transform.rotation = Quaternion.LookRotation(toCam);
    }
}
