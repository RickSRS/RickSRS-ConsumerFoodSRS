using ConsumerFoodSRS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ConsumerFoodSRS.Areas.Administration.Controllers;

[Area("Administration")]
[Authorize(Roles = "Admin")]
public class AdminImagensController : Controller
{
    private readonly ConfigurationImagens _configurationImagens;
    private readonly IWebHostEnvironment _hostEnvironment;

    public AdminImagensController(IOptions<ConfigurationImagens> configurationImagens, IWebHostEnvironment webHostEnvironment)
    {
        _configurationImagens = configurationImagens.Value;
        _hostEnvironment = webHostEnvironment;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> UploadFiles(List<IFormFile> files)
    {
        if (!files.Any())
        {
            ViewData["Erro"] = "Erro: Arquivo(s) não selecionado(s)";
            return View(ViewData);
        }

        if(files.Count > 10)
        {
            ViewData["Erro"] = "Erro: No maximo 10 arquivos selecionados";
            return View(ViewData);
        }

        long size = files.Sum(f => f.Length);
        var filePathsName = new List<string>();
        var filePath = Path.Combine(_hostEnvironment.WebRootPath, _configurationImagens.NomePastaImagensProduto);

        foreach (var item in files)
        {
            if (item.FileName.ToUpper().Contains(".JPG") || item.FileName.ToUpper().Contains(".GIF") || item.FileName.ToUpper().Contains(".PNG"))
            {
                var fileNameWithPath = $"{filePath}\\{item.FileName}";
                filePathsName.Add(fileNameWithPath);

                using(var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    await item.CopyToAsync(stream);
                }
            }
        }

        ViewData["Resultado"] = $"{files.Count} arquivo(s) salvo(s) com sucesso";
        ViewBag.Arquivos = filePathsName;

        return View(ViewData);  
    }
}