using UnityEngine;
using System.Collections;

public class TchinTchinController : MonoBehaviour
{


    public Transform hand;

    public float moveHandSpeed = 1;
    public float slowMotionTime = 0.2f;
    public float slowdownTimeScale = 0.2f;

    void Start()
    {

    }

    void Update()
    {
        hand.Translate(Vector3.forward * moveHandZ * moveHandSpeed * Time.deltaTime);


        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(SlowMotion());

        }
    }

    float moveHandZ = 0;

    bool freelook = false;

    IEnumerator SlowMotion()
    {

        moveHandZ = 1;
        Time.timeScale = slowdownTimeScale;

        yield return new WaitForSeconds(slowMotionTime);

        Time.timeScale = 1;
        moveHandZ = -1;

        yield return new WaitForSeconds(slowMotionTime);

        moveHandZ = 0;
    }
}
