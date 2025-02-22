namespace InterviewManagementSystem.Application.DTOs.JobDTOs;

public record JobForRetrieveDTO : BaseJobDTO
{
    public Guid Id { get; set; }
    public bool IsDeleted { get; set; }
    public string[] Levels { get; set; } = [];
    public JobStatus? JobStatus { get; set; } = new();
    public string[] RequiredSkills { get; set; } = [];
}



public sealed record JobForPaginationRetrieveDTO : JobForRetrieveDTO { }


public sealed record JobOpenForRetrieveDTO
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
}


public sealed record JobForDetailRetrieveDTO : JobForRetrieveDTO
{
    public string[] Benefits { get; set; } = [];
    public AuditInformation? AuditInformation { get; set; } = new();
}
