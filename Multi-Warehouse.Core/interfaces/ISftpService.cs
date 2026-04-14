
namespace Multi_Warehouse.Core.interfaces
{
    public interface ISftpService
    {
        Task UploadReportAsync(byte[] fileData, string remoteFileName);
    }
}
