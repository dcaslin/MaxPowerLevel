using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Destiny2;
using Destiny2.Entities.Items;
using MaxPowerLevel.Models;
using MaxPowerLevel.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace MaxPowerLevel.Controllers
{
    [Route("[controller]")]
    public class CharacterController : Controller
    {
        private readonly IDestinyService _destiny;
        private readonly IManifestService _manifest;
        private readonly IMaxPowerService _maxPower;

        private static readonly ISet<ItemSlot> _includedSlots =
            new HashSet<ItemSlot>
            {
                ItemSlot.KineticWeapon,
                ItemSlot.EnergyWeapon,
                ItemSlot.PowerWeapon,
                ItemSlot.Helmet,
                ItemSlot.Gauntlet,
                ItemSlot.ChestArmor,
                ItemSlot.LegArmor,
                ItemSlot.ClassArmor,
            };

        public CharacterController(IDestinyService destiny, IManifestService manifest, IMaxPowerService maxPower)
        {
            _destiny = destiny;
            _manifest = manifest;
            _maxPower = maxPower;
        }
        
        [HttpGet("{type}/{id}/{characterId}")]
        public async Task<IActionResult> Details(int type, long id, long characterId)
        {
            var membershipType = (BungieMembershipType)type;

            var model = new CharacterViewModel()
            {
                Items = await _maxPower.ComputeMaxPowerAsync(membershipType, id, characterId),
            };

            return View(model);
        }
    }
}