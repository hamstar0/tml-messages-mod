using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Libraries.NPCs.Attributes;


namespace Messages.Definitions {
	public partial class Message {
		public static string RenderFormattedDescription( int npcType, string description ) {
			string npcName = NPCNameAttributeLibraries.GetQualifiedName( npcType );

			int npcWho = NPC.FindFirstNPC( npcType );
			if( npcWho != -1 ) {
				NPC npc = Main.npc[npcWho];

				if( npc?.active == true && npc.netID == npcType ) {
					npcName = npc.FullName;
				}
			}

			IEnumerable<string> descLines = description.Split( '\n' );
			descLines = descLines.Select( s => s.Length >= 2 ? "\n"+s+"\n" : s );
			string desc = string.Join( "\n", descLines );

			return "[c/FFFF80:"+npcName+" says]: "+desc;
		}
	}
}
