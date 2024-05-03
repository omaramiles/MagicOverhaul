using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace MagicOverhaul.Content.Projectiles
{
	public class StoneBall : ModProjectile
	{

        public override void SetDefaults() {
            Projectile.CloneDefaults(ProjectileID.BoulderStaffOfEarth);
            Projectile.width = 10;
			Projectile.height = 10;
			Projectile.penetrate = 3;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.aiStyle = 1;
		}

        public override void AI()
        {
            if (Main.rand.NextBool(10))
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Stone, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity) {
			Projectile.penetrate--;
			if (Projectile.penetrate <= 0) {
				Projectile.Kill();
			}
			else {
				Projectile.ai[0] += 0.1f;
				if (Projectile.velocity.X != oldVelocity.X) {
					Projectile.velocity.X = -oldVelocity.X;
				}
				if (Projectile.velocity.Y != oldVelocity.Y) {
					Projectile.velocity.Y = -oldVelocity.Y;
                }
                Projectile.velocity.X *= 0.15f;
                Projectile.velocity.Y *= 0.3f;
                SoundEngine.PlaySound(SoundID.Item21, Projectile.position);
			}
			return false;
		}

        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Stone, Projectile.oldVelocity.X * 0.1f, Projectile.oldVelocity.Y * 0.1f);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
			Projectile.ai[0] += 0.1f;
			Projectile.velocity *= 0.75f;
		}
	}
}