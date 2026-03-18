namespace AstroBookings.Rockets;

public sealed class InMemoryRocketRepository : IRocketRepository
{
    private readonly Dictionary<int, Rocket> rockets = new();
    private int nextId = 1;

    public IReadOnlyCollection<Rocket> GetAll() => rockets.Values.OrderBy(rocket => rocket.Id).ToArray();

    public Rocket Create(RocketDraft rocket)
    {
        var createdRocket = new Rocket(nextId++, rocket.Name, rocket.Range, rocket.Capacity);
        rockets[createdRocket.Id] = createdRocket;
        return createdRocket;
    }

    public bool TryGetById(int id, out Rocket rocket)
    {
        if (rockets.TryGetValue(id, out var existingRocket))
        {
            rocket = existingRocket;
            return true;
        }

        rocket = default!;
        return false;
    }

    public bool TryUpdate(int id, RocketDraft rocket, out Rocket updatedRocket)
    {
        if (!rockets.ContainsKey(id))
        {
            updatedRocket = default!;
            return false;
        }

        updatedRocket = new Rocket(id, rocket.Name, rocket.Range, rocket.Capacity);
        rockets[id] = updatedRocket;
        return true;
    }

    public bool Delete(int id) => rockets.Remove(id);
}
