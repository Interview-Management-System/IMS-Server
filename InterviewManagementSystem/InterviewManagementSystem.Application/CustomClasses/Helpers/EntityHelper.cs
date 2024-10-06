using InterviewManagementSystem.Domain.Entities.AppUsers;
using InterviewManagementSystem.Domain.Entities.Interviews;
using InterviewManagementSystem.Domain.Entities.Jobs;
using InterviewManagementSystem.Domain.Entities.Offers;
using Microsoft.EntityFrameworkCore;

namespace InterviewManagementSystem.Application.CustomClasses.Helpers;

public static class EntityHelper
{

    internal static readonly Dictionary<Type, List<string>> EntitySearchFieldMappings = new()
    {
        { typeof(Offer), new List<string> { "Candidate", "Approver", "CreatedBy" } }
        // Add more entities and their fields as necessary
    };



    internal static bool IsNavigationProperty<T>(DbContext context, string propertyName)
    {
        // Get the entity type for the specified type
        var entityType = context.Model.FindEntityType(typeof(T));

        if (entityType == null)
        {
            throw new ArgumentException($"Entity type {typeof(T).Name} is not found in the DbContext.");
        }

        // Check if the specified property is a navigation property
        var navigationProperty = entityType.GetNavigations().FirstOrDefault(n => n.Name == propertyName);

        return navigationProperty != null;
    }



    /// <summary>
    /// For searching by name
    /// </summary>
    public static class EntityPropertyMapping
    {

        public static Dictionary<string, string?> BuildOfferSearchFieldMapping(string? nameToSearch)
        {
            return new()
            {
                { $"{nameof(Candidate)}.{nameof(Candidate.UserName)}", nameToSearch },
            };
        }


        public static Dictionary<string, string?> BuildAppUserSearchFieldMapping(string? nameToSearch)
        {
            return new()
            {
                { $"{nameof(AppUser.UserName)}", nameToSearch },
            };
        }


        public static Dictionary<string, string?> BuildJobSearchFieldMapping(string? nameToSearch)
        {
            return new()
            {
                { $"{nameof(Job.Title)}", nameToSearch },
            };
        }



        public static Dictionary<string, string?> BuildInterviewSearchFieldMapping(string? nameToSearch)
        {
            return new()
            {
                { $"{nameof(InterviewSchedule.Title)}", nameToSearch },
            };
        }
    }




    /// <summary>
    /// For filtering by enum id
    /// </summary>
    public static class EntityEnumMapping
    {

        public static Dictionary<string, Enum> BuildOfferEnumFilter(OfferStatusEnum? offerStatusId, DepartmentEnum? departmentId)
        {
            return new Dictionary<string, Enum>
            {
                { nameof(Offer.OfferStatusId), offerStatusId ?? default },
                { nameof(Offer.DepartmentId), departmentId ?? default }
            };
        }



        public static Dictionary<string, Enum> BuildUserEnumFilter(RoleEnum? roleId)
        {
            return new Dictionary<string, Enum>
            {
                { nameof(AppUser.Roles), roleId ?? default },
            };
        }



        public static Dictionary<string, Enum> BuildJobEnumFilter(JobStatusEnum? jobStatusId)
        {
            return new Dictionary<string, Enum>
            {
                { nameof(Job.JobStatusId), jobStatusId ?? default }
            };

        }


        public static Dictionary<string, Enum> BuildInterviewEnumFilter(InterviewStatusEnum? interviewStatusId)
        {
            return new Dictionary<string, Enum>
            {
                { nameof(InterviewSchedule.InterviewScheduleStatusId), interviewStatusId ?? default }
            };

        }
    }







}
