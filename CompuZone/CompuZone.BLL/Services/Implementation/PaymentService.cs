using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CompuZone.BLL.DTOs.Payment;
using CompuZone.BLL.DTOs.Response;
using CompuZone.BLL.Services.Interfaces;
using CompuZone.DAL.Entities;
using CompuZone.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CompuZone.BLL.Services.Implementation
{
    public class PaymentService : IPaymentService
    {
        private readonly IMapper _mapper;
        private readonly IPaymentRepo _prepo;
        public PaymentService(IMapper mapper, IPaymentRepo prepo)
        {
            _mapper = mapper;
            _prepo = prepo;
        }
        public async Task<ResponseDto<ResPaymentDto>> CreateAsync(ReqPaymentDto dto)
        {
            Payment payment = _mapper.Map<ReqPaymentDto, Payment>(dto);

            Payment pmt = await _prepo.AddAsync(payment);

            if (pmt == null) throw new Exception("an error occurred while creating a payment");

            var Pdto = _mapper.Map<Payment, ResPaymentDto>(pmt);

            return new ResponseDto<ResPaymentDto>
            {
                Data = Pdto,
                Message = "Payment Created Successfully",
                IsSuccess = true
            };
        }

        public async Task<ResponseDto<bool>> DeleteAsync(int id)
        {
            bool result = await _prepo.DeleteAsync(id);

            if (!result) throw new Exception("An error occurred while deleting Payment");

            return new ResponseDto<bool>
            {
                Data = result,
                Message = "Payment Deleted Successfully",
                IsSuccess = true
            };
        }

        public async Task<ResponseDto<List<ResPaymentDto>>> GetAllAsync()
        {
            List<Payment> payments = await _prepo.GetAllAsync().ToListAsync();
            List<ResPaymentDto> Pdto = _mapper.Map<List<Payment>, List<ResPaymentDto>>(payments);
            return new ResponseDto<List<ResPaymentDto>>
            {
                Data = Pdto,
                Message = "Payments Retrieved Successfully",
                IsSuccess = true
            };
        }

        public async Task<ResponseDto<ResPaymentDto>> GetByIdAsync(int id)
        {
            Payment payment =  await _prepo.GetByIdAsync(id);
            if (payment == null) throw new Exception("Payment not found");
            ResPaymentDto Pdto = _mapper.Map<Payment, ResPaymentDto>(payment);
            return new ResponseDto<ResPaymentDto>
            {
                Data = Pdto,
                Message = "Payment Retrieved Successfully",
                IsSuccess = true
            };
        }

        public async Task<ResponseDto<bool>> UpdateAsync(int id, ReqPaymentDto dto)
        {
            Payment payment = _mapper.Map<ReqPaymentDto, Payment>(dto);
            bool result = await _prepo.UpdateAsync(payment);
            if (!result) throw new Exception("An error occurred while updating Payment");
            return new ResponseDto<bool>
            {
                Data = result,
                Message = "Payment Updated Successfully",
                IsSuccess = true
            };
        }
    }
}
