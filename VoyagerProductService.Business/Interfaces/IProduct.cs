using System.Collections.Generic;
using VoyagerProductService.Domain.Dtos;

namespace VoyagerProductService.Business.Interfaces
{
    public interface IProduct
    {
        decimal ExecuteProcess(List<string> products);

    }
}
