using ContactsAPI.Data;
using ContactsAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ContactsAPIDbContext _dbContext;
        public ContactsController(ContactsAPIDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]

        public async Task<IActionResult> GetContacts()
        {
            if (_dbContext == null)
            {
                return NotFound();
            }
            return Ok(await _dbContext.Contacts.ToListAsync());
        }

        [HttpPost]

        public async  Task<IActionResult> AddContact(AddContactRequest addContactRequest)
        {
            Contact contact = new()
            {
                Name = addContactRequest.Name,
                Email = addContactRequest.Email,
                Address = addContactRequest.Address,
                Phone = addContactRequest.PhoneNumber
            };
            await _dbContext.Contacts.AddAsync(contact);   
            await _dbContext.SaveChangesAsync();
                return Ok(contact);

        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute]Guid id , UpdateContactRequest UpdatedContact )
        {
            var contact = await _dbContext.Contacts.FindAsync(id);

            if (contact != null) 
            {
                contact.Name = UpdatedContact.Name;
                contact.Address = UpdatedContact.Address;
                contact.Phone = UpdatedContact.PhoneNumber;
                contact.Email = UpdatedContact.Email;

                await _dbContext.SaveChangesAsync();
                return Ok(contact);
            }
            return NotFound();

        }
        [HttpGet]
        [Route("{id:guid}")]

        public async Task<IActionResult> SearchContact([FromRoute] Guid id)
        {
            var contact = await _dbContext.Contacts.FindAsync(id);
           if(contact == null)
           {
                return NotFound();
           }
           return Ok(contact);
        }

        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> DeleteContact([FromRoute]Guid id)
        {
            var contact = await _dbContext.Contacts.FindAsync(id);

            if (contact != null)
            {
                _dbContext.Remove(contact);
                await _dbContext.SaveChangesAsync();
                return Ok(contact);
            }
            return NotFound();
            
        }

    }
}