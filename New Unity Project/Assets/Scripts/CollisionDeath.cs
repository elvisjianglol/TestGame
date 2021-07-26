using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CollisionDeath : MonoBehaviour
{

    [SerializeField] private float restartDelay = 1f;


    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Invoke("Restart", restartDelay);
        }
        else Destroy(collision.collider.gameObject, restartDelay);
    }

    void Restart()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
