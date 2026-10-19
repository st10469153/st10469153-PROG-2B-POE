using System.ComponentModel.DataAnnotations;
using RaceDayApplicationApi.Data;
using RaceDayApplicationApi.Models;
using RaceDayApplicationApi.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseRegistrationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantController : ControllerBase
    {
        private readonly AppDbContext _dbcontext;
        public ParticipantController(AppDbContext dbContext)
        {
            this._dbcontext = dbContext;
        }
        [HttpGet]
        public IActionResult GetAllParticipants()
        {
            var students = _dbcontext.Participants;
            if (students is null) { return NotFound(); }
            return Ok(students);
        }
        [HttpGet("ParticipantId")]
        public IActionResult GetParticipant(int ParticipantId)
        {
            var student = _dbcontext.Participants.Find(ParticipantId);
            if (student is null) { return NotFound(); }
            return Ok(student);

        }
        [HttpPost]
        public IActionResult AddParticipant(AddParticipantDto addParticipantDto)
        {
            var studentEntity = new Participant()
            {
                Name = addParticipantDto.Name,
                Email = addParticipantDto.Email,
            };
            _dbcontext.Participants.Add(studentEntity);
            _dbcontext.SaveChanges();

            return Ok(studentEntity);
        }
        [HttpPut("ParticipantId")]
        public IActionResult UpdateParticipant(int ParticipantId, UpdateParticipantDto updateParticipantDto)
        {
            var student = _dbcontext.Participants.Find(ParticipantId);
            if (student is null) { return NotFound(); }
            student.Name = updateParticipantDto.Name;
            student.Email = updateParticipantDto.Email;
            _dbcontext.SaveChanges();
            return Ok(student);
        }
        [HttpDelete("ParticipantId")]
        public IActionResult DeleteParticipant(int ParticipantId)
        {
            var student = _dbcontext.Participants.Find(ParticipantId);
            if (student is null) { return NotFound(); }
            _dbcontext.Participants.Remove(student);
            return Ok(student);
        }
    }
}
