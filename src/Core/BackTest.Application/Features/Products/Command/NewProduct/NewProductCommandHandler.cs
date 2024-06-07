using BackTest.Application.Features.Products.Command.vms;
using BackTest.Application.Persistence;
using BackTest.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackTest.Application.Features.Products.Command.NewProduct
{
    public class NewProductCommandHandler : IRequestHandler<NewProductCommand, NewProductResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public NewProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<NewProductResponse> Handle(NewProductCommand request, CancellationToken cancellationToken)
        {

            var producto = new Product
            {
                Codigo = GenerateProductCode(),
                Nombre = request.Nombre,
                Precio = request.Precio
            };

            _unitOfWork.Repository<Product>().AddEntity(producto);
            var result = await _unitOfWork.Complete();

            if (result <= 0)
            {
                throw new Exception($"Hubo un problema al registrar el producto");
            }

            var newproduct = new NewProductResponse
            {
                Codigo = producto.Codigo,
                Nombre = request.Nombre,
                Precio = request.Precio
            };
            return newproduct;
        }
        private string GenerateProductCode()
        {
            return "PR-" + (_unitOfWork.Repository<Product>().GetAllAsync().Result.Count() + 1).ToString("D5");
        }
    }
}
