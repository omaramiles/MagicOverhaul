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
            Projectile.width = 16;
			Projectile.height = 16;
			Projectile.penetrate = 3;
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
				Projectile.velocity *= 0.5f;
				SoundEngine.PlaySound(SoundID.Item21, Projectile.position);
			}
			return false;
		}

        public override void OnKill(int timeLeft) {
			while (Projectile.alpha < 255){
				Projectile.alpha -= 1;
			}
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
			Projectile.ai[0] += 0.1f;
			Projectile.velocity *= 0.75f;
		}
	}
}