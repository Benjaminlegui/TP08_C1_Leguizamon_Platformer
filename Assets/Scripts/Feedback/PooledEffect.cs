using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class PooledEffect : Spawn
{
    private ParticleSystem particles;

    private void Awake()
    {
        particles = GetComponent<ParticleSystem>();

        ParticleSystem.MainModule main = particles.main;
        main.playOnAwake = false;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    public override void OnGet()
    {
        particles.Play(true);
    }

    public override void OnRelease()
    {
        particles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    private void OnParticleSystemStopped()
    {
        Despawn();
    }
}
