using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ParticleSystem))]
public class particleAttractorLinear : MonoBehaviour {

    public List<Transform> targets;
    public float speed = 5f;

    ParticleSystem ps;
    ParticleSystemRenderer psr;
    ParticleSystem.Particle[] m_Particles;
    List<Vector4> particleTargets = new List<Vector4>();
    int numParticlesAlive;
    int nextTarget = 0;

    static List<ParticleSystemVertexStream> psvsList = new List<ParticleSystemVertexStream>(new ParticleSystemVertexStream[] {ParticleSystemVertexStream.Custom1X});

	void Start () {
		ps = GetComponent<ParticleSystem>();
        psr = GetComponent<ParticleSystemRenderer>();

        psr.SetActiveVertexStreams(psvsList);
	}
	void Update () {
        ps.GetCustomParticleData(particleTargets, ParticleSystemCustomData.Custom1);

		m_Particles = new ParticleSystem.Particle[ps.main.maxParticles];
		numParticlesAlive = ps.GetParticles(m_Particles);
		float step = speed * Time.deltaTime;
		for (int i = 0; i < numParticlesAlive; i++) {
            Vector4 v4 = particleTargets[i];
            if (v4.x == 0 || v4.x > targets.Count) {
                if (targets.Count == 0) {
                    v4.x = -1;
                }
                else {
                    v4.x = (++nextTarget % targets.Count) + 1;
                    particleTargets[i] = v4;
                }
            }
            m_Particles[i].position = Vector3.SlerpUnclamped(m_Particles[i].position, targets[(int)v4.x-1].position, step);
		}
		ps.SetParticles(m_Particles, numParticlesAlive);

        ps.SetCustomParticleData(particleTargets, ParticleSystemCustomData.Custom1);
	}
}
