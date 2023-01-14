using UnityEngine;

public class PizzaBox : MonoBehaviour
{

    private AudioSource pizzaSound;

    void Awake()
    {
        pizzaSound = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == GameObject.FindGameObjectWithTag("Event System").GetComponent<PlayerStats>().activePlayer)
        {
            pizzaSound.Play();
            GameObject.FindGameObjectWithTag("Event System").GetComponent<Settings>().UnlockTrophy(5);
        }
    }
}
