using System;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;

public class Verb_ShootJam : Verb_LaunchProjectile
{
	public bool isJammed;

	public SoundDef jamSound = SoundDef.Named("Misfire");

	protected  int ShotsPerBurst => ((Verb)this).verbProps.burstShotCount;

	public override void WarmupComplete()
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Expected I4, but got Unknown
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Invalid comparison between Unknown and I4
		if (isJammed)
		{
			if (new System.Random().Next(0, 2) == 1)
			{
				isJammed = false;
				MoteMaker.ThrowText(new Vector3((float)((Verb)this).caster.get_Position().x + 1f, ((Verb)this).caster.get_Position().y, (float)((Verb)this).caster.get_Position().z + 1f), ((Verb)this).caster.get_Map(), "Jam Cleared", Color.white, -1f);
				SoundStarter.PlayOneShot(((Thing)((Verb)this).ownerEquipment).def.soundInteract, SoundInfo.op_Implicit(new TargetInfo(((Verb)this).caster.get_Position(), ((Verb)this).caster.get_Map(), false)));
			}
			return;
		}
		int num = 0;
		QualityCategory val = default;
		QualityUtility.TryGetQuality(((Verb)this).caster, ref val);
		if (Rand.Range(1, (int)val switch
		{
			0 => 30,
			2 => 40,
			1 => 50,
			3 => 60,
			4 => 70,
			6 => 80,
			5 => 90,
			7 => 100,
			8 => 10000,
			_ => 60,
		}) == 1)
		{
			MoteMaker.ThrowText(new Vector3((float)((Verb)this).caster.get_Position().x + 1f, ((Verb)this).caster.get_Position().y, (float)((Verb)this).caster.get_Position().z + 1f), ((Verb)this).caster.get_Map(), "Jammed", Color.white, -1f);
			SoundStarter.PlayOneShot(jamSound, SoundInfo.op_Implicit(new TargetInfo(((Verb)this).caster.get_Position(), ((Verb)this).caster.get_Map(), false)));
			isJammed = true;
			return;
		}
		((Verb)this).WarmupComplete();
		if (((Verb)this).get_CasterIsPawn() && ((Verb)this).get_CasterPawn().skills != null)
		{
			float num2 = 10f;
			if (((LocalTargetInfo)( ((Verb)this).currentTarget)).get_Thing() != null && (int)((LocalTargetInfo)( ((Verb)this).currentTarget)).get_Thing().def.category == 1)
			{
				num2 = ((!GenHostility.HostileTo(((LocalTargetInfo)( ((Verb)this).currentTarget)).get_Thing(), ((Verb)this).caster)) ? 50f : 240f);
			}
			((Verb)this).get_CasterPawn().skills.Learn(SkillDefOf.Shooting, num2, false);
		}
	}
}
