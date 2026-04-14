using Microsoft.Extensions.Options;
using Multi_Warehouse.Core;
using Multi_Warehouse.Core.interfaces;
using Renci.SshNet;
namespace Multi_Warehouse.API.Services
{
    public class SftpService : ISftpService
    {
        private readonly Sftpsettings _settings;

        public SftpService(IOptions<Sftpsettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task UploadReportAsync(byte[] fileData, string remoteFileName)
        {
            using var client = new SftpClient(
                _settings.Host,
                _settings.Port,
                _settings.Username,
                _settings.Password
            );

            client.Connect();

            using var stream = new MemoryStream(fileData);

            var remoteFullPath = $"{_settings.RemotePath}{remoteFileName}";

            client.UploadFile(stream, remoteFullPath, true); // 可覆蓋檔案

            client.Disconnect();

            await Task.CompletedTask;
        }
    }
}
