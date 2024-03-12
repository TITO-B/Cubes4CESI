using Microsoft.AspNetCore.Mvc;
using WebAPI.DAL;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SiteController : ControllerBase
    {
        //Propriété de connexion à bdd
        private readonly MyAppDbContext _context;

        //Constructeur
        public SiteController(MyAppDbContext context)
        {
            _context = context;
        }


        #region Méthodes get
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var sites = _context.Sites.ToList();
                if (sites.Count == 0)
                {
                    return NotFound("Aucun site trouvé.");
                }
                return Ok(sites);
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
                var site = _context.Sites.Find(id);
                if (site == null)
                {
                    return NotFound($"Aucun site trouvé avec l'id : {id}");
                }
                return Ok(site);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Méthode post
        [HttpPost]
        public IActionResult Post(Site model)
        {
            try
            {
                _context.Add(model);
                _context.SaveChanges();
                return Ok("Site créer.");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Méthode put

        [HttpPut]
        public IActionResult Put(Site model)
        {
            if (model == null || model.SiteId == 0)
            {
                if (model == null)
                {
                    return BadRequest("Le modele de donnée est invalide");
                }
                else if (model.SiteId == 0)
                {
                    return BadRequest($"L'Id du site : {model.SiteId} est invalide");
                }
            }
            try
            {
                var site = _context.Sites.Find(model.SiteId);
                if (site == null)
                {
                    return NotFound($"Aucun site trouvé avec l'id {model.SiteId}");
                }
                site.SiteId = model.SiteId;
                site.Ville = model.Ville;
                _context.SaveChanges();
                return Ok("Détails du site mis à jour");
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
                var site = _context.Sites.Find(id);
                if (site == null)
                {
                    return NotFound($"Aucun site trouvé avec l'id {id}");
                }
                _context.Sites.Remove(site);
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
