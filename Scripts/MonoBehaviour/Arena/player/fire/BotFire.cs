using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotFire : FireController
{

	protected override void fire ()
	{
		
		base.fire ();
//		cooldownFire = 60;
//		for (int i = 0; i < 2; i++) {
//			var f = createFire ();
//			var p = bulletSpawn.position;
//			p.y += i * .6f;
//			f.speedY = (-.1f + .2f * i) * .5f;
//			f.setPosition (p, -1);
//		}
//
//
	}

	protected override void firePosition (FireBehaviour fireBehaviour)
	{
		fireBehaviour.setPosition (bulletSpawn.position, -1);
	}

	protected override void handleFire ()
	{
		SendMessageUpwards ("hitEnemy");
	}
}
