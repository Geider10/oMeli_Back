using oMeli_Back.Context;
using oMeli_Back.Entities;
using oMeli_Back.DTOs.Store;
using oMeli_Back.DTOs;
using Microsoft.EntityFrameworkCore;
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
            var storeSchedules = _context.Schedules.Where(s => s.StoreId == Guid.Parse(scheduleDto.StoreId)).ToList();
            var repiteDat = storeSchedules.Find(s => s.Day == scheduleDto.Day);
            if (repiteDat != null) throw new Exception("Schedule already exists for this day");

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

        public async Task<GeneralRes> UpdateSchedule(string scheduleId, UpdateScheduleDto scheduleDto)
        {
            var scheduleExists = await _context.Schedules.FirstOrDefaultAsync(s => s.Id == Guid.Parse(scheduleId));
            if (scheduleExists == null) throw new Exception("Schedule not found");
            var storeSchedules = _context.Schedules.Where(s => s.StoreId == scheduleExists.StoreId).ToList();

            var repiteDay = storeSchedules.Find(s => s.Day == scheduleDto.Day && s.Id != scheduleExists.Id);
            if (repiteDay != null) throw new Exception("Schedule already exists for this day");
            var repriteHours = scheduleExists.HourStart == scheduleDto.HourStart && scheduleExists.HourEnd == scheduleDto.HourEnd && scheduleExists.Day == scheduleDto.Day;
            if (repriteHours) throw new Exception("Schedule already exists for this hours");

            scheduleExists.Day = scheduleDto.Day;
            scheduleExists.HourStart = scheduleDto.HourStart;
            scheduleExists.HourEnd = scheduleDto.HourEnd;

            _context.Schedules.Update(scheduleExists);
            await _context.SaveChangesAsync();

            return new GeneralRes { Ok = true, Message = "Schedule updated" };

        }
        
        public async Task<GeneralRes> DeleteSchedule(string scheduleId)
        {
            var scheduleExists = await _context.Schedules.FirstOrDefaultAsync(s => s.Id == Guid.Parse(scheduleId));
            if (scheduleExists == null) throw new Exception("Schedule not found");

            _context.Schedules.Remove(scheduleExists);
            await _context.SaveChangesAsync();

            return new GeneralRes { Ok = true, Message = "Schedule deleted" };
        }

        public async Task<List<GetSchedulesByStoreIdDto>> GetSchedules (string storeId)
        {
            var schedules = await _context.Schedules
                .Where(s => s.StoreId == Guid.Parse(storeId))
                .Select(s => new GetSchedulesByStoreIdDto
                {
                    ScheduleId = s.Id.ToString(),
                    Day = s.Day,
                    HourStart = s.HourStart,
                    HourEnd = s.HourEnd
                }).ToListAsync();
            if (schedules == null) throw new Exception("Schedules not found");

            return schedules;
        }
    }
}
