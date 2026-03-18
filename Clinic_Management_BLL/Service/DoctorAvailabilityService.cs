using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_BLL.Service
{
    using Clinic_Management_BLL.Data;
    using Clinic_Management_BLL.ResultWraper;
    using Clinic_Management_DAL.Data;
    using Clinic_Management_Entities.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class AppointmentSlot
    {
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }

        public override string ToString()
            => $"{StartAt:HH:mm} - {EndAt:HH:mm}";
    }



    public sealed class DoctorAvailabilityService
    {
        // DAL dependencies (use your static Data classes or inject services)
        // You can replace these calls with your own Service layer if you prefer.
        public Result<IEnumerable<AppointmentSlot>> GetDoctorAvailableSlots(int doctorId, DateTime date)
        {
            try
            {
                if (doctorId <= 0)
                    return Result<IEnumerable<AppointmentSlot>>.Fail("Invalid doctor id.");

                date = date.Date;

                // 1) Check override for this date
                var ov = DoctorDayOverrideData.GetByDoctorAndDate(doctorId, date);
                // implement DAL: returns DoctorDayOverride? (or null)

                IEnumerable<AppointmentSlot> baseSlots;

                if (ov != null && ov.IsOverride)
                {
                    // If day off => no slots
                    if (ov.IsDayOff)
                        return Result<IEnumerable<AppointmentSlot>>.Ok(Enumerable.Empty<AppointmentSlot>());

                    // 2) Build from override sessions
                    var sessions = DoctorDayOverrideSessionData.GetByOverrideId(ov.OverrideId);
                    baseSlots = BuildSlotsFromOverrideSessions(date, sessions);
                }
                else
                {
                    // 3) Build from weekly schedule template
                    var dayOfWeek = (byte)date.DayOfWeek; // 0=Sunday..6=Saturday
                    var schedules = DoctorScheduleData.GetByDoctorAndDay(doctorId, dayOfWeek);
                    baseSlots = BuildSlotsFromSchedules(date, schedules);
                }

                // 4) Remove taken slots by existing appointments (not deleted, not cancelled)
                var from = date;
                var to = date.AddDays(1);

                var booked = AppointmentData.GetByDoctorId(
                    doctorId,
                    from: from,
                    to: to,
                    includeDeleted: false);

                var available = RemoveBookedSlots(baseSlots, booked);

                return Result<IEnumerable<AppointmentSlot>>.Ok(available);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<AppointmentSlot>>.Fail(ex.Message);
            }
        }

        // =========================================================
        // Helpers: slot builders
        // =========================================================

        private static IEnumerable<AppointmentSlot> BuildSlotsFromOverrideSessions(
            DateTime date,
            IEnumerable<DoctorDayOverrideSession> sessions)
        {
            if (sessions == null) return Enumerable.Empty<AppointmentSlot>();

            var result = new List<AppointmentSlot>();

            foreach (var s in sessions)
            {
                if (s.SlotMinutes <= 0) continue;

                var start = date.Add(s.StartTime);
                var end = date.Add(s.EndTime);

                result.AddRange(SplitToSlots(start, end, s.SlotMinutes));
            }

            return result
                .OrderBy(x => x.StartAt)
                .ToList();
        }

        private static IEnumerable<AppointmentSlot> BuildSlotsFromSchedules(
            DateTime date,
            IEnumerable<DoctorSchedule> schedules)
        {
            if (schedules == null) return Enumerable.Empty<AppointmentSlot>();

            var result = new List<AppointmentSlot>();

            foreach (var sch in schedules)
            {
                if (!sch.IsActive) continue;
                if (sch.SlotMinutes <= 0) continue;

                var start = date.Add(sch.StartTime);
                var end = date.Add(sch.EndTime);

                result.AddRange(SplitToSlots(start, end, sch.SlotMinutes));
            }

            return result
                .OrderBy(x => x.StartAt)
                .ToList();
        }

        private static IEnumerable<AppointmentSlot> SplitToSlots(DateTime start, DateTime end, int slotMinutes)
        {
            if (slotMinutes <= 0) yield break;
            if (end <= start) yield break;

            var cur = start;

            while (cur.AddMinutes(slotMinutes) <= end)
            {
                yield return new AppointmentSlot
                {
                    StartAt = cur,
                    EndAt = cur.AddMinutes(slotMinutes)
                };
                cur = cur.AddMinutes(slotMinutes);
            }
        }

        // =========================================================
        // Helpers: remove booked
        // =========================================================
        private static IEnumerable<AppointmentSlot> RemoveBookedSlots(
            IEnumerable<AppointmentSlot> slots,
            IEnumerable<Appointment> bookedAppointments)
        {
            if (slots == null) return Enumerable.Empty<AppointmentSlot>();

            var booked = (bookedAppointments ?? Enumerable.Empty<Appointment>())
                .Where(a => !a.IsDeleted && a.Status != (byte)enAppointmentStatus.Cancelled)
                .Select(a => (Start: a.StartAt, End: a.EndAt))
                .ToList();

            // remove any slot that overlaps any booked interval
            return slots.Where(slot => !booked.Any(b => Overlaps(slot.StartAt, slot.EndAt, b.Start, b.End)))
                        .OrderBy(s => s.StartAt)
                        .ToList();
        }

        private static bool Overlaps(DateTime aStart, DateTime aEnd, DateTime bStart, DateTime bEnd)
        {
            // Overlap rule: aStart < bEnd && aEnd > bStart
            return aStart < bEnd && aEnd > bStart;
        }
    }

}
