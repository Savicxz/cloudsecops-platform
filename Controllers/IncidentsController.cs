using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CloudSecOps.Data;
using CloudSecOps.Models;
using CloudSecOps.ViewModels;

namespace CloudSecOps.Controllers
{
    public class IncidentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IncidentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Incidents
        public async Task<IActionResult> Index()
        {
            var incidents = await _context.Incidents
                .OrderByDescending(i => i.CreatedAt)
                .ToListAsync();

            return View(incidents);
        }

        // GET: Incidents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incident = await _context.Incidents
                .FirstOrDefaultAsync(m => m.Id == id);

            if (incident == null)
            {
                return NotFound();
            }

            var notes = await _context.InvestigationNotes
                .Where(n => n.IncidentId == incident.Id)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();

            var viewModel = new IncidentDetailsViewModel
            {
                Incident = incident,
                InvestigationNotes = notes,
                NewNote = new InvestigationNote
                {
                    IncidentId = incident.Id
                }
            };

            return View(viewModel);
        }

        // POST: Incidents/AddNote
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNote(int incidentId, string noteText, string addedBy)
        {
            var incident = await _context.Incidents.FindAsync(incidentId);

            if (incident == null)
            {
                return NotFound();
            }

            if (string.IsNullOrWhiteSpace(noteText))
            {
                return RedirectToAction(nameof(Details), new { id = incidentId });
            }

            var note = new InvestigationNote
            {
                IncidentId = incidentId,
                NoteText = noteText,
                AddedBy = string.IsNullOrWhiteSpace(addedBy) ? "Unassigned Analyst" : addedBy,
                CreatedAt = DateTime.UtcNow
            };

            _context.InvestigationNotes.Add(note);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = incidentId });
        }

        // GET: Incidents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Incidents/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Category,Severity,Status,Description,ReportedBy")] Incident incident)
        {
            if (ModelState.IsValid)
            {
                incident.CreatedAt = DateTime.UtcNow;

                _context.Add(incident);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(incident);
        }

        // GET: Incidents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incident = await _context.Incidents.FindAsync(id);

            if (incident == null)
            {
                return NotFound();
            }

            return View(incident);
        }

        // POST: Incidents/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Category,Severity,Status,Description,ReportedBy")] Incident incident)
        {
            if (id != incident.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingIncident = await _context.Incidents.FindAsync(id);

                if (existingIncident == null)
                {
                    return NotFound();
                }

                existingIncident.Title = incident.Title;
                existingIncident.Category = incident.Category;
                existingIncident.Severity = incident.Severity;
                existingIncident.Status = incident.Status;
                existingIncident.Description = incident.Description;
                existingIncident.ReportedBy = incident.ReportedBy;

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(incident);
        }

        // GET: Incidents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incident = await _context.Incidents
                .FirstOrDefaultAsync(m => m.Id == id);

            if (incident == null)
            {
                return NotFound();
            }

            return View(incident);
        }

        // POST: Incidents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var incident = await _context.Incidents.FindAsync(id);

            if (incident != null)
            {
                _context.Incidents.Remove(incident);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}