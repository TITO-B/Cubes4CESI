using Microsoft.AspNetCore.Mvc;
using WebAPI.DAL;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalarieController : ControllerBase
    {
        //Propriété de connexion à bdd
        private readonly MyAppDbContext _context;

        //Constructeur
        public SalarieController(MyAppDbContext context)
        {
            _context = context;
        }

        #region Méthodes get
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var salaries = _context.Salaries.ToList();
                if (salaries.Count == 0)
                {
                    return NotFound("Aucun salarié trouvé.");
                }
                return Ok(salaries);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var salarie = _context.Salaries.Find(id);
                if (salarie == null)
                {
                    return NotFound($"Aucun salarié trouvé avec l'id : {id}");
                }
                return Ok(salarie);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Filtered")]
        public IActionResult GetFiltered(int? siteId, int? serviceId, string? nomContient = "")
        {
            try
            {
                var salaries = _context.Salaries
                    .Where(s =>
                        (!siteId.HasValue || s.SiteId == siteId) &&
                        (!serviceId.HasValue || s.ServiceId == serviceId) &&
                        (string.IsNullOrEmpty(nomContient) || s.Nom.Contains(nomContient) || s.Prenom.Contains(nomContient))
                    )
                    .OrderBy(s => s.Nom)
                    .ToList();

                if (salaries.Count == 0)
                {
                    return NotFound("Aucun salarié trouvé avec les filtres spécifiés.");
                }

                return Ok(salaries);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Méthode post
        [HttpPost]
        public IActionResult Post(Salarie model)
        {
            try
            {
                _context.Add(model);
                _context.SaveChanges();
                return Ok("Salarié créer.");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Méthode put
        [HttpPut]
        public IActionResult Put(Salarie model)
        {
            if (model == null || model.SalarieId == 0)
            {
                if (model == null)
                {
                    return BadRequest("Le modele de donnée est invalide");
                }
                else if (model.SalarieId == 0)
                {
                    return BadRequest($"L'Id du salarié : {model.SalarieId} est invalide");
                }
            }
            try
            {
                var salarie = _context.Salaries.Find(model.SalarieId);
                if (salarie == null)
                {
                    return NotFound($"Aucun salarié trouvé avec l'id {model.SalarieId}");
                }
                salarie.SalarieId = model.SalarieId;
                salarie.Nom = model.Nom;
                salarie.Prenom = model.Prenom;
                salarie.TelephoneFixe = model.TelephoneFixe;
                salarie.TelephonePortable = model.TelephonePortable;
                salarie.Email = model.Email;
                salarie.ServiceId = model.ServiceId;
                salarie.SiteId = model.SiteId;
                _context.SaveChanges();
                return Ok("Détails du salarié mis à jour");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
        #endregion

        #region Méthode delete
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                var salarie = _context.Salaries.Find(id);
                if (salarie == null)
                {
                    return NotFound($"Aucun salarié trouvé avec l'id {id}");
                }
                _context.Salaries.Remove(salarie);
                _context.SaveChanges();
                return Ok("Détails du salarié mis à jour");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
        #endregion

    }
}
