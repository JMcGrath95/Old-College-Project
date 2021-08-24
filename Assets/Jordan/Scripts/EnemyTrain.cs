using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrain : MonoBehaviour
{
    GameObject Player;
    public ChargeLine chargeLinePrefab;
    MeshRenderer mesh;
    Vector3 starpos = new Vector3(-1000, -1000, -1000);
    float Starttime;
    bool reset = false;
    public float speed = 1.0f;
    public int dmg = 10;

    void Start()
    {
        GameController.GameStarted += GameController_GameStarted;
    }

    private void GameController_GameStarted()
    {
        StartCoroutine(StartTrain());
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    IEnumerator StartTrain()
    {
        gameObject.transform.position = starpos;
        Starttime = Time.time;

        yield return new WaitForSeconds(15);
        yield return SpawnLine();
    }

    IEnumerator SpawnLine()
    {
        ChargeLine c = Instantiate(chargeLinePrefab, Player.transform.position, Quaternion.Euler(new Vector3(90, Random.Range(0f, 360f), 0)));
        Vector3 a = c.Point1.transform.position;
        Vector3 b = c.Point2.transform.position;
        yield return new WaitForSeconds(1);
        yield return RunCharge(2, a, b);

    }


    IEnumerator RunCharge(float time, Vector3 a, Vector3 b)
    {

        if (GameObject.FindGameObjectWithTag("Line"))
        {
            ChargeLine c = GameObject.FindGameObjectWithTag("Line").GetComponent<ChargeLine>();
            float i = 0.0f;
            float rate = (1.0f / time) * speed;
            while (i < 1.0f)
            {
                gameObject.transform.LookAt(c.Point2.transform.position);
                gameObject.transform.Rotate(new Vector3(0, 180, 0));
                i += Time.deltaTime * rate;
                this.transform.position = Vector3.Lerp(a, b, i);

                yield return null;
            }
            reset = true;
            yield return ResetAll();
        }
        else
        {
            yield return null;
        }
    }
    IEnumerator ResetAll()
    {
        if (reset)
        {
            gameObject.transform.position = starpos;
            Destroy(GameObject.FindGameObjectWithTag("Line"));
            yield return new WaitForSeconds(15);
            reset = false;
            yield return SpawnLine();

            yield return null;
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        iDamageable iDamageable;
        Debug.Log("Hit");

        if (other.gameObject == Player)
        {
            if (other.TryGetComponent(out iDamageable))
            {
                iDamageable.TakeDamage(dmg);
            }
        }
    }

    private void OnDestroy()
    {
        GameController.GameStarted -= GameController_GameStarted;
    }

}
