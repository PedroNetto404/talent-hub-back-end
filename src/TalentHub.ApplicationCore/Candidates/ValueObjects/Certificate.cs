namespace TalentHub.ApplicationCore.Candidates.ValueObjects;

public record Certificate(
    string Name,
    string Institution,
    double Workload,
    string Url
);
