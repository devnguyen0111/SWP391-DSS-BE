using DiamondShopSystem.API.DTO;
using Microsoft.AspNetCore.Mvc;
using Services.Products;

[Route("api/[controller]")]
[ApiController]
public class SizesController : ControllerBase
{
    private readonly ISizeService _service;

    public SizesController(ISizeService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<List<SizeRequest>> GetAllSizes()
    {
        return Ok(_service.GetAllSizes());
    }

    [HttpGet("{id}")]
    public ActionResult<SizeRequest> GetSizeById(int id)
    {
        var size = _service.GetSizeById(id);
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
