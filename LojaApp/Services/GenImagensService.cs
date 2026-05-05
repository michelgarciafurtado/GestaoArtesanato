namespace LojaApp.Services;

public class GenImagensService
{
    private readonly IWebHostEnvironment _env;
    private readonly IConfiguration _config;

    public GenImagensService(IWebHostEnvironment env, IConfiguration config)
    {
        _env = env;
        _config = config;
    }
    public string UploadImagem(IFormFile imagemUpload, string nomeImagem, string tipo)
    {
        var ext = Path.GetExtension(imagemUpload.FileName);
        
        if (ext != ".jpg" && ext != ".jpeg" && ext != ".png" && ext != ".webp")
        {
            throw new InvalidOperationException("Apenas arquivos .jpg, .jpeg, .png e .webp são permitidos.");
        }

        var fileName = ParaUrlAmigavel(nomeImagem.Trim()) + ext;
        var DirPath = _config[$"ImagensPath:{tipo}"];

        var filePath = Path.Combine(_env.WebRootPath, DirPath);
       
        if (!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
        }

        filePath = Path.Combine(filePath, fileName);
        
        try
        {
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                imagemUpload.CopyTo(stream);
            }
            return Path.Combine(DirPath, fileName).Replace("\\","/");
        }
        catch (Exception ex) 
        {
            throw new IOException("Erro ao salvar a imagem: " + ex.Message);
        }
    }

    public bool ExcluirImagem(string caminhoRelativo)
    {
        var filePath = Path.Combine(_env.WebRootPath, caminhoRelativo);
        if (File.Exists(filePath))
        {
            try
            {
                File.Delete(filePath);
                return true;
            }
            catch (Exception ex)
            {
                throw new IOException("Erro ao excluir a imagem: " + ex.Message);
            }
        }
        return false;
    }

    private static string ParaUrlAmigavel(string name)
    {
        // Remove acentos
        var bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(name);
        string cleanName = System.Text.Encoding.ASCII.GetString(bytes);

        // Remove espaços e deixa minúsculo
        cleanName = cleanName.Replace(" ", "-").ToLower();

        // Remove qualquer coisa que não seja letra, número ou traço
        return System.Text.RegularExpressions.Regex.Replace(cleanName, @"[^a-z0-9\-\.]", "");
    }
}
