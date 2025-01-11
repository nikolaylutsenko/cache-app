namespace Medicine.Database.Enteties;

public class Ingredient
{
    public required Guid Id { get; set; }
    public required Guid MedicineId { get; set; }
    public required Guid SubstanceId { get; set; }
    public required decimal Quantity { get; set; }
    public required bool IsActive { get; set; }

    // navigation props
    public required Medicine Medicine { get; set; }
    public required Substance Substance { get; set; }
}
