using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Repository.Products;
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
    public ActionResult<List<MetaltypeRequest>> GetAllMetaltypes()
    {
        return Ok(_repository.GetAllMetaltypes());
    }

    [HttpGet("{id}")]
    public ActionResult<MetaltypeRequest> GetMetaltypeById(int id)
    {
        var metaltype = _repository.GetMetaltypeById(id);
        if (metaltype == null)
        {
            return NotFound();
        }
        var metaltypeRequest = new MetaltypeRequest
        {
            MetaltypeId = metaltype.MetaltypeId,
            MetaltypeName = metaltype.MetaltypeName,
            MetaltypePrice = metaltype.MetaltypePrice
        };
        return Ok(metaltypeRequest);
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
