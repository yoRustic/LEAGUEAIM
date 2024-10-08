﻿using ImGuiNET;

namespace LEAGUEAIM.Utilities
{
	public static class Fonts
	{
		public static ImFontPtr Icons;
		public static ImFontPtr IconsSm;
		public static ImFontPtr IconsLg;
		public static ImFontPtr Header;
		public static ImFontPtr Menu;
		public static ImFontPtr MenuSm;
		public static ImFontPtr MenuLg;
		public static ImFontPtr Code;
		public static bool Replaced = false;
		public static int SelectedFont = -1;

		public unsafe static bool Load(LARenderer renderer)
		{
			if (!Fonts.Replaced)
			{
				bool res = renderer.ReplaceFont(config =>
				{
					string menuFontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "LEAGUEAIM", "fonts", "ClashDisplay-Semibold.ttf");
					string headerFontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "LEAGUEAIM", "fonts", "ClashDisplay-Bold.ttf");
					string iconFontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "LEAGUEAIM", "fonts", "FontAwesome.ttf");
					string codeFontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "LEAGUEAIM", "fonts", "FiraCode.ttf");
					ushort[] customRange = [IconFonts.FontAwesome6.IconMin, IconFonts.FontAwesome6.IconMax, 0];
					var io = ImGui.GetIO();


					if (File.Exists(menuFontPath)) {
						Menu = io.Fonts.AddFontFromFileTTF(menuFontPath, 15, config, io.Fonts.GetGlyphRangesDefault());
						MenuSm = io.Fonts.AddFontFromFileTTF(menuFontPath, 14, config, io.Fonts.GetGlyphRangesDefault());
						MenuLg = io.Fonts.AddFontFromFileTTF(menuFontPath, 18, config, io.Fonts.GetGlyphRangesDefault());
					}

					if (File.Exists(codeFontPath))
					{
						Code = io.Fonts.AddFontFromFileTTF(codeFontPath, 15, config, io.Fonts.GetGlyphRangesDefault());
					}

					fixed (ushort* p = &customRange[0])
						if (File.Exists(iconFontPath))
						{
							Icons = io.Fonts.AddFontFromFileTTF(iconFontPath, 16, config, new IntPtr(p));
							IconsSm = io.Fonts.AddFontFromFileTTF(iconFontPath, 12, config, new IntPtr(p));
							IconsLg = io.Fonts.AddFontFromFileTTF(iconFontPath, 20, config, new IntPtr(p));
						}

					if (File.Exists(headerFontPath))
						Header = io.Fonts.AddFontFromFileTTF(headerFontPath, 36, config, io.Fonts.GetGlyphRangesDefault());
				});

				Fonts.Replaced = true;

				return res;
			}

			return false;
		}

		public static string[] GetInstalledFonts()
		{
			string[] fonts = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "*.ttf");

			// remove the path and extension
			for (int i = 0; i < fonts.Length; i++)
			{
				fonts[i] = Path.GetFileNameWithoutExtension(fonts[i]);
			}

			return fonts;
		}
		public static void SetMenuFont(string fontPath, float size)
		{
			var io = ImGui.GetIO();
			Menu = io.Fonts.AddFontFromFileTTF(fontPath, size);
			MenuSm = io.Fonts.AddFontFromFileTTF(fontPath, size - 3);
			MenuLg = io.Fonts.AddFontFromFileTTF(fontPath, size + 3);
			Fonts.Replaced = false;
		}
	}
}
