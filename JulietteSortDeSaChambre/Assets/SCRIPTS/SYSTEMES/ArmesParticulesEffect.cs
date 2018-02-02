using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmesParticulesEffect : MonoBehaviour {

    public GameObject[] ParticuleMobHurt;
    // ParticuleMobHurt
    // 0 - BloodEffect

    private SoundEntity soundGenerator;

    public AudioClip[] SoundParticuleMobHurt;
    // Sound ParticuleMobHurt
    // 0 - Son Chair

    public GameObject[] ParticuleWall;
    // ParticuleWall
    // 0 - 

    public AudioClip[] SoundParticuleWall;
    // Sound ParticuleWall
    // 0 - 

    private void Start()
    {
        soundGenerator = gameObject.GetComponent<SoundEntity>();
    }

    public void ParticulePlay (EnumArmes armeActuel, Vector3 hitPoint, bool TouchMob) {

        switch (armeActuel)
        {
            case EnumArmes.VIDE:

                break;

            case EnumArmes.POELE:

                break;

            case EnumArmes.PAIN:

                break;

            case EnumArmes.PIED_LIT:
                if (!TouchMob)
                {
                    Instantiate(ParticuleWall[0], new Vector3(hitPoint.x, hitPoint.y + 0.5f, hitPoint.z), Quaternion.identity);
                    soundGenerator.playOneShot(SoundParticuleWall[0], 0.3f);
                }
                
                break;

            case EnumArmes.CHANDELIER:

                break;

            case EnumArmes.PELLE:

                break;

            case EnumArmes.BAGUETTE_MAGIQUE:

                break;
        }
        if (TouchMob)
        {
            Instantiate(ParticuleMobHurt[0], new Vector3(hitPoint.x, hitPoint.y + 0.5f, hitPoint.z), Quaternion.identity);
            soundGenerator.playOneShot(SoundParticuleMobHurt[0], 0.3f);
        }
    }

}
