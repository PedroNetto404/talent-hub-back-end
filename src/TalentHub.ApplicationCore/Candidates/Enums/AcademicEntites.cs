using Ardalis.SmartEnum;

namespace TalentHub.ApplicationCore.Candidates.Enums;

public sealed class AcademicEntity : SmartEnum<AcademicEntity>
{
    public static readonly AcademicEntity AcademicCenter = new(1, "Centro/Diretorio Acadêmico");
    public static readonly AcademicEntity Enactus = new(2, "ENACTUS");
    public static readonly AcademicEntity JuniorEnterprise = new(3, "Empresa Junior");
    public static readonly AcademicEntity Athletics = new(4, "Atlética");
    public static readonly AcademicEntity AcademicLeague = new(5, "Liga Acadêmica");
    public static readonly AcademicEntity ScientificInitiation = new(6, "Iniciação Científica");
    public static readonly AcademicEntity Mentorship = new(7, "Monitoria");
    public static readonly AcademicEntity AIESEC = new(8, "AIESEC");
    public static readonly AcademicEntity Exchange = new(9, "Intercâmbio");
    public static readonly AcademicEntity DrummingBand = new(10, "Bateria");
    public static readonly AcademicEntity CollegeTV = new(11, "TV da Faculdade");
    public static readonly AcademicEntity CreaJunior = new(44, "Crea Junior");
    public static readonly AcademicEntity Arenas = new(45, "ARENAS");
    public static readonly AcademicEntity Brasa = new(46, "BRASA");

    private AcademicEntity(int value, string name) : base(name, value)
    {
    }
}