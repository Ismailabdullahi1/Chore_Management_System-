using Lab5.Models;
using Microsoft.AspNetCore.Mvc;

public class ChoreController : Controller
{
    private static List<Chore> chores = new List<Chore>
    {
        new Chore { Id = 1, Title = "Complete lab assignment", Description = "Finish the lab assignment for Web Core MVC", DueDate = DateTime.Now.AddDays(7), Status = "Ongoing" },
        new Chore { Id = 2, Title = "Study for exam", Description = "Prepare for upcoming exam on MVC concepts", DueDate = DateTime.Now.AddDays(14), Status = "Finished" }
    };

    // Display the list of chores
    public IActionResult List()
    {
        return View(chores);
    }

    // Show the create form
    public IActionResult Create()
    {
        return View();
    }

    // Handle the create form submission
    [HttpPost]
    public IActionResult Create(Chore chore)
    {
        if (ModelState.IsValid)
        {
            chore.Id = chores.Max(c => c.Id) + 1; // Generate a new Id
            chores.Add(chore);
            return RedirectToAction("List");
        }
        return View(chore);
    }

    // Show the update form
    public IActionResult Update(int id)
    {
        var chore = chores.FirstOrDefault(c => c.Id == id);
        if (chore == null)
        {
            return NotFound();
        }
        return View(chore);
    }

    // Handle the update form submission
    [HttpPost]
    public IActionResult Update(int id, Chore chore)
    {
        var existingChore = chores.FirstOrDefault(c => c.Id == id);
        if (existingChore == null)
        {
            return NotFound();
        }

        existingChore.Title = chore.Title;
        existingChore.Description = chore.Description;
        existingChore.DueDate = chore.DueDate;
        existingChore.Status = chore.Status;

        return RedirectToAction("List");
    }

    // Delete a chore
    public IActionResult Delete(int id)
    {
        var chore = chores.FirstOrDefault(c => c.Id == id);
        if (chore == null)
        {
            return NotFound();
        }
        chores.Remove(chore);
        return RedirectToAction("List");
    }
}
