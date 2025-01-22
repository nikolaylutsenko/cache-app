namespace Medicine.Database.Enteties;

using System.ComponentModel;

public class MedicineSpecification : IDatabaseEntity
{
    public required Guid Id { get; set; }
    public DosageForms? Form { get; set; }
    public string? Dosage { get; set; }
    public required Guid MedicineId { get; set; }

    // navigation props
    public required Medicine Medicine { get; set; }
}

public enum DosageForms
{
    [Description("Tablets")]
    Tablet = 100,

    [Description("Capsules")]
    Capsule = 200,

    [Description("Syrups")]
    Syrup = 300,

    [Description("Injections")]
    Injection = 400,

    [Description("Creams")]
    Cream = 500,

    [Description("Ointments")]
    Ointment = 600,

    [Description("Powders")]
    Powder = 700,

    [Description("Suppositories")]
    Suppository = 800,

    [Description("Drops")]
    Drop = 900,

    [Description("Patches")]
    Patch = 1000,
}
