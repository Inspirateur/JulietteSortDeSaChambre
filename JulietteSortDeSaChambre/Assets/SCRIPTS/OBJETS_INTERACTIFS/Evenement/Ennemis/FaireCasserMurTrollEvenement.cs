using System;


public class FaireCasserMurTrollEvenement : Evenement {
	public IA_Agent troll;

	public override void activation(){
		((TRO_E_CasseMur)troll.getEtatCourant()).enAttente = false;
	}
}


