using DG.Tweening;
using trailmarch.actors;
using trailmarch.consts;
using trailmarch.vo;
using UnityEngine;

namespace trailmarch.view.behaviours
{
	public class MobBehavior : MonoBehaviour
	{
		//public List<AudioClip> Footstep { get; set; }
		public MobActor Mob { get; set; }

		public MobBehavior()
		{
			//this.gameObject.AddComponent<AudioSource>();
			//Footstep = new List<AudioClip>();
		}

		public void Spawn(float duration)
		{
            gameObject.transform.DOScale(gameObject.transform.localScale, duration)
                .SetEase(Ease.Linear)
                .OnComplete(OnSpawnComplete)
                .PlayForward();
		}

		public void Walk(Vector3 walkTo, float duration)
		{
			PlayAnimation(MobAnimationType.Walk);

            gameObject.transform.DOMove(Mob.Offset + walkTo, duration)
                .SetEase(Ease.InOutQuad)
                .OnComplete(OnWalkComplete)
                .PlayForward();
		}

		public void Turn(Vector3 rotTo, float duration)
		{
			PlayAnimation(MobAnimationType.Walk);

            gameObject.transform.DORotate(Mob.Offset + rotTo, duration)
                .SetEase(Ease.InOutQuad)
                .OnComplete(OnWalkComplete)
                .PlayForward();
		}

		private void OnWalkComplete()
		{
			PlayAnimation(MobAnimationType.Idle);
		}

		private void OnSpawnComplete()
		{
			PlayAnimationInstant(MobAnimationType.Idle);
		}

		public void PlayAnimation(MobAnimationType animType, float fadeTime = 0.3f)
		{
			MobAnimation anim = Mob.Definition.GetRandomAnimationByType(animType);
			GetComponent<Animation>()[anim.Name].wrapMode = anim.Wrap;

			GetComponent<Animation>().CrossFade(anim.Name, fadeTime);
		}

		public void PlayAnimationInstant(MobAnimationType animType)
		{
			MobAnimation anim = Mob.Definition.GetRandomAnimationByType(animType);
			GetComponent<Animation>()[anim.Name].wrapMode = anim.Wrap;

			GetComponent<Animation>().Play(anim.Name);
		}

		public void QueueAnimation(MobAnimationType animType, float fadeTime = 0.3f)
		{
			MobAnimation anim = Mob.Definition.GetRandomAnimationByType(animType);
			GetComponent<Animation>()[anim.Name].wrapMode = anim.Wrap;

			GetComponent<Animation>().CrossFadeQueued(anim.Name, fadeTime, QueueMode.CompleteOthers);
		}

		public void QueueAnimationInstant(MobAnimationType animType)
		{
			MobAnimation anim = Mob.Definition.GetRandomAnimationByType(animType);
			GetComponent<Animation>()[anim.Name].wrapMode = anim.Wrap;

			GetComponent<Animation>().PlayQueued(anim.Name, QueueMode.CompleteOthers);
		}

		public void playFootstepSound()
		{
			//AudioSource.PlayClipAtPoint(Footstep[Random.Range(0, Footstep.Count)], Mob.Position);
			//AudioSrc.clip = Footstep[Random.Range(0, Footstep.Count)];
			//AudioSrc.Play();
		}
	}
}
