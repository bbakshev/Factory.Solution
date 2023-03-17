using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Factory.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Factory.Controllers
{
  public class EngineersController : Controller
  {
    private readonly FactoryContext _db;

    public EngineersController(FactoryContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Engineer> model = _db.Engineers.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Engineer eng)
    {
      if (!ModelState.IsValid)
      {
        return View(eng);
      }
      _db.Engineers.Add(eng);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Edit(int id)
    {
      Engineer thisEngineer = _db.Engineers.FirstOrDefault(eng => eng.EngineerId == id);
      return View(thisEngineer);
    }

    [HttpPost]
    public ActionResult Edit(Engineer eng)
    {
      _db.Engineers.Update(eng);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Engineer thisEngineer = _db.Engineers
                                .Include(eng => eng.JoinEntities)
                                .ThenInclude(join => join.Machine)
                                .FirstOrDefault(eng => eng.EngineerId == id);
      return View(thisEngineer);
    }

    public ActionResult AddMachine(int id)
    {
      Engineer thisEngineer = _db.Engineers.FirstOrDefault(Engineers => Engineers.EngineerId == id);
      ViewBag.MachineId = new SelectList(_db.Machines, "MachineId", "Name");
      return View(thisEngineer);
    }

    [HttpPost]
    public ActionResult AddMachine(Engineer eng, int machineId)
    {
#nullable enable
      EngineerMachine? joinEntity = _db.EngineerMachine.FirstOrDefault(join => (join.MachineId == machineId && join.EngineerId == eng.EngineerId));
#nullable disable
      if (joinEntity == null && machineId != 0)
      {
        _db.EngineerMachine.Add(new EngineerMachine() { MachineId = machineId, EngineerId = eng.EngineerId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = eng.EngineerId });
    }

    public ActionResult Delete(int id)
    {
      Engineer thisEngineer = _db.Engineers.FirstOrDefault(Engineers => Engineers.EngineerId == id);
      return View(thisEngineer);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Engineer thisEngineer = _db.Engineers.FirstOrDefault(Engineers => Engineers.EngineerId == id);
      _db.Engineers.Remove(thisEngineer);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}