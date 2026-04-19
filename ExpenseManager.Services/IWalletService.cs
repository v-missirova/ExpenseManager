using System;
using System.Collections.Generic;
using ExpenseManager.Common.Enums;
using ExpenseManager.DTOModels;

namespace ExpenseManager.Services
{
    public interface IWalletService
    {
        Task<List<WalletListDTO>> GetAllWalletsAsync();
        Task<DTOModels.WalletDetailsDTO> GetWalletDetailsAsync(Guid walletId);

        Task<Guid> AddWalletAsync(string name, Currency currency, decimal initialBalance);
        Task DeleteWalletAsync(Guid id);


    }
}