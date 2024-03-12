using Microsoft.AspNetCore.Mvc;
using WebAPI.DAL;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        //Propriété de connexion à bdd
        private readonly MyAppDbContext _context;

        //Constructeur
        public ServiceController(MyAppDbContext context)
        {
            _context = context;
        }


        #region Méthodes get
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var services = _context.Services.ToList();
                if (services.Count == 0)
                {
                    return NotFound("Aucun service trouvé.");
                }
                return Ok(services);
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
                var services = _context.Services.Find(id);
                if (services == null)
                {
                    return NotFound($"Aucun service trouvé avec l'id : {id}");
                }
                return Ok(services);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Méthode post
        [HttpPost]
        public IActionResult Post(Service model)
        {
            try
            {
                _context.Add(model);
                _context.SaveChanges();
                return Ok("Service créer.");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        #endregion


        #region Méthode put

        [HttpPut]
        public IActionResult Put(Service model)
        {
            if (model == null || model.ServiceId == 0)
            {
                if (model == null)
                {
                    return BadRequest("Le modele de donnée est invalide");
                }
                else if (model.ServiceId == 0)
                {
                    return BadRequest($"L'Id du service : {model.ServiceId} est invalide");
                }
            }
            try
            {
                var service = _context.Services.Find(model.ServiceId);
                if (service == null)
                {
                    return NotFound($"Aucun service trouvé avec l'id {model.ServiceId}");
                }
                service.ServiceId = model.ServiceId;
                service.Nom = model.Nom;
                _context.SaveChanges();
                return Ok("Détails du service mis à jour");
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
                var service = _context.Services.Find(id);
                if (service == null)
                {
                    return NotFound($"Aucun service trouvé avec l'id {id}");
                }
                _context.Services.Remove(service);
                _context.SaveChanges();
                return Ok("Détails du site mis à jour");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
        #endregion

    }
}
