using SG.NightlyAudit.Domain;
using SG.NightlyAudit.DTO;
using System;

namespace SG.NightlyAudit.DTODomainConvertor
{
    public static class JobConvertor
    {
        public static JobDTO GetJobDTO(Job jobEntity)
        {
            JobDTO jobDTO = new JobDTO();
            jobDTO.AuditId = jobEntity.AuditId;
            jobDTO.JobId = jobEntity.JobId;
            jobDTO.ParametersChosen = jobEntity.ParametersChosen;

            ScheduleObject scheduleObject = new ScheduleObject();
            scheduleObject.JobId = jobEntity.JobId;
            SchedulingTypes schTypes = SchedulingTypes.Daily;
            Enum.TryParse<SchedulingTypes>(jobEntity.JobSchedule.ScheduleType.ToString(), out schTypes);
            scheduleObject.StartDateTime = jobEntity.StartDateTime;
            scheduleObject.ScheduleType = schTypes;
            scheduleObject.ReoccurEveryX = jobEntity.JobSchedule.ReoccurEveryX;
            scheduleObject.ReoccurEveryXMonths = jobEntity.JobSchedule.ReoccurEveryXMonths;
            scheduleObject.SelectedWeek = jobEntity.JobSchedule.SelectedWeek;
            scheduleObject.SelectedDates = jobEntity.JobSchedule.SelectedDates;
            jobDTO.ScheduleObject = scheduleObject;
            return jobDTO;
        }

        public static Job GetJobEntity(JobDTO jobDTO)
        {
            Job jobEntity = new Job();
            jobEntity.JobId = jobDTO.JobId;
            jobEntity.AuditId = jobDTO.AuditId;
            jobEntity.ParametersChosen = jobDTO.ParametersChosen;
            jobEntity.CronExpression = CronExpressionGenerator.GenerateCronExpression(jobDTO.ScheduleObject);
            jobEntity.StartDateTime = jobDTO.ScheduleObject.StartDateTime;
            jobEntity.IsEnabled = jobDTO.IsEnabled;
            JobSchedule jobSchedule = new JobSchedule();
            jobSchedule.JobId = jobDTO.JobId;
            jobSchedule.ScheduleType = (int)jobDTO.ScheduleObject.ScheduleType;
            jobSchedule.StartDateTime = jobDTO.ScheduleObject.StartDateTime;
            jobSchedule.ReoccurEveryX = jobDTO.ScheduleObject.ReoccurEveryX;
            jobSchedule.ReoccurEveryXMonths = jobDTO.ScheduleObject.ReoccurEveryXMonths;
            jobSchedule.SelectedWeek = jobDTO.ScheduleObject.SelectedWeek;
            jobSchedule.SelectedDates = jobDTO.ScheduleObject.SelectedDates;
            jobEntity.JobSchedule = jobSchedule;
            return jobEntity;

        }
    }
}
