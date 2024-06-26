﻿using DinkToPdf.Contracts;
using Firebase.Auth;
using Firebase.Storage;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using PRN231.TicketBooking.Common.ConfigurationModel;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Service.Contract;
using RestSharp;
using System.Net;

namespace PRN231.TicketBooking.Service.Implementation
{
    public class FirebaseService : GenericBackendService, IFirebaseService
    {
        private readonly IConverter _pdfConverter;
        private AppActionResult _result;
        private FirebaseConfiguration _firebaseConfiguration;
        private readonly IConfiguration _configuration;

        public FirebaseService(IConverter pdfConverter, IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider)
        {
            _pdfConverter = pdfConverter;
            _result = new();
            _firebaseConfiguration = Resolve<FirebaseConfiguration>();
            _configuration = configuration;
           
        }

        public async Task<AppActionResult> DeleteFileFromFirebase(string pathFileName)
        {
            var _result = new AppActionResult();
            try
            {
                var auth = new FirebaseAuthProvider(new FirebaseConfig(_firebaseConfiguration.ApiKey));

                var account = await auth.SignInWithEmailAndPasswordAsync(_firebaseConfiguration.AuthEmail, _firebaseConfiguration.AuthPassword);
                var storage = new FirebaseStorage(
             _firebaseConfiguration.Bucket,
             new FirebaseStorageOptions
             {
                 AuthTokenAsyncFactory = () => Task.FromResult(account.FirebaseToken),
                 ThrowOnCancel = true
             });
                await storage
                    .Child(pathFileName)
                    .DeleteAsync();
                _result.Messages.Add("Delete image successful");
            }
            catch (FirebaseStorageException ex)
            {
                _result.Messages.Add($"Error deleting image: {ex.Message}");
            }
            return _result;
        }

        public async Task<string> GetUrlImageFromFirebase(string pathFileName)
        {
            var a = pathFileName.Split("/");
            pathFileName = $"{a[0]}%2F{a[1]}";
            var api = $"https://firebasestorage.googleapis.com/v0/b/yogacenter-44949.appspot.com/o?name={pathFileName}";
            if (string.IsNullOrEmpty(pathFileName))
            {
                return string.Empty;
            }

            var client = new RestClient();
            var request = new RestRequest(api);
            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var jmessage = JObject.Parse(response.Content);
                var downloadToken = jmessage.GetValue("downloadTokens").ToString();
                return
                    $"https://firebasestorage.googleapis.com/v0/b/{_configuration["Firebase:Bucket"]}/o/{pathFileName}?alt=media&token={downloadToken}";
            }

            return string.Empty;
        }

        public async Task<AppActionResult> UploadFileToFirebase(IFormFile file, string pathFileName)
        {
            var _result = new AppActionResult();
            bool isValid = true;
            if (file == null || file.Length == 0)
            {
                isValid = false;
                _result.Messages.Add("The file is empty");
            }
            if (isValid)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    var stream = new MemoryStream(memoryStream.ToArray());
                    var auth = new FirebaseAuthProvider(new FirebaseConfig(_firebaseConfiguration.ApiKey));
                    var account = await auth.SignInWithEmailAndPasswordAsync(_firebaseConfiguration.AuthEmail, _firebaseConfiguration.AuthPassword);
                    string destinationPath = $"{pathFileName}";

                    var task = new FirebaseStorage(
                    _firebaseConfiguration.Bucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(account.FirebaseToken),
                        ThrowOnCancel = true
                    })
                    .Child(destinationPath)
                    .PutAsync(stream);
                    var downloadUrl = await task;

                    if (task != null)
                    {
                        _result.Result = downloadUrl;
                    }
                    else
                    {
                        _result.IsSuccess = false;
                        _result.Messages.Add("Upload failed");
                    }
                }
            }
            return _result;
        }
        //public async Task<List<IFormFile>> GetFilesAsFormFilesAsync(List<string> fileUrls)
        //{
        //    var storageClient = StorageClient.Create();
        //    var formFiles = new List<IFormFile>();

        //    foreach (var fileUrl in fileUrls)
        //    {
        //        using (var memoryStream = new MemoryStream())
        //        {
        //            await storageClient.DownloadObjectAsync(_firebaseConfiguration.Bucket, fileUrl, memoryStream);
        //            memoryStream.Position = 0;
        //            formFiles.Add(new FormFile(memoryStream, 0, memoryStream.Length, null, Path.GetFileName(fileUrl))
        //            {
        //                Headers = new HeaderDictionary(),
        //                ContentType = "application/octet-stream"
        //            });
        //        }
        //    }

        //    return formFiles;
        //}
    }
}