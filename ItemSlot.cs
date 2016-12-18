﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.UI;
using Terraria;
using ItemChecklist.UI;

namespace ItemChecklist
{
	internal class ItemSlot : UIElement
	{
		public static Texture2D backgroundTexture = Main.inventoryBack9Texture;

		private Texture2D _texture;
		//	private float _visibilityActive = 1f;
		//		private float _visibilityInactive = 0.4f;
		private float scale = 0.6f;
		private int id;
		private Item item;

		public ItemSlot(int id)
		{
			this._texture = Main.itemTexture[id];
			this.id = id;
			this.item = new Item();
			item.SetDefaults(id);

			this.Width.Set(backgroundTexture.Width * scale, 0f);
			this.Height.Set(backgroundTexture.Height * scale, 0f);
		}

		public override int CompareTo(object obj)
		{
			ItemSlot other = obj as ItemSlot;
			return id.CompareTo(other.id);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			//spriteBatch.Draw(this._texture, dimensions.Position(), Color.White * (base.IsMouseHovering ? this._visibilityActive : this._visibilityInactive));

			spriteBatch.Draw(backgroundTexture, dimensions.Position(), null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
			//Texture2D texture2D = Main.itemTexture[this.item.type];
			Rectangle rectangle2;
			if (Main.itemAnimations[id] != null)
			{
				rectangle2 = Main.itemAnimations[id].GetFrame(_texture);
			}
			else
			{
				rectangle2 = _texture.Frame(1, 1, 0, 0);
			}
			float num = 1f;
			float num2 = (float)backgroundTexture.Width * scale;
			if ((float)rectangle2.Width > num2 || (float)rectangle2.Height > num2)
			{
				if (rectangle2.Width > rectangle2.Height)
				{
					num = num2 / (float)rectangle2.Width;
				}
				else
				{
					num = num2 / (float)rectangle2.Height;
				}
			}
			Vector2 drawPosition = dimensions.Position();
			drawPosition.X += (float)backgroundTexture.Width * scale / 2f - (float)rectangle2.Width * num / 2f;
			drawPosition.Y += (float)backgroundTexture.Height * scale / 2f - (float)rectangle2.Height * num / 2f;

			item.GetColor(Color.White);
			Color alphaColor = Main.LocalPlayer.GetModPlayer<ItemChecklistPlayer>(ItemChecklist.instance).foundItem[id] ? this.item.GetAlpha(Color.White) : Color.Black;
			Color colorColor = Main.LocalPlayer.GetModPlayer<ItemChecklistPlayer>(ItemChecklist.instance).foundItem[id] ? this.item.GetColor(Color.White) : Color.Black;
			//spriteBatch.Draw(_texture, drawPosition, new Rectangle?(rectangle2), this.item.GetAlpha(Color.White), 0f, Vector2.Zero, num, SpriteEffects.None, 0f);
			spriteBatch.Draw(_texture, drawPosition, new Rectangle?(rectangle2), alphaColor, 0f, Vector2.Zero, num, SpriteEffects.None, 0f);
			if (this.item.color != Color.Transparent)
			{
				spriteBatch.Draw(_texture, drawPosition, new Rectangle?(rectangle2), colorColor, 0f, Vector2.Zero, num, SpriteEffects.None, 0f);
			}
			//if (this.item.stack > 1)
			//{
			//	spriteBatch.DrawString(Main.fontItemStack, this.item.stack.ToString(), new Vector2(dimensions.Position().X + 10f * scale, dimensions.Position().Y + 26f * scale), Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
			//}


			if (IsMouseHovering)
			{
				ItemChecklistUI.hoverText = item.name + (item.modItem != null ? " [" + item.modItem.mod.Name + "]" : "");
			}
			//if (IsMouseHovering)
			//{
			//	string hoverText = item.name + " " + item.type;
			//	float x = Main.fontMouseText.MeasureString(hoverText).X;
			//	Vector2 vector = new Vector2((float)Main.mouseX, (float)Main.mouseY) + new Vector2(16f, 0f);
			//	if (vector.Y > (float)(Main.screenHeight - 30))
			//	{
			//		vector.Y = (float)(Main.screenHeight - 30);
			//	}
			//	if (vector.X > (float)(Parent.GetDimensions().Width + Parent.GetDimensions().X - x - 16))
			//	{
			//		vector.X = (float)(Parent.GetDimensions().Width + Parent.GetDimensions().X - x - 16);
			//	}
			//	Utils.DrawBorderStringFourWay(spriteBatch, Main.fontMouseText, hoverText, vector.X, vector.Y, new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), Color.Black, Vector2.Zero, 1f);
			//}
		}

		//public override void MouseOver(UIMouseEvent evt)
		//{
		//	base.MouseOver(evt);
		//	Main.PlaySound(12, -1, -1, 1, 1f, 0f);
		//}
	}
}