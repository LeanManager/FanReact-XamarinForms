#region

using System;
using System.Collections.Generic;

#endregion

namespace FanReact {
	public class EnumUtils {
		public static Dictionary<int, string> Colors = new Dictionary<int, string> {
			//{ 1, "#00592d" }, // Fan React
			//{ 1, "#00604f" }, //MLL
			{1, "#035688"}, //Upward
			{2, "#DDDDDD"},
			{3, "#E0E0E0"},
			{4, "#7f7f7f"},
			{5, "#7f7f7f"},
			{6, "#339933"},
			{7, "#cc0000"},
			{8, "#f2f2f2"},
			{9, "#000000"},
			{10, "#FFFFFF"},
			{11, "#d3d3d3"},
			{12, "#000000"},
			{13, "#3b5998"},
			{14, "#00aced"},
			{15, "#ff0000"},
			{16, "#FFFF00"},
			{17, "#C65B24"},
			{18, "#7AA230"},
			{19, "398023"},
			{20, "#035688"},
			{21, "#F6F6F6"},
			{22, "#999999"},
			{23, "#96000A"},
			{24, "#EEEEEE"},
			{25, "#355f9f"},
			{26, "#2ca9e0"},
			{27, "#008200"},
			{28, "#7A232E"},
			{29, "#F5A623"}
		};

		public static string GetDescription(ColorCodes code) {
			return Colors[(int) code];
		}
	}
}