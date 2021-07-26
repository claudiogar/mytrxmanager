using System;
using System.ComponentModel.DataAnnotations;

namespace api.Controllers
{
    public class GetQueryParameters
    {
        const int maxPageSize = 50;
        private int _limit = 10;

        [Range(maximum: int.MaxValue, minimum:0)]
        public int? StartingId { get; set; }

        [Range(maximum: maxPageSize, minimum: -maxPageSize)]
        public int Limit
        {
            get
            {
                return _limit;
            }
            set
            {
                _limit = value;
            }
        }
    }
}
