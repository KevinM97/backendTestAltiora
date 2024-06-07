using BackTest.Application.Persistence;
using BackTest.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackTest.Application.Features.Products.Command.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
            private readonly IUnitOfWork _unitOfWork;

            public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }
        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var productToDelete = await _unitOfWork.Repository<Product>().GetByIdAsync(request.ProductId);
            if (productToDelete == null)
            {
                throw new Exception("El producto no fue encontrado.");
            }

            _unitOfWork.Repository<Product>().DeleteEntity(productToDelete);
            await _unitOfWork.Complete();

            return true; 
        }
    }
}
