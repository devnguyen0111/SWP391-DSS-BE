using DiamondShopSystem.API.DTO;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Repository.Products;
using Services.Products;
using System.Collections.Generic;

[Route("api/[controller]")]
[ApiController]
public class SizesController : ControllerBase
{
    private readonly ISizeRepository _repository;

    public SizesController(ISizeRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Size>> GetAllSizes()
    {
        return Ok(_repository.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<Size> GetSizeById(int id)
    {
        var size = _repository.GetSizeById(id);
        if (size == null)
        {
            return NotFound();
        }
        var sizeService = new SizeRequest
        {
            SizeId = size.SizeId,
            SizeName = size.SizeName,
            SizePrice = size.SizePrice,
            SizeValue = size.SizeValue,
        };
        return Ok(sizeService);
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
