using SaitynaiAPI.DTOs.ThreadDTOs;
using System.Collections.Generic;

namespace SaitynaiAPI.DTOs.CategoryDTOs
{
    public class GetCategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<GetThreadCollectionResponse> Threads { get; set; }
    }
}
