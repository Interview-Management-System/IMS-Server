﻿using InterviewManagementSystem.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using NpgsqlTypes;

namespace InterviewManagementSystem.Domain.Entities.AppUsers;

public partial class AppUser : IdentityUser<Guid>, ISearchable
{
    public DateTime? Dob { get; set; }

    public bool Gender { get; set; }

    public DateTime? CreateAt { get; set; } = DateTime.Now;

    public DateTime? UpdateAt { get; set; }

    public string? Address { get; set; }

    public string? Note { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public DepartmentEnum? DepartmentId { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }

    public string? AvatarLink { get; set; }

    public NpgsqlTsVector SearchVector { get; set; }

    // k
    public virtual ICollection<AppUserClaim> AppUserClaims { get; set; } = new List<AppUserClaim>();

    public virtual ICollection<AppUserLogin> AppUserLogins { get; set; } = new List<AppUserLogin>();

    public virtual ICollection<AppUserToken> AppUserTokens { get; set; } = new List<AppUserToken>();




    public virtual Candidate? CandidateIdNavigation { get; set; }

    public virtual ICollection<Candidate> CandidateRecruiters { get; set; } = new List<Candidate>();

    public virtual AppUser? CreatedByNavigation { get; set; }

    public virtual Department? Department { get; set; }



    // k
    public virtual ICollection<InterviewSchedule> InterviewScheduleCandidates { get; set; } = new List<InterviewSchedule>();

    public virtual ICollection<InterviewSchedule> InterviewScheduleCreatedByNavigations { get; set; } = new List<InterviewSchedule>();

    public virtual ICollection<InterviewSchedule> InterviewScheduleRecruiterOwners { get; set; } = new List<InterviewSchedule>();

    public virtual ICollection<InterviewSchedule> InterviewScheduleUpdatedByNavigations { get; set; } = new List<InterviewSchedule>();
    public virtual ICollection<InterviewSchedule> InterviewSchedules { get; set; } = new List<InterviewSchedule>();




    public virtual ICollection<AppUser> InverseCreatedByNavigation { get; set; } = new List<AppUser>();

    public virtual ICollection<AppUser> InverseUpdatedByNavigation { get; set; } = new List<AppUser>();




    // k
    //public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();

    public virtual ICollection<Job> JobCreatedByNavigations { get; set; } = new List<Job>();

    public virtual ICollection<Job> JobUpdatedByNavigations { get; set; } = new List<Job>();


    //
    public virtual ICollection<Offer> OfferApprovers { get; set; } = new List<Offer>();

    public virtual ICollection<Offer> OfferCreatedByNavigations { get; set; } = new List<Offer>();

    public virtual ICollection<Offer> OfferRecruiterOwners { get; set; } = new List<Offer>();

    public virtual ICollection<Offer> OfferUpdatedByNavigations { get; set; } = new List<Offer>();




    public virtual AppUser? UpdatedByNavigation { get; set; }


    public virtual ICollection<AppRole> Roles { get; set; } = new List<AppRole>();



    public AppUser()
    {
        GenerateId();
    }
}




public partial class AppUser
{

    public void GenerateId()
    {
        Id = Guid.NewGuid();
    }


    public void Activate()
    {
        IsActive = true;
    }


    public void DeActivate()
    {
        IsActive = false;
    }




    public void Delete()
    {
        IsDeleted = true;
    }


    public void UnDoDelete()
    {
        IsDeleted = true;
    }



    public void AddInterviewSchedule(InterviewSchedule interviewSchedule)
    {
        InterviewSchedules.Add(interviewSchedule);
    }


    public void RemoveInterviewSchedule(InterviewSchedule interviewSchedule)
    {
        InterviewSchedules.Remove(interviewSchedule);
    }
}