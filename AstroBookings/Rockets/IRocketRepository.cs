namespace AstroBookings.Rockets;

public interface IRocketRepository
{
    IReadOnlyCollection<Rocket> GetAll();
    Rocket Create(RocketDraft rocket);
    bool TryGetById(int id, out Rocket rocket);
    bool TryUpdate(int id, RocketDraft rocket, out Rocket updatedRocket);
    bool Delete(int id);
}
