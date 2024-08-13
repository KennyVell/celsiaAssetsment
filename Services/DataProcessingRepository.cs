using OfficeOpenXml;
using celsiaAssetsment.Models;
using celsiaAssetsment.Interfaces;
using celsiaAssetsment.Data;
using Microsoft.EntityFrameworkCore;

namespace celsiaAssetsment.Services
{
    public class DataProcessingRepository : IDataProcessingRepository
    {
        private readonly CelsiaAssetsmentContext _context;

        public DataProcessingRepository(CelsiaAssetsmentContext context)
        {
            _context = context;
        }

        public async Task ProcessFileAsync(string filePath)
        {
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var sheet = package.Workbook.Worksheets[0];
                int rowCount = sheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    // Obtener datos de la fila
                    string clientName = sheet.Cells[row, 6].Value.ToString()!;
                    string clientIdentityNumber = sheet.Cells[row, 7].Value.ToString()!;
                    string clientAddress = sheet.Cells[row, 8].Value.ToString()!;
                    string clientPhone = sheet.Cells[row, 9].Value.ToString()!;
                    string clientEmail = sheet.Cells[row, 10].Value.ToString()!;

                    string platformName = sheet.Cells[row, 11].Value.ToString()!;

                    string invoiceNumber = sheet.Cells[row, 12].Value.ToString()!;
                    string invoicePeriod = sheet.Cells[row, 13].Value.ToString()!;
                    float billedAmount = float.Parse(sheet.Cells[row, 14].Value.ToString()!);
                    float paidAmount = float.Parse(sheet.Cells[row, 15].Value.ToString()!);

                    // Manejar la fecha y hora de la transacción
                    var transactionDateTimeValue = sheet.Cells[row, 2].Value;
                    DateTime transactionDateTime;
                    if (transactionDateTimeValue is DateTime dt)
                    {
                        transactionDateTime = dt;
                    }
                    else if (transactionDateTimeValue is double serialDate)
                    {
                        transactionDateTime = DateTime.FromOADate(serialDate);
                    }
                    else
                    {
                        throw new FormatException($"El valor '{transactionDateTimeValue}' no se puede convertir a DateTime.");
                    }

                    float transactionAmount = float.Parse(sheet.Cells[row, 3].Value.ToString()!);
                    string transactionStatus = sheet.Cells[row, 4].Value.ToString()!;
                    string transactionType = sheet.Cells[row, 5].Value.ToString()!;

                    // Verificar y agregar cliente
                    var client = await _context.Clients
                        .FirstOrDefaultAsync(c => c.Email == clientEmail);
                    if (client == null)
                    {
                        client = new Client
                        {
                            Name = clientName,
                            Address = clientAddress,
                            IdentityNumber = clientIdentityNumber,
                            Phone = clientPhone,
                            Email = clientEmail
                        };
                        _context.Clients.Add(client);
                        await _context.SaveChangesAsync();  // Guardar para obtener el ID del cliente
                    }

                    // Verificar y agregar plataforma
                    var platform = await _context.Platforms
                        .FirstOrDefaultAsync(p => p.Name == platformName);
                    if (platform == null)
                    {
                        platform = new Platform
                        {
                            Name = platformName
                        };
                        _context.Platforms.Add(platform);
                        await _context.SaveChangesAsync();  // Guardar para obtener el ID de la plataforma
                    }

                    // Verificar y agregar factura
                    var invoice = await _context.Invoices
                        .FirstOrDefaultAsync(i => i.Number == invoiceNumber);
                    if (invoice == null)
                    {
                        invoice = new Invoice
                        {
                            Number = invoiceNumber,
                            Period = invoicePeriod,
                            Billed_Amount = billedAmount,
                            Paid_Amount = paidAmount,
                            ClientId = client.Id
                        };
                        _context.Invoices.Add(invoice);
                        await _context.SaveChangesAsync();  // Guardar para obtener el ID de la factura
                    }

                    // Agregar transacción
                    var transaction = new Transaction
                    {
                        Date_Time = transactionDateTime,
                        Amount = transactionAmount,
                        Status = transactionStatus,
                        Type = transactionType,
                        ClientId = client.Id,
                        PlatformId = platform.Id,
                        InvoiceId = invoice.Id
                    };
                    _context.Transactions.Add(transaction);
                    await _context.SaveChangesAsync();  // Guardar para completar el proceso de esta fila
                }
            }
        }
    }
}