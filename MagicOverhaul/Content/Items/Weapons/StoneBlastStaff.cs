using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;
using MagicOverhaul.Content.Projectiles;

namespace MagicOverhaul.Content.Items.Weapons
{
	public class StoneBlastStaff : ModItem
	{
        // The Display Name and Tooltip of this item can be edited in the Localization/en-US_Mods.Tutorial.hjson file.

		public override void SetDefaults()
		{
            Item.staff[Type] = true;
            Item.width = 34;
            Item.height = 40;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item69;

            Item.noMelee = true;
            Item.mana = 8;
            Item.damage = 8;
            Item.knockBack = 3.2f;


            Item.useTime = 50;
            Item.useAnimation = 45;

            Item.shoot = ModContent.ProjectileType<StoneBall>();
            Item.shootSpeed = 5f;
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
			recipe.AddIngredient(ItemID.StoneBlock, 50);
            recipe.AddIngredient(ItemID.FallenStar, 3);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}