namespace AstroBookings.Rockets;

public static class RocketValidation
{
    public static bool TryValidate(RocketRequest request, out RocketDraft rocket, out Dictionary<string, string[]> errors)
    {
        errors = new Dictionary<string, string[]>();

        var name = request.Name?.Trim() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(name))
        {
            errors[nameof(request.Name)] = ["Name is required."];
        }

        var rangeValue = request.Range?.Trim() ?? string.Empty;
        if (!RocketMapping.TryParseRange(rangeValue, out var range))
        {
            errors[nameof(request.Range)] = ["Range must be one of: suborbital, orbital, moon, mars."];
        }

        if (request.Capacity is < 1 or > 10)
        {
            errors[nameof(request.Capacity)] = ["Capacity must be between 1 and 10."];
        }

        if (errors.Count > 0)
        {
            rocket = default!;
            return false;
        }

        rocket = new RocketDraft(name, range, request.Capacity);
        return true;
    }
}
