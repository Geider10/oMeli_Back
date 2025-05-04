using oMeli_Back.Context;
using oMeli_Back.Entities;
using oMeli_Back.DTOs.Store;
using oMeli_Back.DTOs;
namespace oMeli_Back.Services.Store
{
    public class ScheduleService
    {
        private AppDBContext _context;
        public ScheduleService(AppDBContext context)
        {
            _context = context;
        }

        public async Task<GeneralRes> CreateSchedule(CreateScheduleDto scheduleDto)
        {
            var schedule = new ScheduleEntity
            {
                StoreId = Guid.Parse(scheduleDto.StoreId),
                Day = scheduleDto.Day,
                HourStart = scheduleDto.HourStart,
                HourEnd = scheduleDto.HourEnd,
            };

            await _context.Schedules.AddAsync(schedule);
            await _context.SaveChangesAsync();

            return new GeneralRes { Ok = true, Message = "Schedule created" };
        }
    }
}
