using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APIassignment.Models;

namespace APIassignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private static List<Product> _bloodBank = new List<Product>
        {
            new Product
            {
                Id = 1,
                DonorName = "Jane Smith",
                Age = 25,
                BloodType = "A+",
                ContactInfo = "987-654-3210",
                Quantity = 1,
                CollectionDate = new DateTime(2024, 11, 18, 15, 0, 0),
                ExpirationDate = new DateTime(2024, 12, 18, 15, 0, 0),
                Status = "Available"
            },
            new Product
            {
                Id = 2,
                DonorName = "Robert Johnson",
                Age = 35,
                BloodType = "B+",
                ContactInfo = "555-123-4567",
                Quantity = 3,
                CollectionDate = new DateTime(2024, 11, 17, 12, 0, 0),
                ExpirationDate = new DateTime(2024, 12, 17, 12, 0, 0),
                Status = "Used"
            },
            new Product
            {
                Id = 3,
                DonorName = "Emily Davis",
                Age = 28,
                BloodType = "O-",
                ContactInfo = "444-987-1234",
                Quantity = 2,
                CollectionDate = new DateTime(2024, 11, 16, 9, 30, 0),
                ExpirationDate = new DateTime(2024, 12, 16, 9, 30, 0),
                Status = "Available"
            },
            new Product
            {
                Id = 4,
                DonorName = "Michael Brown",
                Age = 40,
                BloodType = "AB+",
                ContactInfo = "333-555-6789",
                Quantity = 1,
                CollectionDate = new DateTime(2024, 11, 20, 11, 0, 0),
                ExpirationDate = new DateTime(2024, 12, 20, 11, 0, 0),
                Status = "Reserved"
            },
            new Product
            {
                Id = 5,
                DonorName = "Sarah Wilson",
                Age = 22,
                BloodType = "A-",
                ContactInfo = "666-888-1234",
                Quantity = 2,
                CollectionDate = new DateTime(2024, 11, 15, 14, 0, 0),
                ExpirationDate = new DateTime(2024, 12, 15, 14, 0, 0),
                Status = "Available"
            },
            new Product
            {
                Id = 6,
                DonorName = "Chris Taylor",
                Age = 31,
                BloodType = "B-",
                ContactInfo = "777-222-9876",
                Quantity = 1,
                CollectionDate = new DateTime(2024, 11, 19, 8, 0, 0),
                ExpirationDate = new DateTime(2024, 12, 19, 8, 0, 0),
                Status = "Used"
            },
            new Product
            {
                Id = 7,
                DonorName = "Anna White",
                Age = 29,
                BloodType = "O+",
                ContactInfo = "888-333-6547",
                Quantity = 2,
                CollectionDate = new DateTime(2024, 11, 21, 10, 30, 0),
                ExpirationDate = new DateTime(2024, 12, 21, 10, 30, 0),
                Status = "Available"
            },
            new Product
            {
                Id = 8,
                DonorName = "David Hall",
                Age = 34,
                BloodType = "AB-",
                ContactInfo = "999-444-8765",
                Quantity = 3,
                CollectionDate = new DateTime(2024, 11, 14, 13, 0, 0),
                ExpirationDate = new DateTime(2024, 12, 14, 13, 0, 0),
                Status = "Available"
            },
            new Product
            {
                Id = 9,
                DonorName = "Mia Lewis",
                Age = 26,
                BloodType = "O-",
                ContactInfo = "111-555-7890",
                Quantity = 1,
                CollectionDate = new DateTime(2024, 11, 22, 12, 0, 0),
                ExpirationDate = new DateTime(2024, 12, 22, 12, 0, 0),
                Status = "Reserved"
            },
            new Product
            {
                Id = 10,
                DonorName = "James Martin",
                Age = 38,
                BloodType = "A+",
                ContactInfo = "222-666-4321",
                Quantity = 2,
                CollectionDate = new DateTime(2024, 11, 23, 16, 0, 0),
                ExpirationDate = new DateTime(2024, 12, 23, 16, 0, 0),
                Status = "Available"
            }
        };

        /*Create (POST /api/bloodbank): Add a new entry to the blood bank list. The
        input should include donor details, blood type, quantity, and
        collection/expiration dates.*/
        [HttpPost]
        public IActionResult Create([FromBody] Product newProduct)
        {
            if (newProduct == null)
            {
                return BadRequest("Product details cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(newProduct.DonorName) ||
                string.IsNullOrWhiteSpace(newProduct.BloodType) ||
                string.IsNullOrWhiteSpace(newProduct.ContactInfo) ||
                newProduct.Quantity <= 0 ||
                newProduct.CollectionDate == default ||
                newProduct.ExpirationDate <= newProduct.CollectionDate)
            {
                return BadRequest("Invalid product details.");
            }

            newProduct.Id = _bloodBank.Count > 0 ? _bloodBank[^1].Id + 1 : 1;
            _bloodBank.Add(newProduct);

            return CreatedAtAction(nameof(Create), new { id = newProduct.Id }, newProduct);
        }

        // Read (GET /api/bloodbank): Retrieve all entries in the blood bank list.
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_bloodBank);
        }

        // Read (GET /api/bloodbank/{id}): Retrieve a specific blood entry by its Id.
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _bloodBank.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }
            return Ok(product);
        }

        /*Update(PUT /api/bloodbank/{ id}): Update an existing entry(e.g., modify
        donor details or update blood status).*/
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Product updatedProduct)
        {
            if (updatedProduct == null)
            {
                return BadRequest("Product details cannot be null.");
            }

            var existingProduct = _bloodBank.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }

            if (!string.IsNullOrWhiteSpace(updatedProduct.DonorName))
            {
                existingProduct.DonorName = updatedProduct.DonorName;
            }

            if (!string.IsNullOrWhiteSpace(updatedProduct.BloodType))
            {
                existingProduct.BloodType = updatedProduct.BloodType;
            }

            if (!string.IsNullOrWhiteSpace(updatedProduct.ContactInfo))
            {
                existingProduct.ContactInfo = updatedProduct.ContactInfo;
            }

            if (updatedProduct.Quantity >= 0)
            {
                existingProduct.Quantity = updatedProduct.Quantity;
            }

            if (updatedProduct.Age >= 0)
            {
                existingProduct.Age = updatedProduct.Age;
            }

            if (updatedProduct.CollectionDate != default)
            {
                existingProduct.CollectionDate = updatedProduct.CollectionDate;
            }

            if (updatedProduct.ExpirationDate != default && updatedProduct.ExpirationDate > existingProduct.CollectionDate)
            {
                existingProduct.ExpirationDate = updatedProduct.ExpirationDate;
            }

            if (!string.IsNullOrWhiteSpace(updatedProduct.Status))
            {
                existingProduct.Status = updatedProduct.Status;
            }

            return Ok(existingProduct);
        }

        // Delete (DELETE /api/bloodbank/{id}): Remove an entry from the list based on its Id.
        
                [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _bloodBank.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }

            _bloodBank.Remove(product);
            return Ok(_bloodBank);
        }

        // Pagination GET /api/bloodbank? page = { pageNumber }&size={pageSize}:
        [HttpGet("paginated")]
        public IActionResult GetPaginatedList(int page = 1, int size = 10)
        {
            if (page <= 0 || size <= 0)
            {
                return BadRequest("Page and size must be greater than zero.");
            }

            var totalProducts = _bloodBank.Count();
            var skip = (page - 1) * size;
            var paginatedProducts = _bloodBank.Skip(skip).Take(size).ToList();
            var totalPages = (int)Math.Ceiling((double)totalProducts / size);

            var result = new
            {
                TotalProducts = totalProducts,
                TotalPages = totalPages,
                CurrentPage = page,
                PageSize = size,
                Data = paginatedProducts
            };

            return Ok(result);
        }

        // Search and filter Functionality
        [HttpGet("search")]
        public IActionResult Search([FromQuery] string? bloodType, [FromQuery] string? status, [FromQuery] string? donorName)
        {
            var query = _bloodBank.AsQueryable();

            if (!string.IsNullOrWhiteSpace(bloodType))
            {
                query = query.Where(p => p.BloodType != null && p.BloodType.Equals(bloodType, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                query = query.Where(p => p.Status != null && p.Status.Equals(status, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(donorName))
            {
                query = query.Where(p => p.DonorName != null && p.DonorName.Contains(donorName, StringComparison.OrdinalIgnoreCase));
            }

            var results = query.ToList();

            if (!results.Any())
            {
                return NotFound("No matching products found.");
            }

            return Ok(results);
        }
    }
}
