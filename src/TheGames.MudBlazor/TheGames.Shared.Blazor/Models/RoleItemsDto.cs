using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGames.Shared.Blazor.Models
{
    public class RoleItemsDto<T>
    {
        public IReadOnlyList<T>? Items { get; set; }
    }
}
