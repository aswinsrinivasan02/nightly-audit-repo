using SG.NightlyAudit.DTO;
using System;
using System.Text;

namespace ScheduleEngine.Utilities
{
    public static class CronExpressionGenerator
    {
        public static string GenerateCronExpression(ScheduleObject scheduleObject)
        {
            switch (scheduleObject.ScheduleType)
            {
                case SchedulingTypes.OneTime:
                    return GenerateOneTimeExpression(scheduleObject);
                case SchedulingTypes.Daily:
                    return GenerateDailyExpression(scheduleObject);
                case SchedulingTypes.Weekly:
                    return GenerateWeeklyExpression(scheduleObject);
                case SchedulingTypes.Monthly:
                    return GenerateMonthlyExpression(scheduleObject);
                default:
                    throw new Exception("Invalid ScheduleType");
            }
        }

        private static string GenerateDailyExpression(ScheduleObject scheduleObject)
        {
            StringBuilder expression = new StringBuilder();
            expression.Append("0 ");
            expression.Append(scheduleObject.StartDateTime.Minute);
            expression.Append(" ");
            expression.Append(scheduleObject.StartDateTime.Hour);
            expression.Append(" 1/");
            expression.Append(scheduleObject.ReoccurEveryX);
            expression.Append(" * ? *");
            return expression.ToString();
        }

        private static string GenerateWeeklyExpression(ScheduleObject scheduleObject)
        {
            StringBuilder expression = new StringBuilder();
            expression.Append("0 ");
            expression.Append(scheduleObject.StartDateTime.Minute);
            expression.Append(" ");
            expression.Append(scheduleObject.StartDateTime.Hour);
            expression.Append(" ? * ");
            expression.Append("MON,TUE,WED");
            expression.Append(" *");
            return expression.ToString();
        }

        private static string GenerateMonthlyExpression(ScheduleObject scheduleObject)
        {
            return string.Empty;
        }

        private static string GenerateOneTimeExpression(ScheduleObject scheduleObject)
        {
            return string.Empty;
        }
    }
}
