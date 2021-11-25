using GaussWeapons;
using Verse;

public class CompExtraDamage : ThingComp
{
	public CompProperties_ExtraDamage compProp;

	public string damageDef;

	public int damageAmount;

	public float chanceToProc;

	public int count;

	public override void Initialize(CompProperties vprops)
	{
		((ThingComp)this).Initialize(vprops);
		compProp = vprops as CompProperties_ExtraDamage;
		if (compProp != null)
		{
			damageDef = compProp.damageDef;
			damageAmount = compProp.damageAmount;
			chanceToProc = compProp.chanceToProc;
		}
		else
		{
			Log.Warning("Could not find a CompProperties_ExtraDamage for CompExtraDamage.");
			count = 9876;
		}
	}

	public override void PostExposeData()
	{
		((ThingComp)this).PostExposeData();
		Scribe_Values.Look<int>(ref count, "count", 1, false);
	}
}
