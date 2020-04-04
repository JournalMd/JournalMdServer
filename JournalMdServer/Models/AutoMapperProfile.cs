using AutoMapper;
using JournalMdServer.DTOs.Users;
using JournalMdServer.DTOs.Activities;
using JournalMdServer.DTOs.BodyMeasurements;
using JournalMdServer.DTOs.Goals;
using JournalMdServer.DTOs.Habits;
using JournalMdServer.DTOs.Journals;
using JournalMdServer.DTOs.Notes;
using JournalMdServer.DTOs.Routines;
using JournalMdServer.DTOs.Tasks;
using JournalMdServer.DTOs.WeightMeasurements;

namespace JournalMdServer.Models
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Users
            CreateMap<RegisterInput, User>();
            CreateMap<User, RegisterOutput>();
            CreateMap<User, UserOutput>();

            // Activities
            CreateMap<Activity, ActivityInput>();
            CreateMap<ActivityOutput, Activity>();

            // BodyMeasurement
            CreateMap<BodyMeasurement, BodyMeasurementOutput>();
            CreateMap<BodyMeasurementInput, BodyMeasurement>();

            // Goal
            CreateMap<Goal, GoalOutput>();
            CreateMap<GoalInput, Goal>();

            // Habit
            CreateMap<Habit, HabitOutput>();
            CreateMap<HabitInput, Habit>();

            // Journal
            CreateMap<Journal, JournalOutput>();
            CreateMap<JournalInput, Journal>();

            // Notes
            CreateMap<Note, NoteOutput>();
            CreateMap<NoteInput, Note>();

            // Routine
            CreateMap<Routine, RoutineOutput>();
            CreateMap<RoutineInput, Routine>();

            // Task
            CreateMap<Task, TaskOutput>();
            CreateMap<TaskInput, Task>();

            // WeightMeasurements
            CreateMap<WeightMeasurement, WeightMeasurementOutput>();
            CreateMap<WeightMeasurementInput, WeightMeasurement>();
        }
    }
}
