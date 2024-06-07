using BackTest.Application.Persistence;
using BackTest.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackTest.Application.Features.Products.Command.EditProduct
{
    public class EditProductCommandHandler : IRequestHandler<EditProductCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EditProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(EditProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.Repository<Product>().GetByIdAsync(request.ProductId);

            if (product == null)
            {
                throw new Exception("El producto no fue encontrado.");
            }

            product.Nombre = request.Nombre;
            product.Precio = request.Precio;
            product.Codigo = request.Codigo;

            _unitOfWork.Repository<Product>().UpdateEntity(product);
            var result = await _unitOfWork.Complete();

            return result > 0; 
        }
    }
}
