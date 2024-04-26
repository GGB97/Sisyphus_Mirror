using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDB
{
    private Dictionary<int, ParticleData> _particles = new Dictionary<int, ParticleData>();

    public ParticleDB()
    {
        var res = Resources.Load<ParticleDB_Sheet>("Particle/Data/ParticleDB_Sheet");
        var particleSO = Object.Instantiate(res);
        var entities = particleSO.Entities;

        if (entities == null || entities.Count <= 0) return;

        var entityCount = entities.Count;

        for (int i = 0; i < entityCount; i++)
        {
            var particle = entities[i];

            if (_particles.ContainsKey(particle.Id)) _particles[particle.Id] = particle;
            else
            {
                _particles.Add(particle.Id, particle);
            }
        }
    }

    public ParticleData Get(int id)
    {
        if (_particles.ContainsKey(id)) return _particles[id];

        return null;
    }

    public IEnumerator ParticleEnumerator()
    {
        return _particles.GetEnumerator();
    }
}
