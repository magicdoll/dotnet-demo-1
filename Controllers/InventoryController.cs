using DapperWebApiExample.Services;
using Microsoft.AspNetCore.Mvc;

namespace DapperWebApiExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly InventoryService _inventoryService;

        public InventoryController(InventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet("fnGetAllAvailableEquipment")]
        public async Task<ActionResult<ApiResponse<IEnumerable<dynamic>>>> GetAllAvailableEquipment()
        {
            var products = await _inventoryService.GetAllAvailableEquipment();
            var response = new ApiResponse<IEnumerable<dynamic>>(products);
            return Ok(response);
        }

        [HttpGet("fnGetItems")]
        public async Task<ActionResult<ApiResponse<IEnumerable<dynamic>>>> GetItems()
        {
            var products = await _inventoryService.GetItems();
            var response = new ApiResponse<IEnumerable<dynamic>>(products);
            return Ok(response);
        }

        [HttpGet("fnGetUser")]
        public async Task<ActionResult<ApiResponse<IEnumerable<dynamic>>>> GetUser()
        {
            var products = await _inventoryService.GetUser();
            var response = new ApiResponse<IEnumerable<dynamic>>(products);
            return Ok(response);
        }

        [HttpGet("fnGetSelectPO")]
        public async Task<ActionResult<ApiResponse<IEnumerable<dynamic>>>> GetSelectPO()
        {
            var products = await _inventoryService.GetSelectPO();
            var response = new ApiResponse<IEnumerable<dynamic>>(products);
            return Ok(response);
        }

        [HttpGet("fnGetSelectAllAddLog")]
        public async Task<ActionResult<ApiResponse<IEnumerable<dynamic>>>> GetSelectAllAddLog()
        {
            var products = await _inventoryService.GetSelectAllAddLog();
            var response = new ApiResponse<IEnumerable<dynamic>>(products);
            return Ok(response);
        }

        [HttpGet("fnGetSelectAllRemoveLog")]
        public async Task<ActionResult<ApiResponse<IEnumerable<dynamic>>>> GetSelectAllRemoveLog()
        {
            var products = await _inventoryService.GetSelectAllRemoveLog();
            var response = new ApiResponse<IEnumerable<dynamic>>(products);
            return Ok(response);
        }

        [HttpGet("fnGetAddLogByID/{id}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<dynamic>>>> GetAddLogByID(string id)
        {
            var products = await _inventoryService.GetAddLogByID(id);
            var response = new ApiResponse<IEnumerable<dynamic>>(products);
            return Ok(response);
        }

        [HttpGet("fnGetRemoveLogByID/{id}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<dynamic>>>> GetRemoveLogByID(string id)
        {
            var products = await _inventoryService.GetRemoveLogByID(id);
            var response = new ApiResponse<IEnumerable<dynamic>>(products);
            return Ok(response);
        }

        [HttpPost("fnSetAddItemType")]
        public async Task<ActionResult<ApiResponse<IEnumerable<dynamic>>>> SetAddItemType([FromBody] dynamic payload)
        {
            var products = await _inventoryService.SetAddItemType(payload);
            var response = new ApiResponse<IEnumerable<dynamic>>(products);
            return Ok(response);
        }

        [HttpPost("fnSetEditItemType")]
        public async Task<ActionResult<ApiResponse<IEnumerable<dynamic>>>> SetEditItemType([FromBody] dynamic payload)
        {
            var products = await _inventoryService.SetEditItemType(payload);
            var response = new ApiResponse<IEnumerable<dynamic>>(products);
            return Ok(response);
        }

        [HttpPost("fnSetDeleteItemType")]
        public async Task<ActionResult<ApiResponse<IEnumerable<dynamic>>>> SetDeleteItemType([FromBody] dynamic payload)
        {
            var products = await _inventoryService.SetDeleteItemType(payload);
            var response = new ApiResponse<IEnumerable<dynamic>>(products);
            return Ok(response);
        }

        [HttpPost("fnSetAddItem")]
        public async Task<ActionResult<ApiResponse<IEnumerable<dynamic>>>> SetAddItem([FromBody] dynamic payload)
        {
            var products = await _inventoryService.SetAddItem(payload);
            var response = new ApiResponse<IEnumerable<dynamic>>(products);
            return Ok(response);
        }

        [HttpPost("fnSetRemoveItem")]
        public async Task<ActionResult<ApiResponse<IEnumerable<dynamic>>>> SetRemoveItem([FromBody] dynamic payload)
        {
            var products = await _inventoryService.SetRemoveItem(payload);
            var response = new ApiResponse<IEnumerable<dynamic>>(products);
            return Ok(response);
        }

    }
}
