using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Porte : ObjetEnvironnemental
{

    private Animator anim;

    public bool isDecorative;

    [HideInInspector]
    public List<ObjetNecessaire> objN = new List<ObjetNecessaire>();
    private PrincesseObjetProgression juliette;



    // Use this for initialization
    void Awake()
    {
        anim = GetComponent<Animator>();
        juliette = GameObject.FindGameObjectWithTag("Player").GetComponent<PrincesseObjetProgression>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Activation()
    {
        if (!isDecorative)
        {
            if (isActivable())
            {
                OuverturePorte();
            }
            else
            {
                sm.playOneShot(BesoinItemPourActivation);
            }
        }
    }


    private bool isActivable()
    {

        foreach (ObjetNecessaire obj in objN)
        {
            if (juliette.listObjet.ContainsKey(obj.objet))
            {
                if (juliette.listObjet[obj.objet] < obj.nombre)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        return true;
    }

    public override EnumIconeInterraction getIconeInteraction()
    {
        if (isDecorative)
        {
            return EnumIconeInterraction.icone_null;
        }
        if (isActivable())
        {
            return EnumIconeInterraction.icone_default;
        }
        else
        {
            return EnumIconeInterraction.icone_non_default;
        }



    }

    public void OuverturePorte()
    {
        anim.SetBool("isOpen", true);
        isDecorative = true;
        foreach (ObjetNecessaire obj in objN)
        {
            juliette.removeItem(obj.objet, obj.nombre);
        }
    }

    public void setOpen(bool open){
        anim.SetBool("isOpen", open);
        isDecorative = open;
    }

    public void setOpenEndDoor(bool open){
        anim.SetBool("isOpen", open);
        anim.SetBool("lockUnlock", open);
        isDecorative = open;
    }
}
