using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretWin : MonoBehaviour {

    public CameraShake shake;
    public GameObject darkEnergyContainer, blackHoleStartContainer, ball, gameMusicGO;

    public AudioClip[] sounds;

    public AudioSource source;

    float modifier = 3f;

    private bool end = false;

    void Update()
    {
        if(end)
        {
            ball.transform.localScale *= (modifier * Time.deltaTime);
        }
    }

    public void StartSecretWin()
    {
        StartCoroutine(SecretWinCutscene());
    }

    IEnumerator SecretWinCutscene()
    {
        Debug.Log("Ohhh boy");
        gameMusicGO.GetComponent<AudioSource>().Stop();
        gameMusicGO.SetActive(false);

        GetComponent<Rigidbody2D>().mass = 9999;
        GetComponent<Rigidbody2D>().angularDrag = 999;
        GetComponent<Rigidbody2D>().drag = 999;

        yield return new WaitForSeconds(0.25f);

        shake.StartShake(0, 0.075f, 9.5f, 0.1f);
        source.clip = sounds[0];
        source.Play();

        yield return new WaitForSeconds(1f);

        Dialogue._instance.StartDialogue(58, true);


        yield return new WaitForSeconds(8.5f);
        source.Stop();
        source.clip = sounds[1];
        source.Play();

        shake.StartShake(0, 0.05f, 4.2f, 0.15f);
        darkEnergyContainer.SetActive(true);

        yield return new WaitForSeconds(4.2f);

        source.Stop();
        source.clip = sounds[2];
        source.Play();

        shake.StartShake(0, 0.025f, 15.1f, 0.25f);
        blackHoleStartContainer.SetActive(true);

        yield return new WaitForSeconds(15.1f);

        Debug.Log("WEW");

        source.Stop();
        source.clip = sounds[3];
        source.Play();

        shake.StartShake(0, 0.015f, 10, 0.3f);

        blackHoleStartContainer.transform.localScale = new Vector3(2, 2, 2);
        blackHoleStartContainer.GetComponentInChildren<Animator>().speed = 2;

        yield return new WaitForSeconds(3f);

        source.Stop();
        source.PlayOneShot(sounds[4]);
       
        end = true;

        yield return new WaitForSeconds(5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
