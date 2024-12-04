namespace TalentHub.ApplicationCore.Resources.Applications.Enums;

public enum ApplicationStatus
{
    /// <summary>
    /// A aplicação foi submetida pelo candidato.
    /// </summary>
    Submitted = 0,

    /// <summary>
    /// A aplicação está sendo revisada pelo recrutador.
    /// </summary>
    UnderReview = 1,

    /// <summary>
    /// A aplicação foi aceita e o candidato passou para a próxima etapa.
    /// </summary>
    Accepted = 2,

    /// <summary>
    /// A aplicação foi rejeitada pelo recrutador.
    /// </summary>
    Rejected = 3,

    /// <summary>
    /// A aplicação foi retirada pelo próprio candidato.
    /// </summary>
    Withdrawn = 4,

    /// <summary>
    /// A aplicação está em espera, aguardando decisão.
    /// </summary>
    OnHold = 5,

    /// <summary>
    /// O processo de aplicação foi concluído.
    /// </summary>
    Completed = 6
}
