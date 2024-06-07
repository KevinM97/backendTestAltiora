using BackTest.Application.Features.Products.Queries.vms;
using BackTest.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackTest.Application.Features.Products.Queries.GetProductList
{
    public class GetProductListQuery : IRequest<IReadOnlyList<ProductResponse>>
    {
    }
}
