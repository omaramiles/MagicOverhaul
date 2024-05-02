using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;
using MagicOverhaul.Content.Projectiles;

namespace MagicOverhaul.Content.Items.Weapons
{
	public class MOStoneStaff : ModItem
	{
        // The Display Name and Tooltip of this item can be edited in the Localization/en-US_Mods.Tutorial.hjson file.

		public override void SetDefaults()
		{
            Item.width = 34;
            Item.height = 40;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item71;

            Item.noMelee = true;
            Item.mana = 8;
            Item.damage = 8;
            Item.knockBack = 3.2f;


            Item.useTime = 35;
            Item.useAnimation = 30;

            Item.shoot = ModContent.ProjectileType<StoneBall>();
            Item.shootSpeed = 8f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            const int NumProjectiles = 8; // The number of projectiles that this gun will shoot.

            for (int i = 0; i < NumProjectiles; i++)
            {
                // Rotate the velocity randomly by 30 degrees at max.
                Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(15));

                // Decrease velocity randomly for nicer visuals.
                newVelocity *= 1f - Main.rand.NextFloat(0.3f);

                // Create a projectile.
                Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
            }

            return false; // Return false because we don't want tModLoader to shoot projectile
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DirtBlock, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}