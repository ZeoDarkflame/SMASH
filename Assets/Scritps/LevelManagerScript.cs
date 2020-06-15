using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerScript: MonoBehaviour
{
    public GameObject simplehazard;
    public GameObject simplehazard1;
    public GameObject nsssimplehazard;
    public GameObject followhazard;
    public GameObject auxillaryobject = null;
    public PauseMenu pausemenuscript;
    private int currentlevel;
    public AudioSource bgsound;
    // Start is called before the first frame update
    void Start()
    {
        cleanup();
        currentlevel = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (pausemenuscript.level == 1 && currentlevel != 1)
        {
            //Invoke("cleanup", 1);     not required for level 1
            Invoke("level1setup", 2);
            currentlevel = 1;
        }
        if(pausemenuscript.level == 2 && currentlevel != 2)
        {
            cleanup();
            Invoke("level2setup", 2);
            currentlevel = 2;
        }
        if(pausemenuscript.level == 3 && currentlevel != 3)
        {
            cleanup();
            Invoke("level3setup", 2);
            currentlevel = 3;
        }
        if(pausemenuscript.level == 4 && currentlevel != 4)
        {
            cleanup();
            Invoke("level4setup", 2);
            currentlevel = 4;
        }
        if (pausemenuscript.level == 5 && currentlevel != 5)
        {
            cleanup();
            Invoke("level5setup", 2);
            currentlevel = 5;
        }
        if (pausemenuscript.level == 6 && currentlevel != 6)
        {
            cleanup();
            Invoke("level6setup", 2);
            currentlevel = 6;
        }
        if (pausemenuscript.level == 7 && currentlevel != 7)
        {
            cleanup();
            Invoke("level7setup", 2);
            currentlevel = 7;
        }
        if(pausemenuscript.level > 7 && currentlevel != pausemenuscript.level)
        {
            cleanup();
            float randvar = Random.Range(1, 11);
            if(randvar < 4)
            {
                Invoke("randlevel1", 2);
            } else if(randvar < 8)
            {
                Invoke("randlevel2", 2);
            } else if(randvar < 11)
            {
                Invoke("randlevel3", 2);
            }
            currentlevel = pausemenuscript.level;
        }
    }

    private void cleanup()
    {
        simplehazard.SetActive(false);
        simplehazard1.SetActive(false);
        nsssimplehazard.SetActive(false);
        followhazard.SetActive(false);
        Destroy(auxillaryobject);
    }

    private void level1setup()
    {
        simplehazard.SetActive(true);
        simplehazard.transform.position = new Vector3(0, 6, simplehazard.transform.position.z);
    }

    private void level2setup()
    {
        simplehazard.SetActive(true);
        simplehazard.transform.position = new Vector3(-4, 6, simplehazard.transform.position.z);
        simplehazard1.SetActive(true);
        simplehazard1.transform.position = new Vector3(4, 6, simplehazard1.transform.position.z);
    }

    private void level3setup()
    {
        nsssimplehazard.SetActive(true);
        nsssimplehazard.transform.position = new Vector3(0, 6, nsssimplehazard.transform.position.z);
        NSSimpleHazardScript script = nsssimplehazard.GetComponent<NSSimpleHazardScript>();
        script.superfallprob = 3;
    }

    private void level4setup()
    {
        nsssimplehazard.SetActive(true);
        nsssimplehazard.transform.position = new Vector3(0, 6, nsssimplehazard.transform.position.z);
        NSSimpleHazardScript script = nsssimplehazard.GetComponent<NSSimpleHazardScript>();
        script.superfallprob = 7;
    }

    private void level5setup()
    {
        followhazard.SetActive(true);
        followhazard.transform.position = new Vector3(0, 6, followhazard.transform.position.z);
        FollowHazardScript script = followhazard.GetComponent<FollowHazardScript>();
        script.cansuperfall = false;
    }

    private void level6setup()
    {
        nsssimplehazard.SetActive(true);
        auxillaryobject = Instantiate(nsssimplehazard, nsssimplehazard.transform.position, Quaternion.identity);
        auxillaryobject.transform.position = new Vector3(0, 8, nsssimplehazard.transform.position.z);
        nsssimplehazard.transform.position = new Vector3(0, 6, nsssimplehazard.transform.position.z);
        NSSimpleHazardScript script1 = nsssimplehazard.GetComponent<NSSimpleHazardScript>();
        NSSimpleHazardScript script2 = auxillaryobject.GetComponent<NSSimpleHazardScript>();
        script1.superfallprob = 3;
        script2.superfallprob = 3;
    }

    private void level7setup()
    {
        followhazard.SetActive(true);
        FollowHazardScript script = followhazard.GetComponent<FollowHazardScript>();
        followhazard.transform.position = new Vector3(0, 6, followhazard.transform.position.z);
        script.cansuperfall = true;
    }

    private void randlevel1()
    {
        simplehazard.SetActive(true);
        simplehazard1.SetActive(true);
        auxillaryobject = Instantiate(simplehazard, simplehazard.transform.position, Quaternion.identity);
        simplehazard.transform.position = new Vector3(-7, 7, simplehazard.transform.position.z);
        simplehazard1.transform.position = new Vector3(7, 7, simplehazard1.transform.position.z);
        auxillaryobject.transform.position = new Vector3(0, 7, simplehazard.transform.position.z);
    }

    public void randlevel2()
    {
        simplehazard.SetActive(true);
        simplehazard1.SetActive(true);
        nsssimplehazard.SetActive(true);
        simplehazard.transform.position = new Vector3(-7, 7, simplehazard.transform.position.z);
        simplehazard1.transform.position = new Vector3(7, 7, simplehazard1.transform.position.z);
        nsssimplehazard.transform.position = new Vector3(0, 7, nsssimplehazard.transform.position.z);
        NSSimpleHazardScript script = nsssimplehazard.GetComponent<NSSimpleHazardScript>();
        script.superfallprob = 4;
    }

    public void randlevel3()
    {
        simplehazard.SetActive(true);
        nsssimplehazard.SetActive(true);
        followhazard.SetActive(true);
        simplehazard.transform.position = new Vector3(-7, 7, simplehazard.transform.position.z);
        nsssimplehazard.transform.position = new Vector3(6, 8, nsssimplehazard.transform.position.z);
        followhazard.transform.position = new Vector3(-2, 5.5f, followhazard.transform.position.z);
        NSSimpleHazardScript nsscript = nsssimplehazard.GetComponent<NSSimpleHazardScript>();
        nsscript.superfallprob = 2;
        FollowHazardScript fscript = followhazard.GetComponent<FollowHazardScript>();
        fscript.cansuperfall = false;
    }
}
