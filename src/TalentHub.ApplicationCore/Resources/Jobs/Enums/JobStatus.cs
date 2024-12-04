namespace TalentHub.ApplicationCore.Resources.Jobs.Enums;

public enum JobStatus
{
    /// <summary>
    /// A vaga está aberta e disponível para candidaturas.
    /// </summary>
    Open = 0,

    /// <summary>
    /// A vaga foi preenchida e não está mais disponível.
    /// </summary>
    Closed = 1,

    /// <summary>
    /// A vaga foi suspensa temporariamente.
    /// </summary>
    Suspended = 2,

    /// <summary>
    /// A vaga está em revisão antes de ser publicada.
    /// </summary>
    UnderReview = 3,

    /// <summary>
    /// A vaga foi cancelada e não será preenchida.
    /// </summary>
    Cancelled = 4
}
