using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tamagotchi.Models;

namespace Tamagotchi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PetController : ControllerBase
  {
    public DatabaseContext db { get; set; } = new DatabaseContext();

    [HttpGet("pets")]
    public List<Pet> GetAllPets()
    {
      var getAllPets = db.Pets.OrderBy(m => m.Name);
      return getAllPets.ToList();
    }

    [HttpGet("{id}")]
    public Pet GetOnePet(int id)
    {
      var onePet = db.Pets.FirstOrDefault(i => i.Id == id);
      return onePet;
    }

    [HttpPost]
    public Pet CreateNewPet(Pet item)
    {
      db.Pets.Add(item);
      db.SaveChanges();
      return item;
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteOne(int id)
    {
      var item = db.Pets.FirstOrDefault(f => f.Id == id);
      if (item == null)
      {
        return NotFound();
      }
      db.Pets.Remove(item);
      db.SaveChanges();
      return Ok();
    }

    [HttpPut("{id}/play")]
    public Pet UpdatePlay(int id, Pet data)
    {
      var item = db.Pets.FirstOrDefault(i => i.Id == id);
      item.HappinessLevel += 5;
      item.HungerLevel += 3;
      db.SaveChanges();
      return item;
    }

    [HttpPut("{id}/feed")]
    public Pet UpdateFeed(int id, Pet data)
    {
      var item = db.Pets.FirstOrDefault(i => i.Id == id);
      item.HappinessLevel += 3;
      item.HungerLevel -= 5;
      db.SaveChanges();
      return item;
    }

    [HttpPut("{id}/scold")]
    public Pet UpdateScold(int id, Pet data)
    {
      var item = db.Pets.FirstOrDefault(i => i.Id == id);
      item.HappinessLevel -= 5;
      db.SaveChanges();
      return item;
    }

  }
}