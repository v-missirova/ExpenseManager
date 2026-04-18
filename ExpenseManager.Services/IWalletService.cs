using System;
using System.Collections.Generic;
using ExpenseManager.DTOModels;

namespace ExpenseManager.Services
{
    public interface IWalletService
    {
        Task<List<WalletListDTO>> GetAllWalletsAsync();
        Task<DTOModels.WalletDetailsDTO> GetWalletDetailsAsync(Guid walletId);
    }
}