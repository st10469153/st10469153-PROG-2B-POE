using System.ComponentModel.DataAnnotations;
using RaceDayApplicationApi.Data;
using RaceDayApplicationApi.Models;
using RaceDayApplicationApi.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RaceDayApplicationApi.Controllers;




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
        var participants = _dbcontext.Participants;
        if (participants is null) { return NotFound(); }
        return Ok(participants);
    }
    [HttpGet("ParticipantId")]
    public IActionResult GetParticipant(int ParticipantId)
    {
        var participant = _dbcontext.Participants.Find(ParticipantId);
        if (participant is null) { return NotFound(); }
        return Ok(participant);

    }
    [HttpPost]
    public IActionResult AddParticipant(AddParticipantDto addParticipantDto)
    {
        var participantEntity = new Participant()
        {
            Name = addParticipantDto.Name,
            Email = addParticipantDto.Email,
        };
        _dbcontext.Participants.Add(participantEntity);
        _dbcontext.SaveChanges();

        return Ok(participantEntity);
    }
    [HttpPut("ParticipantId")]
    public IActionResult UpdateParticipant(int ParticipantId, UpdateParticipantDto updateParticipantDto)
    {
        var participant = _dbcontext.Participants.Find(ParticipantId);
        if (participant is null) { return NotFound(); }
        participant.Name = updateParticipantDto.Name;
        participant.Email = updateParticipantDto.Email;
        _dbcontext.SaveChanges();
        return Ok(participant);
    }
    [HttpDelete("ParticipantId")]
    public IActionResult DeleteParticipant(int ParticipantId)
    {
        var participant = _dbcontext.Participants.Find(ParticipantId);
        if (participant is null) { return NotFound(); }
        _dbcontext.Participants.Remove(participant);
        return Ok(participant);
    }
}
