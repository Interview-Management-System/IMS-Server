namespace InterviewManagementSystem.Application.DTOs.JobDTOs;

public record JobRetrieveDTO : BaseJobDTO
{
    public Guid Id { get; set; }
    public bool IsDeleted { get; set; }
    public string[] Levels { get; set; } = [];
    public JobStatus? JobStatus { get; set; } = new();
    public string[] RequiredSkills { get; set; } = [];
}



public sealed record JobPaginationRetrieveDTO : JobRetrieveDTO { }


public sealed record JobOpenRetrieveDTO
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
}


public sealed record JobDetailRetrieveDTO : JobRetrieveDTO
{
    public string[] Benefits { get; set; } = [];
    public AuditInformation? AuditInformation { get; set; } = new();
}
