using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Repository;
using System.Collections.Generic;

[Route("api/[controller]")]
[ApiController]
public class MetaltypeController : ControllerBase
{
    private readonly IMetaltypeRepository _repository;

    public MetaltypeController(IMetaltypeRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Metaltype>> GetAllMetaltypes()
    {
        return Ok(_repository.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<Size> GetMetaltypeById(int id)
    {
        var size = _repository.GetMetaltypeById(id);
        if (size == null)
        {
            return NotFound();
        }
        return Ok(size);
    }

    /*[HttpPost]
    public ActionResult CreateSize(Size size)
    {
        _repository.Add(size);
        return CreatedAtAction(nameof(GetSizeById), new { id = size.SizeId }, size);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateSize(int id, Size size)
    {
        if (id != size.SizeId)
        {
            return BadRequest();
        }

        _repository.Update(size);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteSize(int id)
    {
        var size = _repository.GetById(id);
        if (size == null)
        {
            return NotFound();
        }

        _repository.Delete(id);
        return NoContent();
    }*/
}
