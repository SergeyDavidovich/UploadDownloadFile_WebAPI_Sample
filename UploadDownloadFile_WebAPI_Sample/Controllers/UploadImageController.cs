﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UploadDownloadFile_WebAPI_Sample.Models;

namespace UploadDownloadFile_WebAPI_Sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadImageController
    {
        public static IWebHostEnvironment _environment;
        public UploadImageController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        //Chose image from directory and upload it on server
        //https://localhost:44382/api/UploadImage
        [HttpPost]
        public async Task<string> ImageUpload([FromForm] UploadImage_Model objFile)
        {
            if (!Directory.Exists(_environment.WebRootPath + "\\Upload\\"))
                Directory.CreateDirectory(_environment.WebRootPath + "\\Upload\\");

            await using (FileStream FileStream = System.IO.File.Create
                (_environment.WebRootPath + "\\Upload\\" + objFile.files.FileName))
            {
                objFile.files.CopyTo(FileStream);
                FileStream.Flush();
                return "added file " + objFile.files.FileName;
            }
        }
    }
}