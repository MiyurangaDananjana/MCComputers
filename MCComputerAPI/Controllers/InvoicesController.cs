
using MCComputerAPI.Data.Interfaces;
using MCComputerAPI.Models.DTOs;
using MCComputerAPI.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MCComputerAPI;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class InvoicesController : ControllerBase
{
    private readonly IInvoicesRepositories _invoiceRepo;

    public InvoicesController(IInvoicesRepositories invoiceRepo)
    {
        _invoiceRepo = invoiceRepo;
    }

    // GET: api/invoices
    [HttpGet]
    public async Task<IActionResult> GetAllInvoices()
    {
        var invoices = await _invoiceRepo.GetAllInvoicesAsync();
        return Ok(invoices);
    }

    // GET: api/invoices/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetInvoiceById(int id)
    {
        var invoice = await _invoiceRepo.GetInvoiceByIdAsync(id);
        if (invoice == null)
        {
            return NotFound();
        }


        return Ok(invoice);
    }

    // POST: api/invoices
    [HttpPost]
    public async Task<IActionResult> CreateInvoice([FromBody] InvoiceDTOs invoiceDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        Invoice invoice = new Invoice
        {
            CustomerId = invoiceDto.CustomerId,
            TotalAmmount = invoiceDto.TotalAmmount,
            CreateDate = DateTime.UtcNow
        };

        List<InvoiceItems> invoiceItems = new List<InvoiceItems>();

        foreach (var item in invoiceDto.InvoiceItems)
        {
            InvoiceItems items = new InvoiceItems();
            items.ProductId = item.ProductId;
            items.Quantity = item.Quantity;
            items.UnitPrice = item.Price;
            invoiceItems.Add(items);
        }

        var createdInvoice = await _invoiceRepo.AddInvoiceAsync(invoice, invoiceItems);
        return CreatedAtAction(nameof(GetInvoiceById), new { id = createdInvoice.InvoiceId }, createdInvoice);
    }

    // // PUT: api/invoices/{id}
    // [HttpPut("{id}")]
    // public async Task<IActionResult> UpdateInvoice(int id, [FromBody] InvoiceDTOs invoice)
    // {
    //     if (id != invoice.InvoiceId)
    //     {
    //         return BadRequest("Invoice ID mismatch.");
    //     }

    //     if (!ModelState.IsValid)
    //     {
    //         return BadRequest(ModelState);
    //     }

    //     var result = await _invoiceRepo.UpdateInvoiceAsync(invoice);

    //     if (!result)
    //     {
    //         return NotFound();
    //     }

    //     return NoContent();
    // }

    // DELETE: api/invoices/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInvoice(int id)
    {
        var result = await _invoiceRepo.DeleteInvoiceAsync(id);
        if (!result)
        {
            return NotFound();
        }


        return NoContent();
    }

}