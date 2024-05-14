using AuthApi.Domain.Adapters.Services.File;

namespace AuthApi.Infra;

public class FileService : IFileService
{
    private readonly string _supabaseUrl;
    private readonly string _supabaseKey;

    public FileService(string supabaseUrl, string supabaseKey)
    {
        _supabaseUrl = supabaseUrl;
        _supabaseKey = supabaseKey;
    }

    public async Task<string> UploadAvatar(byte[] content, string filename)
    {
        const string bucket = "ProfileAvatar";

        var options = new Supabase.SupabaseOptions
        {
            AutoConnectRealtime = true
        };

        var client = new Supabase.Client(_supabaseUrl, _supabaseKey, options);
        await client.InitializeAsync();

        await client.Storage
            .From(bucket)
            .Upload(content, filename);

        var publicUrl = client.Storage
            .From(bucket)
            .GetPublicUrl(filename);

        return publicUrl;
    }
}
