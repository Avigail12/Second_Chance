using API.Data;
using API.Dto;
using API.Halpers;
using API.Model;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{
    //[Route("api/users/{userid}/products/{productid}/photos")]
    // product-add componnent 
    //[Authorize]
    [Route("api/users/{userid}/products/{productid}/photos")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDataRepository _repo;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _Cloudinary;

        public PhotosController(IMapper mapper, IDataRepository repo, IOptions<CloudinarySettings> CloudinaryConfig)
        {
            _mapper = mapper;
            _repo = repo;
            _cloudinaryConfig = CloudinaryConfig;

            Account acc = new Account(

                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
                );

            _Cloudinary = new Cloudinary(acc);
        }
       

        [HttpGet("{id}", Name = "GetPhoto")]

        public async Task<IActionResult> GetPhoto(int id)
        {
            var photoFromRepo = await _repo.GetPhoto(id);

            var photo = _mapper.Map<PhotoForReturnDto>(photoFromRepo);

            return Ok(photo);
        }

        [HttpPost]
        public async Task<IActionResult> AddPhotoForProduct(int userid, int productid, [FromForm]PhotoForCreationDto photoForCreationDto)
        {
            if (userid != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var productFromRepo = await _repo.GetProduct(productid);

            var file = photoForCreationDto.File;

            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uplaodParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.FileName, stream),
                        Transformation = new Transformation().Width(300).Height(300).Crop("fill").Gravity("face")
                    };

                    uploadResult = _Cloudinary.Upload(uplaodParams);
                }
            }

            photoForCreationDto.Url = uploadResult.Uri.ToString();
            photoForCreationDto.PublicId = uploadResult.PublicId;

            var photo = _mapper.Map<Photo>(photoForCreationDto);

            if (!productFromRepo.Photos.Any(x => x.IsMain))
                photo.IsMain = true;

            _repo.Add<Photo>(photo);

            productFromRepo.Photos.Add(photo);

            if (await _repo.SaveAll())
            {   
                var photoForReturnDto = _mapper.Map<PhotoForReturnDto>(photo);
                return Ok(photoForReturnDto);
                //return CreatedAtRoute("GetPhoto", new { userid = userid, id = photo.Id }, photoForReturnDto);
            }

            return BadRequest("Could not add the photo");
        }
    }
}
