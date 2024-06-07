using BackTest.Application.Features.Products.Queries.vms;
using BackTest.Application.Persistence;
using BackTest.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackTest.Application.Features.Products.Queries.GetProductList
{
    public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, IReadOnlyList<ProductResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProductListQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<ProductResponse>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.Repository<Product>().GetAllAsync();
            var productos = products.Select(x => new ProductResponse
            {
                Id = x.Id,
                Codigo = x.Codigo,
                Nombre = x.Nombre,
                Precio = x.Precio
            }).ToList();
            return productos;

        }
    }
}
