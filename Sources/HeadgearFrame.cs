using RimWorld;
using UnityEngine;
using Verse;

public class HeadgearFrame : Apparel
{
	public bool init = false;

	public override void DrawWornExtras()
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Invalid comparison between Unknown and I4
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Invalid comparison between Unknown and I4
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Invalid comparison between Unknown and I4
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		if (!ThingUtility.DestroyedOrNull((Thing)(object)((Apparel)this).get_Wearer()))
		{
			if (!init)
			{
				ApparelGraphicRecord item = default;
				ApparelGraphicRecordGetter.TryGetGraphicApparel((Apparel)(object)this, ((Apparel)this).get_Wearer().story.bodyType, ref item);
				((Apparel)this).get_Wearer().get_Drawer().renderer.graphics.apparelGraphics.Remove(item);
				PortraitsCache.Clear();
				init = true;
			}
			if (init && ((Apparel)this).get_Wearer() == null)
			{
				init = false;
			}
			if ((int)PawnUtility.GetPosture(((Apparel)this).get_Wearer()) == 0)
			{
				Material material = GraphicDatabase.Get<Graphic_Multi>(((Thing)this).def.graphicData.texPath, ShaderDatabase.Cutout, Vector2.one, ((Thing)this).get_DrawColor()).MatAt(((Thing)((Apparel)this).get_Wearer()).get_Rotation(), (Thing)null);
				Vector3 s = new(1f, 1f, 1f);
				Matrix4x4 matrix = default;
				Vector3 vector = new(0f, 0f, 0f);
				if ((int)((Apparel)this).get_Wearer().story.bodyType == 1)
				{
					if (((Thing)((Apparel)this).get_Wearer()).get_Rotation() == Rot4.get_West())
					{
						vector = new Vector3(-0.05f, 0f, 0f);
					}
					else if (((Thing)((Apparel)this).get_Wearer()).get_Rotation() == Rot4.get_East())
					{
						vector = new Vector3(0.05f, 0f, 0f);
					}
				}
				if ((int)((Apparel)this).get_Wearer().story.bodyType != 1)
				{
					if (((Thing)((Apparel)this).get_Wearer()).get_Rotation() == Rot4.get_West())
					{
						vector = new Vector3(-0.1f, 0f, 0f);
					}
					else if (((Thing)((Apparel)this).get_Wearer()).get_Rotation() == Rot4.get_East())
					{
						vector = new Vector3(0.1f, 0f, 0f);
					}
				}
				if (RestUtility.Awake(((Apparel)this).get_Wearer()) || !((Apparel)this).get_Wearer().get_Downed())
				{
					matrix.SetTRS(((Thing)((Apparel)this).get_Wearer()).get_DrawPos() + new Vector3(0f, 1f, 0.365f) + vector, Quaternion.AngleAxis(0f, Vector3.up), s);
					Graphics.DrawMesh(MeshPool.humanlikeHeadSet.MeshAt(((Thing)((Apparel)this).get_Wearer()).get_Rotation()), matrix, material, 0);
				}
			}
		}
		if (Find.get_TickManager().get_TicksGame() % 120 == 0)
		{
			init = false;
		}
		((Apparel)this).DrawWornExtras();
	}

	public override void Tick()
	{
		((ThingWithComps)this).Tick();
		if (ThingUtility.DestroyedOrNull((Thing)(object)((Apparel)this).get_Wearer()) && init)
		{
			init = false;
		}
	}
}
